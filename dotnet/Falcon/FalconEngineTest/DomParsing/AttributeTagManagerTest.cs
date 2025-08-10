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
        [Fact]
        public void GetAllAttributesAcceptedHtml()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.html);

            Assert.Equal(14, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.accesskey);
            AssertContains(attributes, FamilyAttributeEnum.contenteditable);
            AssertContains(attributes, FamilyAttributeEnum.draggable);
            AssertContains(attributes, FamilyAttributeEnum.dir);
            AssertContains(attributes, FamilyAttributeEnum.manifest);
            AssertContains(attributes, FamilyAttributeEnum.tabindex);
            AssertContains(attributes, FamilyAttributeEnum.spellcheck);
            AssertContains(attributes, FamilyAttributeEnum.xmlns);
            AssertGlobalAttributes(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedMeta()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.meta);

            Assert.Equal(12, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.charset);
            AssertContains(attributes, FamilyAttributeEnum.content);
            AssertContains(attributes, FamilyAttributeEnum.dir);
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

            Assert.Equal(25, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.accesskey);
            AssertContains(attributes, FamilyAttributeEnum.asAttr);
            AssertContains(attributes, FamilyAttributeEnum.blocking);
            AssertContains(attributes, FamilyAttributeEnum.crossorigin);
            AssertContains(attributes, FamilyAttributeEnum.disabled);
            AssertContains(attributes, FamilyAttributeEnum.draggable);
            AssertContains(attributes, FamilyAttributeEnum.dir);
            AssertContains(attributes, FamilyAttributeEnum.integrity);
            AssertContains(attributes, FamilyAttributeEnum.href);
            AssertContains(attributes, FamilyAttributeEnum.hreflang);
            AssertContains(attributes, FamilyAttributeEnum.media);
            AssertContains(attributes, FamilyAttributeEnum.referrerpolicy);
            AssertContains(attributes, FamilyAttributeEnum.rel);
            AssertContains(attributes, FamilyAttributeEnum.role);
            AssertContains(attributes, FamilyAttributeEnum.sizes);
            AssertContains(attributes, FamilyAttributeEnum.spellcheck);
            AssertContains(attributes, FamilyAttributeEnum.tabindex);
            //AssertContains(attributes,  FamilyAttributeEnum.hidden);
            AssertContains(attributes, FamilyAttributeEnum.translate);
            AssertContains(attributes, FamilyAttributeEnum.type);
            AssertGlobalAttributes(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedSpan()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.span);

            Assert.Equal(50, attributes.Count);
            AssertGlobalAttributes(attributes);
            AssertOnClickEvent(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedA()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.a);

            Assert.Equal(63, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.accesskey);
            AssertContains(attributes, FamilyAttributeEnum.download);
            AssertContains(attributes, FamilyAttributeEnum.dir);
            AssertContains(attributes, FamilyAttributeEnum.draggable);
            AssertContains(attributes, FamilyAttributeEnum.href);
            AssertContains(attributes, FamilyAttributeEnum.hreflang);
            AssertContains(attributes, FamilyAttributeEnum.hidden);
            AssertContains(attributes, FamilyAttributeEnum.target);
            AssertContains(attributes, FamilyAttributeEnum.rel);
            AssertContains(attributes, FamilyAttributeEnum.referrerpolicy);
            AssertContains(attributes, FamilyAttributeEnum.role);
            AssertContains(attributes, FamilyAttributeEnum.tabindex);
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

            Assert.Equal(6, attributes.Count);
            AssertGlobalAttributes(attributes);
        }

        [Fact]
        public void GetAllAttributesAcceptedDiv()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.div);

            Assert.Equal(57, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.accesskey);
            AssertContains(attributes, FamilyAttributeEnum.contenteditable);
            AssertContains(attributes, FamilyAttributeEnum.dir);
            AssertContains(attributes, FamilyAttributeEnum.draggable);
            AssertContains(attributes, FamilyAttributeEnum.hidden);
            AssertContains(attributes, FamilyAttributeEnum.spellcheck);
            AssertContains(attributes, FamilyAttributeEnum.tabindex);
            AssertGlobalAttributes(attributes);
            AssertOnClickEvent(attributes);
        }

        private void AssertGlobalAttributes(List<string> attributes)
        {
            AssertContains(attributes, FamilyAttributeEnum.classCss);
            AssertContains(attributes, FamilyAttributeEnum.data_);
            AssertContains(attributes, FamilyAttributeEnum.id);
            AssertContains(attributes, FamilyAttributeEnum.lang);
            AssertContains(attributes, FamilyAttributeEnum.style);
            AssertContains(attributes, FamilyAttributeEnum.title);
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
            Assert.Contains(attributes, o => o == value);
        }
    }
}