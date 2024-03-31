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
        if (transform.position.magnitude > _areaRadius) //Si se sale del �rea
            return -transform.position; //Mirar a (0,0,0)

        Vector3 separation = Vector3.zero; //Separaci�n de otros
        Vector3 alineation = Vector3.zero; //Alineaci�n de la orientaci�n con el grupo
        Vector3 cohesion = Vector3.zero; //Posici�n de movimiento con el grupo
        Vector3 center = Vector3.zero; //Variable de apoyo para cohesi�n

        int neighbourCount = 0; //Contador de los vecinos

        foreach (var flock in flocks) //Para cada uno de los que son como �l
        {
            if (flock == this) continue; //Si es �l, pasamos al siguiente�

            float dist = Vector3.Distance(flock.transform.position, transform.position); //Calculamos la distancia
            if (dist > _neighborhoodDistance) continue; //Si es mayor que la distancia de considerarlo vecino, pasamos a la siguiente interacci�n del bucle for

            neighbourCount++;

            //SEPARATION
            if(dist < _separationRadius) //Si el vecino est� demasiado cerca, por debajo de la distancia de separaci�n
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

        center /= neighbourCount; //Para sacar la posicion media, dividimos la acumulaci�n de posiciones entre el n�mero del vecino
        cohesion = center - transform.position; //Vector direcci�n a la posici�n central que deber�a alcanzar

        return (separation.normalized * _separationFactor
                + cohesion.normalized * _cohesionFactor
                + alineation.normalized * _alignmentFactor); //Devolvemos un vector combinado
    }
}
