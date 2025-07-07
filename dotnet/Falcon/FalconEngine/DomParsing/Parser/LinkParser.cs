using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class LinkParser : ITagParser
    {
        private AttributeTagParser _attributeTagParser;
        private IDeleteUselessSpace _deleteUselessSpace;

        public LinkParser(IDeleteUselessSpace deleteUselessSpace)
        {
            _deleteUselessSpace = deleteUselessSpace;
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
                TagStart = _deleteUselessSpace.CleanTagStart(html)
            };
        }
    }
}