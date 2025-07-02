using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing
{
    public class FindTags
    {
        private string _html;
        private List<string> _tags;
        private List<string> _tagsStart;
        private List<string> _tagsEnd;
        private List<string> _tagsComplete;
        private Dictionary<string, string> _tagsTogether;

        public FindTags()
        {
            _tags = new List<string>();
            _tagsStart = new List<string>();
            _tagsEnd = new List<string>();
            _tagsComplete = new List<string>();
            _tagsTogether = new Dictionary<string, string>();
        }

        public List<string> Find(string html)
        {
            _html = html;

            FindTagsInHtml();

            ClassTags();

            LinkTags();

            CompleteTags();

            return _tagsComplete;
        }

        private void FindTagsInHtml()
        {
            int startTagIndex = 0;
            int endTagIndex = 0;
            for (int i = 0; i < _html.Length; i++)
            {
                var caracter = _html[i];
                if (caracter == '<')
                    startTagIndex = i;
                else if (caracter == '>')
                    endTagIndex = i;

                if ((startTagIndex > 0 || endTagIndex > 0) && (endTagIndex > startTagIndex))
                {
                    string tag = _html.Substring(startTagIndex, endTagIndex - startTagIndex + 1);
                    _tags.Add(tag);
                    startTagIndex = 0;
                    endTagIndex = 0;
                }
            }
        }

        private void ClassTags()
        {
            foreach (var tag in _tags)
            {
                if (tag.Contains("/"))
                    _tagsEnd.Add(tag);
                else
                    _tagsStart.Add(tag);
            }
        }

        private void LinkTags()
        {
            for (int i = 0; i < _tagsStart.Count; i++)
            {
                var tagStart = _tagsStart[i];
                var tagEndPossible = CalculateTagEndPossible(tagStart);
                bool isAlone = true;
                for (int j = 0; j < _tagsEnd.Count; j++)
                {
                    var tagEnd = _tagsEnd[j];
                    if (tagEnd == tagEndPossible)
                    {
                        _tagsTogether.Add(tagStart, tagEnd);
                        isAlone = false;
                    }
                }
                if (isAlone)
                    _tagsTogether.Add(tagStart, string.Empty);
            }
        }

        private string CalculateTagEndPossible(string tagStart)
        {
            string tagBase = tagStart.Split(" ")[0].Replace("<", string.Empty).Replace(">", string.Empty);
            string tagEnd = string.Concat("</", tagBase, ">");
            return tagEnd;
        }


        private void CompleteTags()
        {

            foreach (var tags in _tagsTogether)
            {
                string tagEnd = tags.Value;
                if (tagEnd == string.Empty)
                    _tagsComplete.Add(tags.Key);
                else
                {
                    int indexStart = _html.IndexOf(tags.Key);
                    int indexEnd = _html.IndexOf(tags.Value);
                    string tagComplete = _html.Substring(indexStart, indexEnd - indexStart + tagEnd.Length);
                    _tagsComplete.Add(tagComplete);
                }
            }
        }
    }
}