using Microsoft.AspNetCore.Mvc;
using Tech.Challenge.I.Api.Filters;
using Tech.Challenge.I.Application.UseCase.DDD.Register;
using Tech.Challenge.I.Communication.Request;

namespace Tech.Challenge.I.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class ContactController : TechChallangeController
{
    //[HttpPost]
    //[ProducesResponseType(typeof(IEnumerable<ContactResponseJson>), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> RegisterDDD(
    //    [FromServices] IRegisterRegionDDDUseCase useCase,
    //    [FromBody] RequestRegionDDDJson request)
    //{
    //    await useCase.Execute(request);

    //    return Created(string.Empty, null);
    //}
}
