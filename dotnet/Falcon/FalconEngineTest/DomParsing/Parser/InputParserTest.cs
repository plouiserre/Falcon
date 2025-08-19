using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class InputParserTest
    {
        [Fact]
        public void ParseInputSubmit()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.inputSubmit);
            var inputTagParser = TestFactory.InitInputParser();

            var tag = inputTagParser.Parse(html);

            AssertFormPage.AssertInputSubmit(tag);
        }

        [Fact]
        public void ParseInputFile()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.inputFile);
            var inputTagParser = TestFactory.InitInputParser();

            var tag = inputTagParser.Parse(html);

            AssertFormPage.AssertInputFile(tag);
        }

        [Fact]
        public void ParseInputRadioCheck()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.radioUndefined);
            var inputTagParser = TestFactory.InitInputParser();

            var tag = inputTagParser.Parse(html);

            AssertFormPage.AssertRadioUndefined(tag);
        }

        [Fact]
        public void ParseInputText()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.inputFirstName);
            var inputTagParser = TestFactory.InitInputParser();

            var tag = inputTagParser.Parse(html);

            AssertFormPage.AssertInputFirstName(tag);
        }
    }
}