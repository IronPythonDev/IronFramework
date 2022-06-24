using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace IronFramework.Core.Server.Controllers.Interaction
{
    public class InteractionBuilder : IInteractionBuilder
    {
        private readonly Interaction interaction;

        public InteractionBuilder()
        {
            interaction = new Interaction
            {
                Id = Guid.NewGuid(),
            };
        }

        public Interaction Build()
        {
            return interaction;
        }

        public IInteractionBuilder AddPosition(Position position)
        {
            interaction.Positions.Add(position);
            return this;
        }
        public IInteractionBuilder SetDescription(string description)
        {
            interaction.Description = description;
            return this;
        }
        public IInteractionBuilder SetRange(float range)
        {
            interaction.Range = range;
            return this;
        }
        public IInteractionBuilder AddHandler(Action<IPlayer> handler)
        {
            interaction.OnInteractionEvent += p => handler(p);
            return this;
        }
    }
}
