﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tech.Challenge.I.Api.Filters;
using Tech.Challenge.I.Application.UseCase.Contact.Delete;
using Tech.Challenge.I.Application.UseCase.Contact.Recover;
using Tech.Challenge.I.Application.UseCase.Contact.Register;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Communication.Request.Enum;
using Tech.Challenge.I.Communication.Response;
using Tech.Challenge.I.Exceptions;

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
    [Route("RecoverAllContacts")]
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

    [HttpGet]
    [Route("RecoverContactsByRegion")]
    [ProducesResponseType(typeof(IEnumerable<ResponseContactJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverContactsByRegion(
        [FromQuery] [Required] RegionRequestEnum region,
        [FromServices] IRecoverContactUseCase useCase)
    {
        var result = await useCase.Execute(region);

        if (result.Any())
            return Ok(result);

        return NoContent();
    }

    [HttpGet]
    [Route("RecoverContactsByDDD")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RecoverContactsByDDD(
        [FromQuery] [Required] int ddd,
        [FromServices] IRecoverContactUseCase useCase)
    {
        var result = await useCase.Execute(ddd);

        if (result.Any())
            return Ok(result);

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(typeof(IEnumerable<ResponseContactJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Remove(
        [FromQuery] [Required] Guid id,
        [FromServices] IDeleteContactUseCase useCase)
    {
        var result = await useCase.Execute(id);

        if (result)
            return NoContent();

        return UnprocessableEntity(ErrorsMessages.NoContactsFound);
    }
}
