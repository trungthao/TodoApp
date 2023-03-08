using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoApp.Entities.LinkModels;
using TodoApp.Presentation.ActionFilters;
using TodoApp.Service.Contracts;
using TodoApp.Shared.DataTransferObjects;
using TodoApp.Shared.RequestParameters;

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
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetTaskItems(Guid listTaskId, [FromQuery] TaskItemParameters taskItemParameters)
        {
            var linkParams = new LinkParameters(taskItemParameters, HttpContext);
            var result = await _serviceManager.TaskItemService.GetTaskItemsAsync(listTaskId, linkParams, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.metaData));

            return Ok(result.linkResponse.HasLinks ? result.linkResponse.LinkedEntities : result.linkResponse.ShapedEntities);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskItemForListTask(Guid listTaskId, Guid id)
        {
            await _serviceManager.TaskItemService.DeleteTaskItemForListTaskAsync(listTaskId, id, trackChanges: false);
            return NoContent();
        }

        [HttpGet("{id:guid}", Name = "GetTaskItemForListTask")]
        public async Task<IActionResult> GetTaskItemForListTask(Guid listTaskId, Guid id)
        {
            var taskItemDto = await _serviceManager.TaskItemService.GetTaskItemForListTaskAsync(listTaskId, id, trackChanges: false);
            return Ok(taskItemDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItemForListTask(Guid listTaskId, [FromBody] TaskItemForCreationDto taskItemForCreationDto)
        {
            if (taskItemForCreationDto is null)
                return BadRequest("TaskItemForCreationDto is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var taskItemToReturn = await _serviceManager.TaskItemService.CreateTaskItemAsync(listTaskId, taskItemForCreationDto, trackChanges: false);

            return CreatedAtRoute("GetTaskItemForListTask", new { listTaskId, id = taskItemToReturn.Id }, taskItemToReturn);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTaskItemForListTask(Guid listTaskId, Guid id, [FromBody] TaskItemForUpdateDto taskItemForUpdateDto)
        {
            if (taskItemForUpdateDto is null)
                return BadRequest("TaskItemForUpdateDto is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _serviceManager.TaskItemService.UpdateTaskItemForListTaskAsync(listTaskId, id, taskItemForUpdateDto, listTaskTrackChanges: false, taskItemTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("id:guid")]
        public async Task<IActionResult> PartiallyUpdateTaskItem(Guid listTaskId, Guid id, [FromBody] JsonPatchDocument<TaskItemForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("TaskItemForUpdateDto is null");

            var result = await _serviceManager.TaskItemService.GetTaskItemForPatchAsync(listTaskId, id, listTrackChanges: false, itemTrackChanges: true);
            patchDoc.ApplyTo(result.taskItemToPatch, ModelState);
            TryValidateModel(result.taskItemToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _serviceManager.TaskItemService.SaveChangesForPatchAsync(result.taskItemToPatch, result.taskItemEntity);

            return NoContent();
        }
    }
}