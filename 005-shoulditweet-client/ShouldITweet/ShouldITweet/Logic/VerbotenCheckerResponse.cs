using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShouldITweetClient.Logic
{
    public class VerbotenCheckerResponse
    {
        private VerbotenCheckerResponse()
        {
            _violations = new List<string>();
        }

        private VerbotenCheckerResponse(bool isSafeText, List<string> violations)
        {
            _IsSafeText = isSafeText;
            _violations = violations;
        }

        private bool _IsSafeText;
        private List<string> _violations;
        public bool IsSafeText { get { return _IsSafeText; } }
        public IList<string> Violations { get { return _violations.AsReadOnly(); } }

        internal static VerbotenCheckerResponse GetHappyEmptyResponse()
        {
            return new VerbotenCheckerResponse(true, new List<string>());
        }

        internal void FailItAndAddViolation(string violation)
        {
            _IsSafeText = false;
            _violations.Add(violation);
        }
    }
}