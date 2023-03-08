using Microsoft.AspNetCore.Mvc;
using TodoApp.Service.Contracts;

namespace TodoApp.Presentation.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/listtask")]
    public class ListTaskV2Controller : ControllerBase
    {
        private readonly IServiceManager _service;

        public ListTaskV2Controller(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("", Name = "GetAllListTask")]
        public async Task<IActionResult> GetAllListTask()
        {
            var listTask = await _service.ListTaskService.GetAllListTaskAsync(trackChanges: false);
            return Ok(listTask);
        }
    }
}