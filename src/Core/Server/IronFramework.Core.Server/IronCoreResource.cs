using AltV.Net;
using AltV.Net.Async;
using IronFramework.Core.Server.Controllers.Interaction;

namespace IronFramework.Core.Server
{
    public class IronCoreResource : AsyncResource
    {
        public override void OnStart()
        {
            InteractionController.Add(new InteractionBuilder()
                .SetDescription("Simple Interaction")
                .SetRange(2)
                .AddPosition(new AltV.Net.Data.Position(0, 0, 71))
                .AddHandler((p) => Alt.Log($"Interaction: {p.Name}")));
        }

        public override void OnStop()
        {
            
        }
    }
}