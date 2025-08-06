using System;
using BuildingBlocks.Domain.Models;

namespace CleanArchProject.Domain.Models;

public class UsersModel : TrackableEntity
{

public Guid Id { get; set; }
public string userName { get; set; } = null!;
public string password { get; set; } = null!;

public string name{ get; set; } = string.Empty;

public string surname{ get; set; } = string.Empty;

public Guid carId{ get; set; }

}
