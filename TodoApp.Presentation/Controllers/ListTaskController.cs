using System;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Presentation.ActionFilters;
using TodoApp.Presentation.ModelBinders;
using TodoApp.Service.Contracts;
using TodoApp.Shared.DataTransferObjects;

namespace TodoApp.Presentation.Controllers
{
    [Route("api/listtask")]
    [ApiController]
    public class ListTaskController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ListTaskController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateListTaskCollection([FromBody] IEnumerable<ListTaskForCreationDto> listTaskCollection)
        {
            var result = await _service.ListTaskService.CreateListTaskCollectionAsync(listTaskCollection);

            return CreatedAtRoute("ListTaskCollection", new { result.ids }, result.listTasks);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllListTask()
        {
            var listTask = await _service.ListTaskService.GetAllListTaskAsync(trackChanges: false);
            return Ok(listTask);
        }

        [HttpGet("collection/({ids})", Name = "ListTaskCollection")]
        public async Task<IActionResult> GetListTaskCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var listTask = await _service.ListTaskService.GetListTaskByIdsAsync(ids);
            return Ok(listTask);
        }

        [HttpGet("{id:guid}", Name = "ListTaskId")]
        public async Task<IActionResult> GetListTask(Guid id)
        {
            var result = await _service.ListTaskService.GetListTaskAsync(id, trackChanges: false);
            return Ok(result);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateListTask([FromBody] ListTaskForCreationDto listTaskForCreationDto)
        {
            var createdListTaskDto = await _service.ListTaskService.CreateListTaskAsync(listTaskForCreationDto);
            return CreatedAtRoute("ListTaskId", new { id = createdListTaskDto.Id }, createdListTaskDto);
        }

        [HttpDelete("listTaskId:guid")]
        public async Task<IActionResult> DeleteListTask(Guid listTaskId)
        {
            await _service.ListTaskService.DeleteListTaskAsync(listTaskId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{listTaskId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateListTask(Guid listTaskId, ListTaskForUpdateDto listTaskForUpdateDto)
        {
            await _service.ListTaskService.UpdateListTaskAsync(listTaskId, listTaskForUpdateDto, trackChanges: true);
            return NoContent();
        }
    }
}

