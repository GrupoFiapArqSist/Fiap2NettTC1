using TicketNow.Service.Services.Business;

namespace TicketNow.Infra.Data.Context
{
    public static class SeedData
    {
        public static void EnsureSeedData(this ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            if (context.AllMigrationsApplied())
            {
                SeedHistoryEvaluator.ApplySeedHistory(context, serviceProvider);
            }
        }
    }
}
