CREATE DATABASE EventBooker;

USE EventBooker;

CREATE TABLE SiteEvents (
    Id INT NOT NULL PRIMARY KEY, 
    Name VARCHAR(255) NOT NULL,        
    Venue VARCHAR(255) NOT NULL,       
    Date DATETIME NOT NULL             
);
