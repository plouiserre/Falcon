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


        [Fact]
        public void DetectAttributsLangAndDirInHtmlTag()
        {
            var discoverAttributs = new DiscoverAttributs();

            string tagHtmlStart = "<html lang=\"en\" dir=\"auto\">";
            var attributs = discoverAttributs.Find(tagHtmlStart);

            Assert.Equal(2, attributs.Count);
            Assert.Equal(FamilyAttributeEnum.lang, attributs[0].FamilyAttribute);
            Assert.Equal("en", attributs[0].Value);
            Assert.Equal(FamilyAttributeEnum.dir, attributs[1].FamilyAttribute);
            Assert.Equal("auto", attributs[1].Value);
        }
    }
}