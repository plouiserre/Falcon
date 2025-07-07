using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class LinkParser : ITagParser
    {
        private AttributeTagParser _attributeTagParser;

        public LinkParser()
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
            var attributes = _attributeTagParser.Parse(html);
            return new TagModel()
            {
                Attributes = attributes,
                NameTag = NameTagEnum.link,
                TagFamily = TagFamilyEnum.NoEnd,
                TagStart = CleanTagStart(html)
            };
        }

        //TODO put in deleteuselesspace
        private string CleanTagStart(string html)
        {
            return html.TrimEnd();
        }
    }
}