using System.ComponentModel;

namespace BuscarCep.Domain.Enum
{
    public enum EMediaType
    {
        [Description("application/json")]
        Json,

        [Description("text/csv")]
        Csv,

        [Description("application/x-www-form-urlencoded")]
        UrlEncoded,
    }
}
