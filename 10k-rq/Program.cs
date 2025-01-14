using Npgsql;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

List<BookingRequest> requests = new List<BookingRequest>();
ConcurrentBag<BookingRequest> requestsBag = new ConcurrentBag<BookingRequest>();

app.MapPost("/bookEventsMemoryList", (BookingRequest request) =>
{
    requests.Add(request);
    return ($"booking request has been pleaced : event:  {request.EventId} seat {request.SeatNumber}, for customer {request.CustomerId}");
}).WithName("BookEventMemoryList")
.WithOpenApi();

app.MapPost("/bookEventsMemoryBag", (BookingRequest request) =>
{
    requestsBag.Add(request);
    return ($"booking request has been pleaced : event:  {request.EventId} seat {request.SeatNumber}, for customer {request.CustomerId}");
}).WithName("BookEventMemoryBag")
.WithOpenApi();

app.MapPost("/bookEventsDb", (BookingRequest request) =>
{
    AddBooking(request);
    return ($"booking request has been pleaced : event:  {request.EventId} seat {request.SeatNumber}, for customer {request.CustomerId}");
}).WithName("BookEventDb")
.WithOpenApi();

app.MapGet("/bookEventsMemoryListCount", () =>
{
    return requests.Count;
});

app.MapGet("/bookEventsMemoryBagCount", () =>
{
    return requestsBag.Count;
});

app.Run();

 void AddBooking(BookingRequest request)
{
            // Connection string to the PostgreSQL database
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=eventbooker";

        // SQL query to insert data into the eventBooking table
        string insertQuery = "INSERT INTO bookingrequest (EventId, CustomerId, SeatNumber) VALUES (@EventId, @CustomerId, @SeatNumber)";

        try
        {
            // Establish connection to PostgreSQL database
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Prepare the SQL command
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@EventId", request.EventId); // Example event ID
                    command.Parameters.AddWithValue("@CustomerId", request.CustomerId); // Example user ID
                    command.Parameters.AddWithValue("@SeatNumber", request.SeatNumber); // Current date and time

                    // Execute the SQL command
                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted into the eventBooking table.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
}

internal record struct BookingRequest(int SeatNumber, int EventId, int CustomerId);
