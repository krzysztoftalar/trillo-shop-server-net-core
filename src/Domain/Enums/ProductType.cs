using System.Runtime.Serialization;

namespace Domain.Enums
{
    public enum ProductType
    {
        [EnumMember(Value = "Woman")]
        Woman,
        [EnumMember(Value = "Men")]
        Men
    }
}