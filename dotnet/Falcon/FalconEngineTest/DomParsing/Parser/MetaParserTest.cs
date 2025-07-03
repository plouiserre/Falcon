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

        private MetaParser _metaParser;

        public MetaParserTest()
        {
            _metaParser = new MetaParser();
        }

        [Fact]
        public void ParseMetaCharset()
        {
            string html = HtmlData.MetaCharset;

            var tag = _metaParser.Parse(html);

            Assert.Equal(html, tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Equal(NameTagEnum.meta, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Single(tag.Attributes);
            Assert.Equal(FamilyAttributeEnum.charset, tag.Attributes[0].FamilyAttribute);
            Assert.Equal("UTF-8", tag.Attributes[0].Value);
            Assert.Null(tag.Children);
        }

        [Fact]
        public void ParseMetaProperty()
        {
            string html = HtmlData.MetaViewPort;

            var tag = _metaParser.Parse(html);

            Assert.Equal(html, tag.TagStart);
            Assert.Null(tag.TagEnd);
            Assert.Equal(NameTagEnum.meta, tag.NameTag);
            Assert.Null(tag.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tag.TagFamily);
            Assert.Equal(2, tag.Attributes.Count);
            Assert.Equal(FamilyAttributeEnum.name, tag.Attributes[0].FamilyAttribute);
            Assert.Equal("viewport", tag.Attributes[0].Value);
            Assert.Equal(FamilyAttributeEnum.content, tag.Attributes[1].FamilyAttribute);
            Assert.Equal("width=device-width, initial-scale=1.0", tag.Attributes[1].Value);
            Assert.Null(tag.Children);
        }
    }
}