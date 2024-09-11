using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CloneType
{
    public static T PrototypeDeepClone<T>(this T clone)
    {
            var formatter = new BinaryFormatter();
            using (var strem = new MemoryStream())
            {
                formatter.Serialize(strem, clone);
                strem.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(strem);
            }
    }
}