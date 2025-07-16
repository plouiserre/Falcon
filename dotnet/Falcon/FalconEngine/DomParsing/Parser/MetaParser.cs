using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class MetaParser : ITagParser
    {

        private string _html;
        private string _tagStart;
        private IAttributeTagParser _attributeTagParser;
        private IIdentifyTag _identifyTag;

        public MetaParser(IIdentifyTag identifyTag, IAttributeTagParser attributeTagParser)
        {
            _attributeTagParser = attributeTagParser;
            _identifyTag = identifyTag;
        }

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }

        public List<TagModel> DeterminateChildren(string html)
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public TagModel Parse(string html)
        {
            _html = html;
            var tag = _identifyTag.Analyze(_html);
            return tag;
        }
    }
}