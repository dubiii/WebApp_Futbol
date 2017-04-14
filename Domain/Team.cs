using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener almenos 50 caracteres")]
        [Index("Team_Name_LeagueId_Index", IsUnique = true, Order = 1)]
        [Display(Name = "Equipo")]
        public string Name { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(3, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres", MinimumLength = 3)]
        [Index("Team_Initials_LeagueId_Index", IsUnique = true, Order = 1)]
        public string Initials { get; set; }

        [Index("Team_Name_LeagueId_Index", IsUnique = true, Order = 2)]
        [Index("Team_Initials_LeagueId_Index", IsUnique = true, Order = 2)]
        [Display(Name = "Liga")]
        public int LeagueId { get; set; }

        public virtual League League { get; set; }
    }
}
