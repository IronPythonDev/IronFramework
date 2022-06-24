using AltV.Net.Client;
using IronFramework.Core.Shared.EventsNames;
using IronFramework.Core.Shared.Extensions;

namespace IronFramework.Core.Client.Controllers.HelpText
{

    public static class HelpTextController
    {
        static IList<Shared.Controllers.HelpText> HelpTexts { get; set; } = new List<Shared.Controllers.HelpText>();

        public static void Init()
        {
            Alt.OnServer<IDictionary<string, object>>(ClientEvents.HELPTEXT_CONTROLLER_CREATE_TEXT_EVENT, (helpText) => Task.Run(() => Add(helpText.ToObject<Shared.Controllers.HelpText>())));
        }

        public static void Add(Shared.Controllers.HelpText helpText)
        {
            Alt.Log($"New help text -> Text: {helpText.Text}, Position -> X: {helpText.Position.X}, Y: {helpText.Position.Y}, Z: {helpText.Position.Z}");
            HelpTexts.Add(helpText);
        }
    }
}
