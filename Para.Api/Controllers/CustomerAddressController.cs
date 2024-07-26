using Microsoft.AspNetCore.Mvc;
using MediatR;
using Para.Business.Command;
using Para.Business.Query;
using Para.Base.Response;
using Para.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;
using Para.Bussiness.Cqrs;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/customeraddress
        [HttpGet]
        public async Task<IActionResult> GetCustomerAddresses()
        {
            var query = new GetAllCustomerAddressesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/customeraddress/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAddressById(long id)
        {
            var query = new GetCustomerAddressByIdQuery( id );
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST: api/customeraddress
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAddress([FromBody] CustomerAddressRequest request)
        {
            var command = new CreateCustomerAddressCommand( request );
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/customeraddress/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAddress(long id, [FromBody] CustomerAddressRequest request)
        {
            var command = new UpdateCustomerAddressCommand( id,request );
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/customeraddress/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAddress(long id)
        {
            var command = new DeleteCustomerAddressCommand( id );
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
