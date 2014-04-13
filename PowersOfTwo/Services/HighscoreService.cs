using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PowersOfTwo.Services
{
    public class HighscoreService
    {
        private readonly string _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MadCowDevelopment",
            "PowersOfTwo");

        private readonly string _fileName;

        public HighscoreService()
        {
            Directory.CreateDirectory(_filePath);
            _fileName = Path.Combine(_filePath, "PowersOfTwoScore.xml");
        }

        public void AddScore(int scoreToAdd)
        {
            var scores = GetScores();
            scores.Add(scoreToAdd);
            scores = scores.OrderByDescending(p => p).Take(3).ToList();
            var doc = new XDocument();
            var scoresElement = new XElement("Scores");
            doc.Add(scoresElement);
            foreach (var score in scores)
            {
                scoresElement.Add(new XElement("Score", score));
            }

            doc.Save(_fileName);
        }

        public List<int> GetScores()
        {
            if(!File.Exists(_fileName)) return new List<int>();

            var doc = XDocument.Load(_fileName);
            var scores = (from score in doc.Descendants("Score")
                          orderby score.Value descending 
                          select Int32.Parse(score.Value)).ToList();
            return scores;
        }
    }
}