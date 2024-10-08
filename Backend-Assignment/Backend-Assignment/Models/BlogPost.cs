﻿using System.ComponentModel.DataAnnotations;

namespace Backend_Assignment.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
