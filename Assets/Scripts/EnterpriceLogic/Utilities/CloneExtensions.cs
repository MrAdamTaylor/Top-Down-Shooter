using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EnterpriceLogic.Utilities
{
    public static class CloneExtensions
    {
        public static T PrototypeDeepClone<T>(this T clone)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, clone);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}