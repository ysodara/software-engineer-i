using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Peak_Performance.Models
{
    public class CoachProfileViewModel
    {
        private readonly PeakPerformance db = new PeakPerformance();
        public CoachProfileViewModel(int id)
        {
            coach = db.Coaches.Find(id);
            teams = db.Teams.Where(t => t.CoachId == id).ToList();
            CoachProfileId = coach.CoachId;
            athletes = new List<Athlete>();
            foreach (Team team in teams)
            {
                IEnumerable<Athlete> athletelist = db.Athletes.Where(a => a.TeamId == team.TeamId).ToList();
                if (athletelist != null)
                {
                    athletes.AddRange(athletelist);
                }
            }

        }
        public int CoachProfileId { get; set; }
        public virtual IEnumerable<Team> teams { get; set; }
        public virtual Coach coach { get; set; }
        public virtual List<Athlete> athletes { get; set; }

    }
}