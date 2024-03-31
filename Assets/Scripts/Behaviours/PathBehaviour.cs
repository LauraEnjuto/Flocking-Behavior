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

public class PathBehaviour : SteeringBehaviour
{
    [Header("Path")]
    public List<Transform> pathPointsList;

    private Transform _currentPathPoint;

    void Start()
    {
        UpdateTargetWaypoint();
    }

    protected override void Update()
    {
        // Si se acaba de iniciar o ha alcanzado la posición objetivo (con margen), pasamos al siguiente punto de ruta
        if(_currentPathPoint == null || Vector3.Distance(transform.position, _currentPathPoint.position) < 1)
            UpdateTargetWaypoint();

        base.Update();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        // Pinta los puntos de ruta y traza líneas entre estos
        Gizmos.color = Color.yellow;
        for(int i = 0; i < pathPointsList.Count; i++)
        {
            Gizmos.DrawWireSphere(pathPointsList[i].position, 0.5f);

            if(i == pathPointsList.Count - 1)
                Gizmos.DrawLine(pathPointsList[i].position, pathPointsList[0].position);
            else
                Gizmos.DrawLine(pathPointsList[i].position, pathPointsList[i+1].position);
        }

    }
    
    // La velocidad depende de si tiene puntos de ruta (velocidad máx.) o si no tiene (se queda parado)
    protected override float CalculateSpeed()
    {
        return pathPointsList.Count > 0 ? 1 : 0;
    }

    // Si tiene puntos de ruta se orienta hacia el siguiente, si no tiene se queda mirando al frente
    protected override Vector3 CalculateDirection()
    {
        return pathPointsList.Count > 0 ? _currentPathPoint.position - transform.position : transform.forward;
    }

    // Configura el próximo punto de ruta al que dirigirse
    private void UpdateTargetWaypoint()
    {
        if(pathPointsList.Count > 0)
        {
            if(_currentPathPoint == null) // Primera vez
                _currentPathPoint = pathPointsList[0];
            else
            {
                int index = pathPointsList.IndexOf(_currentPathPoint);
                _currentPathPoint = pathPointsList[(index + 1) % pathPointsList.Count];
            }
        }
    }
}
