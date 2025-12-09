using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace multi_tenant_chatBot.ApiService;

public class ApiKey:IAsyncActionFilter
{

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var endPoint = context.HttpContext.GetEndpoint();
        var requiredApiKey = endPoint?.Metadata.GetMetadata<RequiredApiKeyAttribute>();

        if (requiredApiKey == null)
        {
            await next();
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue("x-api-key", out var extractedKey))
        {
            context.Result=new UnauthorizedObjectResult("Api key is missing");
            return;
        }
        context.HttpContext.Items["ApiKey"] =extractedKey.ToString();
        await next();
            
    }
}