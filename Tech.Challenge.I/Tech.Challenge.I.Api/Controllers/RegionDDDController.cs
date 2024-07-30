using Microsoft.AspNetCore.Mvc;
using Tech.Challenge.I.Api.Filters;
using Tech.Challenge.I.Application.UseCase.DDD;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class RegionDDDController : TechChallangeController
{
    [HttpPost]
    [ProducesResponseType(typeof(RegionDDDResponseJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterDDD(
        [FromServices] IRegisterRegionDDDUseCase useCase,
        [FromBody] RequestRegistrationRegionDDDJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
