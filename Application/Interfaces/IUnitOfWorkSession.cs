namespace Application.Interfaces
{
    public interface IUnitOfWorkSession
    {
        void Commit();
        void RollBack();
    }
}
