using System;
using BuildingBlocks.Domain.Models;

namespace CleanArchProject.Domain.Models;

public class carModel : TrackableEntity
{
    public Guid Id { get; set; }

    public string carName{ get; set; } = string.Empty;
}
