using System;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Service.Contracts;
using TodoApp.Shared.DataTransferObjects;

namespace TodoApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/listtask/{listTaskId}/taskitems")]
    public class TaskItemController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TaskItemController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetTaskItems(Guid listTaskId)
        {
            var taskItems = _serviceManager.TaskItemService.GetTaskItems(listTaskId, trackChanges: false);
            return Ok(taskItems);
        }

        [HttpGet("{id:guid}", Name = "GetTaskItemForListTask")]
        public IActionResult GetTaskItemForListTask(Guid listTaskId, Guid id)
        {
            var taskItemDto = _serviceManager.TaskItemService.GetTaskItemForListTask(listTaskId, id, trackChanges: false);
            return Ok(taskItemDto);
        }

        [HttpPost]
        public IActionResult CreateTaskItemForListTask(Guid listTaskId, [FromBody] TaskItemForCreationDto taskItemForCreationDto)
        {
            if (taskItemForCreationDto is null)
                return BadRequest("TaskItemForCreationDto is null");

            var taskItemToReturn = _serviceManager.TaskItemService.CreateTaskItem(listTaskId, taskItemForCreationDto, trackChanges: false);

            return CreatedAtRoute("GetTaskItemForListTask", new { listTaskId, id = taskItemToReturn.Id }, taskItemToReturn);
        }
    }
}