using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.CleanData
{
    public interface IDeleteUselessSpace
    {
        string PurgeUselessCaractersAroundTag(string html);
    }
}