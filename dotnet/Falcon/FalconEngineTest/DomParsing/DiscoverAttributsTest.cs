using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing;
using FalconEngine.Models;

namespace FalconEngineTest.DomParsing
{
    public class DiscoverAttributsTest
    {
        public DiscoverAttributsTest()
        {

        }

        [Fact]
        public void DetectAttributLangInHtmlTag()
        {
            var discoverAttributs = new DiscoverAttributs();

            string tagHtmlStart = "<html lang=\"en\">";
            var attributs = discoverAttributs.Find(tagHtmlStart);

            Assert.Single(attributs);
            Assert.Equal(FamilyAttributeEnum.lang, attributs[0].FamilyAttribute);
            Assert.Equal("en", attributs[0].Value);
        }
    }
}