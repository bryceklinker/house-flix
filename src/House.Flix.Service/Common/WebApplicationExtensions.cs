namespace House.Flix.Service.Common;

public static class WebApplicationExtensions
{
    public static WebApplication UseHouseFlixService(this WebApplication app)
    {
        app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseRouting();
        app.UseEndpoints(e =>
        {
            e.MapControllers();
        });
        return app;
    }
}
