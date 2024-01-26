using System;
using System.Collections.Generic;

namespace ZooAPI.Models;

public partial class VeterinaryRecord
{
    public int Id { get; set; }

    public int? AnimalId { get; set; }

    public int? VetId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Diagnosis { get; set; }

    public string? Prescription { get; set; }

    public virtual AnimalRecord? Animal { get; set; }
}
