﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Tournament
    {
        [Key]
        public int TournamentId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener almenos 50 caracteres")]
        [Index("Tournament_Name_Index", IsUnique = true)]
        [Display(Name = "Tournament")]
        public string Name { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }
        public virtual ICollection<TournamentGroup> Groups { get; set; }

        public virtual ICollection<Date> Dates { get; set; }


    }
}
