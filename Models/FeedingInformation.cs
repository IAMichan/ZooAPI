using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZooAPI.Models;

public partial class FeedingInformation
{
    [Key]
    public int Id { get; set; }

    public int? AnimalId { get; set; }

    public int? HeadkeeperId { get; set; }

    public int? FeedingDateId { get; set; }

    public virtual AnimalRecord? Animal { get; set; }
}
