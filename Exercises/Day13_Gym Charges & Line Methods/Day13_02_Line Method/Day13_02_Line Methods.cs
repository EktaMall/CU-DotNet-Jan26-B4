namespace LineMethods
{
    internal class Program
    {
        public static void DisplayLine()
        {
            Console.WriteLine(new string('-', 40));
        }

        public static void DisplayLine(char ch)
        {
            Console.WriteLine(new string(ch, 40));
        }

        public static void DisplayLine(char ch, int length)
        {
            Console.WriteLine(new string(ch, length));
        }
        static void Main(string[] args)
        {

            DisplayLine();
            DisplayLine('+');
            DisplayLine('$', 60);
        }
    }
}