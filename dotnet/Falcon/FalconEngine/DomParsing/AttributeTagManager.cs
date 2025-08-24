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
            _allAttributesAutorizedByTag[NameTagEnum.input] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.option] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.div] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.body] = new List<FamilyAttributeEnum>();
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
            SetInputAttributes();
            SetOptionAttributes();
            SetPAttributes();
            SetDivAttributes();
            SetBodyAttributes();
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
            SetOnEventAttribut(NameTagEnum.a);
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
            SetOnEventAttribut(NameTagEnum.span);
        }

        private void SetInputAttributes()
        {
            SetGlobalAttributes(NameTagEnum.input);
            SetOnEventAttribut(NameTagEnum.input);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.accept);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.alt);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.autocapitalize);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.autocomplete);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.capture);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.checkedAttr);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.dirname);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.disabled);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.form);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.formaction);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.formenctype);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.formmethod);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.formnovalidate);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.formtarget);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.height);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.list);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.max);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.maxlength);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.min);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.minlength);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.multiple);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.name);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.pattern);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.placeholder);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.popovertarget);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.popovertargetaction);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.readonlyAttr);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.required);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.size);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.src);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.step);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.type);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.value);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.width);
        }

        private void SetOptionAttributes()
        {
            _allAttributesAutorizedByTag[NameTagEnum.option].Add(FamilyAttributeEnum.disabled);
            _allAttributesAutorizedByTag[NameTagEnum.option].Add(FamilyAttributeEnum.label);
            _allAttributesAutorizedByTag[NameTagEnum.option].Add(FamilyAttributeEnum.selected);
            _allAttributesAutorizedByTag[NameTagEnum.option].Add(FamilyAttributeEnum.value);
        }

        private void SetPAttributes()
        {
            SetGlobalAttributes(NameTagEnum.p);
        }

        private void SetDivAttributes()
        {
            SetGlobalAttributes(NameTagEnum.div);
            SetOnEventAttribut(NameTagEnum.div);
            _allAttributesAutorizedByTag[NameTagEnum.div].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[NameTagEnum.div].Add(FamilyAttributeEnum.hidden);
            _allAttributesAutorizedByTag[NameTagEnum.div].Add(FamilyAttributeEnum.tabindex);
            _allAttributesAutorizedByTag[NameTagEnum.div].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.div].Add(FamilyAttributeEnum.draggable);
            _allAttributesAutorizedByTag[NameTagEnum.div].Add(FamilyAttributeEnum.contenteditable);
            _allAttributesAutorizedByTag[NameTagEnum.div].Add(FamilyAttributeEnum.spellcheck);
        }

        private void SetBodyAttributes()
        {
            SetGlobalAttributes(NameTagEnum.body);
            SetOnEventAttribut(NameTagEnum.body);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.translate);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.tabindex);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.draggable);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.hidden);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.inert);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.enterkeyhint);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.inputmode);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.isAttr);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.popover);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.data_);
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

        private void SetOnEventAttribut(NameTagEnum nameTag)
        {
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onclick);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.ondblclick);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onmousedown);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onmouseup);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onmouseover);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onmouseout);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onmousemove);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.oncontextmenu);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onmouseenter);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onmouseleave);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onkeydown);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onkeypress);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onkeyup);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onfocus);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onblur);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onchange);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.oninput);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onselect);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onsubmit);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onreset);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.ondrag);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.ondragstart);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.ondragend);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.ondragenter);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.ondragover);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.ondragleave);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.ondrop);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.oncopy);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.oncut);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onpast);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onplay);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onpause);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onended);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onvolumechange);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onwheel);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onscroll);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onresize);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onerror);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onoad);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onunload);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.ontransitionend);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onanimationstart);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onanimationend);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.onanimationiteration);
        }
    }
}