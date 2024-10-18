using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ITrackingRepository
    {
        public Task AddActivityTrackingAsync(ActivityTracking activityTracking);
        public Task<IEnumerable<ActivityTracking>> GetAllTrackingsForUserAsync(string userId);


    }
}
