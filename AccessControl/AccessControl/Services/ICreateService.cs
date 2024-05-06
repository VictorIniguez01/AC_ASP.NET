namespace AccessControl.Services
{
    public interface ICreateService<T, TI>
    {
        List<string> Errors { get; }
        Task<T> Add(TI insertDto);
        bool Validate(TI insertDto);
    }
}
