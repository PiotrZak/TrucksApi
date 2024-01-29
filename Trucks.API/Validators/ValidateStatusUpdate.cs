namespace Trucks.API.Validators
{
    public class TruckValidator
    {

        public static string ValidateTruckStatusUpdate(TruckStatus currentStatus, TruckStatus newStatus)
        {
            if (newStatus == TruckStatus.OutOfService || currentStatus == TruckStatus.OutOfService)
            {
                // "Out Of Service" status can be set regardless of the current status
                return ($"Truck status changed to {newStatus}.");
            }

            if (currentStatus == TruckStatus.Returning && newStatus == TruckStatus.Loading)
            {
                // Allow "Loading" when the truck is "Returning"
                return "Truck status changed to Loading";
            }

            // Check the order of the statuses
            var allowedTransitions = new Dictionary<TruckStatus, TruckStatus[]>
        {
            { TruckStatus.Loading, new[] { TruckStatus.ToJob } },
            { TruckStatus.ToJob, new[] { TruckStatus.AtJob } },
            { TruckStatus.AtJob, new[] { TruckStatus.Returning } }
        };

            if (!allowedTransitions.TryGetValue(currentStatus, out var allowedNextStatuses) || !allowedNextStatuses.Contains(newStatus))
            {
                return $"Invalid truck status transition from {currentStatus} to {newStatus}.";
            }
            return ($"Truck status changed to {newStatus}.");
        }
    }
}

public class InvalidStatusUpdateException : Exception
{
    public InvalidStatusUpdateException(string message) : base(message) { }
}
