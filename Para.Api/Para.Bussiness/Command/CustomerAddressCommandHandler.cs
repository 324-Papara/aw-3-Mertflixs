using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command
{
    public class CustomerAddressCommandHandler : IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
        IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            command.InsertUser = "System";
            await _unitOfWork.CustomerAddressRepository.Insert(command);
            await _unitOfWork.Complete();

            var res = _mapper.Map<CustomerAddressResponse>(command);
            return new ApiResponse<CustomerAddressResponse>(res);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            command.Id = request.CustomerAddressId;
            command.InsertUser = "System";
            _unitOfWork.CustomerAddressRepository.Update(command);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CustomerAddressRepository.Delete(request.CustomerAddressId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}