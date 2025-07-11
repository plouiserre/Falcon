using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public class LinkParser : ITagParser
    {
        private IIdentifyTag _identifyTag;

        public LinkParser(IIdentifyTag identifyTag)
        {
            _identifyTag = identifyTag;
        }

        public string CleanHtml(TagModel tag, string html)
        {
            throw new NotImplementedException();
        }

        public List<TagModel> DeterminateChildren(string html)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(TagModel tag)
        {
            throw new NotImplementedException();
        }

        public TagModel Parse(string html)
        {
            var tag = _identifyTag.Analyze(html);
            tag.TagFamily = TagFamilyEnum.NoEnd;
            return tag;
        }
    }
}