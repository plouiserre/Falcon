using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.IdentifyTagParsing
{
    public class IdentifyStartTagEndTagTest
    {
        [Fact]
        public void IdentifyTitleStartAndEndTag()
        {
            var identifyTag = new IdentifyStartTagEndTag();

            identifyTag.DetermineStartEndTags("<title>Document</title>");

            Assert.Equal("<title>", identifyTag.StartTag);
            Assert.Equal("</title>", identifyTag.EndTag);
        }

        [Fact]
        public void IdentifyMetaNameStartAndEndTag()
        {
            var identifyTag = new IdentifyStartTagEndTag();

            identifyTag.DetermineStartEndTags(HtmlPageSimpleData.GetHtml(TagHtmlSimple.metaviewPort));

            Assert.Equal(HtmlPageSimpleData.GetHtml(TagHtmlSimple.metaviewPort), identifyTag.StartTag);
            Assert.Null(identifyTag.EndTag);
        }

        [Fact]
        public void IdentifyHeadStartAndEndTag()
        {
            var identifyTag = new IdentifyStartTagEndTag();

            identifyTag.DetermineStartEndTags(HtmlPageSimpleData.GetHtml(TagHtmlSimple.head));

            Assert.Equal("<head>", identifyTag.StartTag);
            Assert.Equal("</head>", identifyTag.EndTag);
        }

        [Fact]
        public void TryIdentifyStartAndEndTagsWithNoTags()
        {
            var identifyTag = new IdentifyStartTagEndTag();

            var exception = Assert.Throws<NoStartTagException>(() => identifyTag.DetermineStartEndTags("Hello world!!!"));

            Assert.Equal(ErrorTypeParsing.starttagmissing, exception.ErrorType);
            Assert.Equal("Hello world!!! doesn't contains tags", exception.Message);
        }

        [Fact]
        public void TryToIdentifyStartAndEndTagsWithBadFormatting()
        {
            string html = "<p Hello world</p>";
            var identifyTag = new IdentifyStartTagEndTag();

            var exception = Assert.Throws<StartTagBadFormattedException>(() => identifyTag.DetermineStartEndTags(html));

            Assert.Equal(ErrorTypeParsing.starttagbadformatting, exception.ErrorType);
            Assert.Equal("<p Hello world</p> doesn't have tags well formatted", exception.Message);
        }
    }
}