using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;

public class GlobalExceptionMiddleware
{
	private readonly RequestDelegate _next;

	public GlobalExceptionMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			Log.Error(ex, "Beklenmeyen bir hata olustu");
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";

			var response = new
			{
				message = "Sunucuda bir hata olustu",
				details = ex.Message 
			};
			await context.Response.WriteAsJsonAsync(response);
		}
	}
}