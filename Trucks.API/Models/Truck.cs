using MediatR;

public enum TruckStatus
{
    OutOfService,
    Loading,
    ToJob,
    AtJob,
    Returning
}

public class Truck
{
    public int TruckId { get; set; }
    public string UniqueCode { get; set; }
    public string Name { get; set; }
    public TruckStatus Status { get; set; }
    public string? Description { get; set; }
}

public record GetTruckQuery(int TruckId) : IRequest<Truck>;
public record GetAllTrucksQuery() : IRequest<IEnumerable<Truck>>;
public record GetFilteredTrucksQuery(string? Name, TruckStatus? Status, string? Description): IRequest<IEnumerable<Truck>>;


public record UpdateTruckStatusCommand(int TruckId, TruckStatus Status): IRequest<string>;
public record UpdateTruckCommand(int TruckId, string UniqueCode, string Name, string? Description) : IRequest<bool>;
public record InsertTruckCommand(string UniqueCode, string Name, TruckStatus Status, string? Description) : IRequest<bool>;
public record DeleteTruckCommand(int TruckId): IRequest<bool>;