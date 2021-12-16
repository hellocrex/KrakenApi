using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    public class Ledgers
    {
        /// <summary>
        /// Reference Id
        /// </summary>
        [JsonProperty("refid")]
        public string RefId { get; set; }

        /// <summary>
        /// Unix timestamp of ledger
        /// </summary>
        [JsonProperty("time")] 
        public string Time { get; set; }

        /// <summary>
        /// Type of ledger entry
        /// </summary>
        [JsonProperty("type")]
        public LedgerType LedgerType { get; set; }

        /// <summary>
        /// Additional info relating to the ledger entry type, where applicable
        /// </summary>
        [JsonProperty("subtype")]
        public string SubType { get; set; }

        /// <summary>
        /// Asset class
        /// </summary>
        [JsonProperty("aclass")]
        public string Aclass { get; set; }

        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Resulting balance
        /// </summary>
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
    }
}
