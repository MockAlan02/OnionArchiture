using Application.Wrappers;
using System.Net;
using System.Text.Json;

namespace WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate? _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Suceeded = false, Message = ex?.Message! };
        
              
                switch (ex)
                {
                    case Application.Exceptions.ApiException e:
                        //Custom application Error type Api
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case Application.Exceptions.ValidationException e:
                        //Custom aplication Error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        //Not found Error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
