using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweet.Logic
{
    public interface IVerbotenChecker
    {
        VerbotenCheckerResponse ValidateText(string text);
    }
    public class VerbotenChecker : IVerbotenChecker
    {
        public static readonly string NullVerbotenPhraseProvider = "Verboten Phrase Provider is NULL"; // Inline this, should be const, use nameof
        private IVerbotenPhraseProvider VerbotenPhraseProvider;
        public VerbotenChecker(IVerbotenPhraseProvider verbotenPhraseProvider)
        {
            VerbotenPhraseProvider = verbotenPhraseProvider;
        }
        public VerbotenCheckerResponse ValidateText(string text)
        {
            // Make response immutible
            var response = new VerbotenCheckerResponse() { IsSafeText = true, Violations = new List<string>() };

            if (string.IsNullOrWhiteSpace(text))
                return response;

            if (VerbotenPhraseProvider == null)
                throw new ArgumentException(NullVerbotenPhraseProvider); // Too Late

            

            foreach (var phrase in VerbotenPhraseProvider.GetVerbotenPhrases()) // To LINQ
            {
                if (string.IsNullOrWhiteSpace(phrase)) // Not needed
                    continue;

                if (text.IndexOf(phrase, StringComparison.InvariantCultureIgnoreCase) >= 0) // Current Culture, Contains
                {
                    response.IsSafeText = false;    // Factory methods
                    response.Violations.Add(phrase);
                }                    
            }

            return response;
        }
    }
}