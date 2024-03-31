using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehavior : SteeringBehaviour
{
    private float _areaRadius;
    private float _neighborhoodDistance;
    private float _separationRadius;
    private float _alignmentFactor;
    private float _separationFactor;
    private float _cohesionFactor;

    private static List<FlockBehavior> flocks = new List<FlockBehavior>();

    private Spawner _spawner;

    private void OnEnable()
    {
        flocks.Add(this);

        _spawner = this.GetComponentInParent<Spawner>();
        _areaRadius = _spawner.areaRadius;
        _neighborhoodDistance = _spawner.neighborhoodDistance;
        _separationRadius = _spawner.separationRadius;
        _alignmentFactor = _spawner.alignmentFactor;
        _separationFactor = _spawner.separationFactor;
        _cohesionFactor = _spawner.cohesionFactor;
    }

    private void OnDisable()
    {
        flocks.Remove(this);
    }

    protected override float CalculateSpeed()
    {
        return 1;
    }

    protected override Vector3 CalculateDirection()
    {
        if (transform.position.magnitude > _areaRadius) //Si se sale del área
            return -transform.position; //Mirar a (0,0,0)

        Vector3 separation = Vector3.zero; //Separación de otros
        Vector3 alineation = Vector3.zero; //Alineación de la orientación con el grupo
        Vector3 cohesion = Vector3.zero; //Posición de movimiento con el grupo
        Vector3 center = Vector3.zero; //Variable de apoyo para cohesión

        int neighbourCount = 0; //Contador de los vecinos

        foreach (var flock in flocks) //Para cada uno de los que son como él
        {
            if (flock == this) continue; //Si es él, pasamos al siguienteç

            float dist = Vector3.Distance(flock.transform.position, transform.position); //Calculamos la distancia
            if (dist > _neighborhoodDistance) continue; //Si es mayor que la distancia de considerarlo vecino, pasamos a la siguiente interacción del bucle for

            neighbourCount++;

            //SEPARATION
            if(dist < _separationRadius) //Si el vecino está demasiado cerca, por debajo de la distancia de separación
            {
                float n = 1 - dist / _separationRadius; //Factor de importancia, inversamente proporcional
                separation += (transform.position - flock.transform.position).normalized * n; //Acumulamos ese vector al total
            }

            //ALINEATION
            alineation += flock.transform.forward; //Acumula en el total el vector frente de ese vecino

            //COHESION
            center += flock.transform.position + flock.transform.forward; //Acumulamos todas las posiciones de los vecinos desplazados

        }

        if (neighbourCount == 0) return transform.forward;

        center /= neighbourCount; //Para sacar la posicion media, dividimos la acumulación de posiciones entre el número del vecino
        cohesion = center - transform.position; //Vector dirección a la posición central que debería alcanzar

        return (separation.normalized * _separationFactor
                + cohesion.normalized * _cohesionFactor
                + alineation.normalized * _alignmentFactor); //Devolvemos un vector combinado
    }
}
