using TodoApp.Shared.RequestParameters;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Entities.LinkModels
{
    public record LinkParameters(TaskItemParameters TaskItemParameters, HttpContext Context);
}