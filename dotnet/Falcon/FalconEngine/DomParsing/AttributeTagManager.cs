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
            _allAttributesAutorizedByTag[NameTagEnum.a] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.span] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.p] = new List<FamilyAttributeEnum>();
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
            SetAAttributesAutorized();
            SetSpanAttributes();
            SetPAttributes();
        }

        private void SetHtmlAttributesAutorized()
        {
            SetGlobalAttributes(NameTagEnum.html);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.contenteditable);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.draggable);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.manifest);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.spellcheck);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.tabindex);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.xmlns);
        }

        private void SetMetaAttributesAutorized()
        {
            SetGlobalAttributes(NameTagEnum.meta);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.charset);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.content);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.httpequiv);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.name);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.scheme);
        }

        private void SetLinkAttributesAutorized()
        {

            SetGlobalAttributes(NameTagEnum.link);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.asAttr);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.blocking);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.crossorigin);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.disabled);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.draggable);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.integrity);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.href);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.hreflang);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.media);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.referrerpolicy);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.rel);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.role);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.sizes);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.spellcheck);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.tabindex);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.translate);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.type);
        }

        //TODO m'occuper des attributs aria-* et hidden
        private void SetAAttributesAutorized()
        {
            SetGlobalAttributes(NameTagEnum.a);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.download);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.draggable);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.hidden);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.href);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.hreflang);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.referrerpolicy);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.rel);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.role);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.tabindex);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.target);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.type);
        }

        private void SetSpanAttributes()
        {
            SetGlobalAttributes(NameTagEnum.span);
        }

        private void SetPAttributes()
        {
            SetGlobalAttributes(NameTagEnum.p);
        }

        private void SetGlobalAttributes(NameTagEnum nameTag)
        {
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.classCss);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.data_);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.id);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.lang);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.style);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.title);
        }
    }
}