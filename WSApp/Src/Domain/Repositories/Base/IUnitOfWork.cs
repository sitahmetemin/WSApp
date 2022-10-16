namespace WSApp.Src.Domain.Repositories.Base
{
    public interface IUnitOfWork
    {
        Task<int> SaveChanges();
    }
}
