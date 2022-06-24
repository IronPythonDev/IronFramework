using AltV.Net.Client.Async;
using IronFramework.Core.Client.Controllers.HelpText;
using IronFramework.Core.Client.Controllers.Interaction;

namespace IronFramework.Core.Client
{
    public class IronCoreResource : AsyncResource
    {
        public override void OnStart()
        {
            InteractionController.Init();
            HelpTextController.Init();
        }

        public override void OnStop()
        {
            
        }
    }
}