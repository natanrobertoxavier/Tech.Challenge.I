﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tech.Challenge.I.Application.Services.Cryptography;
using Tech.Challenge.I.Application.Services.LoggedUser;
using Tech.Challenge.I.Application.Services.Token;
using Tech.Challenge.I.Application.UseCase.DDD;
using Tech.Challenge.I.Application.UseCase.User.ChangePassword;
using Tech.Challenge.I.Application.UseCase.User.Login;
using Tech.Challenge.I.Application.UseCase.User.Register;

namespace Tech.Challenge.I.Application;

public static class Initializer
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAdditionalKeyPassword(services, configuration);
        AddJWTToken(services, configuration);
        AddUseCases(services);
    }

    private static void AddAdditionalKeyPassword(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Settings:Password:AdditionalKeyPassword");
        services.AddScoped(option => new PasswordEncryptor(section.Value));
    }

    private static void AddJWTToken(IServiceCollection services, IConfiguration configuration)
    {
        var sectionLifeTime = configuration.GetRequiredSection("Settings:Jwt:LifeTimeTokenMinutes");
        var sectionKey = configuration.GetRequiredSection("Settings:Jwt:KeyToken");
        services.AddScoped(option => new TokenController(int.Parse(sectionLifeTime.Value), sectionKey.Value));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<ILoggedUser, LoggedUser>();
        services.AddScoped<IRegisterRegionDDDUseCase, RegisterRegionDDDUseCase>();
    }
}
