using System;
using Logic.Models;

namespace Logic.Interfaces
{
    public interface IStrategy
    {
        Tuple<Size, int, int, int,int> SetDifficulty();
    }
}