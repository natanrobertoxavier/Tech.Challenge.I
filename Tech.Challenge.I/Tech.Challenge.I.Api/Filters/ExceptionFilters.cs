using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tech.Challenge.I.Exceptions.ExceptionBase;
using Tech.Challenge.I.Communication.Response;
using Tech.Challenge.I.Exceptions;

namespace Tech.Challenge.I.Api.Filters;

public class ExceptionFilters
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is TechChallengeException)
        {
            ProcessTechChallengeException(context);
        }
        else
        {
            LancarErroDesconhecido(context);
        }
    }

    private static void ProcessTechChallengeException(ExceptionContext context)
    {
        if (context.Exception is ValidationErrosException)
        {
            TratarErrosDeValidacaoException(context);
        }
        else if (context.Exception is InvalidLoginException)
        {
            TratarLoginException(context);
        };
    }

    private static void TratarErrosDeValidacaoException(ExceptionContext context)
    {
        var erroDeValidacaoException = context.Exception as ValidationErrosException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ErrorResponseJson(erroDeValidacaoException.MensagensDeErro));
    }

    private static void TratarLoginException(ExceptionContext context)
    {
        var erroLogin = context.Exception as InvalidLoginException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new ErrorResponseJson(erroLogin.Message));
    }

    private static void LancarErroDesconhecido(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ErrorResponseJson(ErrorMessages.UnknowError));
    }
}
