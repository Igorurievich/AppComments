using System;
using System.Collections.Generic;
using System.Text;

namespace App.Comments.Common.Entities
{
    public class Comment
    {
        public Comment(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }
    }
}
