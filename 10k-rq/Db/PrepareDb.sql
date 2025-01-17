CREATE DATABASE EventBooker;

USE EventBooker;

CREATE TABLE Venue (
    Id SERIAL PRIMARY KEY, 
    Name VARCHAR(255) NOT NULL,        
    SeatsCount INT NOT NULL             
);

CREATE TABLE SiteEvents (
    Id SERIAL PRIMARY KEY, 
    Name VARCHAR(255) NOT NULL,        
    VenueId INT NOT NULL,       
    EventStartDate DATE NOT NULL,   
    FOREIGN KEY (VenueId) REFERENCES Venue(Id)
    )

CREATE TABLE  BookingRequest(
    Id INT NOT NULL PRIMARY KEY, 
    SeatNumber INT NOT NULL,
    EventId INT NOT NULL,
    CustomerId INT NOT NULL,
    FOREIGN KEY (EventId) REFERENCES SiteEvents(Id)
)

INSERT INTO  Venue (Name, SeatsCount)
VALUES ('Boston_Stadion', 100000), ('NewYork_Stadion', 100000)

INSERT INTO SiteEvents (Name, VenueId, EventStartDate)
VALUES ('Concert1', 1, '2025-01-17')