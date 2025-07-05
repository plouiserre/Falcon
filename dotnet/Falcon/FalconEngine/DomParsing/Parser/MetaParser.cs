using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class MetaParser : ITagParser
    {

        private string _html;
        private string _tagStart;
        private IAttributeTagParser _attributeTagParser;

        public MetaParser()
        {
            _attributeTagParser = new AttributeTagParser();
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
            var attributes = _attributeTagParser.Parse(html);
            DetermineTagStart();
            return new TagModel()
            {
                Attributes = attributes,
                NameTag = NameTagEnum.meta,
                TagFamily = TagFamilyEnum.NoEnd,
                TagStart = _tagStart
            };
        }

        private void DetermineTagStart()
        {
            int startIndex = _html.IndexOf("<");
            int endIndex = _html.IndexOf(">");
            _tagStart = _html.Substring(startIndex, endIndex - startIndex + 1);
        }
    }
}