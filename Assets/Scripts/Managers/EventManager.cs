using System;
using System.Collections.Generic;

public class EventManager
{
    public EventHandler<KeyValuePair<CannonObject, bool>> OnCannonProjectileEvent;
    public EventHandler<KeyValuePair<ObjectType, int>> OnUpdatePlayerState;
    public EventHandler<bool> OnLevelTimerIsUp;
    public EventHandler<float> OnLevelTimerUpdate;
}
