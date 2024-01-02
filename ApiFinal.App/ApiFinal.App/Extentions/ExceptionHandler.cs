using ApiFinal.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace ApiFinal.App.Extentions
{
    public static class ExceptionHandler
    {
        public static void CustomeExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(opt =>
            {
                opt.Run(async context =>
                {
                    string message = "Internal error";
                    int statuscode = (int)HttpStatusCode.InternalServerError;   
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var exception = contextFeature.Error;
                    if (exception is ItemNotFoundException)
                    {
                        statuscode = 404;
                        message = exception.Message;
                    }
                    else if(exception is ItemAlreadyExist)
                    {
                        statuscode = 400;
                        message = exception.Message;
                    }

                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = statuscode;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            statusCode = statuscode,
                            Message = message
                        }
                        ));
                    }
                });
            });
        }
    }
}
