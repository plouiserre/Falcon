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

        [Theory]
        [InlineData("form")]
        [InlineData("year")]
        [InlineData("someone")]
        public void NotValidateGoodValueAttributesTypeInButton(string typeAttributeValue)
        {
            string? html = string.Concat("<input type=\"", typeAttributeValue, "\">");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("image")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("submit")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void ValidateGoodValueAttributesTypeInInput(string typeAttributeValue)
        {
            string? html = string.Concat("<input type=\"", typeAttributeValue, "\">");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("hidden")]
        [InlineData("image")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("submit")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseAcceptAttributeIsNotWithTypeFile(string type)
        {
            string? html = string.Format($"<input accept=\"image/*\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ValidateBecauseAcceptAttributeIsWithTypeFile()
        {
            string? html = "<input accept=\"image/*\" type=\"file\" >";
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("submit")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseAltAttributeIsNotWithTypeImg(string typeAttributeValue)
        {
            string? html = string.Concat($"<input alt=\"cute dog/*\" type=\"{typeAttributeValue}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ValidateBecauseAltAttributeIsWithTypeImg()
        {
            string? html = "<input alt=\"cute dog/*\" type=\"image\" >";
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("email")]
        [InlineData("password")]
        [InlineData("url")]
        public void NotValidateBecauseAutocapitalizeIsWithUrlEmailOrPasswordType(string type)
        {
            string? html = string.Concat($"<input autocapitalize=\"none\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("image")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("submit")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("week")]
        public void ValidateBecauseAutocapitalizeIsNotWithUrlEmailOrPasswordType(string type)
        {
            string? html = string.Concat($"<input autocapitalize=\"none\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }


        [Theory]
        [InlineData("checkbox")]
        [InlineData("radio")]
        [InlineData("button")]
        public void NotValidateBecauseAutocompleteIsWithCheckboxRadioOrButtonType(string type)
        {
            string? html = string.Concat($"<input autocomplete=\"on\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("image")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("submit")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void ValidateBecauseAutocompleteIsNotWithCheckboxRadioOrButtonType(string type)
        {
            string? html = string.Concat($"<input autocomplete=\"none\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("hidden")]
        [InlineData("image")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("submit")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseCaptureAttributeIsNotWithTypeFile(string type)
        {
            string? html = string.Format($"<input capture=\"user\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ValidateBecauseCaptureAttributeIsWithTypeFile()
        {
            string? html = string.Format($"<input capture=\"user\"  type=\"file\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }


        [Theory]
        [InlineData("button")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("image")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("submit")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseCheckedAttributeIsNotWithTypeCheckboxRadio(string type)
        {
            string? html = string.Format($"<input checked type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("checked")]
        [InlineData("radio")]
        public void ValidateBecauseCheckedAttributeIsWithTypeCheckboxRadio(string type)
        {
            string? html = string.Format($"<input checked type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }


        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("file")]
        [InlineData("image")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("submit")]
        [InlineData("time")]
        [InlineData("week")]
        public void NotValidateBecauseDirnameAttributeIsNotWithTypeItsType(string type)
        {
            string? html = string.Format($"<input dirname=\"champ.dir\" type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("hidden")]
        [InlineData("text")]
        [InlineData("search")]
        [InlineData("url")]
        [InlineData("tel")]
        [InlineData("email")]
        public void ValidateBecauseDirnameAttributeIsWithTypeItsType(string type)
        {
            string? html = string.Format($"<input dirname=\"champ.dir\" type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseFormactionAttributeIsNotWithImageSubmit(string type)
        {
            string? html = string.Format($"<input formaction=\"/submit\" type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("image")]
        [InlineData("submit")]
        public void ValidateBecauseFormactionAttributeIsWithTypeImageSubmit(string type)
        {
            string? html = string.Format($"<input formaction=\"/submit\" type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseFormenctypeAttributeIsNotWithImageSubmit(string type)
        {
            string? html = string.Format($"<input formenctype=\"multipart/form-data\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("image")]
        [InlineData("submit")]
        public void ValidateBecauseFormenctypeAttributeIsWithTypeImageSubmit(string type)
        {
            string? html = string.Format($"<input formenctype=\"multipart/form-data\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseFormmethodAttributeIsNotWithImageSubmit(string type)
        {
            string? html = string.Format($"<input formmethod=\"post\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("image")]
        [InlineData("submit")]
        public void ValidateBecauseFormmethodAttributeIsWithTypeImageSubmit(string type)
        {
            string? html = string.Format($"<input formmethod=\"post\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseFormNoValidateAttributeIsNotWithImageSubmit(string type)
        {
            string? html = string.Format($"<input formnovalidate  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("image")]
        [InlineData("submit")]
        public void ValidateBecauseFormnovalidateAttributeIsWithTypeImageSubmit(string type)
        {
            string? html = string.Format($"<input formnovalidate  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseFormtargetAttributeIsNotWithImageSubmit(string type)
        {
            string? html = string.Format($"<input formtarget=\"_blank\"   type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("image")]
        [InlineData("submit")]
        public void ValidateBecauseFormtargetAttributeIsWithTypeImageSubmit(string type)
        {
            string? html = string.Format($"<input formtarget=\"_blank\"   type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("hidden")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("password")]
        [InlineData("radio")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("submit")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NotValidateBecauseHeightAttributeIsNotWithImage(string type)
        {
            string? html = string.Format($"<input height=\"100\"  type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Fact]
        public void ValidateBecauseHeightAttributeIsWithTypeImage()
        {
            string? html = string.Format("<input height=\"100\" type=\"image\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("button")]
        [InlineData("checkbox")]
        [InlineData("hidden")]
        [InlineData("password")]
        [InlineData("radio")]
        public void NotValidateBecauseListAttributeIsWithBadTypes(string type)
        {
            string? html = string.Format($"<input list=\"suggestions\" type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("color")]
        [InlineData("date")]
        [InlineData("datetime-local")]
        [InlineData("email")]
        [InlineData("file")]
        [InlineData("image")]
        [InlineData("month")]
        [InlineData("number")]
        [InlineData("range")]
        [InlineData("reset")]
        [InlineData("search")]
        [InlineData("submit")]
        [InlineData("tel")]
        [InlineData("text")]
        [InlineData("time")]
        [InlineData("url")]
        [InlineData("week")]
        public void NValidateBecauseListAttributeIsNotWithBadTypes(string type)
        {
            string? html = string.Format($"<input list=\"suggestions\" type=\"{type}\" >");
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        // [Theory]
        // [InlineData("button")]
        // [InlineData("checkbox")]
        // [InlineData("color")]
        // [InlineData("date")]
        // [InlineData("datetime-local")]
        // [InlineData("email")]
        // [InlineData("file")]
        // [InlineData("hidden")]
        // [InlineData("image")]
        // [InlineData("month")]
        // [InlineData("number")]
        // [InlineData("password")]
        // [InlineData("radio")]
        // [InlineData("range")]
        // [InlineData("reset")]
        // [InlineData("search")]
        // [InlineData("submit")]
        // [InlineData("tel")]
        // [InlineData("text")]
        // [InlineData("time")]
        // [InlineData("url")]
        // [InlineData("week")]
    }
}