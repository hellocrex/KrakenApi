using System;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Filters
{
    /// <summary>
    /// Фильтр, задающий ограничения на параметры ордера по инструменту
    /// </summary>
    public class SymbolFilter : ICloneable
    {
        /// <summary>
        /// Тип фильтра
        /// </summary>
        [JsonProperty("filterType")]
        [JsonConverter(typeof(StringEnumExConverter), SymbolFilterType.Unknown)]
        public SymbolFilterType FilterType { get; set; }

        /// <inheritdoc />
        public object Clone()
        {
            return CreateInstanceForClone();
        }

        /// <summary>
        /// Создание экземпляра объекта, возвращаемого методом <see cref="Clone"/>
        /// </summary>
        /// <returns></returns>
        protected virtual SymbolFilter CreateInstanceForClone()
        {
            return new SymbolFilter
            {
                FilterType = FilterType
            };
        }
    }
}
