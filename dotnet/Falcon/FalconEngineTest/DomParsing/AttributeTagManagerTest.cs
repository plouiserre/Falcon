using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing;
using FalconEngine.Models;

namespace FalconEngineTest.DomParsing
{
    public class AttributeTagManagerTest
    {
        //TODO rework this UT with asser
        [Fact]
        public void GetAllAttributesAcceptedHtml()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.html);

            Assert.Equal(29, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.manifest);
            AssertContains(attributes, FamilyAttributeEnum.xmlns);
            AssertGlobalAttributes(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedMeta()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.meta);

            Assert.Equal(32, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.charset);
            AssertContains(attributes, FamilyAttributeEnum.content);
            AssertContains(attributes, FamilyAttributeEnum.httpequiv);
            AssertContains(attributes, FamilyAttributeEnum.name);
            AssertContains(attributes, FamilyAttributeEnum.scheme);
            AssertGlobalAttributes(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedLink()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.link);

            Assert.Equal(41, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.asAttr);
            AssertContains(attributes, FamilyAttributeEnum.blocking);
            AssertContains(attributes, FamilyAttributeEnum.crossorigin);
            AssertContains(attributes, FamilyAttributeEnum.disabled);
            AssertContains(attributes, FamilyAttributeEnum.integrity);
            AssertContains(attributes, FamilyAttributeEnum.href);
            AssertContains(attributes, FamilyAttributeEnum.hreflang);
            AssertContains(attributes, FamilyAttributeEnum.media);
            AssertContains(attributes, FamilyAttributeEnum.referrerpolicy);
            AssertContains(attributes, FamilyAttributeEnum.rel);
            AssertContains(attributes, FamilyAttributeEnum.role);
            AssertContains(attributes, FamilyAttributeEnum.sizes);
            AssertContains(attributes, FamilyAttributeEnum.type);
            AssertGlobalAttributes(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedSpan()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.span);

            Assert.Equal(71, attributes.Count);
            AssertGlobalAttributes(attributes);
            AssertOnClickEvent(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedA()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.a);

            Assert.Equal(80, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.download);
            AssertContains(attributes, FamilyAttributeEnum.href);
            AssertContains(attributes, FamilyAttributeEnum.hreflang);
            AssertContains(attributes, FamilyAttributeEnum.target);
            AssertContains(attributes, FamilyAttributeEnum.rel);
            AssertContains(attributes, FamilyAttributeEnum.referrerpolicy);
            AssertContains(attributes, FamilyAttributeEnum.role);
            AssertContains(attributes, FamilyAttributeEnum.type);
            AssertGlobalAttributes(attributes);
            AssertOnClickEvent(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedP()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.p);

            Assert.Equal(27, attributes.Count);
            AssertGlobalAttributes(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedDiv()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.div);

            Assert.Equal(71, attributes.Count);
            AssertGlobalAttributes(attributes);
            AssertOnClickEvent(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedBody()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.body);

            Assert.Equal(74, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.inert);
            AssertContains(attributes, FamilyAttributeEnum.enterkeyhint);
            AssertContains(attributes, FamilyAttributeEnum.popover);
            AssertGlobalAttributes(attributes);
            AssertOnClickEvent(attributes);
        }

        [Fact]
        public void CheckAllTagAcceptedUniversalAttributs()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();
            var tags = new List<NameTagEnum>()
            {
                NameTagEnum.a, NameTagEnum.body, NameTagEnum.div, NameTagEnum.doctype, NameTagEnum.form, NameTagEnum.h1, NameTagEnum.head, NameTagEnum.html,
                NameTagEnum.input, NameTagEnum.label, NameTagEnum.link, NameTagEnum.meta, NameTagEnum.option, NameTagEnum.p, NameTagEnum.select,
                NameTagEnum.span, NameTagEnum.title
            };

            foreach (var tag in tags)
            {
                var attributesSearched = attributeTagManager.GetAttributes(tag);

                AssertGlobalAttributes(attributesSearched);
            }
        }

        private void AssertGlobalAttributes(List<string> attributes)
        {
            AssertContains(attributes, FamilyAttributeEnum.accesskey);
            AssertContains(attributes, FamilyAttributeEnum.autocapitalize);
            AssertContains(attributes, FamilyAttributeEnum.classCss);
            AssertContains(attributes, FamilyAttributeEnum.contenteditable);
            AssertContains(attributes, FamilyAttributeEnum.contextmenu);
            AssertContains(attributes, FamilyAttributeEnum.data_);
            AssertContains(attributes, FamilyAttributeEnum.dir);
            AssertContains(attributes, FamilyAttributeEnum.draggable);
            AssertContains(attributes, FamilyAttributeEnum.dropzone);
            AssertContains(attributes, FamilyAttributeEnum.exportparts);
            AssertContains(attributes, FamilyAttributeEnum.hidden);
            AssertContains(attributes, FamilyAttributeEnum.id);
            AssertContains(attributes, FamilyAttributeEnum.inputmode);
            AssertContains(attributes, FamilyAttributeEnum.isAttr);
            AssertContains(attributes, FamilyAttributeEnum.itemid);
            AssertContains(attributes, FamilyAttributeEnum.itemprop);
            AssertContains(attributes, FamilyAttributeEnum.itemref);
            AssertContains(attributes, FamilyAttributeEnum.itemscope);
            AssertContains(attributes, FamilyAttributeEnum.itemtype);
            AssertContains(attributes, FamilyAttributeEnum.lang);
            AssertContains(attributes, FamilyAttributeEnum.part);
            AssertContains(attributes, FamilyAttributeEnum.slot);
            AssertContains(attributes, FamilyAttributeEnum.spellcheck);
            AssertContains(attributes, FamilyAttributeEnum.style);
            AssertContains(attributes, FamilyAttributeEnum.tabindex);
            AssertContains(attributes, FamilyAttributeEnum.title);
            AssertContains(attributes, FamilyAttributeEnum.translate);
        }

        private void AssertOnClickEvent(List<string> attributes)
        {
            AssertContains(attributes, FamilyAttributeEnum.onclick);
            AssertContains(attributes, FamilyAttributeEnum.ondblclick);
            AssertContains(attributes, FamilyAttributeEnum.onmousedown);
            AssertContains(attributes, FamilyAttributeEnum.onmouseup);
            AssertContains(attributes, FamilyAttributeEnum.onmouseover);
            AssertContains(attributes, FamilyAttributeEnum.onmouseout);
            AssertContains(attributes, FamilyAttributeEnum.onmousemove);
            AssertContains(attributes, FamilyAttributeEnum.oncontextmenu);
            AssertContains(attributes, FamilyAttributeEnum.onmouseenter);
            AssertContains(attributes, FamilyAttributeEnum.onmouseleave);
            AssertContains(attributes, FamilyAttributeEnum.onkeydown);
            AssertContains(attributes, FamilyAttributeEnum.onkeypress);
            AssertContains(attributes, FamilyAttributeEnum.onkeyup);
            AssertContains(attributes, FamilyAttributeEnum.onfocus);
            AssertContains(attributes, FamilyAttributeEnum.onblur);
            AssertContains(attributes, FamilyAttributeEnum.onchange);
            AssertContains(attributes, FamilyAttributeEnum.oninput);
            AssertContains(attributes, FamilyAttributeEnum.onselect);
            AssertContains(attributes, FamilyAttributeEnum.onsubmit);
            AssertContains(attributes, FamilyAttributeEnum.onreset);
            AssertContains(attributes, FamilyAttributeEnum.ondrag);
            AssertContains(attributes, FamilyAttributeEnum.ondragstart);
            AssertContains(attributes, FamilyAttributeEnum.ondragend);
            AssertContains(attributes, FamilyAttributeEnum.ondragenter);
            AssertContains(attributes, FamilyAttributeEnum.ondragover);
            AssertContains(attributes, FamilyAttributeEnum.ondragleave);
            AssertContains(attributes, FamilyAttributeEnum.ondrop);
            AssertContains(attributes, FamilyAttributeEnum.oncopy);
            AssertContains(attributes, FamilyAttributeEnum.oncut);
            AssertContains(attributes, FamilyAttributeEnum.onpast);
            AssertContains(attributes, FamilyAttributeEnum.onplay);
            AssertContains(attributes, FamilyAttributeEnum.onpause);
            AssertContains(attributes, FamilyAttributeEnum.onended);
            AssertContains(attributes, FamilyAttributeEnum.onvolumechange);
            AssertContains(attributes, FamilyAttributeEnum.onwheel);
            AssertContains(attributes, FamilyAttributeEnum.onscroll);
            AssertContains(attributes, FamilyAttributeEnum.onresize);
            AssertContains(attributes, FamilyAttributeEnum.onerror);
            AssertContains(attributes, FamilyAttributeEnum.onoad);
            AssertContains(attributes, FamilyAttributeEnum.onunload);
            AssertContains(attributes, FamilyAttributeEnum.ontransitionend);
            AssertContains(attributes, FamilyAttributeEnum.onanimationstart);
            AssertContains(attributes, FamilyAttributeEnum.onanimationend);
            AssertContains(attributes, FamilyAttributeEnum.onanimationiteration);
        }

        private void AssertContains(List<string> attributes, FamilyAttributeEnum family)
        {
            string value = family.ToString();
            bool contains = attributes.Any(o => o == value);
            Assert.Contains(attributes, o => o == value);
        }
    }
}