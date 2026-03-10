using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.InMemory
{
    /// <summary>
    /// In-memory implementation of IUnitOfWork.
    /// Since this is an in-memory store, no actual transaction management is needed;
    /// Save simply returns 1 to indicate a successful (no-op) commit.
    /// </summary>
    public sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Simulates saving changes. In-memory store requires no real transaction.
        /// </summary>
        /// <returns>A task resolving to 1, indicating success.</returns>
        public Task<int> Save() => Task.FromResult(1);
    }
}
