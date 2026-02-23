using System;

namespace Week5.Week5
{
    internal class Class3
    {
        static void Main()
        {
            string input = "abcdz";
            string result = Transform(input);
            Console.WriteLine(result);
        }

        static string Transform(string s)
        {
            string vowels = "aeiou";
            string output = "";

            foreach (char ch in s)
            {
                if (vowels.Contains(ch))  
                {
                    int index = vowels.IndexOf(ch);
                    int nextIndex = (index + 1) % vowels.Length;
                    output += vowels[nextIndex];
                }
                else if (ch >= 'a' && ch <= 'z')  
                {
                    char next;

                    if (ch == 'z')
                        next = 'a';  
                    else
                        next = (char)(ch + 1);

                    while (vowels.Contains(next))
                    {
                        if (next == 'z')
                            next = 'a';
                        else
                            next = (char)(next + 1);
                    }
                    output += next;
                }
                else
                {
                    output += ch; 
                }
            }
            return output;
        }
    }
}
