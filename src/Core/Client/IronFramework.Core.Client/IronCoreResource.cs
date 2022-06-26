using AltV.Net.Client;
using AltV.Net.Client.Async;
using IronFramework.Core.Client.Controllers.HelpText;
using IronFramework.Core.Client.Controllers.Interaction;
using IronFramework.Core.Client.Controllers.TextLabels;

namespace IronFramework.Core.Client
{
    public class IronCoreResource : AsyncResource
    {
        public override void OnStart()
        {
            InteractionController.Init();

            new HelpTextController();
            new TextLabelController();

            Alt.OnPlayerSpawn += () =>
            {
                TextLabelController.Create2DTextLabel(new ScreenTextLabel("Iron~r~Framework ~w~Demo", 0.5f, 0.005f, 0.5f, 4, new AltV.Net.Data.Rgba(255, 255, 255, 255)));
            };
        }

        public override void OnStop()
        {

        }
    }
}