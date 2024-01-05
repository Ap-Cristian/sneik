using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces;

namespace Logic.Models
{
    public class HighscoresUseCase : IUseCase
    {

        private readonly IHighscoreReader _highscoreReader;
        public List<int> _scores;
        public List<int> _highscores;

        public HighscoresUseCase(IHighscoreReader reader)
        {
            _highscoreReader = reader;
        }

        public void Execute()
        {
            _scores = _highscoreReader.ReadScores();


            _highscores = _scores.OrderByDescending(x => x).Take(3).ToList();
        }
    }
}