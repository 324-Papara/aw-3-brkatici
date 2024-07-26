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
    public class CustomerAddressCommandHandler :
        IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
        IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            await unitOfWork.CustomerAddressRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerAddressResponse>(mapped);
            return new ApiResponse<CustomerAddressResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerAddress = await unitOfWork.CustomerAddressRepository.GetById(request.Id);

            if (customerAddress == null)
            {
                return new ApiResponse("Customer address not found");
            }

            mapper.Map(request.Request, customerAddress);
            await unitOfWork.Complete();

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerAddress = await unitOfWork.CustomerAddressRepository.GetById(request.Id);

            if (customerAddress == null)
            {
                return new ApiResponse("Customer address not found");
            }

            unitOfWork.CustomerAddressRepository.Delete(customerAddress);
            await unitOfWork.Complete();

            return new ApiResponse();
        }
    }
}
