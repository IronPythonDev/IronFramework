﻿using AltV.Net.Client;
using AltV.Net.Data;

namespace IronFramework.Core.Client.Controllers
{
    public abstract class BaseSyncedEntityController
    {
        private readonly uint entityType;

        public BaseSyncedEntityController(uint entityType)
        {
            this.entityType = entityType;

            Init();
        }

        public IDictionary<ulong, (Position position, IDictionary<string, object> data)> SyncedEntitiesCache = new Dictionary<ulong, (Position, IDictionary<string, object>)>();

        public virtual void Init()
        {
            Alt.OnServer<ulong, int, Position, IDictionary<string, object>>("entitySync:create",
                (entityId, entityType, position, data) => { if (entityType == this.entityType) Task.Run(() => OnCreateSyncedEntity(entityId, position, data)); });
            Alt.OnServer<ulong, int>("entitySync:create",
                (entityId, entityType) => { if (entityType == this.entityType) Task.Run(() => OnVisibleSyncedEntity(entityId, true)); });
            Alt.OnServer<ulong, int>("entitySync:remove",
                (entityId, entityType) => { if (entityType == this.entityType) Task.Run(() => OnVisibleSyncedEntity(entityId, false)); });
        }

        public virtual void OnCreateSyncedEntity(ulong entityId, Position position, IDictionary<string, object> data)
        {
            SyncedEntitiesCache.Add(entityId, (position, data));
            SyncedEntitiesCache[entityId].data["IsVisible"] = true;
        }

        public virtual void OnVisibleSyncedEntity(ulong entityId, bool isVisible)
        {
            SyncedEntitiesCache[entityId].data["IsVisible"] = isVisible;
        }
    }
}
