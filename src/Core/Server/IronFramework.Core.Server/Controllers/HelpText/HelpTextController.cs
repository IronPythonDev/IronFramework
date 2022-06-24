using AltV.Net.Async;
using IronFramework.Core.Shared.EventsNames;

namespace IronFramework.Core.Server.Controllers.HelpText
{
    public static class HelpTextController
    {
        static IList<Shared.Controllers.HelpText> HelpTexts { get; set; } = new List<Shared.Controllers.HelpText>();

        static HelpTextController()
        {
            AltAsync.OnPlayerConnect += AltAsync_OnPlayerConnect;
        }

        private static Task AltAsync_OnPlayerConnect(AltV.Net.Elements.Entities.IPlayer player, string reason)
        {
            foreach (var text in HelpTexts)
            {
                player.Emit(ClientEvents.HELPTEXT_CONTROLLER_CREATE_TEXT_EVENT, text);
            }

            return Task.CompletedTask;
        }

        public static Task Add(Shared.Controllers.HelpText helpText)
        {
            AltAsync.EmitAllClients(ClientEvents.HELPTEXT_CONTROLLER_CREATE_TEXT_EVENT, helpText);

            HelpTexts.Add(helpText);

            return Task.CompletedTask;
        }
    }
}
