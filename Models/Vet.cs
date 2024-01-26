using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZooAPI.Models;

public partial class Vet
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}
