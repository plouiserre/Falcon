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
        public void ParseInputSubmitAndValidateIt()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.inputSubmit);
            var inputTagParser = TestFactory.InitInputParser();

            var tag = inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            AssertFormPage.AssertInputSubmit(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseInputFileAndValidateIt()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.inputFile);
            var inputTagParser = TestFactory.InitInputParser();

            var tag = inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            AssertFormPage.AssertInputFile(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseInputRadioCheckAndValidateIt()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.radioUndefined);
            var inputTagParser = TestFactory.InitInputParser();

            var tag = inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            AssertFormPage.AssertRadioUndefined(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void ParseInputTextAndValidateIt()
        {
            string? html = HtmlPageFormData.GetHtml(TagHtmlForm.inputFirstName);
            var inputTagParser = TestFactory.InitInputParser();

            var tag = inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            AssertFormPage.AssertInputFirstName(tag);
            Assert.True(isValid);
        }

        [Fact]
        public void ValidateInputTagWithAllAcceptedAttributes()
        {
            string? html = "<input accept=\"image/*\"  alt=\"Texte alternatif\" autocapitalize=\"none\" autocomplete=\"on\" capture=\"user\" checked dirname=\"champ.dir\" disabled form=\"monFormulaire\" formaction=\"/submit\" formenctype=\"multipart/form-data\" formmethod=\"post\" formnovalidate formtarget=\"_blank\" height=\"100\" list=\"suggestions\" max=\"100\" maxlength=\"50\" min=\"1\" minlength=\"2\" multiple name=\"monInput\" pattern=\"[A-Za-z0-9]+\" placeholder=\"Entrez une valeur\" popovertarget=\"menu\" popovertargetaction=\"toggle\" readonly required size=\"30\" src=\"image.png\" step=\"1\" type=\"text\" value=\"Valeur par défaut\" width=\"200\" >";
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }


    }
}