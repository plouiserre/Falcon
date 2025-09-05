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
            _allAttributesAutorizedByTag[NameTagEnum.doctype] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.html] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.head] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.meta] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.link] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.title] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.a] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.span] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.h1] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.p] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.label] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.input] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.option] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.select] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.td] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.th] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.tr] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.tbody] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.thead] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.table] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.div] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.form] = new List<FamilyAttributeEnum>();
            _allAttributesAutorizedByTag[NameTagEnum.body] = new List<FamilyAttributeEnum>();
        }

        public List<string> GetAttributes(NameTagEnum tag)
        {
            return _allAttributesAutorizedByTag[tag].Select(o => o.ToString()).ToList();
        }

        public void SetAttributes()
        {
            SetDoctypeAttributesAutorized();
            SetHtmlAttributesAutorized();
            SetHeadAttributesAutorized();
            SetMetaAttributesAutorized();
            SetLinkAttributesAutorized();
            SetTitleAttributesAutorized();
            SetAAttributesAutorized();
            SetSpanAttributesAutorized();
            SetH1AttributesAutorized();
            SetLabelAttributesAutorized();
            SetInputAttributesAutorized();
            SetOptionAttributesAutorized();
            SetSelectAttributesAutorized();
            SetPAttributesAutorized();
            SetTdAttributesAutorized();
            SetThAttributesAutorized();
            SetTrAttributesAutorized();
            SetTbodyAttributesAutorized();
            SetTheadAttributesAutorized();
            SetTableAttributesAutorized();
            SetDivAttributesAutorized();
            SetFormAttributesAutorized();
            SetBodyAttributesAutorized();
        }

        private void SetDoctypeAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.doctype);
        }

        private void SetHtmlAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.html);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.manifest);
            _allAttributesAutorizedByTag[NameTagEnum.html].Add(FamilyAttributeEnum.xmlns);
        }

        private void SetHeadAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.head);
        }

        private void SetMetaAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.meta);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.charset);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.content);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.httpequiv);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.name);
            _allAttributesAutorizedByTag[NameTagEnum.meta].Add(FamilyAttributeEnum.scheme);
        }

        private void SetLinkAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.link);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.asAttr);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.blocking);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.crossorigin);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.disabled);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.integrity);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.href);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.hreflang);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.media);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.referrerpolicy);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.rel);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.role);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.sizes);
            _allAttributesAutorizedByTag[NameTagEnum.link].Add(FamilyAttributeEnum.type);
        }

        private void SetTitleAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.title);
        }

        private void SetAAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.a);
            SetOnEventAttribut(NameTagEnum.a);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.download);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.href);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.hreflang);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.referrerpolicy);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.rel);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.role);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.target);
            _allAttributesAutorizedByTag[NameTagEnum.a].Add(FamilyAttributeEnum.type);
        }

        private void SetSpanAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.span);
            SetOnEventAttribut(NameTagEnum.span);
        }

        private void SetH1AttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.h1);
            SetOnEventAttribut(NameTagEnum.h1);
        }

        private void SetLabelAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.label);
            _allAttributesAutorizedByTag[NameTagEnum.label].Add(FamilyAttributeEnum.forAttr);
            _allAttributesAutorizedByTag[NameTagEnum.label].Add(FamilyAttributeEnum.form);
        }

        private void SetInputAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.input);
            SetOnEventAttribut(NameTagEnum.input);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.accept);
            _allAttributesAutorizedByTag[NameTagEnum.input].Add(FamilyAttributeEnum.alt);
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

        private void SetOptionAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.option);
            _allAttributesAutorizedByTag[NameTagEnum.option].Add(FamilyAttributeEnum.disabled);
            _allAttributesAutorizedByTag[NameTagEnum.option].Add(FamilyAttributeEnum.label);
            _allAttributesAutorizedByTag[NameTagEnum.option].Add(FamilyAttributeEnum.selected);
            _allAttributesAutorizedByTag[NameTagEnum.option].Add(FamilyAttributeEnum.value);
        }

        private void SetSelectAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.select);
            _allAttributesAutorizedByTag[NameTagEnum.select].Add(FamilyAttributeEnum.autocomplete);
            _allAttributesAutorizedByTag[NameTagEnum.select].Add(FamilyAttributeEnum.autofocus);
            _allAttributesAutorizedByTag[NameTagEnum.select].Add(FamilyAttributeEnum.form);
            _allAttributesAutorizedByTag[NameTagEnum.select].Add(FamilyAttributeEnum.multiple);
            _allAttributesAutorizedByTag[NameTagEnum.select].Add(FamilyAttributeEnum.name);
            _allAttributesAutorizedByTag[NameTagEnum.select].Add(FamilyAttributeEnum.required);
            _allAttributesAutorizedByTag[NameTagEnum.select].Add(FamilyAttributeEnum.size);
        }

        private void SetPAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.p);
        }

        private void SetTdAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.td);
            _allAttributesAutorizedByTag[NameTagEnum.td].Add(FamilyAttributeEnum.colspan);
            _allAttributesAutorizedByTag[NameTagEnum.td].Add(FamilyAttributeEnum.headers);
            _allAttributesAutorizedByTag[NameTagEnum.td].Add(FamilyAttributeEnum.rowspan);
        }

        private void SetThAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.th);
            _allAttributesAutorizedByTag[NameTagEnum.th].Add(FamilyAttributeEnum.abbr);
            _allAttributesAutorizedByTag[NameTagEnum.th].Add(FamilyAttributeEnum.colspan);
            _allAttributesAutorizedByTag[NameTagEnum.th].Add(FamilyAttributeEnum.headers);
            _allAttributesAutorizedByTag[NameTagEnum.th].Add(FamilyAttributeEnum.rowspan);
            _allAttributesAutorizedByTag[NameTagEnum.th].Add(FamilyAttributeEnum.scope);
        }

        private void SetTrAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.tr);
        }

        private void SetTbodyAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.tbody);
        }

        private void SetTheadAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.thead);
        }

        private void SetTableAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.table);
        }

        private void SetDivAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.div);
            SetOnEventAttribut(NameTagEnum.div);
        }

        private void SetFormAttributesAutorized()
        {

            SetUniversalAttributes(NameTagEnum.form);
            _allAttributesAutorizedByTag[NameTagEnum.form].Add(FamilyAttributeEnum.action);
            _allAttributesAutorizedByTag[NameTagEnum.form].Add(FamilyAttributeEnum.method);
        }

        private void SetBodyAttributesAutorized()
        {
            SetUniversalAttributes(NameTagEnum.body);
            SetOnEventAttribut(NameTagEnum.body);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.inert);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.enterkeyhint);
            _allAttributesAutorizedByTag[NameTagEnum.body].Add(FamilyAttributeEnum.popover);
        }

        private void SetUniversalAttributes(NameTagEnum nameTag)
        {
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.accesskey);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.autocapitalize);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.classCss);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.contenteditable);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.contextmenu);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.data_);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.dir);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.draggable);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.dropzone);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.exportparts);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.hidden);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.id);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.inputmode);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.isAttr);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.itemid);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.itemprop);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.itemref);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.itemscope);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.itemtype);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.lang);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.part);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.slot);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.spellcheck);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.style);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.tabindex);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.title);
            _allAttributesAutorizedByTag[nameTag].Add(FamilyAttributeEnum.translate);
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