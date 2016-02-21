using ShouldITweet2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Models
{
    public static class Extensions
    {
        public static VerbotenPhraseDto MapToDto(this VerbotenPhrase vp)
        {
            return new VerbotenPhraseDto()
            {
                Id = vp.Id,
                Phrase = vp.Phrase,
                LastModified = vp.LastModified
            };
        }

        public static VerbotenPhrase MapToModel(this VerbotenPhraseDto dto)
        {
            return new VerbotenPhrase(dto.Id, dto.Phrase, dto.LastModified);
        }
    }
}