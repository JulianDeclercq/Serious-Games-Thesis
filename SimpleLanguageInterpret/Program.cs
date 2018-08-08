using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SimpleLanguageInterpret
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Read all lines from the input file
            string originalFileName = "input/NoCommonSense";
            string fileName = $"{originalFileName} (0).txt";

            for (int i = 1; File.Exists(fileName); ++i)
            {
                List<string> steps = new List<string>();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Add the step without its signature
                        if (line.Contains("STEP"))
                            steps.Add(line.Replace("--- STEP:    ", ""));
                    }
                }

                // Transcribe all steps
                List<string> sentences = new List<string>();

                foreach (string step in steps)
                    sentences.Add(Capitalize(TranscribeStep(step)));

                // Write everything to an output file.
                using (StreamWriter writer = new StreamWriter($"output/{Path.GetFileNameWithoutExtension(fileName)}_transcribed.txt"))
                {
                    foreach (string sentence in sentences)
                        if (!string.IsNullOrEmpty(sentence))
                            writer.WriteLine(sentence);
                }

                Console.WriteLine($"Transcribed {fileName}");

                // Update the fileName
                fileName = $"{originalFileName} ({i}).txt";
            }

            Console.ReadKey();
        }

        private static string TranscribeStep(string step)
        {
            // Ignore anons
            Match match = Regex.Match(step, @"`anon(.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return string.Empty;

            match = Regex.Match(step, "spawn_cabin (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The {match.Groups[1]} is in the cabin.";

            match = Regex.Match(step, "spawn_woods (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The {match.Groups[1]} is in the woods.";

            match = Regex.Match(step, "leaveHouseToWoods (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The {match.Groups[1]} leaves the house and goes to the woods.";

            match = Regex.Match(step, "wander_in_woods (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The {match.Groups[1]} wanders in the woods.";

            match = Regex.Match(step, "meet (.+) (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The {match.Groups[1]} meets the {match.Groups[2]}.";

            match = Regex.Match(step, "gets_lost (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The {match.Groups[1]} gets lost in the woods.";

            match = Regex.Match(step, "grandma_no_wolf", RegexOptions.IgnoreCase);
            if (match.Success)
                return "They lived happily ever after.";

            match = Regex.Match(step, "fightWolf (.+) (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The {match.Groups[1]} fights the wolf in the {match.Groups[2]}.";

            match = Regex.Match(step, "wolf_wins (.+) (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The wolf wins the fight against the {match.Groups[1]} in the {match.Groups[2]}.";

            match = Regex.Match(step, "character_wins (.+) (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The {match.Groups[1]} wins the fight against the wolf in the {match.Groups[2]}.";

            match = Regex.Match(step, "eat (.+) (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The wolf eats the {match.Groups[2]} in the {match.Groups[1]}.";

            match = Regex.Match(step, "wolfEncounter", RegexOptions.IgnoreCase);
            if (match.Success)
                return "The wolf meets the girl in the woods.";

            match = Regex.Match(step, "scream", RegexOptions.IgnoreCase);
            if (match.Success)
                return "The girl screams.";

            match = Regex.Match(step, "woodcutterAlerted", RegexOptions.IgnoreCase);
            if (match.Success)
                return "The woodcutter heard the scream through the forest.";

            match = Regex.Match(step, "woodcutterNotAlerted", RegexOptions.IgnoreCase);
            if (match.Success)
                return "The woodcutter didn't hear the scream in the cabin.";

            match = Regex.Match(step, "runAway (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The girl runs away from the {match.Groups[1]}.";

            match = Regex.Match(step, "howl (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The wolf howls at the {match.Groups[1]}.";

            match = Regex.Match(step, "cookTurnips", RegexOptions.IgnoreCase);
            if (match.Success)
                return "Granny cooks turnips.";

            match = Regex.Match(step, "offerCookies (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"Granny offers cookies to the {match.Groups[1]}.";

            match = Regex.Match(step, "eatCookies (.+) (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The {match.Groups[2]} eats cookies.";

            match = Regex.Match(step, "smell (.+) (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The wolf smells the {match.Groups[2]} in the {match.Groups[1]}.";

            match = Regex.Match(step, "cutTree", RegexOptions.IgnoreCase);
            if (match.Success)
                return "The woodcutter cuts a tree.";

            match = Regex.Match(step, "visitGranny (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
                return $"The girl leaves the {match.Groups[1]} to visit granny.";

            return $"Unknown step {step}.";
        }

        private static string Capitalize(string capitalize)
        {
            if (string.IsNullOrEmpty(capitalize))
                return null;

            if (capitalize.Length == 1)
                return capitalize.ToUpper();

            return char.ToUpper(capitalize[0]) + capitalize.Substring(1);
        }
    }
}