using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZooAPI.Models
{
    public partial class AnimalRecord
    {
        [Key]
        public int Id { get; set; }
        public string? AnimalName { get; set; }
        public string? Gender { get; set; }
        public string? Species { get; set; }
        public string? BirthPlace { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? ParentMaleName { get; set; }
        public int? ParentMaleId { get; set; }
        public string? ParentFemaleName { get; set; }
        public int? ParentFemaleId { get; set; }
        public string? Area { get; set; }
        public string? Enclosure { get; set; }

        public virtual ICollection<FeedingDatum> FeedingData { get; set; } = new List<FeedingDatum>();
        public virtual ICollection<FeedingInformation> FeedingInformations { get; set; } = new List<FeedingInformation>();
        public virtual ICollection<VeterinaryRecord> VeterinaryRecords { get; set; } = new List<VeterinaryRecord>();
    }
}
