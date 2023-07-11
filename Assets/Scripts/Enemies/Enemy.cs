using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static Action OnEnemyStarted;
    public static Action OnEnemyDied;
}
