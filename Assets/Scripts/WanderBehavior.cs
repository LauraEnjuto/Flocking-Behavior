using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehavior : SteeringBehavoir
{
    public float targetDistance = 1.0f;

    private Vector3 _targetPosition;
    private Vector2 _posXZ;

    void Start()
    {
        UpdateTargetPosition();
    }

    private void UpdateTargetPosition()
    {
        _posXZ = UnityEngine.Random.insideUnitCircle * 25;
        _targetPosition = new Vector3(_posXZ.x, 0.5f, _posXZ.y);
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, _targetPosition) < targetDistance)
        {
            UpdateTargetPosition();
        }

        base.Update();
    }

    protected override float CalculateSpeed()
    {
        return 1.0f;
    }
    protected override Vector3 CalculateDir()
    {
        return _targetPosition - transform.position;
    }
}
