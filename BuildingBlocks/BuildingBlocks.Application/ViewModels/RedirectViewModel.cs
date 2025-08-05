using System;

namespace BuildingBlocks.Application.ViewModels;

public class RedirectViewModel
{
    public required string Url { get; set; }
    public string Token { get; set; } = string.Empty;
}
