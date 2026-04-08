namespace WebApplication1.Application.Interfaces.IRepositories
{
    public interface IDashboardRepository
    {
        Task<object> GetSummaryAsync();
    }
}
