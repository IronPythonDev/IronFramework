using AltV.Net.Client;
using AltV.Net.Data;

namespace IronFramework.Core.Client.Controllers.TextLabels
{
    public class TextLabel
    {
        private uint drawTick;

        public TextLabel(string text, float x, float y, int scale, int font, Rgba color)
        {
            Text=text;
            X=x;
            Y=y;
            Scale=scale;
            Font=font;
            Color=color;
        }

        public string Text { get; }
        public float X { get; }
        public float Y { get; }
        public int Scale { get; }
        public int Font { get; }
        public Rgba Color { get; }

        public void Draw()
        {
            drawTick = Alt.EveryTick(() =>
            {
                Alt.Natives.BeginTextCommandDisplayText("STRING");
                Alt.Natives.AddTextComponentSubstringPlayerName(Text);
                Alt.Natives.SetTextFont(Font);
                Alt.Natives.SetTextScale(1, Scale);
                Alt.Natives.SetTextWrap(0.0F, 1.0F);
                Alt.Natives.SetTextCentre(true);
                Alt.Natives.SetTextColour(Color.R, Color.G, Color.B, Color.A);
                Alt.Natives.SetTextJustification(0);
                Alt.Natives.EndTextCommandDisplayText(X, Y, 0);
            });
        }
        public void StopDraw() => Alt.ClearEveryTick(drawTick);
    }

    public class World3DTextLabelController : BaseSyncedEntityController
    {
        public World3DTextLabelController() : base(1)
        {
            Alt.EveryTick(StartDrawTick);
        }

        private void StartDrawTick()
        {
            foreach (var entity in SyncedEntitiesCache)
            {
                var data = entity.Value.data;
                var position = entity.Value.position;

                if (!data.TryGetValue("IsVisible", out object? isVisible)) continue;
                if (!(bool)isVisible) continue;

                var color = new Rgba(Convert.ToByte(data["r"]), Convert.ToByte(data["g"]), Convert.ToByte(data["b"]), Convert.ToByte(data["a"]));

                var frameTime = Alt.Natives.GetFrameTime();

                Alt.Natives.SetDrawOrigin(position.X + (frameTime), position.Y + (frameTime), position.Z + (frameTime), 0);
                Alt.Natives.BeginTextCommandDisplayText("STRING");
                Alt.Natives.AddTextComponentSubstringPlayerName((string)data["text"]);
                Alt.Natives.SetTextFont(Convert.ToInt32(data["font"]));
                Alt.Natives.SetTextScale(Convert.ToSingle(data["scale"]), Convert.ToSingle(data["scale"]));
                Alt.Natives.SetTextWrap(0.0f, 1.0f);
                Alt.Natives.SetTextCentre(true);
                Alt.Natives.SetTextProportional(true);
                Alt.Natives.SetTextColour(color.R, color.G, color.B, color.A);

                Alt.Natives.SetTextOutline();
                Alt.Natives.SetTextDropShadow();

                Alt.Natives.EndTextCommandDisplayText(0, 0, 0);
                Alt.Natives.ClearDrawOrigin();
            }
        }
    }
}
