using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehavior2 : MonoBehaviour
{
    [Header("Vbles Publicas")]
    public float maxSpeed;
    public float angularSpeed;

    private float _desiredSpeed;
    private Vector3 _desiredDir;

    // Update is called once per frame
    protected virtual void Update()
    {
        _desiredSpeed= CalculateSpeed();
        _desiredDir = CalculateDirection();

        Rotate();
        Move();
    }

    protected virtual void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_desiredDir), angularSpeed * Time.deltaTime);
    }

    protected virtual void Move()
    {
        //transform.position += _desiredSpeed * transform.forward * Time.deltaTime;
        transform.position += GetCurrentSpeed() * transform.forward * Time.deltaTime;
    }

    public float GetCurrentSpeed()
    {
        return maxSpeed * _desiredSpeed;
    }

    protected abstract float CalculateSpeed();
    protected abstract Vector3 CalculateDirection();

}
