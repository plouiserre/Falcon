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

            Assert.Equal(13, attributes.Count);
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
        }
    }
}