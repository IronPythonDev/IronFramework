using AltV.Net;
using AltV.Net.Async;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
using IronFramework.Core.Plugin;
using IronFramework.Core.Server.Controllers.Interaction;
using IronFramework.Core.Server.Controllers.TextLabel;

namespace IronFramework.Core.Server
{
    public partial class IronCoreResource : AsyncResource
    {
        public static IList<IronPlugin> IncludedPlugins { get; set; } = new List<IronPlugin>();

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

            TextLabelController.Add(new Shared.Controllers.World3DTextLabel("Vlad1234", new System.Numerics.Vector3(0, 0, 71)) { Scale = 0.4f });
            TextLabelController.Add(new Shared.Controllers.World3DTextLabel("Simple Text", new System.Numerics.Vector3(-12.519726753234863f, 5.703808307647705f, 72f), range: 5) { Scale = 0.4f });

            foreach (var method in typeof(IronCoreResource).GetMethods().Where(p => p.Name.Contains("_Plugin"))) method.Invoke(this, null);

            foreach (var plugin in IncludedPlugins) plugin.OnStart();
        }

        public override void OnStop()
        {

        }
    }
}