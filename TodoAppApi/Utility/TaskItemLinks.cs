using Service.Contracts;
using TodoApp.Contracts;
using TodoApp.Entities.LinkModels;
using TodoApp.Shared.DataTransferObjects;
using TodoApp.Entities.Models;
using Microsoft.Net.Http.Headers;

namespace TodoAppApi.Utility
{
    public class TaskItemLinks : ITaskItemLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<TaskItemDto> _dataShaper;

        public TaskItemLinks(LinkGenerator linkGenerator, IDataShaper<TaskItemDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<TaskItemDto> taskItemDtos, string fields, Guid listTaskId, HttpContext httpContext)
        {
            var shapedTasks = ShapeData(taskItemDtos, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkdedTaskItems(taskItemDtos, fields, listTaskId, httpContext, shapedTasks);
            return ReturnShapedTaskItem(shapedTasks);
        }

        private List<Entity> ShapeData(IEnumerable<TaskItemDto> taskItemDtos, string fields) =>
            _dataShaper.ShapeData(taskItemDtos, fields)
            .Select(e => e.Entity)
            .ToList();

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private LinkResponse ReturnShapedTaskItem(List<Entity> shapedTaskItems) => 
            new LinkResponse() { ShapedEntities = shapedTaskItems };

        private LinkResponse ReturnLinkdedTaskItems(IEnumerable<TaskItemDto> taskItemsDto,
            string fields, Guid listTaskId, HttpContext httpContext, List<Entity> shapedTaskItems)
        {
            var taskItemDtoList = taskItemsDto.ToList();
            for (var index = 0; index < taskItemDtoList.Count(); index++)
            {
                var taskItemLinks = CreateLinksForTaskItem(httpContext, listTaskId, taskItemDtoList[index].Id, fields);
                shapedTaskItems[index].Add("Links", taskItemLinks);
            }
            var taskItemCollection = new LinkCollectionWrapper<Entity>(shapedTaskItems);
            var linkedTaskItems = CreateLinksForTaskItems(httpContext, taskItemCollection);
            return new LinkResponse { HasLinks = true, LinkedEntities = linkedTaskItems };
        }

        private List<Link> CreateLinksForTaskItem(HttpContext httpContext, Guid listTaskId, Guid id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(httpContext, "GetTaskItemForListTask", values: new { listTaskId, id, fields }), "self", "GET"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteTaskItemForListTask", values: new { listTaskId, id }), "delete_employee", "DELETE"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateTaskItemForListTask", values: new { listTaskId, id }), "update_employee", "PUT"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateTaskItem", values: new { listTaskId, id }), "partially_update_employee", "PATCH")
            };
            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForTaskItems(HttpContext httpContext, LinkCollectionWrapper<Entity> taskItemsWrapper)
        {
            taskItemsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetEmployeesForCompany", values: new { }), "self", "GET"));
            return taskItemsWrapper;
        }
    }
}