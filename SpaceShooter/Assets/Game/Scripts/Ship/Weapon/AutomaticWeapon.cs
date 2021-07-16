using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : BaseWeapon
{
    private float timeShot = 0;

    public override void HandleInput()
    {
        if (Time.realtimeSinceStartup - timeShot > FireRate)
        {
            Shoot();
            timeShot = Time.realtimeSinceStartup;
        }
    }
}
