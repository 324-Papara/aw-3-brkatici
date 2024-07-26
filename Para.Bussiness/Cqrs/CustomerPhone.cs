using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;

public record UpdateCustomerPhoneCommand(long Id, CustomerPhoneRequest Request) : IRequest<ApiResponse>;

public record DeleteCustomerPhoneCommand(long Id) : IRequest<ApiResponse>;
public record GetAllCustomerPhonesQuery : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;

public record GetCustomerPhoneByIdQuery(long Id) : IRequest<ApiResponse<CustomerPhoneResponse>>;
