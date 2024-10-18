using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class ActivityTracking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int BookmarkId { get; set; }
        [Required]
        public DateTime ClickedAt { get; set; }
        public string SourceUrl { get; set; }
    }
}
