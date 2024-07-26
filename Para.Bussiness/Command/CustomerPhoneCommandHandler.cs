using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Business.Command;
using Para.Schema;
using System.Threading;
using System.Threading.Tasks;
using Para.Bussiness.Cqrs;

namespace Para.Business.Command
{
    public class CustomerPhoneCommandHandler :
        IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
        IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
            await unitOfWork.CustomerPhoneRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerPhoneResponse>(mapped);
            return new ApiResponse<CustomerPhoneResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var customerPhone = await unitOfWork.CustomerPhoneRepository.GetById(request.Id);

            if (customerPhone == null)
            {
                return new ApiResponse("Customer phone not found");
            }

            mapper.Map(request.Request, customerPhone);
            await unitOfWork.Complete();

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var customerPhone = await unitOfWork.CustomerPhoneRepository.GetById(request.Id);

            if (customerPhone == null)
            {
                return new ApiResponse("Customer phone not found");
            }

            unitOfWork.CustomerPhoneRepository.Delete(customerPhone);
            await unitOfWork.Complete();

            return new ApiResponse();
        }
    }
}
