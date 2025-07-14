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

        public TitleParser(IIdentifyTag identifyTag, IDeterminateContent determinateContent)
        {
            _identityTag = identifyTag;
            _determinateContent = determinateContent;
        }

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }

        public List<TagModel> DeterminateChildren(string html)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(TagModel tag)
        {
            throw new NotImplementedException();
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