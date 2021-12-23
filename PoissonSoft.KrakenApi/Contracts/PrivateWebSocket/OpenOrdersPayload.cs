using System.Collections.Generic;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;
using PoissonSoft.KrakenApi.Contracts.UserData;

namespace PoissonSoft.KrakenApi.Contracts.PrivateWebSocket
{
    /// <summary>
    /// Open orders. Feed to show all the open orders belonging to the authenticated user.
    /// Initial snapshot will provide list of all open orders and then any updates to the open orders list will be sent.
    /// For status change updates, such as 'closed', the fields orderid and status will be present in the payload.
    /// </summary>
    public class OpenOrdersPayload
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, OpenOrderInfo> OpenOrderInfo { get; set; }

        /// <summary>
        /// Channel Name of subscription
        /// </summary>
        [JsonProperty("channelName")]
        public int ChannelName { get; set; }

        /// <summary>
        /// sequence number for openOrders subcription
        /// </summary>
        [JsonProperty("sequence")]
        public int Sequence { get; set; }
    }


    public class OpenOrderInfo
    {
        /// <summary>
        /// Referral order transaction id that created this order
        /// </summary>
        [JsonProperty("refid")]
        public string RefId { get; set; }

        /// <summary>
        /// user reference ID
        /// </summary>
        [JsonProperty("userref")]
        public decimal Userref { get; set; }

        /// <summary>
        /// status of order
        /// </summary>
        [JsonProperty("status")]
        public string status { get; set; }

        /// <summary>
        /// unix timestamp of when order was placed
        /// </summary>
        [JsonProperty("opentm")]
        public decimal OpenTime { get; set; }

        /// <summary>
        /// unix timestamp of order start time (if set)
        /// </summary>
        [JsonProperty("starttm")]
        public decimal StartTime { get; set; }

        /// <summary>
        /// unix timestamp of order end time (if set)
        /// </summary>
        [JsonProperty("expiretm")]
        public decimal ExpireTime { get; set; }

        /// <summary>
        /// order description info
        /// </summary>
        [JsonProperty("descr")]
        public OrderDescription Description { get; set; }

        /// <summary>
        /// Asset pair
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }

        /// <summary>
        /// unix timestamp of trade
        /// </summary>
        [JsonProperty("time")]
        public decimal Time { get; set; }

        /// <summary>
        /// type of order (buy/sell)
        /// </summary>
        [JsonProperty("type")]
        public OrderSide Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("orderType")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// volume of order (base currency unless viqc set in oflags)
        /// </summary>
        [JsonProperty("vol")]
        public decimal Volume { get; set; }

        /// <summary>
        /// total volume executed so far (base currency unless viqc set in oflags)
        /// </summary>
        [JsonProperty("vol_exec")]
        public decimal VolumeExec { get; set; }

        /// <summary>
        /// total cost (quote currency unless unless viqc set in oflags)
        /// </summary>
        [JsonProperty("cost")]
        public decimal Cost { get; set; }

        /// <summary>
        /// total fee (quote currency)
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// average price (cumulative; quote currency unless viqc set in oflags)
        /// </summary>
        [JsonProperty("avg_price")]
        public decimal Margin { get; set; }

        /// <summary>
        /// stop price (quote currency, for trailing stops)
        /// </summary>
        [JsonProperty("stopprice")]
        public decimal StopPrice { get; set; }

        /// <summary>
        /// triggered limit price (quote currency, when limit based order type triggered)
        /// </summary>
        [JsonProperty("limitprice")]
        public decimal LimitPrice { get; set; }

        /// <summary>
        /// comma delimited list of miscellaneous info: stopped=triggered by stop price,
        /// touched=triggered by touch price, liquidation=liquidation, partial=partial fill
        /// </summary>
        [JsonProperty("misc")]
        public decimal Misc { get; set; }

        /// <summary>
        /// Optional - comma delimited list of order flags. viqc = volume in quote currency (not currently available),
        /// fcib = prefer fee in base currency, fciq = prefer fee in quote currency, nompp = no market price protection,
        /// post = post only order (available when ordertype = limit)
        /// </summary>
        [JsonProperty("oflags")]
        public decimal Oflags { get; set; }

        /// <summary>
        /// Optional - time in force.
        /// </summary>
        [JsonProperty("timeinforce")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// Optional - cancel reason, present for all cancellation updates (status="canceled") and for some close updates (status="closed")
        /// </summary>
        [JsonProperty("cancel_reason")]
        public string CancelReason { get; set; }

        /// <summary>
        /// Optional - rate-limit counter, present if requested in subscription request. See Trading Rate Limits.
        /// </summary>
        [JsonProperty("ratecount")]
        public int RateCount { get; set; }
    }
}
