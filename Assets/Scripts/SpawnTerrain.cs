using UnityEngine;

public class SpawnTerrain : MonoBehaviour
{
    private TerrainManager manager;

    private void Start()
    {
        manager = FindFirstObjectByType<TerrainManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.SpawnNext();
        }
    }
}