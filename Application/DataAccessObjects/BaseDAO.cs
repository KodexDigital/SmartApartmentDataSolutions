using Application.Implementations;
using Application.Interfaces;
using System.Data;

namespace Application.DataAccessObjects
{
    public class BaseDAO
    {
        protected readonly IDbConnection _con;
        protected UnitOfWorkSession UnitOfWorkSession;
        public BaseDAO(IDbConnection connection)
        {
            _con = connection;
        }

        public IUnitOfWorkSession Begin()
        {
            if (_con.State != ConnectionState.Open)
            {
                _con.Open();
            }
            UnitOfWorkSession = new UnitOfWorkSession(_con);

            return UnitOfWorkSession;
        }

        public void Join(IUnitOfWorkSession token)
        {
            UnitOfWorkSession = (UnitOfWorkSession)token;
        }
    }
}
