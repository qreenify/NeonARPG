using System;
using Unit;
using UnityEngine;

public class AttackRangeDebug : MonoBehaviour
{
    public UnitAction attackAction;
    public Transform debugSphere;

    private void LateUpdate()
    {
        var newPos = debugSphere.position;
        newPos.y = 0.1f;
        debugSphere.position = newPos;
    }
    
    private void OnValidate()
    {
        if (attackAction == null || debugSphere == null) return;
        switch (attackAction)
        {
            case UnitMeleeAttack meleeAttack:
                debugSphere.localScale = new Vector3(meleeAttack.range * 2, debugSphere.localScale.y, meleeAttack.range * 2);
                break;
            case UnitRangedAttack rangedAttack:
                debugSphere.localScale = new Vector3(rangedAttack.range * 2, debugSphere.localScale.y, rangedAttack.range * 2);
                break;
        }
    }
}
