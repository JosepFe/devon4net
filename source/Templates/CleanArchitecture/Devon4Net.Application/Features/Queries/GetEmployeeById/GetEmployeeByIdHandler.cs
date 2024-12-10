using Devon4Net.Application.Dtos;
using Devon4Net.Application.Ports.Projectors;
using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.Common.Errors;
using Devon4Net.Infrastructure.Common.Models;
using MediatR;

namespace Devon4Net.Application.Features.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, DevonResult<EmployeeDto>>
{
    private readonly IEmployeeProjector _employeeProjector;

    public GetEmployeeByIdHandler(IEmployeeProjector employeeProjector)
    {
        _employeeProjector = employeeProjector;
    }

    public async Task<DevonResult<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        Devon4NetLogger.Debug("Started GetEmployeeByIdHandler");
        var employee = await _employeeProjector.GetEmployeeById(request.Id);

        if (employee == null)
        {
            var error = new DevonError("123", "user not found", System.Net.HttpStatusCode.NotFound);
            return DevonResult<EmployeeDto>.Error(error);
        }

        return employee;
    }
}