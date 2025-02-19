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
    public class CustomerDetailQueryHandler : IRequestHandler<GetAllCustomerDetailQuery, ApiResponse<List<CustomerDetailResponse>>>,
        IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetAllCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            List<CustomerDetail> entity = await _unitOfWork.CustomerDetailRepository.GetAll();
            var res = _mapper.Map<List<CustomerDetailResponse>>(entity);
            return new ApiResponse<List<CustomerDetailResponse>>(res);
        }

        public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CustomerDetailRepository.GetById(request.CustomerDetailId);
            var res = _mapper.Map<CustomerDetailResponse>(entity);
            return new ApiResponse<CustomerDetailResponse>(res);
        }
    }
}