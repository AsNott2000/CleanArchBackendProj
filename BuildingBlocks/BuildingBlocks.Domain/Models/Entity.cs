using System;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Domain.Models;

public class Entity
{
    [Key]
    public Guid Id { get; set; }
}
