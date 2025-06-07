using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.CleanData
{
    public class DeleteUselessSpace : IDeleteUselessSpace
    {
        private TagModel _tag;

        public DeleteUselessSpace(TagModel tag)
        {
            _tag = tag;
        }

        public string Clean(string text)
        {
            string textCleaned = string.Empty;
            string textWorking = text.Replace(_tag.TagStart, string.Empty).Replace(_tag.TagEnd, string.Empty);
            bool innerTags = IsInnerTags(textWorking);
            if (textWorking[0] == ' ' && innerTags)
            {
                textCleaned = textCleanedWithSpace(textWorking);
            }
            else if (textWorking[0] == '\n' && innerTags)
            {
                textCleaned = textCleanedWithSpace(textWorking);
            }
            else
                textCleaned = text;
            return textCleaned;
        }

        private string textCleanedWithSpace(string textWorking)
        {
            int positionStartTag = IndexStartTag(textWorking);
            string textWithoutSpace = textWorking.Remove(0, positionStartTag);
            string textCleaned = string.Concat(_tag.TagStart, textWithoutSpace, _tag.TagEnd);
            return textCleaned;
        }

        public bool IsInnerTags(string textWorking)
        {
            return textWorking.Contains('<');
        }

        public int IndexStartTag(string textWorking)
        {
            int position = 0;
            for (int i = 0; i < textWorking.Length; i++)
            {
                char character = textWorking[i];
                if (character == '<')
                {
                    position = i;
                    break;
                }
            }
            return position;
        }
    }
}