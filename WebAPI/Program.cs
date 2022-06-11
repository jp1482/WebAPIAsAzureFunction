using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.Map("/HttpTrigger",()=>{
     return "Hello From HttpTrigger Web API";
});

app.Map("/SaveRecord",()=>{
     return "Save Record Called Successfully.";     
});

app.Map("/SavePerson",()=>{
     return "Save Person Called Successfully.";
});

// app.MapPost("/UserTopicProcessor",async context =>{
//      context.Request.EnableBuffering();
//      var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
//      await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
//      Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));
//      await context.Response.WriteAsJsonAsync( new { Result = "ServiceBus Trigger for User Topic Processor Called"});
// });

app.MapPost("/UserTopicProcessor",async (HttpContext context, [FromBody] ServiceBusRequest input) =>
{   
     Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(input));
     await context.Response.WriteAsJsonAsync( new ServiceBusResponse() { Result = "ServiceBus Trigger for User Topic Processor Called"});
});

app.Run(
    string.Format("http://localhost:{0}", 
    Environment.GetEnvironmentVariable("FUNCTIONS_CUSTOMHANDLER_PORT") ?? "5000"));

public class ServiceBusRequest
{
     public Dictionary<string, object> Data { get; set; } = new();

     public Dictionary<string, object> Metadata { get; set; } = new();
}

public class ServiceBusResponse
{
     public object Result { get; set; }
}