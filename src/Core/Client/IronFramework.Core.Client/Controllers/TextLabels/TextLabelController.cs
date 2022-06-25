using AltV.Net.Client;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Data;

namespace IronFramework.Core.Client.Controllers.TextLabels
{
    public class TextLabel
    {
        private readonly IRmlDocument rmlDocument;
        private readonly IRmlElement rmlElement;

        private string _text;
        private Rgba _color;

        public TextLabel(IRmlDocument rmlDocument, Position position, string text, Rgba color)
        {
            this.rmlDocument = rmlDocument;

            rmlElement = rmlDocument.CreateElement("div");
            rmlElement.AddClass("label");

            IsVisible = false;
            Position=position;
            Text = text;
            Color = color;
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                rmlElement.InnerRml = value.Replace("\n", "<br />");
            }
        }
        public Rgba Color
        {
            get => _color;
            set
            {
                _color = value;
                rmlElement.SetProperty("color", $"rgba({value.R}, {value.G}, {value.B}, {value.A});");
            }
        }
        public Position Position { get; }
        public bool IsVisible
        {
            get => !rmlElement.HasClass("hide");
            set
            {
                if (value) rmlElement.RemoveClass("hide");
                else rmlElement.AddClass("hide");
            }
        }

        public void Draw()
        {
            if (Alt.Natives.IsSphereVisible(Position.X, Position.Y, Position.Z, 0.0099999998f))
            {
                if (!IsVisible) IsVisible = true;

                var screen = Alt.WorldToScreen(0, 0, 70);

                var left = screen.X - (rmlElement.ClientWidth / 2);
                var top = screen.Y - (rmlElement.ClientHeight / 2);

                rmlElement.SetProperty("left", $"{left}px");
                rmlElement.SetProperty("top", $"{top}px");
            }
            else { if (!IsVisible) IsVisible = false; }
        }
    }

    public class TextLabelController : BaseSyncedEntityController
    {
        private readonly IRmlDocument rmlDocument;
        private readonly IRmlElement rmlContainer;

        private uint drawTick = 0;

        public TextLabelController() : base(1)
        {
            Alt.LoadRmlFont("./Inter-Bold.ttf", "inter-bold", false, false);
            rmlDocument = Alt.CreateRmlDocument("./Label.rml");
            rmlContainer = rmlDocument.GetElementById("label-container");

            #region Create RML Text Label
            IList<TextLabel> labels = new List<TextLabel>()
            {
                new TextLabel(rmlDocument, new Position(0, 0, 70), "Text Label!", new Rgba(255, 255, 255, 255)),
                new TextLabel(rmlDocument, new Position(0, 0, 70), "Text Label 1!", new Rgba(245, 40, 145, 0)),
            };
            #endregion
        }

        public override void OnCreateSyncedEntity(ulong entityId, Position position, IDictionary<string, object> data)
        {
            base.OnCreateSyncedEntity(entityId, position, data);
        }
    }
}
