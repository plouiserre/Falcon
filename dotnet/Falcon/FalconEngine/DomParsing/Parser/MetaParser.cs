using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser.Attribute;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class MetaParser : TagParser, ITagParser
    {

        private string _html;
        private TagModel _tag;
        private IIdentifyTag _identifyTag;

        public MetaParser(IIdentifyTag identifyTag, IAttributeTagManager attributeTagManager) : base(attributeTagManager)
        {
            _identifyTag = identifyTag;
        }

        public override TagModel Parse(string html)
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