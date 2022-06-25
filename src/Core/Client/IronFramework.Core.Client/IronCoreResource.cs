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
        }

        public override void OnStop()
        {

        }
    }
}