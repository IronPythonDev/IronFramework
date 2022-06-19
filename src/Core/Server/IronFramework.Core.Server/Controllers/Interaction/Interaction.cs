using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace IronFramework.Core.Server.Controllers.Interaction
{
    public class Interaction
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = "";
        public float Range { get; set; } = 0;
        public IList<Position> Positions { get; set; } = new List<Position>();

        public delegate void OnInteractionDelegate(IPlayer player);
        public event OnInteractionDelegate OnInteractionEvent;
        public void OnInteraction(IPlayer player) => OnInteractionEvent?.Invoke(player);
    }
}
