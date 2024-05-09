namespace Repository.Repository
{
    public interface IUserRepository<DEntity, VEntity, CEntity, ACEntity>
    {
        Task<IEnumerable<DEntity>> GetDevices(int userAcId);

        Task<IEnumerable<VEntity>> GetVisitors(int userAcId);

        Task<IEnumerable<CEntity>> GetCars(int userAcId);

        Task<IEnumerable<ACEntity>> GetAccessVisitor(int userAcId);
    }
}
