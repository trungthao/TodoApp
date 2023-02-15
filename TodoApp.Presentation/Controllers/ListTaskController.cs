using System;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateListTaskCollection([FromBody] IEnumerable<ListTaskForCreationDto> listTaskCollection)
        {
            var result = _service.ListTaskService.CreateListTaskCollection(listTaskCollection);

            return CreatedAtRoute("ListTaskCollection", new { result.ids }, result.listTasks);
        }

        [HttpGet("")]
        public IActionResult GetAllListTask()
        {
            var listTask = _service.ListTaskService.GetAllListTask(trackChanges: false);
            return Ok(listTask);
        }

        [HttpGet("collection/({ids})", Name = "ListTaskCollection")]
        public IActionResult GetListTaskCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var listTask = _service.ListTaskService.GetListTaskByIds(ids);
            return Ok(listTask);
        }

        [HttpGet("{id:guid}", Name = "ListTaskId")]
        public IActionResult GetListTask(Guid id)
        {
            var result = _service.ListTaskService.GetListTask(id, trackChanges: false);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateListTask([FromBody] ListTaskForCreationDto listTaskForCreationDto)
        {
            if (listTaskForCreationDto is null)
                return BadRequest("listTaskForCreationDto is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var createdListTaskDto = _service.ListTaskService.CreateListTask(listTaskForCreationDto);
            return CreatedAtRoute("ListTaskId", new { id = createdListTaskDto.Id }, createdListTaskDto);
        }

        [HttpDelete("listTaskId:guid")]
        public IActionResult DeleteListTask(Guid listTaskId)
        {
            _service.ListTaskService.DeleteListTask(listTaskId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{listTaskId:guid}")]
        public IActionResult UpdateListTask(Guid listTaskId, ListTaskForUpdateDto listTaskForUpdateDto)
        {
            if (listTaskForUpdateDto is null)
                return BadRequest("listTaskForUpdateDto is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _service.ListTaskService.UpdateListTask(listTaskId, listTaskForUpdateDto, trackChanges: true);
            return NoContent();
        }
    }
}

