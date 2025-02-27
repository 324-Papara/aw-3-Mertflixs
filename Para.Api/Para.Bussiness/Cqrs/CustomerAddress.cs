using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record GetAllCustomerAddressQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
public record GetCustomerAddressByIdQuery(long CustomerAddressId) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record CreateCustomerAddressCommand(CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record DeleteCustomerAddressCommand(long CustomerAddressId) : IRequest<ApiResponse>;
public record UpdateCustomerAddressCommand(long CustomerAddressId, CustomerAddressRequest Request) : IRequest<ApiResponse>;