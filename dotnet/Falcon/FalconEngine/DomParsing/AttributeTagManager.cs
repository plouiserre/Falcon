using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{

    public class AttributeTagManager : IAttributeTagManager
    {
        private Dictionary<NameTagEnum, List<FamilyAttributeEnum>> _allAttributesAutorizedByTag;

        public AttributeTagManager()
        {
            _allAttributesAutorizedByTag = new Dictionary<NameTagEnum, List<FamilyAttributeEnum>>();
            _allAttributesAutorizedByTag[NameTagEnum.html] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.meta] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.link] = new List<FamilyAttributeEnum>();
        }

        public List<string> GetAttributes(NameTagEnum tag)
        {
            return _allAttributesAutorizedByTag[tag].Select(o => o.ToString()).ToList();
        }

        public void SetAttributes()
        {
            SetHtmlAttributesAutorized();
            SetMetaAttributesAutorized();
            SetLinkAttributesAutorized();
        }

        private void SetHtmlAttributesAutorized()
        {
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.lang);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.lang);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.xmlns);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.manifest);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.style);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.id);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.classCss);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.data_);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.tabindex);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.contenteditable);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.draggable);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.spellcheck);
        }

        private void SetMetaAttributesAutorized()
        {
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.charset);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.name);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.content);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.httpequiv);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.scheme);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.id);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.classCss);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.style);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.data_);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.lang);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.title);
        }

        private void SetLinkAttributesAutorized()
        {
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.asAttr);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.blocking);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.classCss);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.crossorigin);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.data_);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.disabled);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.draggable);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.id);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.integrity);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.href);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.hreflang);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.lang);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.media);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.referrerpolicy);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.rel);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.role);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.sizes);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.spellcheck);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.style);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.title);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.tabindex);
            //_allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.hidden);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.translate);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.type);
        }
    }
}