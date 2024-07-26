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
    public class CustomerDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/customerdetail
        [HttpGet]
        public async Task<IActionResult> GetCustomerDetails()
        {
            var query = new GetAllCustomerDetailsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/customerdetail/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDetailById(long id)
        {
            var query = new GetCustomerDetailByIdQuery( id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST: api/customerdetail
        [HttpPost]
        public async Task<IActionResult> CreateCustomerDetail([FromBody] CustomerDetailRequest request)
        {
            var command = new CreateCustomerDetailCommand(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/customerdetail/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerDetail(long id, [FromBody] CustomerDetailRequest request)
        {
            var command = new UpdateCustomerDetailCommand(id, request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/customerdetail/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerDetail(long id)
        {
            var command = new DeleteCustomerDetailCommand( id );
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
