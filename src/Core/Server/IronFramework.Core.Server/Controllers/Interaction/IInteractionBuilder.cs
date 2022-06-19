using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using IronFramework.Core.Shared.Abstractions;

namespace IronFramework.Core.Server.Controllers.Interaction
{
    public interface IInteractionBuilder : IBuilder<Interaction>
    {
        IInteractionBuilder AddPosition(Position position);
        IInteractionBuilder AddHandler(Action<IPlayer> handler);
        IInteractionBuilder SetDescription(string description);
        IInteractionBuilder SetRange(float range);
    }
}
