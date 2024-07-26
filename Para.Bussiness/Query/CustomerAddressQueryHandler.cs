using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Data.UnitOfWork;
using Para.Business.Query;
using Para.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Para.Bussiness.Cqrs;

namespace Para.Business.Query
{
    public class CustomerAddressQueryHandler :
        IRequestHandler<GetAllCustomerAddressesQuery, ApiResponse<List<CustomerAddressResponse>>>,
        IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAddressesQuery request, CancellationToken cancellationToken)
        {
            var customerAddresses = await unitOfWork.CustomerAddressRepository.GetAll();
            var response = customerAddresses.Select(ca => mapper.Map<CustomerAddressResponse>(ca)).ToList();
            return new ApiResponse<List<CustomerAddressResponse>>(response);
        }

        public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var customerAddress = await unitOfWork.CustomerAddressRepository.GetById(request.Id);

            if (customerAddress == null)
            {
                return new ApiResponse<CustomerAddressResponse>("Customer address not found");
            }

            var response = mapper.Map<CustomerAddressResponse>(customerAddress);
            return new ApiResponse<CustomerAddressResponse>(response);
        }
    }
}
