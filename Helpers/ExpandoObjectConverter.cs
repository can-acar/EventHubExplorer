using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventHubExplorer.Helpers
{
    public class ExpandoObjectConverter : JsonConverter<ExpandoObject>
    {
        public override ExpandoObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var value = new ExpandoObject();
            var dict = (IDictionary<string, object>)value;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return value;
                }

                var key = reader.GetString();
                reader.Read();
                dict[key] = JsonSerializer.Deserialize(ref reader, typeof(object), options);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, ExpandoObject value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
