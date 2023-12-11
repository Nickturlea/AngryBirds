using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class SaveScore
    {
        private string filePath;

        public SaveScore()
        {
            // Desktop for easy access 
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MadBird.txt");
        }

        public void SavePlayerScore(int lvl, string playerName, int playerScore)
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                // Create a new file and write the score
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine($"From lvl {lvl} |1. {playerName} - {playerScore}");
                }
            }
            else
            {
                // Read existing scores and append the new score
                List<string> lines = File.ReadAllLines(filePath).ToList();
                int count = lines.Count + 1;
                lines.Add($"{count}. {playerName} - {playerScore}");
                File.WriteAllLines(filePath, lines);
            }
        }
    }
}
