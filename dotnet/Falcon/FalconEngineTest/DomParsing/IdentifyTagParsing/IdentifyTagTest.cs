using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.IdentifyTagParsing
{
    public class IdentifyTagTest
    {
        private DeleteUselessSpace _deleteUselessSpace;
        private AttributeTagParser _attributeTagParser;
        private IdentifyTagName _identifyTagName;
        private IdentifyTagFamily _identifyTagFamily;

        public IdentifyTagTest()
        {
            _deleteUselessSpace = new DeleteUselessSpace();
            _attributeTagParser = new AttributeTagParser();
            _identifyTagName = new IdentifyTagName();
            _identifyTagFamily = new IdentifyTagFamily();
        }

        [Fact]
        public void IdentifyClairlyHtmlTagElement()
        {
            string html = HtmlData.HtmlSimpleWithSpace;
            var identifyTag = new IdentifyTag(_deleteUselessSpace, _attributeTagParser, _identifyTagName, _identifyTagFamily);

            var tag = identifyTag.Analyze(html);

            Assert.Equal("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", tag.TagStart);
            Assert.Equal("</html>", tag.TagEnd);
            Assert.Equal(3, tag.Attributes.Count);
            Assert.Equal(FamilyAttributeEnum.lang, tag.Attributes[0].FamilyAttribute);
            Assert.Equal("en", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.dir, tag.Attributes[1].FamilyAttribute);
            Assert.Equal("auto", tag.Attributes[1].Value);
            Assert.Equal(FamilyAttributeEnum.xmlns, tag.Attributes[2].FamilyAttribute);
            Assert.Equal("http://www.w3.org/1999/xhtml", tag.Attributes[2].Value);
        }


        [Fact]
        public void IdentifyClairlyDoctypeTagElement()
        {
            string html = "<!DOCTYPE html>";
            var identifyTag = new IdentifyTag(_deleteUselessSpace, _attributeTagParser, _identifyTagName, _identifyTagFamily);

            var tag = identifyTag.Analyze(html);

            Assert.Equal("<!DOCTYPE html>", tag.TagStart);
            Assert.Null(tag.TagEnd);
        }


        [Fact]
        public void IdentifyClairlWithSoManyHtml()
        {
            string html = "<title>Document</title><link rel=\"stylesheet\" href=\"main.css\">";
            var identifyTag = new IdentifyTag(_deleteUselessSpace, _attributeTagParser, _identifyTagName, _identifyTagFamily);

            var tag = identifyTag.Analyze(html);

            Assert.Equal("<title>", tag.TagStart);
            Assert.Equal("</title>", tag.TagEnd);
            Assert.Null(tag.Attributes);
        }

    }
}