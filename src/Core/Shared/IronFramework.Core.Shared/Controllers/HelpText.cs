using AltV.Net;
using AltV.Net.Data;
using IronPython.AltV.Extensions;

namespace IronFramework.Core.Shared.Controllers
{
    public class HelpText : IWritable
    {
        public string Text { get; set; } = "";
        public Position Position { get; set; } = new Position();

        public void OnWrite(IMValueWriter writer) => writer.WriteObject(w => w
            .WriteProperty(nameof(Text), Text)
            .WriteProperty(nameof(Position), Position));
    }
}
