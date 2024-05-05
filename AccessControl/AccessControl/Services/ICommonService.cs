namespace AccessControl.Services
{
    public interface ICommonService<T, TI>
    {
        List<string> Errors { get; }

        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI insertDto);
        Task<T> Delete(int id);

        bool Validate(TI insertDto);
    }
}
