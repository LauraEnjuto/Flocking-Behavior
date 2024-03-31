// =====================================================================================================
//  Master en creación de videojuegos UMA                
//    @videojuegosUMA
//    https://www.mastervideojuegos.uma.es/
// =====================================================================================================
//  David Báez de Burgos (@dabamaqui) Pursue, Evade, Path, Wander, SteeringBehaviors 
//  Antonio J. Fernández leiva (@afdezleiva). Arrive and classic implementation of Steeringbehaviors
// =========== A bonfire of souls ============
// =====================================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueBehaviour : SteeringBehaviour
{
    [Header("Pursue")]
    public Transform target;
    public float pursueDistance;
    private float _targetDistance;

    // Si no tiene a quien perseguir se queda parado (0), si tiene objetivo la velocidad varía en función a su proximidad
    protected override float CalculateSpeed()
    {
        if(target == null)
            return 0;
        else
        {
            _targetDistance = Vector3.Distance(transform.position, target.position); // Calcula distancia al objetivo
            if(_targetDistance < pursueDistance) // Si la distancia está por debajo de la deseada de persecución
                return _targetDistance / pursueDistance; // Proporcionamos la velocidad (menos distancia --> menos velocidad)
            else
                return 1; // Devolvemos la velocidad máxima siel objetivo está más lejos de la distancia de persecución
        }
    }


    // Si tiene objetivo se orienta hacia él, si no tiene objetivo se queda mirando al frente
    protected override Vector3 CalculateDirection()
    {
        return target == null ? transform.forward : target.position - transform.position;
    }
}
