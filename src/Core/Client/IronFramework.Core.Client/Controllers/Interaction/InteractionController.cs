using AltV.Net.Client;
using IronFramework.Core.Shared.EventsNames;

namespace IronFramework.Core.Client.Controllers.Interaction
{
    public static class InteractionController
    {
        public static void Init()
        {
            Alt.Log("InteractionController inited!");

            Alt.OnKeyUp += Alt_OnKeyUp;
        }

        private static void Alt_OnKeyUp(AltV.Net.Client.Elements.Data.Key key)
        {
            if (key != AltV.Net.Client.Elements.Data.Key.E) return;

            Alt.EmitServer(ServerEvents.INTERACTION_CONTROLLER_PRESS_E_KEY_EVENT);
        }
    }
}
