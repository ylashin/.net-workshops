using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Data
{
    public class VerbotenPhrase
    {
        protected VerbotenPhrase()
        {

        }

        public VerbotenPhrase(Guid id,string phrase,DateTimeOffset lastModified)
        {
            Id = id;
            Phrase = phrase;
            LastModified = lastModified;

        }
        [Key]
        public Guid Id { get; protected set; }
        [Required]
        public string Phrase { get; protected set; }
        public DateTimeOffset LastModified { get; protected set; }
    }
}