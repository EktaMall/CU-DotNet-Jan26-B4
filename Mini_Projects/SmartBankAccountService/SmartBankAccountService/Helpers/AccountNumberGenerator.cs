namespace SmartBankAccountService.Helpers
{
    public static class AccountNumberGenerator
    {
        public static string Generate(int id)
        {
            return $"SB-{DateTime.UtcNow.Year}-{id.ToString("D6")}";
        }
    }
}