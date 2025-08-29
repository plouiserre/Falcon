using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngineTest.Configuration
{
    public static class HtmlTableDataJson
    {
        public static string? AllDataJson = """
        {
            "ScriptTable":"<script src=\"javascript.js\"></script>",
            "DirectorLevel":"<td>3</td>",
            "DirectorType":"<td>Management</td>",
            "DirectorDescription":"<td>Manager of a departement</td>",
            "DirectorLabel":"<td>Director</td>",
            "DirectorTable":"<tr>{DirectorLabel}{DirectorDescription}{DirectorType}{DirectorLevel}</tr>",
            "ArchitectLevel":"<td>3</td>",
            "ArchitectType":"<td>Technical</td>",
            "ArchitectDescription":"<td>Responsible of the quality and the durability of the tech</td>",
            "ArchitectLabel":"<td>Architect</td>",
            "ArchitectTable":"<tr>{ArchitectLabel}{ArchitectDescription}{ArchitectType}{ArchitectLevel}</tr>",
            "ManagerLevel":"<td>2</td>",
            "ManagerType":"<td>Management</td>",
            "ManagerDescription":"<td>Manager of a team</td>",
            "ManagerLabel":"<td>Engineer Manager</td>",
            "ManagerTable":"<tr>{ManagerLabel}{ManagerDescription}{ManagerType}{ManagerLevel}</tr>",
            "TechnicalLeaderLevel":"<td>2</td>",
            "TechnicalLeaderType":"<td>Technical</td>",
            "TechnicalLeaderDescription":"<td>Help developer to build software for the business in the a good way</td>",
            "TechnicalLeaderLabel":"<td>Technical Leader</td>",
            "TechnicalLeaderTable":"<tr>{TechnicalLeaderLabel}{TechnicalLeaderDescription}{TechnicalLeaderType}{TechnicalLeaderLevel}</tr>",
            "ProductOwnerLevel":"<td>1</td>",
            "ProductOwnerType":"<td>Product</td>",
            "ProductOwnerDescription":"<td>Create and ordered features from the wishes of the business</td>",
            "ProductOwnerLabel":"<td>Product Owner</td>",
            "ProductOwnerTable":"<tr>{ProductOwnerLabel}{ProductOwnerDescription}{ProductOwnerType}{ProductOwnerLevel}</tr>",
            "DeveloperLevel":"<td>1</td>",
            "DeveloperType":"<td>Technical</td>",
            "DeveloperDescription":"<td>Make software from specifications</td>",
            "DeveloperLabel":"<td>Software Engineer</td>",
            "DeveloperTable":"<tr>{DeveloperLabel}{DeveloperDescription}{DeveloperType}{DeveloperLevel}</tr>",
            "Tbody":"<tbody>{DeveloperTable}{ProductOwnerTable}{TechnicalLeaderTable}{ManagerTable}{ArchitectTable}{DirectorTable}</tbody>",
            "TitleTable":"<th scope=\"col\">Title</th>",
            "DescriptionTable":"<th scope=\"col\">Description</th>",
            "TypeTable":"<th scope=\"col\">Type</th>",
            "LevelTable":"<th scope=\"col\">Level</th>",
            "Thead":"<thead><tr>{TitleTable}{DescriptionTable}{TypeTable}{LevelTable}</tr></thead>",
            "PosteTable":"<table>{Thead}{Tbody}</table>",
            "MainTag":"{PosteTable}",
            "H1News":"<h1>News!!!</h1>",
            "PNews":"<p>The direction decide to present you the news roles in the organisation.</p>",
            "SectionTag":"{H1News}{PNews}",
            "LiHome":"<li>Home</li>",
            "LiNews":"<li>News</li>",
            "LiOrganisation":"<li>New organisation</li>",
            "UlMenu":"<ul>{LiHome}{LiNews}{LiOrganisation}</ul>",
            "NavTag":"<nav>{UlMenu}</nav>",
            "Body":"<body>{NavTag}{SectionTag}{MainTag}</body>",
            "MetaCharset":"<meta charset=\"UTF-8\">",
            "MetaViewPort":"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">",
            "TitleDocument":"<title>Document</title>",
            "LinkHead":"<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">",
            "Head":"<head>{MetaCharset}{MetaViewPort}{TitleDocument}{LinkHead}</head>",
            "HtmlForm":"<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">{Head}{Body}{ScriptTable}</html>",                  
            "HtmlFormWithDoctype":"<!DOCTYPE html>{HtmlForm}"
        }
        """;
    }

    public class JsonTableDataModel
    {
        public string? ScriptTable { get; set; }
        public string? DirectorLevel { get; set; }
        public string? DirectorType { get; set; }
        public string? DirectorDescription { get; set; }
        public string? DirectorLabel { get; set; }
        public string? DirectorTable { get; set; }
        public string? ArchitectLevel { get; set; }
        public string? ArchitectType { get; set; }
        public string? ArchitectDescription { get; set; }
        public string? ArchitectLabel { get; set; }
        public string? ArchitectTable { get; set; }
        public string? ManagerLevel { get; set; }
        public string? ManagerType { get; set; }
        public string? ManagerDescription { get; set; }
        public string? ManagerLabel { get; set; }
        public string? ManagerTable { get; set; }
        public string? TechnicalLeaderLevel { get; set; }
        public string? TechnicalLeaderType { get; set; }
        public string? TechnicalLeaderDescription { get; set; }
        public string? TechnicalLeaderLabel { get; set; }
        public string? TechnicalLeaderTable { get; set; }
        public string? ProductOwnerLevel { get; set; }
        public string? ProductOwnerType { get; set; }
        public string? ProductOwnerDescription { get; set; }
        public string? ProductOwnerLabel { get; set; }
        public string? ProductOwnerTable { get; set; }
        public string? DeveloperLevel { get; set; }
        public string? DeveloperType { get; set; }
        public string? DeveloperDescription { get; set; }
        public string? DeveloperLabel { get; set; }
        public string? DeveloperTable { get; set; }
        public string? Tbody { get; set; }
        public string? TitleTable { get; set; }
        public string? DescriptionTable { get; set; }
        public string? TypeTable { get; set; }
        public string? LevelTable { get; set; }
        public string? Thead { get; set; }
        public string? PosteTable { get; set; }
        public string? MainTag { get; set; }
        public string? H1News { get; set; }
        public string? PNews { get; set; }
        public string? SectionTag { get; set; }
        public string? LiHome { get; set; }
        public string? LiNews { get; set; }
        public string? LiOrganisation { get; set; }
        public string? UlMenu { get; set; }
        public string? NavTag { get; set; }
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