using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngineTest.Utils;

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

            identifyTag.DetermineStartEndTags(HtmlData.GetMetaViewPort());

            Assert.Equal(HtmlData.GetMetaViewPort(), identifyTag.StartTag);
            Assert.Null(identifyTag.EndTag);
        }

        [Fact]
        public void IdentifyHeadStartAndEndTag()
        {
            var identifyTag = new IdentifyStartTagEndTag();

            identifyTag.DetermineStartEndTags(HtmlData.HeadSimple);

            Assert.Equal("<head>", identifyTag.StartTag);
            Assert.Equal("</head>", identifyTag.EndTag);
        }

        [Fact]
        public void TryIdentifyStartAndEndTagWithNoTags()
        {
            var identifyTag = new IdentifyStartTagEndTag();

            var exception = Assert.Throws<NoStartTagException>(() => identifyTag.DetermineStartEndTags("Hello world!!!"));

            Assert.Equal(ErrorTypeParsing.starttagmissing, exception.ErrorType);
            Assert.Equal("Hello world!!! doesn't contains tags", exception.Message);
        }
    }
}