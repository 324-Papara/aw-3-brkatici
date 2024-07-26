using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Para.Api.Models;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReportController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerReportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerReportResponse>> Get([FromRoute] long customerId)
        {
            var operation = new GetCustomerReportByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
