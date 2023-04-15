using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : Pursuit
{
    public Evade(PlayerModel target, Transform origin, float time) : base(target, origin, time) { }

    public override Vector3 GetDir()
    {
        return -base.GetDir();
    }
}
