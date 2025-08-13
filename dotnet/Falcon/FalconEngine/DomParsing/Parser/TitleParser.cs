using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class TitleParser : ITagParser
    {
        private string _html;
        private IIdentifyTag _identityTag;
        private TagModel _tag;
        private IDeterminateContent _determinateContent;
        private NameTagEnum _nameTag;

        public TitleParser(IIdentifyTag identifyTag, IDeterminateContent determinateContent)
        {
            _identityTag = identifyTag;
            _determinateContent = determinateContent;
            _nameTag = NameTagEnum.title;
        }

        public NameTagEnum GetNameTag()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            bool noAttributes = _tag.Attributes == null || _tag.Attributes.Count == 0;
            bool tagEndPresent = !string.IsNullOrEmpty(_tag.TagEnd);
            return noAttributes && tagEndPresent;
        }

        public TagModel Parse(string html)
        {
            _html = html;
            _tag = _identityTag.Analyze(_html);
            _tag.Content = _determinateContent.FindContent(html, _tag.TagStart, _tag.TagEnd);
            return _tag;
        }
    }
}