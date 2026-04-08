using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.IServices;

namespace WebApplication1.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<object> GetSummaryAsync()
        {
            return await _dashboardRepository.GetSummaryAsync();
        }
    }
}