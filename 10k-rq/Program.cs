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
app.MapPost("/bookEvents", (BookingRequest request) =>
{
    requests.Add(request);
    return ($"booking request has been pleaced : event:  {request.EventId} seat {request.SeatNumber}, for customer {request.CustomerId}");
}).WithName("BookEvent")
.WithOpenApi();

app.MapGet("/bookEvents", () =>
{
    return requests;
});

app.MapGet("/bookEventsCount", () =>
{
    return requests.Count;
});

app.Run();

internal record struct BookingRequest(int SeatNumber, int EventId, int CustomerId);
