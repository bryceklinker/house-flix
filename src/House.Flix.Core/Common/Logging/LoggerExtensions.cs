using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace House.Flix.Core.Common.Logging;

public static class LoggerExtensions
{
    public static async Task Capture<TInput>(this ILogger logger, TInput input, Func<Task> func)
    {
        await logger.Capture(input, async () =>
        {
            await func().ConfigureAwait(false);
            return Unit.Value;
        }).ConfigureAwait(false);
    }
    
    public static async Task<TResult> Capture<TInput, TResult>(this ILogger logger, TInput input, Func<Task<TResult>> func)
    {
        var inputType = input?.GetType().Name ?? "unknown";
        var stopwatch = Stopwatch.StartNew();
        try
        {
            logger.LogInformation("Executing {InputType} {@Input}", inputType, input);
            return await func().ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            stopwatch.Stop();
            logger.LogError(exception, "Failed executing {CqrsType} {@Parameters} after {Duration}", inputType, input,
                stopwatch.ElapsedMilliseconds);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            logger.LogInformation("Finished executing {CqrsType} {@Parameters} after {Duration} ms", inputType, input,
                stopwatch.ElapsedMilliseconds);
        }
    }
}