using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.Models;

namespace FalconEngineTest.CleanData
{
    public class DeleteUselessSpaceTest
    {
        [Fact]
        public void PurgeUselessCaractersBeforeTag()
        {
            string htmlNotClean = "                           \r\n                         <meta charset=\"UTF-8\">";

            var cleaner = new DeleteUselessSpace(null);

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<meta charset=\"UTF-8\">";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void PurgeUselessCaractersAfterTagSimple()
        {
            string htmlNotClean = "<link rel=\"stylesheet\" href=\"main.css\">            \r\n         ";

            var cleaner = new DeleteUselessSpace(null);

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<link rel=\"stylesheet\" href=\"main.css\">";
            Assert.Equal(htmlExpected, htmlClean);
        }

        [Fact]
        public void PurgeUselessCaractersAfterTagComplexe()
        {
            string htmlNotClean = "<head><title>Document</title>\r\n</head>            \r\n         ";

            var cleaner = new DeleteUselessSpace(null);

            string htmlClean = cleaner.PurgeUselessCaractersAroundTag(htmlNotClean);

            string htmlExpected = "<head><title>Document</title></head>";
            Assert.Equal(htmlExpected, htmlClean);
        }
    }
}