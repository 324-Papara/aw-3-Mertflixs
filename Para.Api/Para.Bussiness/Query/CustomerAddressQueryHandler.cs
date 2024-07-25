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

namespace Para.Bussiness.Query
{
    public class CustomerAddressQueryHandler : IRequestHandler<GetAllCustomerAddressQuery, ApiResponse<List<CustomerAddressResponse>>>,
        IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAddressQuery request, CancellationToken cancellationToken)
        {
            List<CustomerAddress> entity = await _unitOfWork.CustomerAddressRepository.GetAll();
            var res = _mapper.Map<List<CustomerAddressResponse>>(entity);
            return new ApiResponse<List<CustomerAddressResponse>>(res);
        }

        public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CustomerAddressRepository.GetById(request.CustomerAddressId);
            var res = _mapper.Map<CustomerAddressResponse>(entity);
            return new ApiResponse<CustomerAddressResponse>(res);
        }
    }
}