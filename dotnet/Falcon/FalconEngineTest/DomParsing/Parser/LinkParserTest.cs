using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class LinkParserTest
    {
        [Fact]
        public void ParseLinkTag()
        {
            string html = HtmlData.LinkHead;
            var linkTagParser = new LinkParser();

            var tag = linkTagParser.Parse(html);

            Assert.Equal(html, tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Equal(NameTagEnum.link, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Equal(2, tag.Attributes.Count);
            Assert.Equal(FamilyAttributeEnum.rel, tag.Attributes[0].FamilyAttribute);
            Assert.Equal("stylesheet", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.href, tag.Attributes[1].FamilyAttribute);
            Assert.Equal("main.css", tag.Attributes[1].Value);
            Assert.Null(tag.Children);
        }
    }
}