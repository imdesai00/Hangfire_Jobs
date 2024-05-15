using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hangfire.InterFace;
using System.Net;

namespace Hangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTestController : ControllerBase
    {
        private readonly IJobTest _jobTestService;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public JobTestController(IJobTest jobTestService, IBackgroundJobClient backgroundJobClient)
        {
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;;
        }

        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            return Ok();
        }
    }
}