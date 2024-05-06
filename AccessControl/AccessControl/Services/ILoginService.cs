namespace AccessControl.Services
{
    public interface ILoginService<T>
    {
        Task<T> Auth(T user);
    }
}
