using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float chunkLength = 30f;

    private int lastIndex = -1;
    private Transform lastEndPoint;

    private void Start()
    {
        // Spawn first chunk
        GameObject first = Instantiate(startPrefab, startPoint.position, Quaternion.identity);

        lastEndPoint = GetEndPoint(first);

        for (int i = 0; i < 3; i++)
        {
            SpawnNext();
        }
    }

    public void SpawnNext()
    {
        GameObject chunk = SpawnTerrain(lastEndPoint.position);
        lastEndPoint = GetEndPoint(chunk);
    }

    public GameObject SpawnTerrain(Vector3 position)
    {
        if (prefabs.Length == 0) return null;

        int index;

        do
        {
            index = Random.Range(0, prefabs.Length);
        }
        while (prefabs.Length > 1 && index == lastIndex);

        lastIndex = index;

        //THIS IS THE FIX (prevents overlap)
        Vector3 offset = Vector3.forward * (chunkLength / 2f);

        GameObject chunk = Instantiate(prefabs[index], position + offset, Quaternion.identity);
        return chunk;
    }

    private Transform GetEndPoint(GameObject chunk)
    {
        return chunk.transform.Find("Endpoint");
    }
}