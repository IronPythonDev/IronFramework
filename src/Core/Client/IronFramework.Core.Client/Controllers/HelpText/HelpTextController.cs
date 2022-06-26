using AltV.Net.Client;
using AltV.Net.Data;
using System.Text.Json;

namespace IronFramework.Core.Client.Controllers.HelpText
{

    public class HelpTextController : BaseSyncedEntityController
    {
        public HelpTextController() : base(3)
        {
        }

        public override void OnCreateSyncedEntity(ulong entityId, Position position, IDictionary<string, object> data)
        {
            base.OnCreateSyncedEntity(entityId, position, data);

            ShowHelpText((string)data["Text"]);
        }
        public override void OnVisibleSyncedEntity(ulong entityId, bool isVisible)
        {
            base.OnVisibleSyncedEntity(entityId, isVisible);

            if (isVisible) ShowHelpText((string)SyncedEntitiesCache[entityId].data["Text"]);
        }
        public void ShowHelpText(string text) => Alt.EmitClient("__ironCore:bridge:js:showHelpText", text, 5000);
    }
}
