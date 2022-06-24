using AltV.Net;
using AltV.Net.EntitySync;
using IronPython.AltV.Extensions;
using System.Numerics;

namespace IronFramework.Core.Shared.Controllers
{
    public class HelpText : Entity, IWritable
    {
        public HelpText(Vector3 position, string text, int dimension = 0, uint range = 1) : base(3, position, dimension, range)
        {
            Text=text;
        }

        public string Text
        {
            get
            {
                if (TryGetData(nameof(Text), out string value)) return value;
                return string.Empty;
            }
            set
            {
                SetData(nameof(Text), value);
            }
        }

        public void OnWrite(IMValueWriter writer) => writer.WriteObject(w => w
            .WriteProperty(nameof(Text), Text)
            .WriteProperty(nameof(Range), Range)
            .WriteProperty(nameof(Dimension), Dimension)
            .WriteProperty(nameof(Position), Position));
    }
}
