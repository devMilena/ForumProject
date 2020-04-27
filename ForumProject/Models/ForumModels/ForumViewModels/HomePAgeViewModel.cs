using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumProject.Models.ForumModels.ForumViewModels
{
    public class HomePageViewModel
    {
        public List<Discussion> Discussions { get; set; }
        public List<Category> Categories { get; set; }
    }
}