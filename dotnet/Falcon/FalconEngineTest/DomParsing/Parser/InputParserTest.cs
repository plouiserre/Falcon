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
            string? html = "<input disabled form=\"monFormulaire\"  minlength=\"2\" name=\"monInput\" pattern=\"[A-Za-z0-9]+\" placeholder=\"Entrez une valeur\" size=\"30\" type=\"text\">";
            var inputTagParser = TestFactory.InitInputParser();

            inputTagParser.Parse(html);
            bool isValid = inputTagParser.IsValid();

            Assert.True(isValid);
        }

        [Fact]
        public void NotValidateGoodValueAttributesTypeInButton()
        {
            var badTypes = new List<string> { "form", "year", "someone" };
            foreach (var typeAttributeValue in badTypes)
            {
                string? html = string.Concat("<input type=\"", typeAttributeValue, "\">");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateGoodValueAttributesTypeInInput()
        {
            var goodTypes = new List<string>() { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "image", "month", "number", "password", "radio",
                            "range", "reset", "search", "submit", "tel", "text", "time", "url", "week" };
            foreach (var typeAttributeValue in goodTypes)
            {
                string? html = string.Concat("<input type=\"", typeAttributeValue, "\">");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseAcceptAttributeIsNotWithTypeFile()
        {
            var badTypes = new List<string>() { "button", "checkbox", "color", "date", "datetime-local", "email", "hidden", "image", "month", "number", "password", "radio",
                            "range", "reset", "search", "submit", "tel", "text", "time", "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input accept=\"image/*\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
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

        [Fact]
        public void NotValidateBecauseAltAttributeIsNotWithTypeImg()
        {
            var badTypes = new List<string>() { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month", "number", "password", "radio",
                            "range", "reset", "search", "submit", "tel", "text", "time", "url", "week" };
            foreach (var typeAttributeValue in badTypes)
            {
                string? html = string.Concat($"<input alt=\"cute dog/*\" type=\"{typeAttributeValue}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
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

        [Fact]
        public void NotValidateBecauseAutocapitalizeIsWithUrlEmailOrPasswordType()
        {
            var badTypes = new List<string>() { "email", "password", "url" };
            foreach (var type in badTypes)
            {
                string? html = string.Concat($"<input autocapitalize=\"none\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseAutocapitalizeIsNotWithUrlEmailOrPasswordType()
        {
            var goodTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "file", "hidden", "image", "month",
                                                "number", "radio", "range", "reset", "search", "submit", "tel", "text", "time",
                                                "week" };
            foreach (var type in goodTypes)
            {
                string? html = string.Concat($"<input autocapitalize=\"none\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }


        [Fact]
        public void NotValidateBecauseAutocompleteIsWithCheckboxRadioOrButtonType()
        {
            var badTypes = new List<string>() { "checkbox", "radio", "button" };
            foreach (var type in badTypes)
            {
                string? html = string.Concat($"<input autocomplete=\"on\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseAutocompleteIsNotWithCheckboxRadioOrButtonType()
        {
            var goodTypes = new List<string> { "color", "date", "datetime-local", "email", "file", "hidden", "image", "month",
                                                "number", "password", "range", "reset", "search", "submit", "tel", "text", "time",
                                                "url", "week" };
            foreach (var type in goodTypes)
            {
                string? html = string.Concat($"<input autocomplete=\"none\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseCaptureAttributeIsNotWithTypeFile()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "email", "hidden", "image", "month",
                                                "number", "password", "range", "reset", "radio", "search", "submit", "tel", "text", "time",
                                                "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input capture=\"user\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
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


        [Fact]
        public void NotValidateBecauseCheckedAttributeIsNotWithTypeCheckboxRadio()
        {
            var badTypes = new List<string> { "button", "color", "date", "datetime-local", "email", "file", "hidden", "image", "month",
                                                "number", "password", "range", "reset", "search", "submit", "tel", "text", "time",
                                                "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input checked type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseCheckedAttributeIsWithTypeCheckboxRadio()
        {
            var goodTypes = new List<string> { "checkbox", "radio" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input checked type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }


        [Fact]
        public void NotValidateBecauseDirnameAttributeIsNotWithTypeItsType()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "file", "image", "month",
                                                "number", "password", "radio", "range", "reset", "submit", "time",
                                                "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input dirname=\"champ.dir\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseDirnameAttributeIsWithTypeItsType()
        {
            var goodTypes = new List<string> { "hidden", "text", "search", "url", "tel", "email" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input dirname=\"champ.dir\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseFormactionAttributeIsNotWithImageSubmit()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month",
                                                "number", "password", "radio", "range", "reset", "search", "tel", "text", "time",
                                                "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input formaction=\"/submit\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseFormactionAttributeIsWithTypeImageSubmit()
        {
            var goodTypes = new List<string> { "image", "submit" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input formaction=\"/submit\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseFormenctypeAttributeIsNotWithImageSubmit()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month",
                                                "number", "password", "radio", "range", "reset", "search", "tel", "text", "time",
                                                "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input formenctype=\"multipart/form-data\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseFormenctypeAttributeIsWithTypeImageSubmit()
        {
            var goodTypes = new List<string> { "image", "submit" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input formenctype=\"multipart/form-data\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseFormmethodAttributeIsNotWithImageSubmit()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month",
                                                "number", "password", "radio", "range", "reset", "search", "tel", "text", "time",
                                                "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input formmethod=\"post\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseFormmethodAttributeIsWithTypeImageSubmit()
        {
            var goodTypes = new List<string> { "image", "submit" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input formmethod=\"post\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseFormNoValidateAttributeIsNotWithImageSubmit()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month",
                                                "number", "password", "radio", "range", "reset", "search", "tel", "text", "time",
                                                "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input formnovalidate  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseFormnovalidateAttributeIsWithTypeImageSubmit()
        {
            var goodTypes = new List<string> { "image", "submit" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input formnovalidate  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseFormtargetAttributeIsNotWithImageSubmit()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month",
                                                "number", "password", "radio", "range", "reset", "search", "tel", "text", "time",
                                                "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input formtarget=\"_blank\"   type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseFormtargetAttributeIsWithTypeImageSubmit()
        {
            var goodTypes = new List<string> { "image", "submit" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input formtarget=\"_blank\"   type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseHeightAttributeIsNotWithImage()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month",
                                                "number", "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time",
                                                "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input height=\"100\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
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

        [Fact]
        public void NotValidateBecauseListAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "button", "checkbox", "hidden", "password", "radio" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input list=\"suggestions\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseListAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string>{"color", "date", "datetime-local", "email", "file", "image", "month", "range", "reset", "search", "submit", "tel", "text",
                                                    "time", "url", "week"};
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input list=\"suggestions\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseMaxAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "email", "file", "hidden", "image", "password", "radio", "reset", "search", "submit", "tel",
                "text", "url"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input max=\"100\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseMaxAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "date", "datetime-local", "month", "number", "range", "time", "week" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input max=\"100\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseMaxLengthAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "file", "hidden", "image", "month", "number",
                "radio", "range", "reset", "submit","time", "week"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input maxlength=\"50\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseMaxLengthAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "email", "password", "search", "url", "tel", "text" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input maxlength=\"50\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseMinAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "email", "file", "hidden", "image", "password", "radio", "reset", "search", "submit", "tel",
                "text", "url"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input min=\"1\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseMinAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "date", "datetime-local", "month", "number", "range", "time", "week" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input min=\"1\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseMinLengthAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "file", "hidden", "image", "month", "number",
                "radio", "range", "reset", "submit","time", "week"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input minlength=\"2\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseMinLengthAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "email", "password", "search", "url", "tel", "text" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input minlength=\"2\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseMultipleAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "hidden", "image", "month", "number",
                    "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time", "url", "week"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input multiple type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseMultipleAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "email", "file" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input multiple type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecausePatternAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "file", "hidden", "image", "month", "number",
                                        "radio", "range", "reset", "submit", "time", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input pattern=\"[A-Za-z0-9]+\"  type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecausePatternAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "email", "password", "search", "tel", "text", "url" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input pattern=\"[A-Za-z0-9]+\"   type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecausePlaceholderAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "file", "hidden", "image", "month", "radio", "range", "reset", "submit", "time", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input placeholder=\"Entrez une valeur\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecausePlaceholderAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "email", "number", "password", "search", "tel", "text", "url" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input placeholder=\"Entrez une valeur\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecausePopovertargetAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "image", "month", "number",
                                                    "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time", "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input popovertarget=\"menu\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecausePopovertargetAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "button" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input popovertarget=\"menu\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecausePopovertargetactionAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "image", "month", "number",
                                                    "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time", "url", "week" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input popovertargetaction=\"menu\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecausePopovertargetactionAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "button" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input popovertargetaction=\"menu\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseReadonlyAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "date", "datetime-local", "email", "file", "image", "month", "number", "password", "reset", "search",
                                                        "submit", "tel", "text", "time", "url", "week"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input readonly type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseReadonlyAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "button", "checkbox", "color", "hidden", "radio", "range" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input readonly type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseRequiredAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "checkbox", "date", "datetime-local", "email", "file", "image", "month", "number", "password", "radio",
                                            "reset", "search", "submit", "tel", "text", "time", "url", "week"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input required type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseRequiredAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "button", "color", "hidden", "range" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input required type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseSizeAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> {"button", "checkbox", "color", "date", "datetime-local", "file", "hidden", "image", "month", "number", "radio", "range", "reset",
                                            "submit", "time", "week"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input size=\"30\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseSizeAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "email", "password", "search", "tel", "text", "url" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input size=\"30\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseSrcAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> {"button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month", "number",
                                            "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time", "url", "week"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input src=\"image.png\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseSrcAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "image" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input src=\"image.png\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseStepAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> {"button", "checkbox", "color", "email", "file", "hidden", "image", "password", "radio", "reset", "search",
                                "submit", "tel", "text", "url"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input step=\"1\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseStepAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "date", "datetime-local", "month", "number", "range", "time", "week" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input step=\"1\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        [Fact]
        public void NotValidateBecauseValueAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> { "image" };
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input value=\"Valeur par défaut\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseValueAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month", "number",
                                            "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time", "url", "week"};
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input value=\"Valeur par défaut\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }



        [Fact]
        public void NotValidateBecauseWidthAttributeIsWithBadTypes()
        {
            var badTypes = new List<string> {"button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "month", "number",
                                            "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time", "url", "week"};
            foreach (var type in badTypes)
            {
                string? html = string.Format($"<input width=\"200\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.False(isValid);
            }
        }

        [Fact]
        public void ValidateBecauseWidthAttributeIsNotWithBadTypes()
        {
            var goodTypes = new List<string> { "image" };
            foreach (var type in goodTypes)
            {
                string? html = string.Format($"<input width=\"200\" type=\"{type}\" >");
                var inputTagParser = TestFactory.InitInputParser();

                inputTagParser.Parse(html);
                bool isValid = inputTagParser.IsValid();

                Assert.True(isValid);
            }
        }

        // new List<string> { "button", "checkbox", "color", "date", "datetime-local", "email", "file", "hidden", "image", "month", "number", "password", "radio", "range", "reset", "search", "submit", "tel", "text", "time", "url", "week"};       
    }
}