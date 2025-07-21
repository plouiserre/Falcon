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
        private IDeterminateContent _determinateContent;
        private IAttributeTagManager _attributeTagManager;

        public HtmlTagParser(IIdentifyTag identifyTag, IDeterminateContent determinateContent,
            IAttributeTagManager attributeTagManager)
        {
            _identifyTag = identifyTag;
            _determinateContent = determinateContent;
            _attributeTagManager = attributeTagManager;
        }

        public TagModel Parse(string html)
        {
            try
            {
                _tag = _identifyTag.Analyze(html);
                _tag.Content = _determinateContent.FindContent(html, _tag.TagStart, _tag.TagEnd);
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
            return isTagGoodFormatted && isContent && attributesAreAutorized;
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

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }

        public List<TagModel> DeterminateChildren(string html)
        {
            throw new NotImplementedException();
        }
    }
}