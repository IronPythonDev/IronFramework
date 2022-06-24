using AltV.Net.EntitySync;
using IronFramework.Core.Shared.EventsNames;

namespace IronFramework.Core.Server.Controllers.HelpText
{

    public static class HelpTextController
    {
        public static Task Add(Shared.Controllers.HelpText helpText)
        {
            AltEntitySync.AddEntity(helpText);

            return Task.CompletedTask;
        }

        public static Task Add(Shared.Controllers.HelpText helpText, AltV.Net.Elements.Entities.IPlayer player)
        {
            player.Emit(ClientEvents.HELPTEXT_CONTROLLER_CREATE_TEXT_EVENT, helpText);

            return Task.CompletedTask;
        }
    }
}
