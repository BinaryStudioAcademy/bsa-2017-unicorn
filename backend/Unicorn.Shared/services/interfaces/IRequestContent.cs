using System.Net.Http;

namespace Unicorn.Shared.services.interfaces
{
    interface IRequestContent
    {
        HttpContent GetContent();
    }
}
