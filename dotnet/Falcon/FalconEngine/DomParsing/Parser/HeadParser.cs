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
        private IDeleteUselessSpace _deleteUselessSpace;
        private IIdentifyTag _identifyTag;
        private IDeterminateChildren _determinateChildren;
        private TagModel _tag;

        public HeadParser(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag,
         IDeterminateChildren determinateChildren)
        {
            _identifyTag = identifyTag;
            _deleteUselessSpace = deleteUselessSpace;
            _determinateChildren = determinateChildren;
        }

        public bool IsValid()
        {
            if (_tag == null)
                throw new ValidationParsingException(ErrorTypeParsing.validation, "Header validation is failing because parsing is not did");
            bool isLimitTagPresent = !string.IsNullOrEmpty(_tag.TagEnd) && !string.IsNullOrEmpty(_tag.TagStart);
            bool isContent = !string.IsNullOrEmpty(_tag.Content);
            return isContent && isLimitTagPresent;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _html = html;
                _html = CleanHtml();
                _tag = _identifyTag.Analyze(_html);
                _tag.Children = _determinateChildren.Find(_tag.Content);
                return _tag;
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