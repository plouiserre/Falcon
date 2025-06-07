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

        public string CleanContent(string text)
        {
            string textCleaned = string.Empty;
            string textWorking = text.Replace(_tag.TagStart, string.Empty).Replace(_tag.TagEnd, string.Empty);
            bool innerTags = IsInnerTags(textWorking);
            textCleaned = DeleteFrontSpace(textWorking, innerTags);
            textCleaned = DeleteBottomSpace(textCleaned, innerTags);
            return textCleaned;
        }

        private string DeleteFrontSpace(string textWorking, bool innerTags)
        {
            string textCleaned = string.Empty;
            if (textWorking[0] == ' ' && innerTags)
            {
                textCleaned = FrontTextCleanedWithSpace(textWorking);
            }
            else if (textWorking[0] == '\n' && innerTags)
            {
                textCleaned = FrontTextCleanedWithSpace(textWorking);
            }
            else
                textCleaned = textWorking;
            return textCleaned;
        }

        private string FrontTextCleanedWithSpace(string textWorking)
        {
            int positionStartTag = IndexStartTag(textWorking);
            string textWithoutSpace = textWorking.Remove(0, positionStartTag);
            return textWithoutSpace;
        }

        private int IndexStartTag(string textWorking)
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

        private string DeleteBottomSpace(string textWorking, bool innerTags)
        {
            string textCleaned = string.Empty;
            int endIndex = textWorking.Length - 1;
            if (textWorking[endIndex] == ' ' && innerTags)
            {
                textCleaned = BottomTextCleanedWithSpace(textWorking, ' ');
            }
            else if (textWorking[endIndex] == '\n' && innerTags)
            {
                textCleaned = BottomTextCleanedWithSpace(textWorking, '\n');
            }
            else
                textCleaned = textWorking;
            return textCleaned;
        }

        private string BottomTextCleanedWithSpace(string textWorking, char caracterSpecial)
        {
            int positionEndContent = FindLastCaracter(textWorking, caracterSpecial);
            int caractersToDelete = textWorking.Length - positionEndContent;
            string textWithoutSpace = textWorking.Remove(positionEndContent, caractersToDelete);
            return textWithoutSpace;
        }

        public int FindLastCaracter(string textWorking, char caracterSpecial)
        {
            int position = 0;
            for (int i = textWorking.Length - 1; i > 0; i--)
            {
                char caracter = textWorking[i];
                if (caracter != caracterSpecial)
                {
                    position = i;
                    break;
                }
            }
            return position;
        }

        public bool IsInnerTags(string textWorking)
        {
            return textWorking.Contains('<');
        }
    }
}