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
    public enum TicketStatusType
    {
        [EnumMember(Value = "NOT_IN_SALE")]
        NOT_IN_SALE,
        [EnumMember(Value = "DOOR_SALE")]
        DOOR_SALE,
        [EnumMember(Value = "IN_RESERVATION")]
        IN_RESERVATION,
        [EnumMember(Value = "ONLINE")]
        ONLINE,
        [EnumMember(Value = "SOLD_OUT")]
        SOLD_OUT
    }
}
