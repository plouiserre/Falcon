using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.Parser.Attribute
{
    public class AnalyzeAttributes : IAnalyzeAttributes
    {
        private string _startTag;
        private string _baseTag;
        private string _onlyAttributes;

        public List<string> Study(string startTag)
        {
            var results = new List<string>();
            _startTag = startTag;

            _baseTag = GetTagName();
            _onlyAttributes = GetOnlyAttributes();

            results = SeparatesAttributes();
            return results;
        }

        private string GetTagName()
        {
            string startTagWorking = _startTag.Split(" ")[0];
            startTagWorking = startTagWorking.Trim();
            return startTagWorking;
        }

        private string GetOnlyAttributes()
        {
            string onlyAttributes = _startTag.Replace(_baseTag, string.Empty);
            onlyAttributes = onlyAttributes.Replace(">", string.Empty);
            onlyAttributes = onlyAttributes.TrimStart();
            return onlyAttributes;
        }

        private List<string> SeparatesAttributes()
        {
            var parts = _onlyAttributes.Split(" ").ToList();
            return parts;
        }
    }
}