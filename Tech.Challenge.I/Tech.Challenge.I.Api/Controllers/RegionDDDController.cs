using Microsoft.AspNetCore.Mvc;
using Tech.Challenge.I.Api.Filters;
using Tech.Challenge.I.Application.UseCase.DDD.Recover;
using Tech.Challenge.I.Application.UseCase.DDD.Register;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Response;

namespace Tech.Challenge.I.Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class RegionDDDController : TechChallangeController
{
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<RegionDDDResponseJson>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterDDD(
        [FromServices] IRegisterRegionDDDUseCase useCase,
        [FromBody] RequestRegionDDDJson request)
    {
        await useCase.Execute(request);

        return Created(string.Empty, null);
    }

    [HttpGet]
    [Route("RecoverAll")]
    [ProducesResponseType(typeof(IEnumerable<RegionDDDResponseJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverAll(
        [FromServices] IRecoverRegionDDDUseCase useCase)
    {
        var result = await useCase.Execute();

        if (result.Any())
            return Ok(result);

        return NoContent();
    }
}
