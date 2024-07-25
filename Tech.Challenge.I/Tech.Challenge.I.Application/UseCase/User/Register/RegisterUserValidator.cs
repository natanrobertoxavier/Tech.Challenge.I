﻿using FluentValidation;
using System.Text.RegularExpressions;
using Tech.Challenge.I.Communication.Request;
using Tech.Challenge.I.Domain.Entities;
using Tech.Challenge.I.Exceptions;

namespace Tech.Challenge.I.Application.UseCase.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage(ErrorMessages.BlankUserName);
        RuleFor(c => c.Email).NotEmpty().WithMessage(ErrorMessages.BlankUserEmail);
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());
        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ErrorMessages.InvalidUserEmail);
        });
    }
}
