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
    public class CustomerAddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> GetAllCustomerAddress()
        {
            var command = new GetAllCustomerAddressQuery();
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpGet("{customerAdressId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> GetCustomerById([FromRoute] long customerAdressId)
        {
            var command = new GetCustomerAddressByIdQuery(customerAdressId);
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerAddressResponse>> CreateCustomerAddress([FromBody] CustomerAddressRequest request)
        {
            var command = new CreateCustomerAddressCommand(request);
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpPut("{customerAddressId}")]
        public async Task<ApiResponse> UpdateCustomerAddress(long customerAddressId, [FromBody] CustomerAddressRequest request)
        {
            var command = new UpdateCustomerAddressCommand(customerAddressId, request);
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpDelete("{customerAddressId}")]
        public async Task<ApiResponse> DeleteCustomerAddress(long customerAddressId)
        {
            var command = new DeleteCustomerAddressCommand(customerAddressId);
            var res = await _mediator.Send(command);
            return res;
        }
    }
}