﻿using System.Net;

namespace PandaIdentity.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try 
            {
                await _next(httpContext);
            }
            catch(Exception ex) 
            {
                var errorId = Guid.NewGuid();
                // Log the exception and send it to the file and console
                _logger.LogError(ex,$"{errorId}: {ex.Message}");

                // Return a custome exception response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong, Try again later"
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }

    }
}
