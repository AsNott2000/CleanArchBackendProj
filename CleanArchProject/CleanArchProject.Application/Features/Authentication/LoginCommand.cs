using System;
using BuildingBlocks.Application.Features;

namespace CleanArchProject.Application.Authentication;

public record LoginCommand(
    string userName,
    string password
) : ICommandQuery<string>;

