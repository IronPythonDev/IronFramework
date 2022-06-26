using AltV.Net.EntitySync;

namespace IronFramework.Core.Server.Controllers.TextLabel
{
    public static class World3DTextLabelController
    {
        public static void Add(Shared.Controllers.World3DTextLabel textLabel)
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
