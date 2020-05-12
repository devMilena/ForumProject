using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForumProject.Models.ForumModels
{
    public class Discussion 
    {
        public int DiscussionId { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int PostCount { get; set; }
        public int ViewCount { get; set; }
        [Required]
        public string Description { get; set; }
        public string UserId { get; set; }
        [ForeignKey ("UserId")]
        public ApplicationUser ApplicationUser{ get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public List<Post> Posts  { get; set; }
    }
}