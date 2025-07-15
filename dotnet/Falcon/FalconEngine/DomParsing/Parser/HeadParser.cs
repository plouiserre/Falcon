using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class HeadParser : ITagParser
    {

        private string _html;
        private string _tagStart;
        private string _tagEnd;
        private IDeleteUselessSpace _deleteUselessSpace;
        private IIdentifyTag _identifyTag;
        private IDeterminateChildren _determinateChildren;

        public HeadParser(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag,
         IDeterminateChildren determinateChildren)
        {
            _identifyTag = identifyTag;
            _deleteUselessSpace = deleteUselessSpace;
            _determinateChildren = determinateChildren;
        }

        public bool IsValid(TagModel tag)
        {
            return tag.TagStart == _tagStart && tag.TagEnd == _tagEnd;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _html = html;
                _html = CleanHtml();
                var tag = _identifyTag.Analyze(_html);
                _tagStart = tag.TagStart;
                _tagEnd = tag.TagEnd;
                tag.Children = _determinateChildren.Find(tag.Content);
                return tag;
            }
            catch (Exception ex)
            {
                string message = $"Une erreur a eu lieu lors du parsing de {html}";
                throw new HeadParsingException(ErrorTypeParsing.head, message);
            }
        }


        private string CleanHtml()
        {
            return _deleteUselessSpace.PurgeUselessCaractersAroundTag(_html);
        }

    }
}