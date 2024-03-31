using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrival2 : Steerinbehavior3
{
    public Transform target;
    public float farSlowingRadius;
    public float closeSlowingRadius;

    private float _targetDistance;

 
    protected override float CalculateSpeed()  //Return a value in [0.0,1.0]
    {
        if (target == null)
            return 0;
        else
        {
            _targetDistance = Vector3.Distance(transform.position, target.position); // Calcula distancia al objetivo

            if (_targetDistance < closeSlowingRadius) // Si la distancia est� por debajo de la m�nima ya deseada
                return 0.0f;
            else if (_targetDistance < farSlowingRadius)
                return (_targetDistance-closeSlowingRadius) / (farSlowingRadius-closeSlowingRadius); // Proporcionamos la velocidad (menos distancia --> menos velocidad)
            else
                return 1; // Devolvemos la velocidad m�xima siel objetivo est� m�s lejos de la distancia de persecuci�n
        }
    }
    protected override Vector3 CalculateDir()
    {
        return (target == null) ? transform.forward : target.position - transform.position;
    }
}
