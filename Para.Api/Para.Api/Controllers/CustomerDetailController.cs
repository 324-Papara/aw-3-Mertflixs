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
    public class CustomerDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> GetAllCustomerDetails()
        {
            var command = new GetAllCustomerDetailQuery();
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpGet("{customerDetailId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> GetCustomerDetailById([FromRoute] long customerDetailId)
        {
            var command = new GetCustomerDetailByIdQuery(customerDetailId);
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerDetailResponse>> CreateCustomerDetail([FromBody] CustomerDetailRequest request)
        {
            var command = new CreateCustomerDetailCommand(request);
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpPut("{customerDetailId}")]
        public async Task<ApiResponse> UpdateCustomerDetail(long customerDetailId, [FromBody] CustomerDetailRequest request)
        {
            var command = new UpdateCustomerDetailCommand(customerDetailId, request);
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpDelete("{customerDetailId}")]
        public async Task<ApiResponse> DeleteCustomerDetail(long customerDetailId)
        {
            var command = new DeleteCustomerDetailCommand(customerDetailId);
            var res = await _mediator.Send(command);
            return res;
        }
    }
}