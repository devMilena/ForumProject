using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForumProject.Models.ForumModels
{
    public class Post
    {
        public int PostId { get; set; }
      
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
       
     
        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser{ get; set; }

        public List<UserPostLike> UsersPostLikes { get; set; }
        public List<UserPostDislike> UsersPostDislikes { get; set; }
       

        [NotMapped]
        public bool LikedByUser { get; set; }

        [NotMapped]
        public bool DislikedByUser { get; set; }

        [NotMapped]
        public bool PostedByUser { get; set; }


    }
}