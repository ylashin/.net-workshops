using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShouldITweetClient.Models
{
    
    public class Tweet
    {
        [Required(ErrorMessage = "Tweet text is required")]
        [MaxLength(140 , ErrorMessage = "Tweet text should be max 140 characters")]
        public string Text { get; set; }
        public bool? VerbotenCheckPassed { get; set; }
        public IList<string> Violations { get; set; }
    }
}