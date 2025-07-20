using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class MetaParser : ITagParser
    {

        private string _html;
        private TagModel _tag;
        private IAttributeTagParser _attributeTagParser;
        private IIdentifyTag _identifyTag;
        private IAttributeTagManager _attributeTagManager;

        public MetaParser(IIdentifyTag identifyTag, IAttributeTagParser attributeTagParser, IAttributeTagManager attributeTagManager)
        {
            _attributeTagParser = attributeTagParser;
            _identifyTag = identifyTag;
            _attributeTagManager = attributeTagManager;
        }

        public TagModel Parse(string html)
        {
            _html = html;
            _tag = _identifyTag.Analyze(_html);
            return _tag;
        }

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }

        public List<TagModel> DeterminateChildren(string html)
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            bool noTagEnd = string.IsNullOrEmpty(_tag.TagEnd);
            bool tagsAreOk = AreAttributesAreAutorized();
            return noTagEnd && tagsAreOk;
        }

        //TODO factorized in a mastertagparser
        private bool AreAttributesAreAutorized()
        {
            bool isOk = true;
            if (_tag.Attributes == null || _tag.Attributes.Count == 0)
                return isOk;
            var allAttributesAutorized = _attributeTagManager.GetAttributes(NameTagEnum.meta);
            if (allAttributesAutorized == null || allAttributesAutorized.Count == 0)
                return false;
            foreach (var attribut in _tag.Attributes)
            {
                var attributKey = attribut.FamilyAttribute;
                isOk = allAttributesAutorized.Any(o => o == attributKey);
                if (!isOk)
                    break;
            }
            return isOk;
        }
    }
}