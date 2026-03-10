# EcoFleet Microservices — Gestión de Vehículos de Renting

Microservicio para la gestión de vehículos de una empresa de renting, implementado siguiendo la arquitectura hexagonal (Ports and Adapters) y los patrones DDD descritos en el template proporcionado.

## Repositorio

```
https://github.com/antoniojgh/GTMotivePruebaTecnica/tree/develop
```

## Ejecución

### Con Docker (recomendado)

```bash
docker-compose up -d
```

La API estará disponible en: `http://localhost:8080/swagger`

### Con Visual Studio

Requiere .NET 9 SDK instalado.

1. Abrir `src/microservice.sln`.
2. Establecer `GtMotive.Estimate.Microservice.Host` como proyecto de inicio.
3. Pulsar F5.

La API estará disponible en: `https://localhost:63098/swagger`

### Ejecución de tests

```bash
dotnet test
```

## Funcionalidades implementadas

El microservicio expone cuatro operaciones REST:

| Operación | Método | Endpoint | Respuesta exitosa |
|---|---|---|---|
| Crear vehículo | POST | `/api/vehicles` | 201 Created |
| Listar vehículos disponibles | GET | `/api/vehicles/available` | 200 OK |
| Alquilar vehículo | POST | `/api/vehicles/rent` | 200 OK |
| Devolver vehículo | POST | `/api/vehicles/return` | 200 OK |

### Restricciones de negocio

- **Antigüedad máxima de 5 años**: Un vehículo cuya fecha de fabricación sea superior a 5 años no puede registrarse en la flota. La validación se aplica en el constructor del Aggregate Root `Vehicle` y devuelve HTTP 422.
- **Máximo 1 alquiler por persona**: Una persona no puede tener más de un vehículo alquilado simultáneamente. La validación se aplica en el Domain Service `VehicleService` y devuelve HTTP 409.

## Arquitectura y patrones aplicados

La implementación sigue la estructura de carpetas y los patrones del template proporcionado:

| Patrón | Implementación |
|---|---|
| Controller + Command (MediatR) | `VehiclesController` → Handlers → Use Cases |
| Presenter + Output Port | Cada Use Case notifica al Presenter vía `IOutputPort` |
| ViewModel (Response DTOs) | Con `[Required]` y XML docs para Swagger |
| Unit of Work | `InMemoryUnitOfWork`, invocado solo desde Use Cases |
| First-Class Collection | `VehicleFleet` con filtrado de disponibilidad |
| Factory | `EntityFactory` implementa `IVehicleFactory` e `IRentalFactory` |
| Value Object | `PersonId`, `VehicleId`, `RentalId` |
| Entity | `Rental` (abstracta, mutable, identificada por `RentalId`) |
| Aggregate Root | `Vehicle` (protege invariantes de negocio) |
| Repository | `InMemoryVehicleRepository`, `InMemoryRentalRepository` |
| Domain Service | `VehicleService` (lógica cross-entity sin I/O) |

### Separación de capas

```
Domain              → Entidades, Value Objects, interfaces (puertos), Domain Service
ApplicationCore     → Use Cases, Input/Output messages, Output Ports
Infrastructure      → Repositorios in-memory, Entity Factory, Logger Adapter
Api                 → Controllers, Handlers, Presenters, Response DTOs
Host                → Composición de dependencias, Dockerfile, Swagger
```

Las dependencias van de fuera hacia dentro: Domain no depende de nadie, ApplicationCore depende solo de Domain, Infrastructure y Api dependen de ApplicationCore, y Host compone todo.

## Decisiones de diseño

### Endpoints de la API

Los endpoints de alquiler y devolución están diseñados como acciones sobre el vehículo (`POST /api/vehicles/rent`, `POST /api/vehicles/return`) en lugar de usar un recurso REST separado (`POST /api/rentals`, `PATCH /api/rentals/{id}`).

El microservicio gestiona vehículos, no alquileres. Los endpoints usan el mismo lenguaje que usaría el negocio — "alquilar un vehículo", "devolver un vehículo" — lo que hace la API más intuitiva. Si en el futuro se necesitara gestionar los alquileres de forma independiente (historial, facturación, extensiones), se crearía un recurso `/api/rentals` dedicado.

### VehicleService como clase concreta

`VehicleService` se inyecta como clase concreta en los Use Cases, siguiendo el patrón del template donde los Domain Services (como `AccountService` en `WithdrawUseCase`) no se definen tras una interfaz. Es un servicio de dominio sin I/O propio; toda su comunicación con el exterior pasa a través de los repositorios y el factory inyectados. No invoca `IUnitOfWork`, ya que esa responsabilidad recae exclusivamente en los Use Cases.

### Limitaciones del enfoque in-memory

`InMemoryUnitOfWork.Save()` es un no-op. Los repositorios persisten directamente en el `ConcurrentDictionary` al llamar a `AddAsync`/`UpdateAsync`, por lo que no hay control transaccional real. Con una implementación basada en EF Core, el `DbContext` coordinaría los cambios y `SaveChangesAsync()` los aplicaría atómicamente.

Los repositorios in-memory clonan las entidades en cada lectura y escritura para simular el aislamiento de una base de datos real y evitar que las mutaciones del dominio afecten al almacén antes de una actualización explícita.

## Uso de interfaces de la plantilla

### Capa de dominio (Domain/Interfaces)

**Interfaces utilizadas:**

- **IAppLogger\<T\>**: Se utiliza en los Use Cases para registrar las operaciones de negocio relevantes (creación de vehículos, alquileres, devoluciones). También se usa en el `BusinessExceptionFilter` proporcionado por la plantilla para registrar las excepciones capturadas.
- **IUnitOfWork**: Se invoca en todos los Use Cases tras las operaciones de persistencia, siguiendo el patrón Unit of Work descrito en el README de la plantilla. La responsabilidad de llamar a `Save()` recae exclusivamente en los Use Cases, nunca en el Domain Service.

**Interfaces no utilizadas:**

- **ITelemetry**: La plantilla ya conecta esta interfaz de forma transparente en `InfrastructureConfiguration`, registrando `AppTelemetry` en producción (con Application Insights) y `NoOpTelemetry` en desarrollo. No se realizan llamadas explícitas a `TrackEvent` o `TrackMetric` desde el código de negocio ya que las necesidades de trazabilidad están cubiertas con `IAppLogger` y la telemetría de producción queda fuera del alcance de esta prueba.
- **IAuthorizationService**: La prueba no requiere autenticación ni autorización. La configuración de IdentityServer presente en `Program.cs` solo se activa en entornos de producción; en modo Development el servicio funciona sin autenticación, permitiendo la ejecución local sin dependencias externas.
- **IBus / IBusFactory**: Estas interfaces están preparadas para implementar eventos de dominio y mensajería asíncrona entre microservicios. Dado que la prueba solicita un microservicio con cuatro operaciones REST síncronas y no requiere comunicación con otros servicios, no se han implementado. En un escenario real de producción, se podrían utilizar para publicar eventos como `VehicleCreated` o `VehicleRented` en una cola de mensajería.

### Capa de aplicación (ApplicationCore/UseCases)

La plantilla define cinco interfaces que constituyen el esqueleto del patrón Use Case + Ports and Adapters. Todas se utilizan en la implementación:

- **IUseCase\<TUseCaseInput\>**: Implementada por los cuatro casos de uso. Cada uno se registra en el contenedor de DI a través de `ApplicationConfiguration`.
- **IUseCaseInput**: Interfaz marcadora implementada por todos los mensajes de entrada, garantizando un contrato uniforme entre los Handlers de MediatR y los Use Cases.
- **IUseCaseOutput**: Interfaz marcadora implementada por todos los mensajes de salida, permitiendo que los Output Ports trabajen con tipos genéricos.
- **IOutputPortStandard\<TUseCaseOutput\>**: Define el método `StandardHandle` para el flujo exitoso. Los Presenters de la capa API la implementan para traducir el resultado en un `IActionResult`.
- **IOutputPortNotFound**: Define el método `NotFoundHandle` para los casos en los que un recurso no se encuentra. Los Presenters lo traducen en una respuesta HTTP 404 Not Found.

## Tests

Se han implementado los tres tipos de tests requeridos por la especificación.

### Test unitario — RentVehicleUseCaseTests

Valida el caso de uso `RentVehicleUseCase` de forma aislada, con todas las dependencias de infraestructura mockeadas con Moq (`IVehicleRepository`, `IRentalRepository`, `IRentalFactory`, `IUnitOfWork`, `IRentVehicleOutputPort`, `IAppLogger`).

`VehicleService` se instancia como clase concreta en lugar de mockearse tras una interfaz. Esta decisión es intencionada:

- **Coherencia con el template**: Los Domain Services en la plantilla (como `AccountService`) se inyectan como clases concretas, no como interfaces. Introducir una `IVehicleService` únicamente para facilitar el mocking no seguiría el patrón establecido.
- **Aislamiento real**: `VehicleService` no tiene I/O propio. Toda su comunicación con el exterior pasa a través de los repositorios y el factory, que sí están mockeados. El test está completamente aislado de cualquier dependencia de infraestructura.

Cubre tres escenarios: flujo exitoso (verifica `StandardHandle` y `Save`), persona con alquiler activo (`PersonAlreadyHasActiveRentalException`) y vehículo no disponible (`VehicleNotAvailableException`).

### Test funcional — CreateVehicleFunctionalTests

Prueba de integración excluyendo el host, tal como indica la especificación. Usa `CompositionRootTestFixture` que monta el contenedor de DI completo con todos los servicios reales (MediatR, Use Cases, Presenters, repositorios in-memory). No hay mocks ni TestServer: el Handler de MediatR se ejecuta directamente.

Verifica que los datos se persisten realmente en el repositorio in-memory y que la restricción de antigüedad de 5 años se propaga correctamente a través de todas las capas.

### Test de infraestructura — VehiclesControllerInfrastructureTests

La especificación indica: "Solo a nivel de host (recepción de la llamada y validación del modelo), no completa."

Se han implementado tres tests sobre `POST /api/vehicles` usando `TestServer` de ASP.NET Core:

- **`PostVehicles_Returns400_WhenRequiredFieldIsMissing`**: Cumple de forma estricta con la especificación. Envía un JSON con un campo requerido a null y verifica que el host devuelve HTTP 400 Bad Request. La validación ocurre en el pipeline de ASP.NET Core (model binding y `[Required]` con `[ApiController]`) antes de que se ejecute ninguna lógica de negocio.
- **`PostVehicles_Returns201_WhenRequestBodyIsValid`** y **`PostVehicles_Returns422_WhenManufactureDateTooOld`**: Van un paso más allá de lo estrictamente requerido para aportar mayor cobertura y verificar que el `BusinessExceptionFilter` traduce correctamente las excepciones de dominio a los códigos HTTP esperados.

La autenticación en los tests se gestiona con `Acheve.TestHost`, evitando la dependencia con IdentityServer.
