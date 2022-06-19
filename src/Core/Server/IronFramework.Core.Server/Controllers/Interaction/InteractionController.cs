using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using IronFramework.Core.Shared.EventsNames;

namespace IronFramework.Core.Server.Controllers.Interaction
{
    public static class InteractionController
    {
        static readonly IList<Interaction> Interactions = new List<Interaction>();

        static InteractionController()
        {
            AltAsync.OnClient<IPlayer, Task>(ServerEvents.INTERACTION_CONTROLLER_PRESS_E_KEY_EVENT, OnPressEKey);
        }

        private static Task OnPressEKey(IPlayer player)
        {
            TriggerPlayer(player, player.Position);

            return Task.CompletedTask;
        }

        public static void Add(IInteractionBuilder interactionBuilder)
        {
            Interactions.Add(interactionBuilder.Build());
        }
        public static void TriggerPlayer(IPlayer player, Position position)
        {
            foreach (var interaction in Interactions.Where(interaction => !interaction.Positions.Any(p => p.Distance(position) <= interaction.Range)))
                interaction.OnInteraction(player);
        }
    }
}
