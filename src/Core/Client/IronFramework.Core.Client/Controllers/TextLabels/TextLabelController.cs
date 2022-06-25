using AltV.Net.Client;
using AltV.Net.Data;

namespace IronFramework.Core.Client.Controllers.TextLabels
{
    public class TextLabelController : BaseSyncedEntityController
    {
        public TextLabelController() : base(1)
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
