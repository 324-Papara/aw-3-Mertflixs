using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Para.Bussiness.Command
{
    public class CustomerPhoneCommandHandler : IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
        IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CustomerPhone>(request.Request);
            command.InsertUser = "System";
            await _unitOfWork.CustomerPhoneRepository.Insert(command);
            await _unitOfWork.Complete();

            var res = _mapper.Map<CustomerPhoneResponse>(command);
            return new ApiResponse<CustomerPhoneResponse>(res);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CustomerPhone>(request.Request);
            command.Id = request.CustomerPhoneId;
            command.InsertUser = "System";
            _unitOfWork.CustomerPhoneRepository.Update(command);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CustomerPhoneRepository.Delete(request.CustomerPhoneId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}