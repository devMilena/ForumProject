using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumProject.Models.ForumModels.ForumViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        public string Text { get; set; }
        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; }
        
    }
}