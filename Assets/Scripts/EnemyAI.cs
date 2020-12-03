using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    /*public TrackingEnemy meleeMode;
    public RangedAttack rangedMode;
    public EnemyPatrol patrolMode;

    public void Update()
    {
        if (meleeMode != null)
        {
            if (meleeMode.DoUpdate()) return;
        }
        if (rangedMode != null)
        {
            if (rangedMode.DoUpdate()) return;
        }
        if (patrolMode != null)
        {
            if (patrolMode.DoUpdate()) return;
        }
    }*/
}
public abstract class AIBehaviour : MonoBehaviour
{
    public abstract bool DoUpdate();
}