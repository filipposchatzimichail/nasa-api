using System;
using System.ComponentModel.DataAnnotations;

namespace Nasa.App.DTOs
{
    public class EpicImageDto
    {
        [Required]
        public DateTime? EpicDate { get; set; }
    }
}
