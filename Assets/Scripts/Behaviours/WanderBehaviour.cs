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

public class WanderBehaviour : SteeringBehaviour
{
    [Header("Wander")]
    public float areaRadious; // Define el radio del área esférica en la que se mueve el pez

    private Vector3 _targetPosition;

    void Start()
    {
        UpdateTargetPosition();
    }

    // Añadimos a la llamada del Update el control de alcanzar la posición objetivo con un margen
    protected override void Update()
    {
        if(Vector3.Distance(transform.position, _targetPosition) < 1)
            UpdateTargetPosition();

        base.Update();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        // Área de movimiento
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Vector3.zero, areaRadious);

        // Posición objetivo
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_targetPosition, .5f);
    }

    // La velocidad será la máxima en este caso (1)
    protected override float CalculateSpeed()
    {
        return 1;
    }

    // Dirección a la posición objetivo
    protected override Vector3 CalculateDirection()
    {
        return _targetPosition - transform.position;
    }

    // Configura una nueva posición objetivo
    private void UpdateTargetPosition()
    {
        _targetPosition = Random.insideUnitSphere * areaRadious;
    }
}
