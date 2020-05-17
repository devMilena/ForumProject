using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumProject.Models.ForumModels.ForumViewModels
{
    public class CreateDiscussionViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name ="Category")]   
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }
    }
}