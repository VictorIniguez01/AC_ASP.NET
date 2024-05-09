using System.Security.Principal;

namespace AccessControl.Services
{
    public interface IUserAcService<TD, TV, TC, TAV, TAD>
    {
        Task<IEnumerable<TD>> GetDevices(int userAcId);

        Task<IEnumerable<TV>> GetVisitors(int userAcId);

        Task<IEnumerable<TC>> GetCars(int userAcId);

        Task<IEnumerable<TAV>> GetAccessVisitor(int userAcId);

        Task<IEnumerable<TAD>> GetAccessDetails(int userAcId);
    }
}
