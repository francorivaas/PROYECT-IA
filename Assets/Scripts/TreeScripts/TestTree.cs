using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    public int bullet;

    public void InitializeTree()
    {
        var dead = new TreeAction(Dead); //esta función la va a hacer en el exectue
        var reload = new TreeAction(Reload);
        var shoot = new TreeAction(Shoot);
        var patrol = new TreeAction(Patrol);

        //var hasAmmo = new TreeQuestion();
        //var hasLife = new TreeQuestion(Dead); //esta función la va a hacer en el exectue
    }

    public bool HasAmmo()
    {
        if (bullet > 0)
            return true;
        else return false;
    }

    public void Dead()
    {
        print("Dead");
    }
    public void Reload()
    {
        print("Reload");
    }

    public void Shoot()
    {
        print("Shoot");
    }

    public void Patrol()
    {
        print("Patrol");
    }
}
