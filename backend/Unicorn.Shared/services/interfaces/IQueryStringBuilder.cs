namespace Unicorn.Shared.services.interfaces
{
    internal interface IQueryStringBuilder
    {
        string BuildFrom<T>(T payload, string separator = ",");
    }
}
