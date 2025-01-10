var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/bookEvent", (BookingRequest request) =>
{
    return ($"book {request.EventId} seat {request.SeatNumber}, for customer {request.CustomerId}");
}).WithName("BookEvent")
.WithOpenApi();

app.Run();

internal record BookingRequest(int SeatNumber, int EventId, int CustomerId);
