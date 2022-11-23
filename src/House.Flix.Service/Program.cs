using House.Flix.PostgreSQL;
using House.Flix.Service.Common;

var builder = WebApplication.CreateBuilder(args).AddHouseFlixService();
var app = builder.Build();
app.UseHouseFlixService();
await app.ApplyMigrationsAsync();
await app.RunAsync();
