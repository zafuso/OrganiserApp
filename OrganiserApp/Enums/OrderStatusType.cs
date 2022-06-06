using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderStatusType
    {
        [EnumMember(Value = "COMPLETED")]
        COMPLETED,
        [EnumMember(Value = "PENDING")]
        PENDING,
        [EnumMember(Value = "CANCELED")]
        CANCELED,
        [EnumMember(Value = "REFUND_PENDING")]
        REFUND_PENDING,
        [EnumMember(Value = "REFUNDED")]
        REFUNDED,
        [EnumMember(Value = "PARTIALLY_REFUNDED")]
        PARTIALLY_REFUNDED,
        [EnumMember(Value = "RELEASED")]
        RELEASED,
        [EnumMember(Value = "PARTIALLY_CANCELED")]
        PARTIALLY_CANCELED
    }
}
