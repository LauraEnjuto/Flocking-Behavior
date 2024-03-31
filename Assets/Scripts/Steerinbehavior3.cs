using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Steerinbehavior3 : MonoBehaviour
{
    [Header("vbles. Steering")]
    public float maxSpeed;
    public float angularSpeed;

    private float _desiredSpeed;
    private Vector3 _desiredDir;
    public float factorCOrrectivo = 5.0f;

    protected virtual void Update()
    {
        _desiredSpeed = CalculateSpeed(); //[0.0,1.0]
        _desiredDir = CalculateDir();

        Rotate();
        Move();
    }

    protected virtual void Move()
    {
        transform.position += transform.forward * GetCurrentSpeed() * Time.deltaTime;
    }

    protected virtual void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_desiredDir), angularSpeed * Time.deltaTime);
        /*
        Vector3 currentVelocity = transform.forward;
        Vector3 steering = _desiredDir - currentVelocity;
        steering = steering.normalized * factorCOrrectivo;

        transform.forward = (currentVelocity + steering).normalized*Time.deltaTime;
        */
    }


    private float GetCurrentSpeed()
    {
        return _desiredSpeed * maxSpeed;
    }
    
    protected abstract float CalculateSpeed();  //Return a value in [0.0,1.0] 
    protected abstract Vector3 CalculateDir();


}
