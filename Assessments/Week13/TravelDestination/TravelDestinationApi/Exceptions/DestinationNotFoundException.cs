namespace TravelDestinationApi.Exceptions
{
    public class DestinationNotFoundException:Exception
    {
        public DestinationNotFoundException():base("The specified destination was not found.")
        {
        }
        public DestinationNotFoundException(string message):base(message)
        {
        }
    }
}
