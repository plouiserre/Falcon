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
        private IManageChildrenTag _manageChildrenTag;
        private TagModel _tag;
        private NameTagEnum _nameTag;

        public HeadParser(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag,
         IManageChildrenTag manageChildrenTag)
        {
            _identifyTag = identifyTag;
            _deleteUselessSpace = deleteUselessSpace;
            _manageChildrenTag = manageChildrenTag;
            _nameTag = NameTagEnum.head;
        }

        public NameTagEnum GetNameTag()
        {
            return _nameTag;
        }

        public bool IsValid()
        {
            if (_tag == null)
                throw new ValidationParsingException(ErrorTypeParsing.validation, "Header validation is failing because parsing is not did");
            bool isLimitTagPresent = !string.IsNullOrEmpty(_tag.TagEnd) && !string.IsNullOrEmpty(_tag.TagStart);
            bool isContent = !string.IsNullOrEmpty(_tag.Content);
            bool areChildrenValid = _manageChildrenTag.ValidateChildren(_tag);
            return isContent && isLimitTagPresent && areChildrenValid;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _html = html;
                _html = CleanHtml();
                _tag = _identifyTag.Analyze(_html);
                _tag.Children = _manageChildrenTag.Identify(_tag, _tag.Content);
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