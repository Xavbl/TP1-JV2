using System;
using UnityEngine;
using UnityEngine.Events;

public class WinTracker : MonoBehaviour
{
    private int nbAliensDead = 0;
    private int nbPortalsDead = 0;
    private int requiredAliensDead = AlienSpawnManager.maxNbEnemies;
    private int requiredPortalsDead = 8;
    public static bool winCondition;

    private void Awake()
    {
        EventChannels.OnAlienDeath += UpdateDeadAlienCount;
        EventChannels.OnPortalDeath += UpdateDeadPortalCount;
        EventChannels.OnWin += WinScreen;
    }

    private void UpdateDeadPortalCount(Portal portal)
    {
        nbPortalsDead++;
    }

    private void UpdateDeadAlienCount(Alien alien)
    {
        nbAliensDead++;
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
