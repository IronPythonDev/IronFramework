using AltV.Net.Client;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Data;

namespace IronFramework.Core.Client.Controllers.TextLabels
{
    public class TextLabelController : BaseSyncedEntityController
    {
        private readonly IRmlDocument rmlDocument;
        private readonly IRmlElement rmlElement;

        private uint drawTick = 0;

        public TextLabelController() : base(1)
        {
            Alt.LoadRmlFont("./Inter-Bold.ttf", "inter-bold", false, false);

            rmlDocument = Alt.CreateRmlDocument("./Label.rml");
            rmlElement = rmlDocument.GetElementById("label-container");
            #region Create RML Text Label
            Rgba color = new(255, 255, 255, 255);
            var text = "VLAD1234";

            rmlElement.AddClass("label");
            rmlElement.AddClass("hide");
            rmlElement.InnerRml = text.Replace("\n", "<br />");
            rmlElement.SetProperty("color", $"rgba({color.R}, {color.G}, {color.B}, {color.A});");
            #endregion

            drawTick = Alt.EveryTick(() =>
            {
                if (Alt.Natives.IsSphereVisible(0, 0, 70, 0.0099999998f))
                {
                    if (rmlElement.HasClass("hide"))
                        rmlElement.RemoveClass("hide");

                    var screen = Alt.WorldToScreen(0, 0, 70);

                    var left = screen.X - (rmlElement.ClientWidth / 2);
                    var top = screen.Y - (rmlElement.ClientHeight / 2);

                    rmlElement.SetProperty("left", $"{left}px");
                    rmlElement.SetProperty("top", $"{top}px");
                }
                else
                {
                    if (rmlElement.HasClass("hide"))
                        rmlElement.AddClass("hide");
                }
            });
        }
    }
}
