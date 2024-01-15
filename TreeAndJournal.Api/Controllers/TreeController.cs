using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TreeAndJournal.Application.Tree.GetTree;

namespace TreeAndJournal.Api.Controllers
{
    public class TreeController : Controller
    {
        private readonly ISender _sender;

        public TreeController(ISender sender)
        {
            _sender = sender;
        }

        [Route("/api.user.tree.get")]
        [HttpPost]
        [SwaggerOperation(Description = "Returns your entire tree. If your tree doesn't exist it will be created automatically.")]
        [ProducesResponseType(typeof(List<NodeDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTree([FromQuery][Required] string treeName)
        {
            var result = await _sender.Send(new GetTreeQuery(treeName));

            return Ok(result);
        }
    }
}
