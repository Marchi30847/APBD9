namespace TripApp.Application.Exceptions;

public static class TripExceptions
{
    public class TripNotFoundException : KeyNotFoundException
    {
        public TripNotFoundException()
            : base("Trip not found.")
        {
        }
    }

    public class TripHasStartedException : InvalidOperationException
    {
        public TripHasStartedException()
            : base("Cannot register for a trip that has already started.")
        {
        }
    }
}