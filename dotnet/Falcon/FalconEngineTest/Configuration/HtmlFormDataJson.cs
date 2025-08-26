using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngineTest.Configuration
{
    public class HtmlFormDataJson
    {
        public static string AllDataJson = """
        {
            "InputSubmit":"<input type=\"Submit\" value=\"Submit\"/>",
            "DivSubmit":"<div class=\"Send\">{InputSubmit}</div>",
            "InputFile":"<input type=\"file\" id=\"avatar\" name=\"avatar\" accept=\".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document\">",
            "LabelResume":"<label for=\"dResume\">Choose a resume</label>",
            "DivResume":"<div class=\"Resume\">{InputFile}{LabelResume}</div>",
            "InputDate":"<input type=\"date\" id=\"dBirthday\" name=\"birthday\" value=\"1992-07-22\" min=\"1918-01-01\" max=\"2025-12-31\" />",
            "LabelDate":"<label for=\"dBirthday\">Birthday</label>",
            "DivDate":"<div class=\"Birthday\">{LabelDate}{InputDate}</div>",
            "RadioMale":"<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"male\"/>",
            "LabelMale":" <label for=\"male\">Male</label>",
            "RadioFemale":"<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"female\"/>",
            "LabelFemale":" <label for=\"female\">Female</label>",
            "RadioUndefined":"<input type=\"radio\" id=\"rgender\" name=\"gender\" value=\"undefined\" checked/>",
            "LabelUndefined":" <label for=\"undefined\">Undefined</label>",
            "LabelGender":"<label for=\"rgender\">Gender</label>",
            "DivGender":"<div class=\"Gender\">{LabelGender}{RadioMale}{LabelMale}{RadioFemale}{LabelFemale}{RadioUndefined}{LabelUndefined}</div>",
            "LabelSituation":"<label for=\"lSituation\">Situation</label>",
            "SelectSituation":"<select name=\"sSituation\" id=\"sSituation\"><option>No Job</option><option>Job in a company</option><option>Entrepreneur</option></select>",
            "DivSituation":"<div class=\"Situation\">{LabelSituation}{SelectSituation}</div>",
            "InputFirstName":"<input type=\"text\" placeholder=\"FirstName\">",
            "InputLastName":"<input type=\"text\" placeholder=\"LastName\">",
            "DivIdentity":"<div class=\"Identity\">{InputFirstName}{InputLastName}</div>",
            "H1Title":"<h1>Present your candidature</h1>",
            "DivH1":"<div class=\"Title\">{H1Title}</div>",
            "Form":"<form method=\"POST\" action=\"/candidate\">{DivH1}{DivIdentity}{DivGender}{DivSituation}{DivDate}{DivResume}{DivSubmit}</form>",
            "Body":"<body>{Form}</body>",
            "MetaCharset":"<meta charset=\"UTF-8\">",
            "MetaViewPort":"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">",
            "TitleDocument":"<title>Document</title>",
            "LinkHead":"<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">",
            "Head":"<head>{MetaCharset}{MetaViewPort}{TitleDocument}{LinkHead}</head>",
            "HtmlForm":"<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">{Head}{Body}</html>",                  
            "HtmlFormWithDoctype":"<!DOCTYPE html>{HtmlForm}"
        }
        """;
    }
    public class JsonFormDataModel
    {
        public string? InputSubmit { get; set; }
        public string? DivSubmit { get; set; }
        public string? InputFile { get; set; }
        public string? LabelResume { get; set; }
        public string? DivResume { get; set; }
        public string? InputDate { get; set; }
        public string? LabelDate { get; set; }
        public string? DivDate { get; set; }
        public string? RadioMale { get; set; }
        public string? LabelMale { get; set; }
        public string? RadioFemale { get; set; }
        public string? LabelFemale { get; set; }
        public string? LabelGender { get; set; }
        public string? RadioUndefined { get; set; }
        public string? LabelUndefined { get; set; }
        public string? DivGender { get; set; }
        public string? LabelSituation { get; set; }
        public string? SelectSituation { get; set; }
        public string? DivSituation { get; set; }
        public string? InputFirstName { get; set; }
        public string? InputLastName { get; set; }
        public string? DivIdentity { get; set; }
        public string? H1Title { get; set; }
        public string? DivH1 { get; set; }
        public string? Form { get; set; }
        public string? Body { get; set; }
        public string? MetaCharset { get; set; }
        public string? MetaViewPort { get; set; }
        public string? TitleDocument { get; set; }
        public string? LinkHead { get; set; }
        public string? Head { get; set; }
        public string? HtmlForm { get; set; }
        public string? HtmlFormWithDoctype { get; set; }
    }
}