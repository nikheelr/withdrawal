namespace Application.Serialization;

public interface ISerialization
{
    /// <summary>
    /// Converts a object into a json string 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string SerializeJson<T>(T source);
}