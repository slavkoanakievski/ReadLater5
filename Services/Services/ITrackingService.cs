﻿using Data.Models;
using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Services
{
    public interface ITrackingService
    {
        Task AddActivityTrackingAsync(ActivityTracking activityTracking);
        Task<IEnumerable<ActivityTracking>> GetAllTrackingsForUserAsync(string userId);
        Task<IEnumerable<BookmarkStatistics>> GetClickStatisticsAsync(string userId);

    }
}
