using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class HtmlTagParser : ITagParser
    {

        private TagModel _tag;
        private IIdentifyTag _identifyTag;
        private IManageChildrenTag _manageChildrenTag;
        private IAttributeTagManager _attributeTagManager;
        private NameTagEnum _nameTag;

        public HtmlTagParser(IIdentifyTag identifyTag, IManageChildrenTag manageChildrenTag,
            IAttributeTagManager attributeTagManager)
        {
            _identifyTag = identifyTag;
            _manageChildrenTag = manageChildrenTag;
            _attributeTagManager = attributeTagManager;
            _nameTag = NameTagEnum.html;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _tag = _identifyTag.Analyze(html);
                _tag.Children = _manageChildrenTag.Identify(_tag, _tag.Content);
                if (string.IsNullOrEmpty(_tag.TagEnd))
                    throw new HtmlParsingException(ErrorTypeParsing.html, $"Une erreur a eu lieu lors du parsing de {html}");
            }
            catch (Exception ex)
            {
                string message = string.Format($"Une erreur a eu lieu lors du parsing de {html}");
                throw new HtmlParsingException(ErrorTypeParsing.html, message);
            }
            return _tag;
        }

        public bool IsValid()
        {
            bool isTagGoodFormatted = !string.IsNullOrEmpty(_tag.TagStart) && !string.IsNullOrEmpty(_tag.TagEnd);
            bool isContent = !string.IsNullOrEmpty(_tag.Content);
            bool attributesAreAutorized = AreAttributesAreAutorized();
            bool areChildrenValid = _manageChildrenTag.ValidateChildren();
            return isTagGoodFormatted && isContent && attributesAreAutorized && areChildrenValid;
        }

        private bool AreAttributesAreAutorized()
        {
            bool isOk = true;
            if (_tag.Attributes == null || _tag.Attributes.Count == 0)
                return isOk;
            var allAttributesAutorized = _attributeTagManager.GetAttributes(NameTagEnum.html);
            if (allAttributesAutorized == null || allAttributesAutorized.Count == 0)
                return false;
            foreach (var attribut in _tag.Attributes)
            {
                var attributKey = attribut.FamilyAttribute;
                isOk = allAttributesAutorized.Any(o => o == attributKey) || attributKey.Contains("data-");
                if (!isOk)
                    break;
            }
            return isOk;
        }

        public NameTagEnum GetNameTag()
        {
            return _nameTag;
        }
    }
}