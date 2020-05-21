using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForumProject.Models.ForumModels
{
    public class UserPostDislike
    {
        public int UserPostDislikeId { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
  
        public Post Post { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}