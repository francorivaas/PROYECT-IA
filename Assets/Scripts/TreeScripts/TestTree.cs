using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    public int bullet;
    public bool enemySpotted;
    public int life;

    ITreeNode root;

    public void InitializeTree()
    {
        var dead = new TreeAction(Dead); //esta función la va a hacer en el exectue
        var reload = new TreeAction(Reload);
        var shoot = new TreeAction(Shoot);
        var patrol = new TreeAction(Patrol);

        var hasAmmo = new TreeQuestion(HasAmmo, shoot, reload);
        var hasLoadedGun = new TreeQuestion(HasAmmo, patrol, reload);
        
        //si ve al enemeigo se fija si tiene balas.
        //si tiene balas pasa a hasAmmo, si no tiene pasa a hasLoadedGun
        var enemySpotted = new TreeQuestion(HasSpottedEnemy, hasAmmo, hasLoadedGun);
        var hasLife = new TreeQuestion(HasLife, enemySpotted, dead); //esta función la va a hacer en el exectue

        root = hasLife;
    }

    private void Awake()
    {
        InitializeTree();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            root.Execute();
        }    
    }

    public bool HasLife()
    {
        return life > 0;    
    }

    public bool HasAmmo()
    {
        if (bullet > 0)
            return true;
        else return false;
    }

    public bool HasSpottedEnemy()
    {
        return enemySpotted;
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
