using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.Data
{
    public class SimulateParsingFormPage
    {
        private static TagModel? _doctypeTag;
        private static TagModel? _htmlTag;
        private static TagModel? _headTag;
        private static HtmlPage? _htmlPage { get; set; }

        public static HtmlPage InitHtmlPage()
        {
            _doctypeTag = GetDoctypeTag();
            _htmlTag = GetTagHtml();
            var tags = new List<TagModel>() { _doctypeTag, _htmlTag };
            _htmlPage = new HtmlPage() { Tags = tags };
            return _htmlPage;
        }

        private static TagModel GetDoctypeTag()
        {
            var doctypeTag = new TagModel()
            {
                NameTag = NameTagEnum.doctype,
                TagStart = "<!DOCTYPE html>",
                TagFamily = TagFamilyEnum.NoEnd
            };
            return doctypeTag;
        }

        private static TagModel GetTagHtml()
        {
            var attributLang = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.lang.ToString(), Value = "en" };
            var attributDir = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.dir.ToString(), Value = "auto" };
            var attributXmlns = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.xmlns.ToString(), Value = "http://www.w3.org/1999/xhtml" };
            _headTag = GetHeadTag();
            var body = GetBodyTag();
            var htmlTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributLang, attributDir, attributXmlns },
                NameTag = NameTagEnum.html,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlPageFormData.GetHtml(TagHtmlForm.htmlForm).Replace("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", string.Empty).Replace("</html>", string.Empty),
                TagStart = "<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">",
                TagEnd = "</html>",
                Children = new List<TagModel>() { _headTag, body }
            };
            return htmlTag;
        }

        private static TagModel GetHeadTag()
        {
            var metaCharsetTag = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.charset.ToString(), Value = "UTF-8" } },
                NameTag = NameTagEnum.meta,
                TagStart = HtmlPageFormData.GetHtml(TagHtmlForm.metaCharset)
            };
            var metaViewPort = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.name.ToString(), Value = "viewport" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.content.ToString(), Value = "width=device-width, initial-scale=1.0" }
                },
                NameTag = NameTagEnum.meta,
                TagStart = HtmlPageFormData.GetHtml(TagHtmlForm.metaViewPort)
            };
            var title = new TagModel()
            {
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.title,
                Content = "Document",
                TagStart = "<title>",
                TagEnd = "</title>"
            };
            var link = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.rel.ToString(), Value = "stylesheet" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.href.ToString(), Value = "main.css" },
                        new AttributeModel(){ FamilyAttribute = "data-preload", Value = "true"}
                },
                NameTag = NameTagEnum.link,
                TagStart = "<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">",
            };
            var headTag = new TagModel()
            {
                NameTag = NameTagEnum.head,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlPageFormData.GetHtml(TagHtmlForm.head).Replace("<head>", string.Empty).Replace("</head>", string.Empty),
                Children = new List<TagModel>() { metaCharsetTag, metaViewPort, title, link },
                TagStart = "<head>",
                TagEnd = "</head>"
            };
            return headTag;
        }

        private static TagModel GetBodyTag()
        {
            var formPost = GetFormPost();
            var bodyTag = new TagModel()
            {
                NameTag = NameTagEnum.body,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlPageFormData.GetHtml(TagHtmlForm.form),
                Children = new List<TagModel>() { formPost },
                TagStart = "<body>",
                TagEnd = "</body>"
            };
            return bodyTag;
        }

        private static TagModel GetFormPost()
        {
            var attributMethod = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.method.ToString(), Value = "POST" };
            var attributAction = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.action.ToString(), Value = "/candidate" };
            string content = string.Concat(HtmlPageFormData.GetHtml(TagHtmlForm.divH1), HtmlPageFormData.GetHtml(TagHtmlForm.divIdentity),
                                HtmlPageFormData.GetHtml(TagHtmlForm.divGender), HtmlPageFormData.GetHtml(TagHtmlForm.divDate), HtmlPageFormData.GetHtml(TagHtmlForm.divResume),
                                HtmlPageFormData.GetHtml(TagHtmlForm.divSubmit));
            var divTitle = GetDivTitle();
            var divIdentity = GetDivIdentity();
            var divGender = GetDivGender();
            var divBirthday = GetDivBirthDay();
            var divResume = GetDivResume();
            var divSend = GetDivSend();
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributMethod, attributAction },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<div id=\"content\">",
                TagEnd = "</div>",
                Children = new List<TagModel>() { divTitle, divIdentity, divGender, divBirthday, divResume, divSend }
            };
            return divTag;
        }

        private static TagModel GetDivTitle()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Title" };
            string content = HtmlPageFormData.GetHtml(TagHtmlForm.h1Title);
            var h1Title = GetH1Title();
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<div class=\"Title\">",
                TagEnd = "</div>",
                Children = new List<TagModel>() { h1Title }
            };
            return divTag;
        }

        private static TagModel GetH1Title()
        {
            string content = "Present your candidature";
            var h1Tag = new TagModel()
            {
                NameTag = NameTagEnum.h1,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<h1>",
                TagEnd = "</h1>"
            };
            return h1Tag;
        }

        private static TagModel GetDivIdentity()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Identity" };
            string content = string.Concat(HtmlPageFormData.GetHtml(TagHtmlForm.inputFirstName), HtmlPageFormData.GetHtml(TagHtmlForm.inputLastName));
            var inputFirstName = GetInputFirstName();
            var inputLastName = GetInputLastName();
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<div class=\"Identity\">",
                TagEnd = "</div>",
                Children = new List<TagModel>() { inputFirstName, inputLastName }
            };
            return divTag;
        }

        private static TagModel GetInputFirstName()
        {
            var inputFirstName = new TagModel()
            {
                NameTag = NameTagEnum.input,
                TagFamily = TagFamilyEnum.NoEndIncomplete,
                Content = string.Empty,
                TagStart = "<input type=\"text\" placeholder=\"FirstName\">"
            };
            return inputFirstName;
        }

        private static TagModel GetInputLastName()
        {
            var inputLastName = new TagModel()
            {
                NameTag = NameTagEnum.input,
                TagFamily = TagFamilyEnum.NoEndIncomplete,
                Content = string.Empty,
                TagStart = "<input type=\"text\" placeholder=\"LastName\"> "
            };
            return inputLastName;
        }

        private static TagModel GetDivGender()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Gender" };
            string content = string.Concat(HtmlPageFormData.GetHtml(TagHtmlForm.labelGender), HtmlPageFormData.GetHtml(TagHtmlForm.radioMale),
                            HtmlPageFormData.GetHtml(TagHtmlForm.labelMale), HtmlPageFormData.GetHtml(TagHtmlForm.radioFemale),
                            HtmlPageFormData.GetHtml(TagHtmlForm.labelFemale), HtmlPageFormData.GetHtml(TagHtmlForm.radioUndefined),
                            HtmlPageFormData.GetHtml(TagHtmlForm.labelUndefined));
            var labelGender = GetLabelGender();
            var radioMale = GetRadioMale();
            var labelMale = GetLabelMale();
            var radioFemale = GetRadioFemale();
            var labelFemale = GetLabelFemale();
            var radioUndefined = GetRadioUndefined();
            var labelUndefined = GetLabelUndefined();
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<div class=\"Identity\">",
                TagEnd = "</div>",
                Children = new List<TagModel>() { labelGender, radioMale, labelMale, radioFemale, labelFemale, radioUndefined, labelUndefined }
            };
            return divTag;
        }

        private static TagModel GetLabelGender()
        {
            string content = "Gender";
            var labelGender = new TagModel()
            {
                NameTag = NameTagEnum.label,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<label for=\"rgender\">",
                TagEnd = "</label>"
            };
            return labelGender;
        }

        private static TagModel GetRadioMale()
        {
            var radioMale = new TagModel()
            {
                NameTag = NameTagEnum.input,
                TagFamily = TagFamilyEnum.NoEnd,
                Content = string.Empty,
                TagStart = "<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"male\"/>"
            };
            return radioMale;
        }

        private static TagModel GetLabelMale()
        {
            string content = "Male";
            var labelGender = new TagModel()
            {
                NameTag = NameTagEnum.label,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<label for=\"rgender\">",
                TagEnd = "</label>"
            };
            return labelGender;
        }

        private static TagModel GetRadioFemale()
        {
            var radioFemale = new TagModel()
            {
                NameTag = NameTagEnum.input,
                TagFamily = TagFamilyEnum.NoEnd,
                Content = string.Empty,
                TagStart = "<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"female\"/>"
            };
            return radioFemale;
        }

        private static TagModel GetLabelFemale()
        {
            string content = "Female";
            var labelGender = new TagModel()
            {
                NameTag = NameTagEnum.label,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<label for=\"rgender\">",
                TagEnd = "</label>"
            };
            return labelGender;
        }

        private static TagModel GetRadioUndefined()
        {
            var radioUndefined = new TagModel()
            {
                NameTag = NameTagEnum.input,
                TagFamily = TagFamilyEnum.NoEnd,
                Content = string.Empty,
                TagStart = "<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"undefined\"/>"
            };
            return radioUndefined;
        }

        private static TagModel GetLabelUndefined()
        {
            string content = "Undefined";
            var labelGender = new TagModel()
            {
                NameTag = NameTagEnum.label,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<label for=\"rgender\">",
                TagEnd = "</label>"
            };
            return labelGender;
        }

        private static TagModel GetDivBirthDay()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Birthday" };
            string content = string.Concat(HtmlPageFormData.GetHtml(TagHtmlForm.labelDate), HtmlPageFormData.GetHtml(TagHtmlForm.inputDate));
            var labelDate = GetLabelDate();
            var inputDate = GetInputBirthDay();
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<div class=\"Birthday\">",
                TagEnd = "</div>",
                Children = new List<TagModel>() { labelDate, inputDate }
            };
            return divTag;
        }

        private static TagModel GetLabelDate()
        {
            string content = "Birthday";
            var labelGender = new TagModel()
            {
                NameTag = NameTagEnum.label,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<label for=\"dBirthday\">",
                TagEnd = "</label>"
            };
            return labelGender;
        }

        private static TagModel GetInputBirthDay()
        {
            var inputBirthDay = new TagModel()
            {
                NameTag = NameTagEnum.input,
                TagFamily = TagFamilyEnum.NoEnd,
                Content = string.Empty,
                TagStart = "<input type=\"date\" id=\"dBirthday\" name=\"birthday\" value=\"1992-07-22\" min=\"1918-01-01\" max=\"2025-12-31\" /> "
            };
            return inputBirthDay;
        }

        private static TagModel GetDivResume()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Resume" };
            string content = string.Concat(HtmlPageFormData.GetHtml(TagHtmlForm.labelResume), HtmlPageFormData.GetHtml(TagHtmlForm.inputFile));
            var labelResume = GetLabelResume();
            var inputResume = GetInputResume();
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<div class=\"Resume\">",
                TagEnd = "</div>",
                Children = new List<TagModel>() { labelResume, inputResume }
            };
            return divTag;
        }

        private static TagModel GetLabelResume()
        {
            string content = "Choose a resume";
            var labelResume = new TagModel()
            {
                NameTag = NameTagEnum.label,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<label for=\"dResume\">",
                TagEnd = "</label>"
            };
            return labelResume;
        }

        private static TagModel GetInputResume()
        {
            var inputResume = new TagModel()
            {
                NameTag = NameTagEnum.input,
                TagFamily = TagFamilyEnum.NoEndIncomplete,
                Content = string.Empty,
                TagStart = "<input type=\"file\" id=\"avatar\" name=\"avatar\" accept=\".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document\"> "
            };
            return inputResume;
        }

        private static TagModel GetDivSend()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Send" };
            string content = HtmlPageFormData.GetHtml(TagHtmlForm.inputSubmit);
            var inputSubmit = GetInputSubmit();
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<div class=\"Send\">",
                TagEnd = "</div>",
                Children = new List<TagModel>() { inputSubmit }
            };
            return divTag;
        }

        private static TagModel GetInputSubmit()
        {
            var inputSubmit = new TagModel()
            {
                NameTag = NameTagEnum.input,
                TagFamily = TagFamilyEnum.NoEnd,
                Content = string.Empty,
                TagStart = "<input type=\"Submit\" value=\"Submit\"/> "
            };
            return inputSubmit;
        }

    }
}