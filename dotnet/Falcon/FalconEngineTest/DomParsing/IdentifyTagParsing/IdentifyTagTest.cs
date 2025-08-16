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

        public IdentifyTagTest()
        {
        }

        [Fact]
        public void IdentifyClairlyHtmlTagElement()
        {
            string html = HtmlPageSimpleData.GetHtmlSimple();
            var identifyTag = TestFactory.InitIdentifyTag();

            var tag = identifyTag.Analyze(html);

            Assert.Equal("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", tag.TagStart);
            Assert.Equal("</html>", tag.TagEnd);
            Assert.Equal(3, tag.Attributes.Count);
            Assert.Equal(FamilyAttributeEnum.lang.ToString(), tag.Attributes[0].FamilyAttribute);
            Assert.Equal("en", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.dir.ToString(), tag.Attributes[1].FamilyAttribute);
            Assert.Equal("auto", tag.Attributes[1].Value);
            Assert.Equal(FamilyAttributeEnum.xmlns.ToString(), tag.Attributes[2].FamilyAttribute);
            Assert.Equal("http://www.w3.org/1999/xhtml", tag.Attributes[2].Value);
            Assert.Equal(NameTagEnum.html, tag.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
        }


        [Fact]
        public void IdentifyClairlyDoctypeTagElement()
        {
            string html = "<!DOCTYPE html>";
            var identifyTag = TestFactory.InitIdentifyTag();

            var tag = identifyTag.Analyze(html);

            Assert.Equal("<!DOCTYPE html>", tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Equal(NameTagEnum.doctype, tag.NameTag);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
        }


        [Fact]
        public void IdentifyClairlWithSoManyHtml()
        {
            string html = "<title>Document</title><link rel=\"stylesheet\" href=\"main.css\">";
            var identifyTag = TestFactory.InitIdentifyTag();

            var tag = identifyTag.Analyze(html);

            Assert.Equal("<title>", tag.TagStart);
            Assert.Equal("</title>", tag.TagEnd);
            Assert.Null(tag.Attributes);
            Assert.Equal(NameTagEnum.title, tag.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
        }

    }
}