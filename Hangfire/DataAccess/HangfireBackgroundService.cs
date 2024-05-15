using Hangfire;
using Hangfire.InterFace;

namespace Hangfire.DataAccess
{
    public class HangfireBackgroundService : BackgroundService
    {
        private readonly IJobTest _jobTestService;

        public HangfireBackgroundService(IJobTest jobTestService)
        {
            _jobTestService = jobTestService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                BackgroundJob.Schedule(() => _jobTestService.Schedulejob(), TimeSpan.FromMinutes(1));
                RecurringJob.AddOrUpdate(() =>  _jobTestService.ReccuringJob(), Cron.Daily(05, 30)); // execute everyday and its time zone is universal (UTC)
        }
    }
}
