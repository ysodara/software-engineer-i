namespace Peak_Performance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Athlete
    {
        public int AthleteId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string PreferredName { get; set; }

        public byte[] ProfilePic { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        [StringLength(200)]
        public string Gender { get; set; }

        public bool Active { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
