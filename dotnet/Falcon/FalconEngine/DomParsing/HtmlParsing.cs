using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class HtmlParsing : IHtmlParsing
    {
        private ITagParser _doctypeParse;
        private ITagParser _htmlParse;
        //temporary start 
        private ITagParser _inputParser;
        private ITagParser _labelParser;
        private ITagParser _selectParser;
        private ITagParser _h1Parser;
        //temporary end
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;
        private string _html;
        private bool _isValidDoctype;
        private bool _isValidHtmlTag;
        private bool _isValidPage;

        public HtmlParsing(ITagParser doctypeParse, ITagParser htmlParse, ITagParser inputParser, ITagParser labelParser,
                            ITagParser selectParser, ITagParser h1Parser, IExtractHtmlRemaining extractHtmlRemaining, IAttributeTagManager attributeTagManager)
        {
            _doctypeParse = doctypeParse;
            _htmlParse = htmlParse;
            _inputParser = inputParser;
            _labelParser = labelParser;
            _selectParser = selectParser;
            _h1Parser = h1Parser;
            _extractHtmlRemaining = extractHtmlRemaining;
            _attributeTagManager = attributeTagManager;
        }

        public HtmlPage Parse(string html, bool isSimulating)
        {
            _html = html;
            _attributeTagManager.SetAttributes();
            var doctypeTag = GetDoctypeTag();
            RemoveUselessHtml(doctypeTag);
            if (!isSimulating)
            {
                var htmlTag = GetTagHtml();
                RemoveTagOpenCloed(htmlTag);
                DeterminateIsValidPage();
                var tags = new List<TagModel>() { doctypeTag, htmlTag };
                var htmlPage = new HtmlPage() { Tags = tags, IsValid = _isValidPage };
                return htmlPage;
            }
            else
            {
                var htmlTag = GetHtmlSimulate();
                var tags = new List<TagModel>() { doctypeTag, htmlTag };
                var htmlPage = new HtmlPage() { Tags = tags, IsValid = true };
                return htmlPage;
            }
        }

        private TagModel GetDoctypeTag()
        {
            var doctypeTag = _doctypeParse.Parse(_html);
            _isValidDoctype = _doctypeParse.IsValid();
            return doctypeTag;
        }

        private TagModel GetTagHtml()
        {
            var htmlTag = _htmlParse.Parse(_html);
            _isValidHtmlTag = _htmlParse.IsValid();
            return htmlTag;
        }

        private void RemoveUselessHtml(TagModel tag)
        {
            _html = _extractHtmlRemaining.Extract(tag, _html, ExtractionMode.ASide);
        }

        private void RemoveTagOpenCloed(TagModel tag)
        {
            _html = _extractHtmlRemaining.Extract(tag, _html, ExtractionMode.Inside);
        }

        private void DeterminateIsValidPage()
        {
            _isValidPage = _isValidDoctype && _isValidHtmlTag;
        }

        //TODO when formpage is developed delete all this methods below
        private TagModel GetHtmlSimulate()
        {
            var attributLang = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.lang.ToString(), Value = "en" };
            var attributDir = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.dir.ToString(), Value = "auto" };
            var attributXmlns = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.xmlns.ToString(), Value = "http://www.w3.org/1999/xhtml" };
            var headTag = GetHeadTag();
            var body = GetBodyTag();
            var htmlTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributLang, attributDir, attributXmlns },
                NameTag = NameTagEnum.html,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "<head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\"></head><body><form method=\"POST\" action=\"/candidate\"><div class=\"Title\"><h1>Present your candidature</h1></div><div class=\"Identity\"><input type=\"text\" placeholder=\"FirstName\"><input type=\"text\" placeholder=\"LastName\"></div><div class=\"Gender\"><label for=\"rgender\">Gender</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"male\"/> <label for=\"male\">Male</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"female\"/> <label for=\"female\">female</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"undefined\" checked/> <label for=\"undefined\">Undefined</label></div><div class=\"Situation\"><label for=\"lSituation\">Situation</label><select name=\"sSituation\" id=\"sSituation\"><option>No Job</option><option>Job in a company</option><option>Entrepreneur</option></select></div><div class=\"Birthday\"><label for=\"dBirthday\">Birthday</label><input type=\"date\" id=\"dBirthday\" name=\"birthday\" value=\"1992-07-22\" min=\"1918-01-01\" max=\"2025-12-31\" /></div><div class=\"Resume\"><input type=\"file\" id=\"avatar\" name=\"avatar\" accept=\".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document\"><label for=\"dResume\">Choose a resume</label></div><div class=\"Send\"><input type=\"Submit\" value=\"Submit\"/></div></form></body>",
                TagStart = "<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">",
                TagEnd = "</html>",
                Children = new List<TagModel>() { headTag, body }
            };
            return htmlTag;
        }

        private TagModel GetHeadTag()
        {
            var metaCharsetTag = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.charset.ToString(), Value = "UTF-8" } },
                NameTag = NameTagEnum.meta,
                TagStart = "<meta charset=\"UTF-8\">"
            };
            var metaViewPort = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.name.ToString(), Value = "viewport" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.content.ToString(), Value = "width=device-width, initial-scale=1.0" }
                },
                NameTag = NameTagEnum.meta,
                TagStart = "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
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
                Content = "<meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">",
                Children = new List<TagModel>() { metaCharsetTag, metaViewPort, title, link },
                TagStart = "<head>",
                TagEnd = "</head>"
            };
            return headTag;
        }

        private TagModel GetBodyTag()
        {
            string content = "<form method=\"POST\" action=\"/candidate\"><div class=\"Title\"><h1>Present your candidature</h1></div><div class=\"Identity\"><input type=\"text\" placeholder=\"FirstName\"><input type=\"text\" placeholder=\"LastName\"></div><div class=\"Gender\"><label for=\"rgender\">Gender</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"male\"/> <label for=\"male\">Male</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"female\"/> <label for=\"female\">female</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"undefined\" checked/> <label for=\"undefined\">Undefined</label></div><div class=\"Situation\"><label for=\"lSituation\">Situation</label><select name=\"sSituation\" id=\"sSituation\"><option>No Job</option><option>Job in a company</option><option>Entrepreneur</option></select></div><div class=\"Birthday\"><label for=\"dBirthday\">Birthday</label><input type=\"date\" id=\"dBirthday\" name=\"birthday\" value=\"1992-07-22\" min=\"1918-01-01\" max=\"2025-12-31\" /></div><div class=\"Resume\"><input type=\"file\" id=\"avatar\" name=\"avatar\" accept=\".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document\"><label for=\"dResume\">Choose a resume</label></div><div class=\"Send\"><input type=\"Submit\" value=\"Submit\"/></div></form>";
            var formPost = GetFormPost();
            var bodyTag = new TagModel()
            {
                NameTag = NameTagEnum.body,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { formPost },
                TagStart = "<body>",
                TagEnd = "</body>"
            };
            return bodyTag;
        }

        private TagModel GetFormPost()
        {
            var attributMethod = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.method.ToString(), Value = "POST" };
            var attributAction = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.action.ToString(), Value = "/candidate" };
            string content = "<div class=\"Title\"><h1>Present your candidature</h1></div><div class=\"Identity\"><input type=\"text\" placeholder=\"FirstName\"><input type=\"text\" placeholder=\"LastName\"></div><div class=\"Gender\"><label for=\"rgender\">Gender</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"male\"/> <label for=\"male\">Male</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"female\"/> <label for=\"female\">female</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"undefined\" checked/> <label for=\"undefined\">Undefined</label></div><div class=\"Situation\"><label for=\"lSituation\">Situation</label><select name=\"sSituation\" id=\"sSituation\"><option>No Job</option><option>Job in a company</option><option>Entrepreneur</option></select></div><div class=\"Birthday\"><label for=\"dBirthday\">Birthday</label><input type=\"date\" id=\"dBirthday\" name=\"birthday\" value=\"1992-07-22\" min=\"1918-01-01\" max=\"2025-12-31\" /></div><div class=\"Resume\"><input type=\"file\" id=\"avatar\" name=\"avatar\" accept=\".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document\"><label for=\"dResume\">Choose a resume</label></div><div class=\"Send\"><input type=\"Submit\" value=\"Submit\"/></div>";
            var divTitle = GetDivTitle();
            var divIdentity = GetDivIdentity();
            var divGender = GetDivGender();
            var divSituation = GetDivSituation();
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
                Children = new List<TagModel>() { divTitle, divIdentity, divGender, divSituation, divBirthday, divResume, divSend }
            };
            return divTag;
        }

        private TagModel GetDivTitle()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Title" };
            string content = "<h1>Present your candidature</h1>";
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

        private TagModel GetH1Title()
        {
            string html = "<h1>Present your candidature</h1>";
            var tag = _h1Parser.Parse(html);
            return tag;
        }

        private TagModel GetDivIdentity()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Identity" };
            string content = "<input type=\"text\" placeholder=\"FirstName\"><input type=\"text\" placeholder=\"LastName\">";
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

        private TagModel GetInputFirstName()
        {
            string html = "<input type=\"text\" placeholder=\"FirstName\">";
            var inputFirstName = _inputParser.Parse(html);
            return inputFirstName;
        }

        private TagModel GetInputLastName()
        {
            string html = "<input type=\"text\" placeholder=\"LastName\">";
            var inputLastName = _inputParser.Parse(html);
            return inputLastName;
        }

        private TagModel GetDivGender()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Gender" };
            string content = "<label for=\"rgender\">Gender</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"male\"/> <label for=\"male\">Male</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"female\"/> <label for=\"female\">female</label><input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"undefined\" checked/> <label for=\"undefined\">Undefined</label>";
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

        private TagModel GetLabelGender()
        {
            string html = "<label for=\"rgender\">Gender</label>";
            var tag = _labelParser.Parse(html);
            return tag;
        }

        private TagModel GetRadioMale()
        {
            string html = "<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"male\"/>";
            var radioMale = _inputParser.Parse(html);
            return radioMale;
        }

        private TagModel GetLabelMale()
        {
            string html = "<label for=\"rgender\">Male</label>";
            var tag = _labelParser.Parse(html);
            return tag;
        }

        private TagModel GetRadioFemale()
        {
            string html = "<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"female\"/>";
            var radioFemale = _inputParser.Parse(html);
            return radioFemale;
        }

        private TagModel GetLabelFemale()
        {
            string html = "<label for=\"rgender\">Female</label>";
            var tag = _labelParser.Parse(html);
            return tag;
        }

        private TagModel GetRadioUndefined()
        {
            string html = "<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"undefined\" checked/>";
            var radioUndefined = _inputParser.Parse(html);
            return radioUndefined;
        }

        private TagModel GetLabelUndefined()
        {
            string html = "<label for=\"rgender\">Undefined</label>";
            var tag = _labelParser.Parse(html);
            return tag;
        }

        private TagModel GetDivSituation()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Situation" };
            string content = "<label for=\"lSituation\">Situation</label><select name=\"sSituation\" id=\"sSituation\"><option>No Job</option><option>Job in a company</option><option>Entrepreneur</option></select>";
            var labelSituation = GetLabelSituation();
            var selectSituation = GetSelectSituation();
            var divTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributId },
                NameTag = NameTagEnum.div,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<div class=\"Situation\">",
                TagEnd = "</div>",
                Children = new List<TagModel>() { labelSituation, selectSituation }
            };
            return divTag;
        }

        private TagModel GetLabelSituation()
        {
            string html = "<label for=\"lSituation\">Situation</label>";
            var tag = _labelParser.Parse(html);
            return tag;
        }

        private TagModel GetSelectSituation()
        {
            string html = "<select name=\"sSituation\" id=\"sSituation\"><option>No Job</option><option>Job in a company</option><option>Entrepreneur</option></select>";
            var selectSelection = _selectParser.Parse(html);
            return selectSelection;
        }

        private TagModel GetDivBirthDay()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Birthday" };
            string content = "<label for=\"dBirthday\">Birthday</label><input type=\"date\" id=\"dBirthday\" name=\"birthday\" value=\"1992-07-22\" min=\"1918-01-01\" max=\"2025-12-31\" />";
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

        private TagModel GetLabelDate()
        {
            string html = "<label for=\"dBirthday\">Birthday</label>";
            var tag = _labelParser.Parse(html);
            return tag;
        }

        private TagModel GetInputBirthDay()
        {
            string html = "<input type=\"date\" id=\"dBirthday\" name=\"birthday\" value=\"1992-07-22\" min=\"1918-01-01\" max=\"2025-12-31\" />";
            var inputBirthDay = _inputParser.Parse(html);
            return inputBirthDay;
        }

        private TagModel GetDivResume()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Resume" };
            string content = "<label for=\"dResume\">Choose a resume</label><input type=\"file\" id=\"avatar\" name=\"avatar\" accept=\".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document\">";
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

        private TagModel GetLabelResume()
        {
            string html = "<label for=\"dResume\">Choose a resume</label>";
            var tag = _labelParser.Parse(html);
            return tag;
        }

        private TagModel GetInputResume()
        {
            string html = "<input type=\"file\" id=\"avatar\" name=\"avatar\" accept=\".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document\">";
            var inputResume = _inputParser.Parse(html);
            return inputResume;
        }

        private TagModel GetDivSend()
        {
            var attributId = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.classCss.ToString(), Value = "Send" };
            string content = "<input type=\"Submit\" value=\"Submit\"/>";
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

        private TagModel GetInputSubmit()
        {
            string html = "<input type=\"Submit\" value=\"Submit\"/> ";
            var inputSubmit = _inputParser.Parse(html);
            return inputSubmit;
        }

    }
}