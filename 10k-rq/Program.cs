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

app.Run();

internal record BookingRequest(int SeatNumber, int EventId, int CustomerId);
