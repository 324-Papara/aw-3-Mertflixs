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
    public class CustomerPhoneControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerPhoneControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> GetAllCustomerPhones()
        {
            var command = new GetAllCustomerPhoneQuery();
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpGet("{customerPhoneId}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> GetCustomerPhoneById([FromRoute] long customerPhoneId)
        {
            var command = new GetCustomerPhoneByIdQuery(customerPhoneId);
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerPhoneResponse>> CreateCustomerPhone([FromBody] CustomerPhoneRequest request)
        {
            var command = new CreateCustomerPhoneCommand(request);
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpPut("{customerPhoneId}")]
        public async Task<ApiResponse> UpdateCustomerPhone(long customerPhoneId, [FromBody] CustomerPhoneRequest request)
        {
            var command = new UpdateCustomerPhoneCommand(customerPhoneId, request);
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpDelete("{customerPhoneId}")]
        public async Task<ApiResponse> DeleteCustomerPhone(long customerPhoneId)
        {
            var command = new DeleteCustomerPhoneCommand(customerPhoneId);
            var res = await _mediator.Send(command);
            return res;
        }
    }
}