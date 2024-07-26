using Microsoft.AspNetCore.Mvc;
using MediatR;
using Para.Business.Query;
using Para.Business.Command;
using Para.Base.Response;
using Para.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;
using Para.Bussiness.Cqrs;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPhoneController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerPhoneController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/customerphone
        [HttpGet]
        public async Task<IActionResult> GetCustomerPhones()
        {
            var query = new GetAllCustomerPhonesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/customerphone/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerPhoneById(long id)
        {
            var query = new GetCustomerPhoneByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST: api/customerphone
        [HttpPost]
        public async Task<IActionResult> CreateCustomerPhone([FromBody] CustomerPhoneRequest request)
        {
            var command = new CreateCustomerPhoneCommand(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/customerphone/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerPhone(long id, [FromBody] CustomerPhoneRequest request)
        {
            var command = new UpdateCustomerPhoneCommand(id, request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/customerphone/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPhone(long id)
        {
            var command = new DeleteCustomerPhoneCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
