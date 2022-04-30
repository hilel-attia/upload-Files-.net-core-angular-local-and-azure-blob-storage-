using System;
using Stargate.API.Infrastructure;

namespace Stargate.API.Services
{
    public class BijectiveUriService : IUriShortener
    {
        public string GetExternalUri(int databaseId)
        {
            var ExternalUri = Bijective.Encode(databaseId);
            return ExternalUri;
        }
    }
}