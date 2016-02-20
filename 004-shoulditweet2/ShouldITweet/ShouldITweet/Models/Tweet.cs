using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Models
{
    
    public class Tweet
    {
        [Required]
        [MaxLength(140)]
        public string Text { get; set; }
        public bool? VerbotenCheckPassed { get; set; }
        public IList<string> Violations { get; set; }
    }
}