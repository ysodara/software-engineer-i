using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW5.Models
{
    public class Assignment

    {        
        /*[ID]              INT IDENTITY(1,1)  NOT NULL,
        [PriorityHW]        INT NOT NULL,
	    [DueDate]           DATE NOT NULL,
	    [DueTime]           TIME NOT NULL,
	    [Department]        NVARCHAR(3)         NOT NULL,
        [CourseNum]         INT NOT NULL,
	    [HomeworkTitle]     NVARCHAR(MAX)       NOT NULL,
        [Notes]             NVARCHAR(MAX)       NULL*/

        public int ID { get; set; }

        [DisplayName("PriorityHW")]
        [RegularExpression("[1-5]+", ErrorMessage = "Range: 1-5") ]
        [Required]
        public int PriorityHW { get; set; }

        [DisplayName("DueDate"), Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime DueDate { get; set; }

        
        [DisplayName("DueTime"), Required]
        [RegularExpression("([01]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]", ErrorMessage = "Time format: hh:mm:ss")]
        public TimeSpan DueTime { get; set; }

        [DisplayName("Department")]
        [StringLength(3), Required]
        public string Department { get; set; }

        [DisplayName("CourseNum"), Required]
        public int CourseNum { get; set; }

        [DisplayName("HomeworkTitle")]
        [Required]
        public string HomeworkTitle { get; set; }

        [DisplayName("Notes")]
        
        public string Notes { get; set; }
    }

}