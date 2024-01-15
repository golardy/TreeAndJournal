using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TreeAndJournal.Application.Nodes.CreateNode;
using TreeAndJournal.Application.Nodes.DeleteNode;
using TreeAndJournal.Application.Nodes.UpdateNode;

namespace TreeAndJournal.Api.Controllers
{
    public class NodeController : Controller
    {
        private readonly ISender _sender;

        public NodeController(ISender sender)
        {
            _sender = sender;
        }

        [Route("/api.user.node.create")]
        [HttpPost]
        [SwaggerOperation(Description = "Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(
            [FromQuery][Required] string treeName,
            [FromQuery][Required] int parentNodeId,
            [FromQuery][Required] string nodeName)
        {
            await _sender.Send(new CreateNodeCommand(treeName, parentNodeId, nodeName));

            return Ok(HttpStatusCode.OK);
        }

        [Route("/api.user.node.rename")]
        [HttpPost]
        [SwaggerOperation(Description = "Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Rename(
            [FromQuery][Required] string treeName,
            [FromQuery][Required] int nodeId,
            [FromQuery][Required] string newNodeName)
        {
            await _sender.Send(new UpdateNodeCommand(treeName, nodeId, newNodeName));

            return Ok(HttpStatusCode.OK);
        }

        [Route("/api.user.node.delete")]
        [HttpPost]
        [SwaggerOperation(Description = "Delete an existing node in your tree. You must specify a node ID that belongs your tree.")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(
            [FromQuery][Required] string treeName,
            [FromQuery][Required] int nodeId)
        {
            await _sender.Send(new DeleteNodeCommand(treeName, nodeId));

            return Ok(HttpStatusCode.OK);
        }
    }
}
