using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class TitleParser : ITagParser
    {
        private string _html;
        private string _content;
        private IIdentifyTag _identityTag;
        private TagModel _tag;

        public TitleParser(IIdentifyTag identifyTag)
        {
            _identityTag = identifyTag;
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
            GetContent();
            _tag.NameTag = NameTagEnum.title;
            _tag.Content = _content;
            _tag.TagFamily = TagFamilyEnum.WithEnd;
            return _tag;
        }

        private void GetContent()
        {
            int startTagIndex = _html.IndexOf(_tag.TagStart);
            int endTagIndex = _html.IndexOf(_tag.TagEnd);
            string allTag = _html.Substring(startTagIndex, endTagIndex + _tag.TagEnd.Length - startTagIndex);
            _content = allTag.Replace(_tag.TagStart, string.Empty).Replace(_tag.TagEnd, string.Empty);
        }
    }
}