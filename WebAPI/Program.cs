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

app.Run(
    string.Format("http://localhost:{0}", 
    Environment.GetEnvironmentVariable("FUNCTIONS_CUSTOMHANDLER_PORT") ?? "5000"));

