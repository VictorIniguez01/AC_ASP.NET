namespace AccessControl.Services
{
    public interface IDeleteService<T>
    {
        Task<T> Delete(int id);
    }
}
