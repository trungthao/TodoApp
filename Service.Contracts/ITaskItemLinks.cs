using Microsoft.AspNetCore.Http;
using TodoApp.Entities.LinkModels;
using TodoApp.Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ITaskItemLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<TaskItemDto> taskItemDto, string fields, Guid companyId, HttpContext httpContext);
    }
}