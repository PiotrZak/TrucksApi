using Trucks.API.Validators;

[TestFixture]
public class TruckStatusValidationTests
{
    [Test]
    public void Valid_Status_Update_To_OutOfService()
    {
        // Arrange
        var currentStatus = TruckStatus.Loading;
        var newStatus = TruckStatus.OutOfService;

        // Act
        var result = TruckValidator.ValidateTruckStatusUpdate(currentStatus, newStatus);

        // Assert
        Assert.That(result, Is.EqualTo($"Truck status changed to {newStatus}."));
    }

    [Test]
    public void Invalid_Status_Update_When_Not_OutOfService()
    {
        // Arrange
        var currentStatus = TruckStatus.ToJob;
        var newStatus = TruckStatus.AtJob;

        // Act
        var result = TruckValidator.ValidateTruckStatusUpdate(currentStatus, newStatus);

        // Assert
        Assert.That(result, Is.EqualTo($"Truck status changed to {newStatus}."));
    }

    [Test]
    public void Valid_Status_Update_When_Returning_To_Loading()
    {
        // Arrange
        var currentStatus = TruckStatus.Returning;
        var newStatus = TruckStatus.Loading;

        // Act
        var result = TruckValidator.ValidateTruckStatusUpdate(currentStatus, newStatus);

        // Assert
        Assert.That(result, Is.EqualTo("Truck status changed to Loading"));
    }

    [Test]
    public void Invalid_Status_Transition()
    {
        // Arrange
        var currentStatus = TruckStatus.AtJob;
        var newStatus = TruckStatus.Loading;

        // Act
        var result = TruckValidator.ValidateTruckStatusUpdate(currentStatus, newStatus);

        // Assert
        Assert.That(result, Is.EqualTo($"Invalid truck status transition from {currentStatus} to {newStatus}."));
    }

    [Test]
    public void Valid_Status_Transition_In_Order()
    {
        // Arrange
        var currentStatus = TruckStatus.Loading;
        var newStatus = TruckStatus.ToJob;

        // Act
        var result = TruckValidator.ValidateTruckStatusUpdate(currentStatus, newStatus);

        // Assert
        Assert.That(result, Is.EqualTo($"Truck status changed to {newStatus}."));
    }
}
