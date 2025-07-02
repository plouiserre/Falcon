using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class MetaParserTest
    {
        [Fact]
        public void ParseMetaCharset()
        {
            string html = HtmlData.MetaCharset;
            var metaParser = new MetaParser();

            var tag = metaParser.Parse(html);

            Assert.Equal(html, tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Equal(NameTagEnum.meta, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Single(tag.Attributes);
            Assert.Equal(FamilyAttributeEnum.charset, tag.Attributes[0].FamilyAttribute);
            Assert.Equal("utf-8", tag.Attributes[0].Value);
            Assert.Null(tag.Children);
        }
    }
}