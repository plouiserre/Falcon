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

            Assert.Equal(16, attributes.Count);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.lang);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.dir);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.xmlns);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.manifest);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.style);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.id);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.classCss);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datapage);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datatheme);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datauser);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.accesskey);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.tabindex);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.contenteditable);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.draggable);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.spellcheck);
        }

        [Fact]
        public void GetAllAttributesAcceptedMeta()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.meta);

            Assert.Equal(14, attributes.Count);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.charset);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.name);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.content);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.httpequiv);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.scheme);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.id);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.classCss);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datapage);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datatheme);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datauser);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.lang);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.dir);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.title);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.style);
        }

        [Fact]
        public void GetAllAttributesAcceptedLink()
        {
            var attributeTagManager = new AttributeTagManager();

            attributeTagManager.SetAttributes();

            var attributes = attributeTagManager.GetAttributes(NameTagEnum.link);

            Assert.Equal(29, attributes.Count);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.accesskey);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.asAttr);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.blocking);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.classCss);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.crossorigin);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datapage);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datapreload);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datapurpose);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datatheme);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.datauser);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.disabled);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.draggable);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.dir);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.id);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.integrity);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.href);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.hreflang);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.lang);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.media);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.referrerpolicy);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.rel);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.role);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.sizes);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.spellcheck);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.style);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.title);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.tabindex);
            //Assert.Contains(attributes, o => o == FamilyAttributeEnum.hidden);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.translate);
            Assert.Contains(attributes, o => o == FamilyAttributeEnum.type);
        }
    }
}