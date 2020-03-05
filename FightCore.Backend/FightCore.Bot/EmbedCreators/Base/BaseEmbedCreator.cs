using System;
using System.Collections.Generic;
using System.Text;

namespace FightCore.Bot.EmbedCreators.Base
{
    public abstract class BaseEmbedCreator
    {
        protected const int MaxFieldLength = 1024;
        protected const int MaxFieldCount = 25;
        protected const int MaxDescriptionLength = 2048;

        protected string ShortenString(string text, int length)
        {
            if (text.Length < length)
            {
                return text;
            }

            return text.Substring(0, length);
        }

        protected string ShortenDescription(string text)
        {
            return ShortenString(text, MaxDescriptionLength);
        }

        protected string ShortenField(string text)
        {
            return ShortenString(text, MaxDescriptionLength);
        }
    }
}
