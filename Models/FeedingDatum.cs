using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZooAPI.Models;

public partial class FeedingDatum
{
    [Key]
    public int Id { get; set; }

    public int? AnimalId { get; set; }

    public DateOnly? FromDate { get; set; }

    public string? Food { get; set; }

    public virtual AnimalRecord? Animal { get; set; }
}
