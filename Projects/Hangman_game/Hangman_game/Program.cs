using System.Collections;

namespace Hangman_game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> words = new List<string>() { "TELESCOPE", "COMPUTER", "SCIENCE", "MANGO", "STUDENT", "FOOD", "SUNSHINE", "RAINBOW", "LAPTOP", "WINTER", "HANGMAN", "AEROPLANE", "UNIVERSITY" };
            Random rnd = new Random();
            string word = words[rnd.Next(words.Count)];

            char[] display = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                display[i] = '_';
            }

            int lives = 6;
            HashSet<char> GuessedLetters = new HashSet<char>();

            while (lives > 0)
            {
                Console.Write("\nGuess a letter: ");

                for (int i = 0; i < display.Length; i++)
                    Console.Write(display[i] + " ");

                Console.WriteLine("\nLives left: " + lives);

                Console.Write("Guessed letters: ");
                foreach (char c in GuessedLetters)
                    Console.Write(c + " ");

                Console.WriteLine();

                Console.Write("Guess a letter: ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("Please enter a valid letter.");
                    continue;
                }

                char guess = char.ToUpper(input[0]);

                if (GuessedLetters.Contains(guess))
                {
                    Console.WriteLine("Already guessed.");
                    continue;
                }

                GuessedLetters.Add(guess);

                bool found = false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == guess)
                    {
                        display[i] = guess;
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Wrong guess!");
                    lives--;
                }
                else
                {
                    Console.WriteLine("Good Catch!");
                }

                bool won = true;
                for (int i = 0; i < display.Length; i++)
                {
                    if (display[i] == '_')
                    {
                        won = false;
                        break;
                    }
                }

                if (won)
                {
                    Console.WriteLine("\nYou WON!");
                    Console.WriteLine("The word was: " + word);
                    return;
                }
            }

            Console.WriteLine("\nGame Over!");
            Console.WriteLine("The word was: " + word);
        }
    }

}