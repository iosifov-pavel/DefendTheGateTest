using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public EventHandler<KeyValuePair<CannonObject, bool>> OnCannonProjectileEvent;
    public EventHandler<KeyValuePair<ObjectType, int>> OnUpdateLevelState;
    public EventHandler OnLevelTimerIsUp;
    public EventHandler<float> OnLevelTimerUpdate;
}
