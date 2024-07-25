using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Para.Bussiness.Command
{
    public class CustomerDetailCommandHandler : IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>,
        IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerDetailCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CustomerDetail>(request.Request);
            command.InsertUser = "System";
            await _unitOfWork.CustomerDetailRepository.Insert(command);
            await _unitOfWork.Complete();

            var res = _mapper.Map<CustomerDetailResponse>(command);
            return new ApiResponse<CustomerDetailResponse>(res);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CustomerDetail>(request.Request);
            command.Id = request.CustomerDetailId;
            command.InsertUser = "System";
            _unitOfWork.CustomerDetailRepository.Update(command);
            await _unitOfWork.Complete();

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CustomerDetailRepository.Delete(request.CustomerDetailId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}