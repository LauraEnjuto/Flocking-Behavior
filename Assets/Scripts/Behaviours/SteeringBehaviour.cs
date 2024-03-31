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

public abstract class SteeringBehaviour : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float angularSpeed = 180f;

    private Vector3 _desiredDir;
    private float _desiredSpeed;

    private const float _STEERING_CORRECTOR = 5.0f;

    protected virtual void Update()
    {
        _desiredSpeed = CalculateSpeed();
        _desiredDir = CalculateDirection();

        Rotate();
        Move();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        // Draw forward
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);

        // Draw target direction
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _desiredDir.normalized * 2);
    }

    // Mueve hacia el frente el pez a la velocidad actual
    protected virtual void Move()
    {
        transform.position += GetCurrentSpeed() * transform.forward * Time.deltaTime;
    }

    // Rota en todo momento hacia la dirección deseada
    protected virtual void Rotate()
    {
        //VERSION UNITY. dejar Sólola instrucción siguiente.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_desiredDir), angularSpeed * Time.deltaTime);
        
        // VERSIÓN ORIGINAL. Implementación por el método tradicional de los Steering behaviors. generalizable más allá de Unity. Sustituir la sentencia anterior por estas cuatro
        //         El factor _STEERING_CORRECTOR  se emplea para suavizar el movimiento; 
        /*
        Vector3 currentvelocity = transform.forward; //*GetCurrentSpeed()*Time.deltaTime;
        Vector3 steering =  _desiredDir - currentvelocity;
        steering = steering.normalized/_STEERING_CORRECTOR ;
        transform.forward = (currentvelocity+steering).normalized*Time.deltaTime;
        */
    }

    // La velocidad la controlamos con un factor 0 --> 1 que se aplica a la velocidad máxima
    public float GetCurrentSpeed()
    {
        return _desiredSpeed * maxSpeed;
    }

    // Métodos abstractos a implementar por herederos
    protected abstract float CalculateSpeed();
    protected abstract Vector3 CalculateDirection();
}
