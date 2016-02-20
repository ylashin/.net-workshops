using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet2.Logic
{
    public interface IVerbotenChecker
    {
        VerbotenCheckerResponse ValidateText(string text);
    }
    public class VerbotenChecker : IVerbotenChecker
    {
        public static readonly string NullVerbotenPhraseProvider = "Verboten Phrase Provider is NULL";
        private IVerbotenPhraseProvider VerbotenPhraseProvider;
        public VerbotenChecker(IVerbotenPhraseProvider verbotenPhraseProvider)
        {
            VerbotenPhraseProvider = verbotenPhraseProvider;
        }
        public VerbotenCheckerResponse ValidateText(string text)
        {
            var response = new VerbotenCheckerResponse() { IsSafeText = true, Violations = new List<string>() };

            if (string.IsNullOrWhiteSpace(text))
                return response;

            if (VerbotenPhraseProvider == null)
                throw new ArgumentException(NullVerbotenPhraseProvider);

            

            foreach (var phrase in VerbotenPhraseProvider.GetVerbotenPhrases())
            {
                if (string.IsNullOrWhiteSpace(phrase))
                    continue;

                if (text.IndexOf(phrase, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    response.IsSafeText = false;
                    response.Violations.Add(phrase);
                }                    
            }

            return response;
        }
    }
}