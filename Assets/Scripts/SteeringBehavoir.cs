using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SteeringBehavoir : MonoBehaviour
{
    [Header("Variables Steering")]
    public float maxSpeed;
    public float angularSpeed;

    private float _desiredSpeed;
    private Vector3 _desiredDir;

    protected virtual void Update()
    {
        _desiredSpeed = CalculateSpeed();
        _desiredDir = CalculateDir();

        Rotate();
        Move();
    }

    protected virtual private void Move()
    {
        transform.position += transform.forward * GetCurrentSpeed() * Time.deltaTime;
    }

    protected virtual private void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_desiredDir), angularSpeed * Time.deltaTime);
    }

    private float GetCurrentSpeed()
    {
        return maxSpeed * _desiredSpeed;
    }

    protected abstract float CalculateSpeed();
    protected abstract Vector3 CalculateDir();
}
