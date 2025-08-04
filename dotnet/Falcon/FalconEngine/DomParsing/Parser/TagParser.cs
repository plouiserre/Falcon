using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{

    public abstract class TagParser : ITagParser
    {
        protected TagModel _tag;
        protected IAttributeTagManager _attributeTagManager;
        private NameTagEnum _nametag;

        protected TagParser(IAttributeTagManager attributeTagManager, NameTagEnum nameTag)
        {
            _attributeTagManager = attributeTagManager;
            _nametag = nameTag;
        }

        public virtual bool IsValid()
        {
            bool noTagEnd = string.IsNullOrEmpty(_tag.TagEnd);
            bool tagsAreOk = AreAttributesAreAutorized();
            return noTagEnd && tagsAreOk;
        }

        protected bool AreAttributesAreAutorized()
        {
            bool isOk = true;
            if (_tag.Attributes == null || _tag.Attributes.Count == 0)
                return isOk;
            var allAttributesAutorized = _attributeTagManager.GetAttributes(_nametag);
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

        public abstract TagModel Parse(string html);

        public NameTagEnum GetNameTag()
        {
            return _nametag;
        }
    }
}