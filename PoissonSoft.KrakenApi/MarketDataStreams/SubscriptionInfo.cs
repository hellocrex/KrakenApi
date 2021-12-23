using System.Collections.Generic;

namespace PoissonSoft.KrakenApi.MarketDataStreams
{
    /// <summary>
    /// Subscription information
    /// </summary>
    public class SubscriptionInfo
    {
        /// <summary>
        /// Subscription Id used to unsubscribe
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Type of Data Stream
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Type of Data Stream
        /// </summary>
        public string[] SubscribeContent { get; set; }

        /// <summary>
        /// Other Stream parameters
        /// </summary>
        public Dictionary<string, string[]> Parameters { get; set; }
    }
}
