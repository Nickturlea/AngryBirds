using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AngryBirds
{
    internal class SaveScore
    {
        private string filePath;

        public SaveScore()
        {
            // Documents folder for easy access
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MadBirdScores.txt");
        }



        public void SavePlayerScore(int lvl, string playerName, int playerScore)
        {
            // Dictionary to store the name and score with the proper level 
            Dictionary<int, List<(string Name, int Score)>> levelScores = new Dictionary<int, List<(string Name, int Score)>>();

            // Checking if the file already exists 
            if (File.Exists(filePath))
            {
                // Read the file lines 
                string[] lines = File.ReadAllLines(filePath);

                // Regular expression to match the score entries
                Regex linePattern = new Regex(@"From Level (?<lvl>\d+) \|.*? (?<name>.+?) - (?<score>\d+)$");
                foreach (string line in lines)
                {
                    Match match = linePattern.Match(line);
                    if (match.Success)
                    {
                        int level = int.Parse(match.Groups["lvl"].Value);
                        string name = match.Groups["name"].Value.Trim();
                        int score = int.Parse(match.Groups["score"].Value);

                        if (!levelScores.ContainsKey(level))
                        {
                            levelScores[level] = new List<(string Name, int Score)>();
                        }
                        levelScores[level].Add((name, score));
                    }
                }
            }

            // Add the new score
            if (!levelScores.ContainsKey(lvl))
            {
                levelScores[lvl] = new List<(string Name, int Score)>();
            }
            levelScores[lvl].Add((playerName, playerScore));

            // Sort each level's scores in descending order
            foreach (var levelScore in levelScores)
            {
                levelScores[levelScore.Key] = levelScore.Value.OrderByDescending(s => s.Score).ToList(); // A little of sql to make easyier to read from the file 
            }

            // Write the scores back to the file
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                foreach (var level in levelScores.Keys.OrderBy(k => k))
                {
                    sw.WriteLine($"----------Level {level}----------");
                    int rank = 1;
                    foreach (var scoreEntry in levelScores[level])
                    {
                        sw.WriteLine($"From Level {level} |{rank}. {scoreEntry.Name} - {scoreEntry.Score}");
                        rank++;
                    }
                }
            }
        }
    }
}
