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
        public List<UserDiscussionLike> UsersDiscussionLikes { get; set; }
        public List<UserDiscussionDislike> UsersDiscussionDislikes { get; set; }
        
        [NotMapped]
        public bool DiscussLikedByUser { get; set; }
        
        [NotMapped]
        public bool DiscussDislikedByUser { get; set; }

        [NotMapped]
        public bool DiscussionCreatedByUser { get; set; }

    }
}