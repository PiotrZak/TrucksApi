CREATE DATABASE [trucks-db]
GO

USE [trucks-db];
GO

CREATE TABLE Trucks (
    TruckID INT PRIMARY KEY,
    UniqueCode VARCHAR(20) UNIQUE NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Status INT NOT NULL,
    Description VARCHAR(1000)
);

GO

INSERT INTO Trucks (TruckID, UniqueCode, Name, Status, Description)
VALUES
    (1, 'ABC123', 'Truck1', 0, 'This truck is currently out of service.'),
    (2, 'XYZ456', 'Truck2', 1, 'This truck is currently loading.'),
    (3, '123ABC', 'Truck3', 4, 'This truck is returning from a job.'),
    (4, '456DEF', 'Truck4', 0, 'This truck is currently out of service.'),
    (5, '789GHI', 'Truck5', 0, 'This truck is currently out of service.'),
    (6, 'JKL321', 'Truck6', 1, 'This truck is currently loading.'),
    (7, 'MNO987', 'Truck7', 2, 'This truck is en route to a job.'),
    (8, 'PQR654', 'Truck8', 3, 'This truck is currently at a job.'),
    (9, 'STU789', 'Truck9', 4, 'This truck is returning from a job.'),
    (10, 'VWX234', 'Truck10', 0, 'This truck is currently out of service.');
GO