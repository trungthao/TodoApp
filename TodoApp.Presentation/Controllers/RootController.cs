using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TodoApp.Entities.LinkModels;

namespace TodoApp.Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType.Contains("application/vnd.codemaze.apiroot"))
            {
                var list = new List<Link>()
                {
                    new Link
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new {}),
                        Rel = "self",
                        Method = "GET"
                    },
                    new Link
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, "GetAllListTask", new {}),
                        Rel = "listtasks",
                        Method = "GET"
                    },
                    new Link
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, "CreateListTask", new {}),
                        Rel = "create_listtask",
                        Method = "POST"
                    },
                };

                return Ok(list);
            }

            return NoContent();
        }
    }
}