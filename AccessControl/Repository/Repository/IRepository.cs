namespace Repository.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetByUserId(int userAcId);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task Save();

        //Busqueda
        IEnumerable<TEntity> Search(Func<TEntity, bool> filter);
    }
}
