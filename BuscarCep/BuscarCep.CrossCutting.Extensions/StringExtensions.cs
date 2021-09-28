namespace BuscarCep.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string Format(this string str, params object[] parameters)
        {
            return string.Format(str, parameters);
        }        
    }
}
