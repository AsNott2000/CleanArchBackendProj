using System;
using Swashbuckle.AspNetCore.Filters;

namespace CleanArchProject.Application.Authentication;

public class LoginCommandExample : IExamplesProvider<LoginCommand>
{
    public LoginCommand GetExamples()
    {
        return new LoginCommand(
            userName: "testuser",
            password: "password123"
        );
    }
}
