using Entity;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class TrackingService : ITrackingService
    {
        private readonly ITrackingRepository _trackingRepository;

        public TrackingService(ITrackingRepository trackingRepository)
        {
            _trackingRepository = trackingRepository;
        }
        public Task AddActivityTrackingAsync(ActivityTracking activityTracking)
        {
            return _trackingRepository.AddActivityTrackingAsync(activityTracking);
        }

        public async Task<IEnumerable<ActivityTracking>> GetAllTrackingsForUserAsync(string userId)
        {
            return await _trackingRepository.GetAllTrackingsForUserAsync(userId);
        }
    }
}
