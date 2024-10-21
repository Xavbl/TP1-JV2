using System;
using UnityEngine;
using UnityEngine.Events;

public class EventChannels : MonoBehaviour
{
    // Joueur
    public static UnityAction<int> OnPlayerHealthChange;
    public static UnityAction<int> OnNumberMissileChange;
    public static UnityAction OnPlayerDeath;

    // Portail
    public static UnityAction<Portal, int> OnPortalHealthChange;
    public static UnityAction<Portal> OnPortalDeath;

    // Ennemi
    public static UnityAction<Alien> OnAlienDeath;

    // Boss
    public static UnityAction<Boss, int> OnBossHealthChange;
    public static UnityAction OnBossDeath;

    // HUD
    public static UnityAction OnWin;
}
