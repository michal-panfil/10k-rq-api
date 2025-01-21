

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM information_schema.tables 
        WHERE table_schema = 'public' AND table_name = 'venue'
    ) THEN

        CREATE TABLE Venue (
            Id SERIAL PRIMARY KEY, 
            Name VARCHAR(255) NOT NULL,        
            SeatsCount INT NOT NULL             
        );
        RAISE NOTICE 'Table Venue created.';
    ELSE
        RAISE NOTICE 'Table Venue already exists.';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM information_schema.tables 
        WHERE table_schema = 'public' AND table_name = 'siteevents'
    ) THEN
        CREATE TABLE SiteEvents (
            Id SERIAL PRIMARY KEY, 
            Name VARCHAR(255) NOT NULL,        
            VenueId INT NOT NULL,       
            EventStartDate DATE NOT NULL,   
            FOREIGN KEY (VenueId) REFERENCES Venue(Id)
            );

            RAISE NOTICE 'Table SiteEvents created.';
    ELSE
        RAISE NOTICE 'Table SiteEvents already exists.';
    END IF;
END $$;

    DO $$
    BEGIN
        IF NOT EXISTS (
            SELECT 1 
            FROM information_schema.tables 
            WHERE table_schema = 'public' AND table_name = 'bookingrequest'
        ) THEN
    CREATE TABLE  BookingRequest(
        Id INT NOT NULL PRIMARY KEY, 
        SeatNumber INT NOT NULL,
        EventId INT NOT NULL,
        CustomerId INT NOT NULL,
        FOREIGN KEY (EventId) REFERENCES SiteEvents(Id),
        CONSTRAINT unique_eventid_seatnumber UNIQUE (EventId, SeatNumber)
    );

        RAISE NOTICE 'Table BookingRequest created.';
    ELSE
        RAISE NOTICE 'Table BookingRequest already exists.';
    END IF;
END $$;


DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM Venue
    ) THEN
        INSERT INTO  Venue (Name, SeatsCount)
        VALUES ('Boston_Stadion', 100000), ('NewYork_Stadion', 100000);
        RAISE NOTICE 'Data inserted into Venue.';
    ELSE
        RAISE NOTICE 'Venue table already contains data.';
    END IF;
END $$;


DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM SiteEvents
    ) THEN
                INSERT INTO SiteEvents (Name, VenueId, EventStartDate)
                VALUES ('Concert1', 1, '2025-01-17');
                RAISE NOTICE 'Data inserted into SiteEvents.';
    ELSE
        RAISE NOTICE 'SiteEvents table already contains data.';
    END IF;
END $$;