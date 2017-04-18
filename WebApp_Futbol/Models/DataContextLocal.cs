﻿using Domain;

namespace WebApp_Futbol.Models
{
    public class DataContextLocal : DataContext
    {
        public System.Data.Entity.DbSet<Domain.Date> Dates { get; set; }

        public System.Data.Entity.DbSet<Domain.TournamentTeam> TournamentTeams { get; set; }
    }
}