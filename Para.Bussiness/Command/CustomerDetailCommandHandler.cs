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
    public class CustomerDetailCommandHandler :
        IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>,
        IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerDetailCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            await unitOfWork.CustomerDetailRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerDetailResponse>(mapped);
            return new ApiResponse<CustomerDetailResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var customerDetail = await unitOfWork.CustomerDetailRepository.GetById(request.Id);

            if (customerDetail == null)
            {
                return new ApiResponse("Customer detail not found");
            }

            mapper.Map(request.Request, customerDetail);
            await unitOfWork.Complete();

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var customerDetail = await unitOfWork.CustomerDetailRepository.GetById(request.Id);

            if (customerDetail == null)
            {
                return new ApiResponse("Customer detail not found");
            }

            unitOfWork.CustomerDetailRepository.Delete(customerDetail);
            await unitOfWork.Complete();

            return new ApiResponse();
        }
    }
}
