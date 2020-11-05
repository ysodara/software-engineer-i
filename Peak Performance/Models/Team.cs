namespace Peak_Performance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            Athletes = new HashSet<Athlete>();
        }

        public int TeamId { get; set; }

        [Required]
        [StringLength(200)]
        public string TeamName { get; set; }

        public int? SportId { get; set; }

        public int? CoachId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Athlete> Athletes { get; set; }

        public virtual Coach Coach { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
