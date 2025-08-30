using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Configuration;
using Newtonsoft.Json;

namespace FalconEngineTest.Utils.HtmlData
{
    public enum TagHtmlTable
    {
        htmlFormWithDoctype, htmlForm, head, linkHead, titleDocument, metaViewPort, metaCharset, body, navTag, ulMenu, liOrganisation,
        liNews, liHome, articleTag, sectionTag, pNews, h1News, mainTag, postTable, thead, trhead, firstTrHead, secondTrHead, thirdTrHead,
        fourthTrHead, levelTable, typeTable, descriptionTable, titleTable, tbody, developerTable, developerLabel, developerDescription,
        developerType, developerLevel, productOwnerTable, productOwnerLabel, productOwnerDescription, productOwnerType, productOwnerLevel,
        technicalLeaderTable, technicalLeaderLabel, technicalLeaderDescription, technicalLeaderType, technicalLeaderLevel, managerTable,
        managerLabel, managerDescription, managerType, managerLevel, architectTable, architectLabel, architectDescription, architectType, architectLevel,
        directorTable, directorLabel, directorDescription, directorType, directorLevel, scriptTable
    }

    public class HtmlPageTableData
    {
        public static string? GetHtml(TagHtmlTable tag)
        {
            var json = GetData();
            switch (tag)
            {
                case TagHtmlTable.htmlFormWithDoctype:
                    return GetHtmlFormWithDoctype(json);
                case TagHtmlTable.htmlForm:
                    return GetHtmlForm(json);
                case TagHtmlTable.head:
                    return GetHead(json);
                case TagHtmlTable.linkHead:
                    return GetLinkHead(json);
                case TagHtmlTable.titleDocument:
                    return GetTitleDocument(json);
                case TagHtmlTable.metaViewPort:
                    return GetMetaViewPort(json);
                case TagHtmlTable.metaCharset:
                    return GetMetaCharset(json);
                case TagHtmlTable.body:
                    return GetBody(json);
                case TagHtmlTable.navTag:
                    return GetNavTag(json);
                case TagHtmlTable.ulMenu:
                    return GetUlMenu(json);
                case TagHtmlTable.liOrganisation:
                    return GetLiOrganisation(json);
                case TagHtmlTable.liNews:
                    return GetLiNews(json);
                case TagHtmlTable.liHome:
                    return GetLiHome(json);
                case TagHtmlTable.articleTag:
                    return GetArticleTag(json);
                case TagHtmlTable.sectionTag:
                    return GetSectionTag(json);
                case TagHtmlTable.pNews:
                    return GetPNews(json);
                case TagHtmlTable.h1News:
                    return GetH1News(json);
                case TagHtmlTable.mainTag:
                    return GetMainTag(json);
                case TagHtmlTable.postTable:
                    return GetPostTable(json);
                case TagHtmlTable.thead:
                    return GetThead(json);
                case TagHtmlTable.trhead:
                    return GetTrhead(json);
                case TagHtmlTable.levelTable:
                    return GetLevelTable(json);
                case TagHtmlTable.typeTable:
                    return GetTypeTable(json);
                case TagHtmlTable.descriptionTable:
                    return GetDescriptionTable(json);
                case TagHtmlTable.titleTable:
                    return GetTitleTable(json);
                case TagHtmlTable.tbody:
                    return GetTbody(json);
                case TagHtmlTable.developerTable:
                    return GetDeveloperTable(json);
                case TagHtmlTable.developerLabel:
                    return GetDeveloperLabel(json);
                case TagHtmlTable.developerDescription:
                    return GetDeveloperDescription(json);
                case TagHtmlTable.developerType:
                    return GetDeveloperType(json);
                case TagHtmlTable.developerLevel:
                    return GetDeveloperLevel(json);
                case TagHtmlTable.productOwnerTable:
                    return GetProductOwnerTable(json);
                case TagHtmlTable.productOwnerLabel:
                    return GetProductOwnerLabel(json);
                case TagHtmlTable.productOwnerDescription:
                    return GetProductOwnerDescription(json);
                case TagHtmlTable.productOwnerType:
                    return GetProductOwnerType(json);
                case TagHtmlTable.productOwnerLevel:
                    return GetProductOwnerLevel(json);
                case TagHtmlTable.technicalLeaderTable:
                    return GetTechnicalLeaderTable(json);
                case TagHtmlTable.technicalLeaderLabel:
                    return GetTechnicalLeaderLabel(json);
                case TagHtmlTable.technicalLeaderDescription:
                    return GetTechnicalLeaderDescription(json);
                case TagHtmlTable.technicalLeaderType:
                    return GetTechnicalLeaderType(json);
                case TagHtmlTable.technicalLeaderLevel:
                    return GetTechnicalLeaderLevel(json);
                case TagHtmlTable.managerTable:
                    return GetManagerTable(json);
                case TagHtmlTable.managerLabel:
                    return GetManagerLabel(json);
                case TagHtmlTable.managerDescription:
                    return GetManagerDescription(json);
                case TagHtmlTable.managerType:
                    return GetManagerType(json);
                case TagHtmlTable.managerLevel:
                    return GetManagerLevel(json);
                case TagHtmlTable.architectTable:
                    return GetArchitectTable(json);
                case TagHtmlTable.architectLabel:
                    return GetArchitectLabel(json);
                case TagHtmlTable.architectDescription:
                    return GetArchitectDescription(json);
                case TagHtmlTable.architectType:
                    return GetArchitectType(json);
                case TagHtmlTable.architectLevel:
                    return GetArchitectLevel(json);
                case TagHtmlTable.directorTable:
                    return GetDirectorTable(json);
                case TagHtmlTable.directorLabel:
                    return GetDirectorLabel(json);
                case TagHtmlTable.directorDescription:
                    return GetDirectorDescription(json);
                case TagHtmlTable.directorType:
                    return GetDirectorType(json);
                case TagHtmlTable.directorLevel:
                    return GetDirectorLevel(json);
                case TagHtmlTable.scriptTable:
                    return GetScriptTable(json);
                default:
                    throw new Exception("Unknown Tag");
            }
        }

        private static string? GetScriptTable(JsonTableDataModel? json)
        {
            return json?.ScriptTable;
        }

        private static string? GetDirectorLevel(JsonTableDataModel? json)
        {
            return json?.DirectorLevel;
        }

        private static string? GetDirectorType(JsonTableDataModel? json)
        {
            return json?.DirectorType;
        }

        private static string? GetDirectorDescription(JsonTableDataModel? json)
        {
            return json?.DirectorDescription;
        }

        private static string? GetDirectorLabel(JsonTableDataModel? json)
        {
            return json?.DirectorLabel;
        }

        private static string? GetDirectorTable(JsonTableDataModel? json)
        {
            return json?.DirectorTable;
        }

        private static string? GetArchitectLevel(JsonTableDataModel? json)
        {
            return json?.ArchitectLevel;
        }

        private static string? GetArchitectType(JsonTableDataModel? json)
        {
            return json?.ArchitectType;
        }

        private static string? GetArchitectDescription(JsonTableDataModel? json)
        {
            return json?.ArchitectDescription;
        }

        private static string? GetArchitectLabel(JsonTableDataModel? json)
        {
            return json?.ArchitectLabel;
        }

        private static string? GetArchitectTable(JsonTableDataModel? json)
        {
            return json?.ArchitectTable;
        }

        private static string? GetManagerLevel(JsonTableDataModel? json)
        {
            return json?.ManagerLevel;
        }

        private static string? GetManagerType(JsonTableDataModel? json)
        {
            return json?.ManagerType;
        }

        private static string? GetManagerDescription(JsonTableDataModel? json)
        {
            return json?.ManagerDescription;
        }

        private static string? GetManagerLabel(JsonTableDataModel? json)
        {
            return json?.ManagerLabel;
        }

        private static string? GetManagerTable(JsonTableDataModel? json)
        {
            return json?.ManagerTable;
        }

        private static string? GetTechnicalLeaderLevel(JsonTableDataModel? json)
        {
            return json?.TechnicalLeaderLevel;
        }

        private static string? GetTechnicalLeaderType(JsonTableDataModel? json)
        {
            return json?.TechnicalLeaderType;
        }

        private static string? GetTechnicalLeaderDescription(JsonTableDataModel? json)
        {
            return json?.TechnicalLeaderDescription;
        }

        private static string? GetTechnicalLeaderLabel(JsonTableDataModel? json)
        {
            return json?.TechnicalLeaderLabel;
        }

        private static string? GetTechnicalLeaderTable(JsonTableDataModel? json)
        {
            return json?.TechnicalLeaderTable;
        }

        private static string? GetProductOwnerLevel(JsonTableDataModel? json)
        {
            return json?.ProductOwnerLevel;
        }

        private static string? GetProductOwnerType(JsonTableDataModel? json)
        {
            return json?.ProductOwnerType;
        }

        private static string? GetProductOwnerDescription(JsonTableDataModel? json)
        {
            return json?.ProductOwnerDescription;
        }

        private static string? GetProductOwnerLabel(JsonTableDataModel? json)
        {
            return json?.ProductOwnerLabel;
        }

        private static string? GetProductOwnerTable(JsonTableDataModel? json)
        {
            return json?.ProductOwnerTable;
        }

        private static string? GetDeveloperLevel(JsonTableDataModel? json)
        {
            return json?.DeveloperLevel;
        }

        private static string? GetDeveloperType(JsonTableDataModel? json)
        {
            return json?.DeveloperType;
        }

        private static string? GetDeveloperDescription(JsonTableDataModel? json)
        {
            return json?.DeveloperDescription;
        }

        private static string? GetDeveloperLabel(JsonTableDataModel? json)
        {
            return json?.DeveloperLabel;
        }

        private static string? GetDeveloperTable(JsonTableDataModel? json)
        {
            return json?.DeveloperTable;
        }

        private static string? GetTbody(JsonTableDataModel? json)
        {
            return json?.Tbody;
        }

        private static string? GetTitleTable(JsonTableDataModel? json)
        {
            return json?.TitleTable;
        }

        private static string? GetDescriptionTable(JsonTableDataModel? json)
        {
            return json?.DescriptionTable;
        }

        private static string? GetTypeTable(JsonTableDataModel? json)
        {
            return json?.TypeTable;
        }

        private static string? GetLevelTable(JsonTableDataModel? json)
        {
            return json?.LevelTable;
        }

        private static string? GetThead(JsonTableDataModel? json)
        {
            return json?.Thead;
        }

        private static string? GetTrhead(JsonTableDataModel? json)
        {
            return json?.Trthead;
        }

        private static string? GetPostTable(JsonTableDataModel? json)
        {
            return json?.PostTable;
        }

        private static string? GetMainTag(JsonTableDataModel? json)
        {
            return json?.MainTag;
        }

        private static string? GetH1News(JsonTableDataModel? json)
        {
            return json?.H1News;
        }

        private static string? GetPNews(JsonTableDataModel? json)
        {
            return json?.PNews;
        }

        private static string? GetArticleTag(JsonTableDataModel? json)
        {
            return json?.ArticleTag;
        }

        private static string? GetSectionTag(JsonTableDataModel? json)
        {
            return json?.SectionTag;
        }

        private static string? GetLiHome(JsonTableDataModel? json)
        {
            return json?.LiHome;
        }

        private static string? GetLiNews(JsonTableDataModel? json)
        {
            return json?.LiNews;
        }

        private static string? GetLiOrganisation(JsonTableDataModel? json)
        {
            return json?.LiOrganisation;
        }

        private static string? GetUlMenu(JsonTableDataModel? json)
        {
            return json?.UlMenu;
        }

        private static string? GetNavTag(JsonTableDataModel? json)
        {
            return json?.NavTag;
        }

        private static string? GetBody(JsonTableDataModel? json)
        {
            return json?.Body;
        }

        private static string? GetMetaCharset(JsonTableDataModel? json)
        {
            return json?.MetaCharset;
        }

        private static string? GetMetaViewPort(JsonTableDataModel? json)
        {
            return json?.MetaViewPort;
        }

        private static string? GetTitleDocument(JsonTableDataModel? json)
        {
            return json?.TitleDocument;
        }

        private static string? GetLinkHead(JsonTableDataModel? json)
        {
            return json?.LinkHead;
        }

        private static string? GetHead(JsonTableDataModel? json)
        {
            return json?.Head;
        }

        private static string? GetHtmlForm(JsonTableDataModel? json)
        {
            return json?.HtmlForm;
        }

        private static string? GetHtmlFormWithDoctype(JsonTableDataModel? json)
        {
            return json?.HtmlFormWithDoctype;
        }

        private static JsonTableDataModel? GetData()
        {
            string dataJson = HtmlFormDataJson.AllDataJson;
            var data = JsonConvert.DeserializeObject<JsonTableDataModel>(dataJson);
            return data;
        }
    }
}