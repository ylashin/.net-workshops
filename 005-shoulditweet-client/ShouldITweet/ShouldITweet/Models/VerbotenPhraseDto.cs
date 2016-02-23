using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShouldITweetClient.Models
{
    public class VerbotenPhraseDto
    {

        [Key]
        public Guid Id { get;  set; }
        [Required]
        [MaxLength(100)]
        public string Phrase { get;  set; }
        public DateTime LastModified { get;  set; }

        
    }
}