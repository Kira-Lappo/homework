using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kira.Async.ServeAndListen
{
    public static class CommandSerializer
    {
        public static IFormatter Serializer { get; } = new BinaryFormatter();
    }
}