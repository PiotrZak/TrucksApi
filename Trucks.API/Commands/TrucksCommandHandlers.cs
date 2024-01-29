using MediatR;
using Trucks.API.Interfaces;
using Trucks.API.Validators;


public class GetTruckCommandHandler : IRequestHandler<GetTruckQuery, Truck>
{
    private readonly ITrucksQueries _trucksQueries;

    public GetTruckCommandHandler(ITrucksQueries truckQueries)
    {
        _trucksQueries = truckQueries;
    }

    public async Task<Truck> Handle(GetTruckQuery truckQuery, CancellationToken cancellationToken)
    {
        return await _trucksQueries.GetTruckMainAsync(truckQuery.TruckId);
    }
}


public class GetAllTrucksQueryHandler : IRequestHandler<GetAllTrucksQuery, IEnumerable<Truck>>
{
    private readonly ITrucksQueries _trucksQueries;

    public GetAllTrucksQueryHandler(ITrucksQueries truckQueries)
    {
        _trucksQueries = truckQueries;
    }

    public async Task<IEnumerable<Truck>> Handle(GetAllTrucksQuery request, CancellationToken cancellationToken)
    {
        var trucks = await _trucksQueries.GetAllTrucksAsync();
        return trucks.OrderBy(truck => truck.Status).ToList(); ;
    }
}

public class GetFilteredTrucksQueryHandler : IRequestHandler<GetFilteredTrucksQuery, IEnumerable<Truck>>
{
    private readonly ITrucksQueries _trucksQueries;

    public GetFilteredTrucksQueryHandler(ITrucksQueries truckQueries)
    {
        _trucksQueries = truckQueries;
    }

    public async Task<IEnumerable<Truck>> Handle(GetFilteredTrucksQuery request, CancellationToken cancellationToken)
    {
        var filteredTrucks = await _trucksQueries.GetFilteredTrucksAsync(request.Name, request.Status, request.Description);
        return filteredTrucks.OrderBy(truck => truck.Status).ToList(); ;
    }
}

public class UpdateTruckStatusCommandHandler : IRequestHandler<UpdateTruckStatusCommand, string>
{
    private readonly ITrucksQueries _trucksQueries;

    public UpdateTruckStatusCommandHandler(ITrucksQueries truckQueries)
    {
        _trucksQueries = truckQueries;
    }

    public async Task<string> Handle(UpdateTruckStatusCommand request, CancellationToken cancellationToken)
    {
        var truckToUpdate = await _trucksQueries.GetTruckMainAsync(request.TruckId);

        if (truckToUpdate == null)
        {
            throw new Exception($"Truck with ID {request.TruckId} not found.");
        }

        var result = TruckValidator.ValidateTruckStatusUpdate(truckToUpdate.Status, request.Status);

        if (result.Contains("Invalid")){
            return result;
        }
        else
        {
            var databaseOperation = await _trucksQueries.UpdateTruckStatusAsync(request.TruckId, request.Status);
            if (databaseOperation)
            {
                return result;
            }
            else
            {
                throw new Exception("Internal database error");
            }
        }
    }
}


public class CreateTruckCommandHandler : IRequestHandler<InsertTruckCommand, bool>
{
    private readonly ITrucksQueries _trucksQueries;

    public CreateTruckCommandHandler(ITrucksQueries truckQueries)
    {
        _trucksQueries = truckQueries;
    }

    public async Task<bool> Handle(InsertTruckCommand request, CancellationToken cancellationToken)
    {
        var operationStatus = await _trucksQueries.InsertTruckAsync(request);
        return operationStatus;
    }
}

public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, bool>
{
    private readonly ITrucksQueries _trucksQueries;

    public UpdateTruckCommandHandler(ITrucksQueries truckQueries)
    {
        _trucksQueries = truckQueries;
    }

    public async Task<bool> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
    {
        var operationStatus = await _trucksQueries.UpdateTruckAsync(request);
        return operationStatus;
    }
}

public class DeleteTruckCommandHandler : IRequestHandler<DeleteTruckCommand, bool>
{
    private readonly ITrucksQueries _trucksQueries;

    public DeleteTruckCommandHandler(ITrucksQueries truckQueries)
    {
        _trucksQueries = truckQueries;
    }

    public async Task<bool> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
    {
        var operationStatus = await _trucksQueries.DeleteTruckAsync(request.TruckId);
        return operationStatus;
    }
}