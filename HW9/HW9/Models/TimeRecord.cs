namespace HW8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeRecord")]
    public partial class TimeRecord
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string EventTitle { get; set; }

        public double Time { get; set; }

        public int? AthleteID { get; set; }

        public int? LocationID { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Location Location { get; set; }
    }
}
