using System.Collections.Generic;
using Logic.Interfaces;

namespace Logic.Helpers
{
    public class HighscoreReader : IHighscoreReader
    {
        private readonly string _path = "../../../../Logic/Helpers/Scores.txt";


        public List<int> ReadScores()
        {
            List<int> highscores = new List<int>();
            string[] lines = System.IO.File.ReadAllLines(_path);
            foreach (string line in lines)
            {
                highscores.Add(int.Parse(line));
            }
            return highscores;
        }
    }
}
