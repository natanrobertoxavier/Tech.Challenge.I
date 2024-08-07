using Microsoft.AspNetCore.Mvc;
using Tech.Challenge.I.Api.Filters;
using Tech.Challenge.I.Application.UseCase.Contact.Recover;
using Tech.Challenge.I.Application.UseCase.Contact.Register;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class ContactController : TechChallangeController
{
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<ResponseContactJson>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterContact(
        [FromServices] IRegisterContactUseCase useCase,
        [FromBody] RequestContactJson request)
    {
        await useCase.Execute(request);

        return Created(string.Empty, null);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ResponseContactJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverAllContacts(
        [FromServices] IRecoverContactUseCase useCase)
    {
        var result = await useCase.Execute();

        if (result.Any())
            return Ok(result);

        return NoContent();
    }
}
