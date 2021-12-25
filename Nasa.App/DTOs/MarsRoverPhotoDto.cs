using Nasa.DataAccess.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nasa.App.DTOs
{
    public class MarsRoverPhotoDto
    {
        [Required]
        public MarsRover? Rover { get; set; }
        [Required]
        public DateTime? EarthDate { get; set; }
        public MarsRoverCamera? Camera { get; set; }
    }
}
