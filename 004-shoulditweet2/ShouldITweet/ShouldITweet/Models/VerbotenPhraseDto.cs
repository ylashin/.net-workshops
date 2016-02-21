using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Models
{
    public class VerbotenPhraseDto
    {

        [Key]
        public Guid Id { get;  set; }
        [Required]
        public string Phrase { get;  set; }
        public DateTime LastModified { get;  set; }

    }
}