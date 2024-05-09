namespace AccessControl.Services
{
    public interface IReadService<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetByUserId(int userAcId);
    }
}
