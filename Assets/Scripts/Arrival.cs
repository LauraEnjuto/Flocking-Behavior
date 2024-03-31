using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrival : SteeringBehavoir
{
    public Transform target;
    public float farSlowingRadius;
    public float closeSlowingRadius;

    private float _targetDistance;

    protected override float CalculateSpeed()
    {
        if(target == null)
            return 0;
        
        return 1.0f;
    }
    protected override Vector3 CalculateDir()
    {
        return (target == null) ? transform.forward : target.position - transform.position; 
    }
}
