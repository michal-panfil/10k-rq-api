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

app.MapGet("/bookEventsMemoryListCount", () =>
{
    return requests.Count;
});

app.MapGet("/bookEventsMemoryBagCount", () =>
{
    return requestsBag.Count;
});

app.Run();

internal record struct BookingRequest(int SeatNumber, int EventId, int CustomerId);
