using AltV.Net;
using AltV.Net.Async;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
using IronFramework.Core.Server.Controllers.Interaction;

namespace IronFramework.Core.Server
{
    public class IronCoreResource : AsyncResource
    {
        public override void OnStart()
        {
            AltEntitySync.Init(
                5, 
                (syncrate) => 100, 
                (threadId) => false, 
                (threadCount, repository) => new ServerEventNetworkLayer(threadCount, repository), 
                (entity, threadCount) => entity.Type, 
                (entityId, entityType, threadCount) => entityType,
                (threadId) => threadId switch
                {
                    //MARKER
                    0 => new LimitedGrid3(50_000, 50_000, 75, 10_000, 10_000, 64),
                    //TEXT
                    1 => new LimitedGrid3(50_000, 50_000, 75, 10_000, 10_000, 32),
                    //PROP
                    2 => new LimitedGrid3(50_000, 50_000, 100, 10_000, 10_000, 1500),
                    //HELP TEXT
                    3 => new LimitedGrid3(50_000, 50_000, 100, 10_000, 10_000, 10),
                    _ => new LimitedGrid3(50_000, 50_000, 175, 10_000, 10_000, 115),
                },
                new IdProvider());

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