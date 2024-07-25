using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReportControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerReportControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Reports")]
        public async Task<IActionResult> GetCustomerReports()
        {
            var command = new GetCustomersReport();
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}