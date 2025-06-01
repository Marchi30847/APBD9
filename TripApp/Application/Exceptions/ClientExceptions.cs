namespace TripApp.Application.Exceptions;

public static class ClientExceptions
{
    public class ClientHasTripsException : InvalidOperationException
    {
        public ClientHasTripsException()
            : base("Client has assigned trips.")
        {
        }
    }

    public class ClientNotFoundException : KeyNotFoundException
    {
        public ClientNotFoundException()
            : base("Client not found.")
        {
        }
    }

    public class ClientPeselAlreadyExistsException : InvalidOperationException
    {
        public ClientPeselAlreadyExistsException()
            : base("A client with the given PESEL already exists.")
        {
        }
    }

    public class ClientAlreadyRegisteredException : InvalidOperationException
    {
        public ClientAlreadyRegisteredException()
            : base("Client is already registered for this trip.")
        {
        }
    }
}