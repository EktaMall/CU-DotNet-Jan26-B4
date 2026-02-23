namespace cslearning
{
    internal class Day7
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter entry logs in one line separated by '|'");
            string input = Console.ReadLine();
            string[] arr = input.Split('|');
            //Kritika sharma
            if (arr.Length != 5)
            {
                Console.WriteLine("INVALID ACCESS LOG");
            }
            string gateCode = arr[0];
            string userInitial = arr[1];
            string accessLevel = arr[2];
            string isActive = arr[3];
            string attempts = arr[4];

            if (gateCode.Length != 2)
            {
                Console.WriteLine("INVALID ACCESS LOG");
            }
            char gateChar = gateCode[0];
            char gateDigit = gateCode[1];

            if (!char.IsLetter(gateChar) || !char.IsDigit(gateDigit))
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            if (userInitial.Length != 1)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            char userInitialChar = userInitial[0];

            if (!char.IsLetter(userInitialChar) || !char.IsUpper(userInitialChar))
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            bool levelParsed = byte.TryParse(accessLevel, out byte level);
            if (!levelParsed || level < 1 || level > 7)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }


            bool isActiveParsed = bool.TryParse(isActive, out bool active);
            if (!isActiveParsed)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }
            bool attemptsParsed = byte.TryParse(attempts, out byte attemptsnum);
            if (!attemptsParsed || attemptsnum > 200)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            string status;

            if (active == false)
            {
                status = "ACCESS DENIED – INACTIVE USER";
            }
            else if (attemptsnum > 100)
            {
                status = "ACCESS DENIED – TOO MANY ATTEMPTS";
            }
            else if (level >= 5)
            {
                status = "ACCESS GRANTED – HIGH SECURITY";
            }
            else
            {
                status = "ACCESS GRANTED – STANDARD";
            }
            Console.WriteLine($"{"Gate",10} : {gateCode}");
            Console.WriteLine($"{"User",10} : {userInitial}");
            Console.WriteLine($"{"Level",10} : {accessLevel}");
            Console.WriteLine($"{"Attempts",10} : {attempts}");
            Console.WriteLine($"{"Status",10} : {status}");

        }
    }
}