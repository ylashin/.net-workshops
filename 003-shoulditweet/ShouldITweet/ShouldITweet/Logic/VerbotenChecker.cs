using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet.Logic
{
    public interface IVerbotenChecker
    {
        bool ValidateText(string text);
    }
    public class VerbotenChecker : IVerbotenChecker
    {
        public static readonly string NullVerbotenPhraseProvider = "Verboten Phrase Provider is NULL";
        private IVerbotenPhraseProvider VerbotenPhraseProvider;
        public VerbotenChecker(IVerbotenPhraseProvider verbotenPhraseProvider)
        {
            VerbotenPhraseProvider = verbotenPhraseProvider;
        }
        public bool ValidateText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return true;

            if (VerbotenPhraseProvider == null)
                throw new ArgumentException(NullVerbotenPhraseProvider);

            foreach(var phrase in VerbotenPhraseProvider.GetVerbotenPhrases())
            {
                if (string.IsNullOrWhiteSpace(phrase))
                    continue;

                if (text.IndexOf(phrase,StringComparison.InvariantCultureIgnoreCase)>=0)
                    return false;
            }

            return true;
        }
    }
}