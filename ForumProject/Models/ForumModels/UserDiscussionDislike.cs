using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForumProject.Models.ForumModels
{
    public class UserDiscussionDislike
    {
        public int UserDiscussionDislikeId { get; set; }
        public int DiscussionId { get; set; }
        [ForeignKey("DiscussionId")]
        public Discussion Discussion { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}