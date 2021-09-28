namespace BuscarCep.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsValidCep(this string cep)
        {
            return (cep.ToString().Length != 8) ? true : false;
        }        
    }
}
