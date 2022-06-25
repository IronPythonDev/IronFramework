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

            Alt.Log($"OnCreateSyncedEntity");

            ShowHelpText((string)data["Text"]);
        }
        public override void OnVisibleSyncedEntity(ulong entityId, bool isVisible)
        {
            base.OnVisibleSyncedEntity(entityId, isVisible);

            Alt.Log($"OnVisibleSyncedEntity: {isVisible}");

            if (isVisible) ShowHelpText((string)SyncedEntitiesDataCache[entityId]["Text"]);
        }
        public void ShowHelpText(string text) => Alt.EmitClient("__ironCore:bridge:js:showHelpText", text, 5000);
    }
}
