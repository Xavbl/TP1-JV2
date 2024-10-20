using System;
using UnityEngine;

public class BossTracker : MonoBehaviour
{
    private int nbAliensDead = 0;
    private int nbPortalsDead = 0;
    private int requiredAliensDead = AlienSpawnManager.maxNbEnemies;
    private int requiredPortalsDead = 8;

    private void Awake()
    {
        EventChannels.OnAlienDeath += UpdateDeadAlienCount;
        EventChannels.OnPortalDeath += UpdateDeadPortalCount;
    }

    private void UpdateDeadPortalCount(Portal portal)
    {
        nbPortalsDead++;
    }

    private void UpdateDeadAlienCount(Alien alien)
    {
        nbAliensDead++;
    }

    private void SpawnBoss()
    {
        if(nbPortalsDead >= requiredPortalsDead && nbAliensDead >= requiredAliensDead)
        {
            Finder.EventChannels.Invoke("PublishBossSpawn", 5f);
        }
    }
}
