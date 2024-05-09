namespace AccessControl.Services
{
    public interface ISessionService<T>
    {
        Task<T> Auth(T user);
    }
}
