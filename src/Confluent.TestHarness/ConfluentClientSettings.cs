using System;
using Confluent.RestClient;

namespace Confluent.TestHarness
{
    public class ConfluentClientSettings : IConfluentClientSettings
    {
        public ConfluentClientSettings(string kafkaBaseUrl, TimeSpan requestTimeout)
        {
            KafkaBaseUrl = kafkaBaseUrl;
            RequestTimeout = requestTimeout;
        }

        public string KafkaBaseUrl { get; private set; }
        public TimeSpan RequestTimeout { get; private set; }
        public string AuthenticationSchema { get; private set; }
        public string AuthenticationParams { get; private set; }
    }
}
