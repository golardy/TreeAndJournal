using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TreeAndJournal.Api.RequestModels;
using TreeAndJournal.Application.Journal.GetFilteredItems;
using TreeAndJournal.Application.Journal.GetItem;

namespace TreeAndJournal.Api.Controllers
{
    [ApiController]
    public class JournalController : Controller
    {
        private readonly ISender _sender;

        public JournalController(ISender sender) 
        { 
            _sender = sender;
        }

        [Route("api.user.journal.getSingle")]
        [HttpPost]
        [SwaggerOperation(Description = "Returns the information about an particular event by ID.")]
        [ProducesResponseType(typeof(List<JournalDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSingle([Required]int id)
        {
            var result = await _sender.Send(new GetJournalItemQuery(id));

            return Ok(result);
        }

        [Route("api.user.journal.getRange")]
        [HttpPost]
        [SwaggerOperation(Description = "Provides the pagination API. Skip means the number of items should be skipped by server." +
            " Take means the maximum number items should be returned by server. All fields of the filter are optional.")]
        [ProducesResponseType(typeof(List<JournalDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRange(
            [FromQuery][Required] int skip, 
            [FromQuery][Required] int take,
            [FromBody][Required] GetJournalFilterBody getRangeFilterParams)
        {
            var result = await _sender.Send(
                new GetFilteredItemsQuery(
                    skip, 
                    take, 
                    getRangeFilterParams.From, 
                    getRangeFilterParams.To, 
                    getRangeFilterParams.Search
               )
            );

            return Ok(result);
        }
    }
}
