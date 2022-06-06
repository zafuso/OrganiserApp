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
    public enum CustomerTicketStatusType
    {
        [EnumMember(Value = "ISSUED")]
        ISSUED,
        [EnumMember(Value = "RESERVED")]
        RESERVED,
        [EnumMember(Value = "IN_TRANSFER")]
        IN_TRANSFER,
        [EnumMember(Value = "IN_UPGRADE")]
        IN_UPGRADE,
        [EnumMember(Value = "REFUNDED")]
        REFUNDED,
        [EnumMember(Value = "TRANSFERRED")]
        TRANSFERRED,
        [EnumMember(Value = "UPGRADED")]
        UPGRADED
    }
}
