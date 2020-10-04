using System.Runtime.Serialization;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Packed")]
        Packed,
        [EnumMember(Value = "Shipped")]
        Shipped
    }
}