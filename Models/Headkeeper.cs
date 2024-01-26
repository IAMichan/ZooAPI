using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZooAPI.Models;

[Table("Headkeeper")]
public partial class Headkeeper
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}
