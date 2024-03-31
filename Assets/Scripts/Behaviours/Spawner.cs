using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{    
    public GameObject spawnPrefab;
    public int spawnCount;
    public float spawnRadius;
    public float areaRadius;

    [Header("Args for flocking")]
    public float neighborhoodDistance = 5f;
    public float separationRadius;

    [Range(0.0f, 1.0f)]
    public float alignmentFactor = 0.25f;

    [Range(0.0f, 1.0f)]
    public float separationFactor = 0.5f;

    [Range(0.0f, 1.0f)]
    public float cohesionFactor = 0.25f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, areaRadius);
    }

    public void Spawn()
    {
        Instantiate(spawnPrefab, Random.insideUnitSphere * spawnRadius, Quaternion.Euler(0, Random.value * 360,0), this.transform);

        //Random color
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.GetComponent<SkinnedMeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }        
    }

    private void Start()
    {
        for(int i=0; i< spawnCount; i++)
        {
            Spawn();
        }        
    }
}
