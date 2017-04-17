using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TournamentGroup
    {
        [Key]
        public int TournamentGroupId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener almenos 50 caracteres")]
        [Index("TournamentGroup_Name_TournamentId_Index", IsUnique = true, Order = 1)]
        [Display(Name = "Grupo")]
        public string Name { get; set; }


        [Index("TournamentGroup_Name_TournamentId_Index", IsUnique = true, Order = 2)]
        [Display(Name = "Torneo")]
        public int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public virtual ICollection<Match> Matches { get; set; }

        public virtual ICollection<TournamentTeam> TournamentTeams { get; set; }

    }
}
