// =====================================================================================================
//  Master en creación de videojuegos UMA                
//    @videojuegosUMA
//    https://www.mastervideojuegos.uma.es/
// =====================================================================================================
//  David Báez de Burgos (@dabamaqui) Pursue, Evade, Path, Wander, SteeringBehaviors 
//  Antonio J. Fernández leiva (@afdezleiva). Arrive and classic implementation of Steeringbehaviors
// =========== A bonfire of souls ============
// =====================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeBehaviour : SteeringBehaviour
{
    public float evadeDistance; // Distancia a la que huir de objetivos
    public LayerMask evadeLayers; // Layers de objetos de los que huir

    //private List<Transform> _evadeObjList;
    private Collider[] _collidersToAvoid; // Colliders de los actuales objetos al alcance de los que huir
    private Vector3 _evadeDir;

    //void Awake()
    //{
    //    _evadeObjList = new List<Transform>();
    //}

    protected override void Update()
    {
        //_evadeObjList.Clear();
        // Almacenamos en todo momento los objetos detectados
        _collidersToAvoid = Physics.OverlapSphere(transform.position, evadeDistance, evadeLayers);

        base.Update();
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;

        // Distancia esférica de detección de huida
        Gizmos.DrawWireSphere(transform.position, evadeDistance);

        // Señalización de objetos de los que se huye
        if(_collidersToAvoid != null && _collidersToAvoid.Length > 0)
        {
            Gizmos.color = Color.white;
            foreach(Collider col in _collidersToAvoid)
            {
                if(col.gameObject != this.gameObject)
                    Gizmos.DrawWireSphere(col.transform.position, 0.5f);
            }
        }       

        base.OnDrawGizmosSelected();
    }

    protected override Vector3 CalculateDirection()
    {
        if(NumObjectsToAvoid() == 0) // Si no tenemos que huir de nada nadamos hacia el frente [Se podría combinar con un wanderBehaviour]
            return transform.forward;
        else
        {
            _evadeDir = Vector3.zero;
            foreach(Collider col in _collidersToAvoid) // Para cada objeto detectado
            {
                Debug.DrawRay(transform.position, (transform.position - col.transform.position) * 10f, Color.yellow);
                if(col.gameObject != this.gameObject) // Si no es el propio objeto
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position); // Distancia a ese objeto
                    // Vamos a aplicar el vector de huída en función de la distancia a la que está ese objeto
                    _evadeDir += (transform.position - col.transform.position).normalized * (1 - distance / evadeDistance);
                }
            }
            return _evadeDir; // Devolvemos un vector con la suma ponderada de todos los vectores de huída
        }
    }

    // Si tiene que huir lo hace a velocidad máxima, si no tiene que huir nada a la mitad de velocidad
    protected override float CalculateSpeed()
    {
        return NumObjectsToAvoid() == 0 ? 0.5f : 1;
    }

    // Devuelve el número de objetos de los que huir sin tenerse en cuenta a si mismo, ya que en este caso también pertenece al layer
    private int NumObjectsToAvoid()
    {
        int cont = 0;
        foreach(Collider col in _collidersToAvoid)
        {
            if(col.gameObject != this.gameObject)
                cont++;
        }
        return cont;
    }
}
