using System;
using UnityEngine;
using UnityEngine.Events;

public class WinTracker : MonoBehaviour
{
    [SerializeField] private int bossTimeThreshold = 120;
    public int nbAliensDead = 0;
    public int nbPortalsDead = 0;
    public bool winCondition;
    private int requiredAliensDead = AlienSpawnManager.maxNbEnemies;
    private int requiredPortalsDead = 8;
    private float time = 0;
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
        time += Time.deltaTime;
    }

    private void WinScreen()
    {
        if(nbPortalsDead >= requiredPortalsDead && nbAliensDead >= requiredAliensDead)
        {
            winCondition = true;
        }
        winCondition = false;
    }

    private void BossSpawn()
    {
        if(winCondition && time < bossTimeThreshold)
        {
            Finder.Boss.Invoke("SpawnBoss", 0f);
        }
    }
}
