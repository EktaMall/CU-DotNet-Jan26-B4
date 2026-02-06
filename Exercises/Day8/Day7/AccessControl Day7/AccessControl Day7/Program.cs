namespace AccessControl_Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string accessInput = Console.ReadLine();

            string[] accessParts = accessInput.Split('|');

            if (accessParts.Length != 5)
            {
                Console.WriteLine("Invalid Access Log");
                return;
            }

            string accessGateCode = accessParts[0];
            if (accessGateCode.Length != 2 || !char.IsLetter(accessGateCode[0]) || !char.IsDigit(accessGateCode[1]))
            {
                Console.WriteLine("Invalid Access Log");
                return;
            }

            string userInitialString = accessParts[1];
            if (userInitialString.Length != 1 || !char.IsUpper(userInitialString[0]))
            {
                Console.WriteLine("Invalid Access Log");
                return;
            }
            char userInitialChar = userInitialString[0];

            if (!byte.TryParse(accessParts[2], out byte userAccessLevel) || userAccessLevel < 1 || userAccessLevel > 7)
            {
                Console.WriteLine("Invalid Access Log");
                return;
            }

            if (!bool.TryParse(accessParts[3], out bool userIsActive))
            {
                Console.WriteLine("Invalid Access Log");
                return;
            }

            if (!byte.TryParse(accessParts[4], out byte loginAttempts) || loginAttempts > 200)
            {
                Console.WriteLine("Invalid Access Log");
                return;
            }

            string accessStatus;

            if (!userIsActive)
            {
                accessStatus = "Access Denied – Inactive User";
            }
            else if (loginAttempts > 100)
            {
                accessStatus = "Access Denied – Too Many Attempts";
            }
            else if (userAccessLevel >= 5)
            {
                accessStatus = "Access Granted – High Security";
            }
            else
            {
                accessStatus = "Access Grnated – Standard";
            }

            Console.WriteLine($"{"Gate".PadRight(10)}: {accessGateCode}");
            Console.WriteLine($"{"User".PadRight(10)}: {userInitialChar}");
            Console.WriteLine($"{"Level".PadRight(10)}: {userAccessLevel}");
            Console.WriteLine($"{"Attempts".PadRight(10)}: {loginAttempts}");
            Console.WriteLine($"{"Status".PadRight(10)}: {accessStatus}");
        }
    }
}