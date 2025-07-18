﻿using System.ComponentModel.DataAnnotations;

namespace StudioGameService.DB.Model
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Description { get; set; }
        public ICollection<GameSelectedGenre> Games { get; set; } = new List<GameSelectedGenre>();
    }
}
