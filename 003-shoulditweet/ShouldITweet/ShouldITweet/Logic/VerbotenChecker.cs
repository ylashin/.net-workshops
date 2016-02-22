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
        public const string NullVerbotenPhraseProvider = "Verboten Phrase Provider is NULL"; 
        // Inline this, should be const, use nameof
        private IVerbotenPhraseProvider VerbotenPhraseProvider;
        public VerbotenChecker(IVerbotenPhraseProvider verbotenPhraseProvider)
        {
            if (verbotenPhraseProvider == null)
                throw new ArgumentException(NullVerbotenPhraseProvider); // Too Late

            VerbotenPhraseProvider = verbotenPhraseProvider;
        }
        public VerbotenCheckerResponse ValidateText(string text)
        {
            // Make response immutible
            var response = VerbotenCheckerResponse.GetHappyEmptyResponse();

            if (string.IsNullOrWhiteSpace(text))
                return response;

            VerbotenPhraseProvider.GetVerbotenPhrases()
                .Where(phrase => !string.IsNullOrWhiteSpace(phrase))
                .Where(phrase => text.IndexOf(phrase, StringComparison.InvariantCultureIgnoreCase) >= 0)
                .ToList().ForEach(phrase =>
            {
                response.FailItAndAddViolation(phrase);
            });
            
            
            return response;
        }
    }
}