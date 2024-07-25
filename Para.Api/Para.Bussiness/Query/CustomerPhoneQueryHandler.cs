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
    public class CustomerPhoneQueryHandler : IRequestHandler<GetAllCustomerPhoneQuery, ApiResponse<List<CustomerPhoneResponse>>>,
        IRequestHandler<GetCustomerPhoneByIdQuery, ApiResponse<CustomerPhoneResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerPhoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhoneQuery request, CancellationToken cancellationToken) {
            List<CutomerPhone> entity = await _unitOfWork.CustomerPhoneRepository.GetAll();
            var res = _mapper.Map<List<CustomerPhoneResponse>>(entity);
            return new ApiResponse<List<CustomerPhoneResponse>>(res);
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByIdQuery request, CancellationToken cancellationToken) {
            var entity = await _unitOfWork.CustomerPhoneRepository.GetById(request.CustomerPhoneId);
            var res = _mapper.Map<CustomerPhoneResponse>(entity);
            return new ApiResponse<CustomerPhoneResponse>(res);
        }
    }
}