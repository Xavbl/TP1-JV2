using System;
using UnityEngine;
using UnityEngine.Events;

public class WinTracker : MonoBehaviour
{
    public int nbAliensDead = 0;
    public int nbPortalsDead = 0;
    public bool winCondition;
    private int requiredAliensDead = AlienSpawnManager.maxNbEnemies;
    private int requiredPortalsDead = 8;
    //public static bool winCondition;

    private void Awake()
    {
        EventChannels.OnAlienDeath += UpdateDeadAlienCount;
        EventChannels.OnPortalDeath += UpdateDeadPortalCount;
        EventChannels.OnWin += WinScreen;
    }

    public void UpdateDeadPortalCount(Portal portal)
    {
        nbPortalsDead++;
    }

    public void UpdateDeadAlienCount(Alien alien)
    {
        nbAliensDead++;
    }

    private void Update()
    {
        nbAliensDead++;
        nbPortalsDead++;
    }

    private void WinScreen()
    {
        if(nbPortalsDead >= requiredPortalsDead && nbAliensDead >= requiredAliensDead)
        {
            winCondition = true;
        }
        winCondition = false;
    }
}
