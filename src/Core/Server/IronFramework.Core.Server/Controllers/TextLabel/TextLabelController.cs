using AltV.Net.EntitySync;

namespace IronFramework.Core.Server.Controllers.TextLabel
{
    public static class TextLabelController
    {
        public static void Add(Shared.Controllers.TextLabel textLabel)
        {
            AltEntitySync.AddEntity(textLabel);
        }

        //public static Task Add(Shared.Controllers.HelpText helpText, AltV.Net.Elements.Entities.IPlayer player)
        //{
        //    player.Emit(ClientEvents.HELPTEXT_CONTROLLER_CREATE_TEXT_EVENT, helpText);

        //    return Task.CompletedTask;
        //}
    }
}
