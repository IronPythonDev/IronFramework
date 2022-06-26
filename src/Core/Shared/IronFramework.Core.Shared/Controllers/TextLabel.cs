using AltV.Net;
using AltV.Net.Data;
using AltV.Net.EntitySync;
using IronPython.AltV.Extensions;
using System.Numerics;

namespace IronFramework.Core.Shared.Controllers
{
    public class World3DTextLabel : Entity, IWritable
    {
        public World3DTextLabel(string text, Vector3 position, Rgba? color = null, int dimension = 0, uint range = 1) : base(1, position, dimension, range)
        {
            Text = text;
            Color = color;
            Font = 4;
            Scale = 1f;
        }

        public float Scale
        {
            get
            {
                if (TryGetData(nameof(Scale).ToLower(), out float value)) return value;
                return 1;
            }
            set
            {
                SetData(nameof(Scale).ToLower(), value);
            }
        }
        public int Font
        {
            get
            {
                if (TryGetData(nameof(Font).ToLower(), out int value)) return value;
                return 1;
            }
            set
            {
                SetData(nameof(Font).ToLower(), value);
            }
        }
        public string Text
        {
            get
            {
                if (TryGetData(nameof(Text).ToLower(), out string value)) return value;
                return string.Empty;
            }
            set
            {
                SetData(nameof(Text).ToLower(), value);
            }
        }
        public Rgba? Color
        {
            get
            {
                if (TryGetData("r", out byte r) && TryGetData("g", out byte g) && TryGetData("b", out byte b) && TryGetData("a", out byte a))
                    return new Rgba(r, g, b, a);

                return null;
            }
            set
            {
                if (value == null)
                {
                    SetData("r", 255);
                    SetData("g", 255);
                    SetData("b", 255);
                    SetData("a", 255);

                    return;
                }

                SetData("r", value.Value.R);
                SetData("g", value.Value.G);
                SetData("b", value.Value.B);
                SetData("a", value.Value.A);
            }
        }

        public void OnWrite(IMValueWriter writer) => writer.WriteObject(p => p
            .WriteProperty(nameof(Text).ToLower(), Text)
            .WriteProperty("r".ToLower(), Color!.Value.R)
            .WriteProperty("g".ToLower(), Color!.Value.G)
            .WriteProperty("b".ToLower(), Color!.Value.B)
            .WriteProperty("a".ToLower(), Color!.Value.A));
    }
}
