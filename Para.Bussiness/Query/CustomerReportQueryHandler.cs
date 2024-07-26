using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;


namespace Para.Bussiness.Query;

public class CustomerReportQueryHandler :IRequestHandler<GetCustomerReportByIdQuery, ApiResponse<CustomerReportResponse>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public CustomerReportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;

    }

    public async Task<ApiResponse<CustomerReportResponse>> Handle(GetCustomerReportByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerReportRepository.GetById(request.CustomerId);
        var mapped = mapper.Map<CustomerReportResponse>(entity);
        return new ApiResponse<CustomerReportResponse>(mapped);
    }

}