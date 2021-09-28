using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace BuscarCep.CrossCutting.Extensions
{
    public static class GenericTypeExtensions
    {
        public static string ToJson<T>(this T myObject)
        {
            return JsonConvert.SerializeObject(myObject, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static string GetDescription<T>(this T source) where T : Enum
        {
            return source.GetType()
                         .GetField(source.ToString())
                         .GetCustomAttributes(typeof(DescriptionAttribute), false)
                         .Select(attribute => ((DescriptionAttribute)attribute).Description)
                         .DefaultIfEmpty(source.ToString())
                         .FirstOrDefault();
        }

        public static bool TryDeserializeJson<T>(this object objToConvert, out T resultParsed)
        {
            resultParsed = default;

            try
            {
                if (!(objToConvert is string))
                    objToConvert = objToConvert.ToJson();

                var jsonSettings = new JsonSerializerSettings
                {
                    Error = (sender, args) =>
                    {
                        if (Debugger.IsAttached)
                            Debugger.Break();

                        Log.Fatal("Falha ao deserializar '{propertie}': {messege}", args.ErrorContext.Member, args.ErrorContext.Error.Message);
                    }
                };

                resultParsed = JsonConvert.DeserializeObject<T>(objToConvert as string, jsonSettings);

                return resultParsed != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
