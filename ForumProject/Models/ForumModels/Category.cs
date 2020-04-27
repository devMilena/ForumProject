using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumProject.Models.ForumModels
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DiscussionsCount { get; set; }
      
    }
}