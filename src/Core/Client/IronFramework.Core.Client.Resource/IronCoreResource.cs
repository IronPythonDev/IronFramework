using AltV.Net.Client;
using AltV.Net.Client.Async;
using IronFramework.Core.Client.Controllers.HelpText;
using IronFramework.Core.Client.Controllers.Interaction;
using IronFramework.Core.Client.Controllers.TextLabels;
using IronFramework.Core.Plugin;

namespace IronFramework.Core.Client.Resource
{
    public partial class IronCoreResource : AsyncResource
    {
        public static IList<IronPlugin> IncludedPlugins { get; set; } = new List<IronPlugin>();

        public override void OnStart()
        {
            InteractionController.Init();

            _=new HelpTextController();
            _=new TextLabelController();

            Alt.OnPlayerSpawn += () =>
            {
                TextLabelController.Create2DTextLabel(new ScreenTextLabel("Iron~r~Framework ~w~Demo", 0.5f, 0.005f, 0.5f, 4, new AltV.Net.Data.Rgba(255, 255, 255, 255)));
            };

            foreach (var method in typeof(IronCoreResource).GetMethods().Where(p => p.Name.Contains("_Plugin"))) method.Invoke(this, null);

            foreach (var plugin in IncludedPlugins) plugin.OnStart();
        }

        public override void OnStop()
        {

        }
    }
}