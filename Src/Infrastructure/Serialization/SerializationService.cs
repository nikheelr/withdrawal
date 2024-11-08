using System.Text.Json;
using Application.Serialization;

namespace Infrastructure.Serialization;

public class SerializationService : ISerialization
{
    public string SerializeJson<T>(T source)
    {
        return JsonSerializer.Serialize(source);
    }
}