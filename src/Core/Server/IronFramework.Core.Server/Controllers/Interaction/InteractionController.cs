using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using IronFramework.Core.Server.Controllers.HelpText;
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
            var interaction = interactionBuilder.Build();

            foreach (var position in interaction.Positions) HelpTextController.Add(new Shared.Controllers.HelpText
            {
                Position = position,
                Text = interaction.Description
            });

            Interactions.Add(interaction);
        }
        public static void TriggerPlayer(IPlayer player, Position position)
        {
            var interactions = Interactions.Where(interaction => interaction.Positions.Any(p => p.Distance(position) <= interaction.Range));

            foreach (var interaction in interactions)
                interaction.OnInteraction(player);
        }
    }
}
