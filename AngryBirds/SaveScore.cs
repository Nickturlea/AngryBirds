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
            // Get the path of the Desktop, then navigate to the LastCommited folder
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            filePath = Path.Combine(desktopPath, "LastCommited", "MadBirdScores.txt");

            // Ensure the directory exists
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
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

                // try to match the pattern against each line
                foreach (string line in lines)
                {
                    Match match = linePattern.Match(line); // Match is just looking for the apperace of string input that was passed through it 
                    if (match.Success) // If a match is found
                    {
                        // Parse the level number from the 'lvl' group and convert it to an integer.
                        int level = int.Parse(match.Groups["lvl"].Value);

                        // Extract the player's name from the 'name' group and trim any whitespace.
                        string name = match.Groups["name"].Value.Trim();

                        // Parse the score from the 'score' group and convert it to an integer.
                        int score = int.Parse(match.Groups["score"].Value);

                        // Check if there's already a list for this level in the dictionary.
                        if (!levelScores.ContainsKey(level))
                        {
                            // If not, create a new list for this level.
                            levelScores[level] = new List<(string Name, int Score)>();
                        }

                        // Add the player's name and score to the list for this level.
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

            // Sort each level's scores
            foreach (var level in levelScores.Keys.ToList())
            {
                levelScores[level] = levelScores[level]
                    .OrderByDescending(s => s.Score)
                    .ThenBy(s => s.Name)
                    .ToList();
            }

            // Write the scores back to the file in the specified format
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                for (int i = 1; i <= levelScores.Keys.Max(); i++)
                {
                    if (levelScores.ContainsKey(i))
                    {
                        // Write the level header
                        sw.WriteLine($"----------Level {i}----------");

                        // Write each score entry
                        foreach (var (Name, Score) in levelScores[i])
                        {
                            sw.WriteLine($"From Level {i} | {Name} - {Score}");
                        }
                    }
                }
            }

        }

    }
}
