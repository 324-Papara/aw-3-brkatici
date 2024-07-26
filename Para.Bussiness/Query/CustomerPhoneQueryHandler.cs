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
    public class CustomerPhoneQueryHandler :
        IRequestHandler<GetAllCustomerPhonesQuery, ApiResponse<List<CustomerPhoneResponse>>>,
        IRequestHandler<GetCustomerPhoneByIdQuery, ApiResponse<CustomerPhoneResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerPhoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhonesQuery request, CancellationToken cancellationToken)
        {
            var customerPhones = await unitOfWork.CustomerPhoneRepository.GetAll();
            var response = customerPhones.Select(cp => mapper.Map<CustomerPhoneResponse>(cp)).ToList();
            return new ApiResponse<List<CustomerPhoneResponse>>(response);
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByIdQuery request, CancellationToken cancellationToken)
        {
            var customerPhone = await unitOfWork.CustomerPhoneRepository.GetById(request.Id);

            if (customerPhone == null)
            {
                return new ApiResponse<CustomerPhoneResponse>("Customer phone not found");
            }

            var response = mapper.Map<CustomerPhoneResponse>(customerPhone);
            return new ApiResponse<CustomerPhoneResponse>(response);
        }
    }
}
