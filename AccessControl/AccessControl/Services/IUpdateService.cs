namespace AccessControl.Services
{
    public interface IUpdateService<T, TU>
    {
        Task<T> Update(int id, TU updateDto);
    }
}
