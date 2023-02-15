using System;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTaskItemForListTask(Guid listTaskId, Guid id)
        {
            _serviceManager.TaskItemService.DeleteTaskItemForListTask(listTaskId, id, trackChanges: false);
            return NoContent();
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

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var taskItemToReturn = _serviceManager.TaskItemService.CreateTaskItem(listTaskId, taskItemForCreationDto, trackChanges: false);

            return CreatedAtRoute("GetTaskItemForListTask", new { listTaskId, id = taskItemToReturn.Id }, taskItemToReturn);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateTaskItemForListTask(Guid listTaskId, Guid id, [FromBody] TaskItemForUpdateDto taskItemForUpdateDto)
        {
            if (taskItemForUpdateDto is null)
                return BadRequest("TaskItemForUpdateDto is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.TaskItemService.UpdateTaskItemForListTask(listTaskId, id, taskItemForUpdateDto, listTaskTrackChanges: false, taskItemTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("id:guid")]
        public IActionResult PartiallyUpdateTaskItem(Guid listTaskId, Guid id, [FromBody] JsonPatchDocument<TaskItemForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("TaskItemForUpdateDto is null");

            var result = _serviceManager.TaskItemService.GetTaskItemForPatch(listTaskId, id, listTrackChanges: false, itemTrackChanges: true);
            patchDoc.ApplyTo(result.taskItemToPatch, ModelState);
            TryValidateModel(result.taskItemToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.TaskItemService.SaveChangesForPatch(result.taskItemToPatch, result.taskItemEntity);

            return NoContent();
        }
    }
}