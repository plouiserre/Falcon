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
            AssertContains(attributes, FamilyAttributeEnum.lang);
            AssertContains(attributes, FamilyAttributeEnum.dir);
            AssertContains(attributes, FamilyAttributeEnum.xmlns);
            AssertContains(attributes, FamilyAttributeEnum.manifest);
            AssertContains(attributes, FamilyAttributeEnum.style);
            AssertContains(attributes, FamilyAttributeEnum.id);
            AssertContains(attributes, FamilyAttributeEnum.classCss);
            AssertContains(attributes, FamilyAttributeEnum.data_);
            AssertContains(attributes, FamilyAttributeEnum.accesskey);
            AssertContains(attributes, FamilyAttributeEnum.tabindex);
            AssertContains(attributes, FamilyAttributeEnum.contenteditable);
            AssertContains(attributes, FamilyAttributeEnum.draggable);
            AssertContains(attributes, FamilyAttributeEnum.spellcheck);
        }

        [Fact]
        public void GetAllAttributesAcceptedMeta()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.meta);

            Assert.Equal(12, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.charset);
            AssertContains(attributes, FamilyAttributeEnum.name);
            AssertContains(attributes, FamilyAttributeEnum.content);
            AssertContains(attributes, FamilyAttributeEnum.httpequiv);
            AssertContains(attributes, FamilyAttributeEnum.scheme);
            AssertContains(attributes, FamilyAttributeEnum.id);
            AssertContains(attributes, FamilyAttributeEnum.classCss);
            AssertContains(attributes, FamilyAttributeEnum.data_);
            AssertContains(attributes, FamilyAttributeEnum.lang);
            AssertContains(attributes, FamilyAttributeEnum.dir);
            AssertContains(attributes, FamilyAttributeEnum.title);
            AssertContains(attributes, FamilyAttributeEnum.style);
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
            AssertContains(attributes, FamilyAttributeEnum.classCss);
            AssertContains(attributes, FamilyAttributeEnum.crossorigin);
            AssertContains(attributes, FamilyAttributeEnum.data_);
            AssertContains(attributes, FamilyAttributeEnum.disabled);
            AssertContains(attributes, FamilyAttributeEnum.draggable);
            AssertContains(attributes, FamilyAttributeEnum.dir);
            AssertContains(attributes, FamilyAttributeEnum.id);
            AssertContains(attributes, FamilyAttributeEnum.integrity);
            AssertContains(attributes, FamilyAttributeEnum.href);
            AssertContains(attributes, FamilyAttributeEnum.hreflang);
            AssertContains(attributes, FamilyAttributeEnum.lang);
            AssertContains(attributes, FamilyAttributeEnum.media);
            AssertContains(attributes, FamilyAttributeEnum.referrerpolicy);
            AssertContains(attributes, FamilyAttributeEnum.rel);
            AssertContains(attributes, FamilyAttributeEnum.role);
            AssertContains(attributes, FamilyAttributeEnum.sizes);
            AssertContains(attributes, FamilyAttributeEnum.spellcheck);
            AssertContains(attributes, FamilyAttributeEnum.style);
            AssertContains(attributes, FamilyAttributeEnum.title);
            AssertContains(attributes, FamilyAttributeEnum.tabindex);
            //AssertContains(attributes,  FamilyAttributeEnum.hidden);
            AssertContains(attributes, FamilyAttributeEnum.translate);
            AssertContains(attributes, FamilyAttributeEnum.type);
        }

        [Fact]
        public void GetAllAttributesAcceptedA()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.a);

            Assert.Equal(19, attributes.Count);
            AssertContains(attributes, FamilyAttributeEnum.href);
            AssertContains(attributes, FamilyAttributeEnum.hidden);
            AssertContains(attributes, FamilyAttributeEnum.target);
            AssertContains(attributes, FamilyAttributeEnum.download);
            AssertContains(attributes, FamilyAttributeEnum.rel);
            AssertContains(attributes, FamilyAttributeEnum.hreflang);
            AssertContains(attributes, FamilyAttributeEnum.type);
            AssertContains(attributes, FamilyAttributeEnum.referrerpolicy);
            AssertContains(attributes, FamilyAttributeEnum.id);
            AssertContains(attributes, FamilyAttributeEnum.classCss);
            AssertContains(attributes, FamilyAttributeEnum.style);
            AssertContains(attributes, FamilyAttributeEnum.title);
            AssertContains(attributes, FamilyAttributeEnum.data_);
            AssertContains(attributes, FamilyAttributeEnum.tabindex);
            AssertContains(attributes, FamilyAttributeEnum.accesskey);
            AssertContains(attributes, FamilyAttributeEnum.role);
            AssertContains(attributes, FamilyAttributeEnum.draggable);
            AssertContains(attributes, FamilyAttributeEnum.lang);
            AssertContains(attributes, FamilyAttributeEnum.dir);
        }

        private void AssertContains(List<string> attributes, FamilyAttributeEnum family)
        {
            string value = family.ToString();
            Assert.Contains(attributes, o => o == value);
        }
    }
}