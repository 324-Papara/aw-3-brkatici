using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerAddressCommand(CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record UpdateCustomerAddressCommand(long Id, CustomerAddressRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerAddressCommand(long Id) : IRequest<ApiResponse>;

public record GetAllCustomerAddressesQuery : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
public record GetCustomerAddressByIdQuery(long Id) : IRequest<ApiResponse<CustomerAddressResponse>>;

