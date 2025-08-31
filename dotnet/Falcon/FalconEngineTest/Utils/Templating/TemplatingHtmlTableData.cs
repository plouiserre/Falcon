using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;

namespace FalconEngineTest.Utils.Templating
{
    public class TemplatingHtmlTableData
    {
        public static string GetHtmlFormData(string html, JsonTableDataModel jsonModel)
        {
            string htmlResult = string.Empty;
            string htmlWorking = html;
            do
            {
                htmlResult = ManageHtmlTemplate(htmlWorking, jsonModel);
                htmlWorking = htmlResult;
            } while (IsTemplatingPresent(htmlWorking));

            return htmlResult;
        }

        private static string ManageHtmlTemplate(string html, JsonTableDataModel jsonModel)
        {
            if (!IsTemplatingPresent(html))
                return html;
            string templateTag = GetTemplateName(html);
            string template = templateTag.Replace("{", string.Empty).Replace("}", string.Empty);
            string valueHtml = GetValue(template, jsonModel);
            string result = html.Replace(templateTag, valueHtml);
            return result;
        }

        private static bool IsTemplatingPresent(string html)
        {
            if (html.Contains("{") && html.Contains("}"))
                return true;
            else
                return false;
        }

        private static string GetTemplateName(string html)
        {
            int firstPartTemplate = html.IndexOf('{');
            int secondPartTemplate = html.IndexOf('}');
            return html.Substring(firstPartTemplate, secondPartTemplate - firstPartTemplate + 1);
        }

        private static string? GetValue(string template, JsonTableDataModel jsonModel)
        {
            switch (template)
            {
                case "ScriptTable":
                    return jsonModel.ScriptTable;
                case "DirectorLevel":
                    return jsonModel.DirectorLevel;
                case "DirectorType":
                    return jsonModel.DirectorType;
                case "DirectorDescription":
                    return jsonModel.DirectorDescription;
                case "DirectorLabel":
                    return jsonModel.DirectorLabel;
                case "DirectorTable":
                    return jsonModel.DirectorTable;
                case "ArchitectLevel":
                    return jsonModel.ArchitectLevel;
                case "ArchitectType":
                    return jsonModel.ArchitectType;
                case "ArchitectDescription":
                    return jsonModel.ArchitectDescription;
                case "ArchitectLabel":
                    return jsonModel.ArchitectLabel;
                case "ArchitectTable":
                    return jsonModel.ArchitectTable;
                case "ManagerLevel":
                    return jsonModel.ManagerLevel;
                case "ManagerType":
                    return jsonModel.ManagerType;
                case "ManagerDescription":
                    return jsonModel.ManagerDescription;
                case "ManagerLabel":
                    return jsonModel.ManagerLabel;
                case "ManagerTable":
                    return jsonModel.ManagerTable;
                case "TechnicalLeaderLevel":
                    return jsonModel.TechnicalLeaderLevel;
                case "TechnicalLeaderType":
                    return jsonModel.TechnicalLeaderType;
                case "TechnicalLeaderDescription":
                    return jsonModel.TechnicalLeaderDescription;
                case "TechnicalLeaderLabel":
                    return jsonModel.TechnicalLeaderLabel;
                case "TechnicalLeaderTable":
                    return jsonModel.TechnicalLeaderTable;
                case "ProductOwnerLevel":
                    return jsonModel.ProductOwnerLevel;
                case "ProductOwnerType":
                    return jsonModel.ProductOwnerType;
                case "ProductOwnerDescription":
                    return jsonModel.ProductOwnerDescription;
                case "ProductOwnerLabel":
                    return jsonModel.ProductOwnerLabel;
                case "ProductOwnerTable":
                    return jsonModel.ProductOwnerTable;
                case "DeveloperLevel":
                    return jsonModel.DeveloperLevel;
                case "DeveloperType":
                    return jsonModel.DeveloperType;
                case "DeveloperDescription":
                    return jsonModel.DeveloperDescription;
                case "DeveloperLabel":
                    return jsonModel.DeveloperLabel;
                case "DeveloperTable":
                    return jsonModel.DeveloperTable;
                case "Tbody":
                    return jsonModel.Tbody;
                case "TitleTable":
                    return jsonModel.TitleTable;
                case "DescriptionTable":
                    return jsonModel.DescriptionTable;
                case "TypeTable":
                    return jsonModel.TypeTable;
                case "LevelTable":
                    return jsonModel.LevelTable;
                case "Thead":
                    return jsonModel.Thead;
                case "Trthead":
                    return jsonModel.Trthead;
                case "PostTable":
                    return jsonModel.PostTable;
                case "MainTag":
                    return jsonModel.MainTag;
                case "H1News":
                    return jsonModel.H1News;
                case "PNews":
                    return jsonModel.PNews;
                case "ArticleTag":
                    return jsonModel.ArticleTag;
                case "SectionTag":
                    return jsonModel.SectionTag;
                case "LiHome":
                    return jsonModel.LiHome;
                case "LiNews":
                    return jsonModel.LiNews;
                case "LiOrganisation":
                    return jsonModel.LiOrganisation;
                case "UlMenu":
                    return jsonModel.UlMenu;
                case "NavTag":
                    return jsonModel.NavTag;
                case "Body":
                    return jsonModel.Body;
                case "MetaCharset":
                    return jsonModel.MetaCharset;
                case "MetaViewPort":
                    return jsonModel.MetaViewPort;
                case "TitleDocument":
                    return jsonModel.TitleDocument;
                case "LinkHead":
                    return jsonModel.LinkHead;
                case "Head":
                    return jsonModel.Head;
                case "HtmlTable":
                    return jsonModel.HtmlTable;
                case "HtmlTableWithDoctype":
                    return jsonModel.HtmlTableWithDoctype;
                default:
                    throw new Exception("Unknown template");
            }
        }
    }
}