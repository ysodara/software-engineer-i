using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HW5.Models;

namespace HW5.DAL
{
    public class AssignmentContext :DbContext
    {
        public AssignmentContext() : base("name=AssignmentDB")
        {
            Database.SetInitializer<AssignmentContext>(null);
        }

        // "Users" is the name of the table to access
        // <User> is the entity that resides in each row of the table
        public virtual DbSet<Assignment> Users { get; set; }
    }
}
