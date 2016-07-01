using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Techsson.Gaming.ControlPanel.Core
{
    public class JsonFormatterSettingsProvider
    {
        public static JsonMediaTypeFormatter CreateJsonFormatter()
        {
            var formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings = CreateJsonSettings();
            return formatter;
        }


        public static JsonSerializerSettings CreateJsonSettings()
        {
            var settings = new JsonMediaTypeFormatter().CreateDefaultSerializerSettings();
            settings.ContractResolver = new BaseClassContractResolver(typeof(ViewModelBase));
            settings.Converters.Add(new StringEnumConverter());
            return settings;
        }

        public class BaseClassContractResolver : CamelCasePropertyNamesContractResolver
        {
            private readonly IEnumerable<Type> _types;

            public BaseClassContractResolver(params Type[] types)
            {
                _types = types ?? new Type[0];
            }


            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                var properties = base.CreateProperties(type, memberSerialization);

                var inheritFromType = _types.FirstOrDefault(x => x.IsAssignableFrom(type));

                if (inheritFromType != null)
                {
                    properties = properties.Where(p => p.DeclaringType == inheritFromType).ToList();
                }

                return properties;
            }
        }
    }
}