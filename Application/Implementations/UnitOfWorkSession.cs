using Application.Interfaces;
using System.Data;

namespace Application.Implementations
{
    public class UnitOfWorkSession : IUnitOfWorkSession
    {
        protected readonly IDbTransaction _transaction;
        public UnitOfWorkSession(IDbConnection conntection)
        {
            _transaction = conntection.BeginTransaction();
        }
        public void RollBack()
        {
            _transaction.Rollback();
        }
        public void Commit()
        {
            _transaction.Commit();
        }

        public IDbTransaction GetTransaction() => _transaction;
    }
}
