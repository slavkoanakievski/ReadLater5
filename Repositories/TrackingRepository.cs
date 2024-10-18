using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly ReadLaterDataContext _context;

        public TrackingRepository(ReadLaterDataContext context)
        {
            _context = context;
        }

        public async Task AddActivityTrackingAsync(ActivityTracking activityTracking)
        {
            await _context.ActivityTrackings.AddAsync(activityTracking);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivityTracking>> GetAllTrackingsForUserAsync(string userId)
        {
            return await _context.ActivityTrackings
            .Where(at => at.UserId == userId)
            .ToListAsync();
        }
    }
}
