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
    public class CustomerDetailQueryHandler :
        IRequestHandler<GetAllCustomerDetailsQuery, ApiResponse<List<CustomerDetailResponse>>>,
        IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetAllCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customerDetails = await unitOfWork.CustomerDetailRepository.GetAll();
            var response = customerDetails.Select(cd => mapper.Map<CustomerDetailResponse>(cd)).ToList();
            return new ApiResponse<List<CustomerDetailResponse>>(response);
        }

        public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var customerDetail = await unitOfWork.CustomerDetailRepository.GetById(request.Id);

            if (customerDetail == null)
            {
                return new ApiResponse<CustomerDetailResponse>("Customer detail not found");
            }

            var response = mapper.Map<CustomerDetailResponse>(customerDetail);
            return new ApiResponse<CustomerDetailResponse>(response);
        }
    }
}
