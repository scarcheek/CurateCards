using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawnPlaneScript : MonoBehaviour
{
    [SerializeField] int visitorCount;
    [SerializeField] GameObject visitorPrefab;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        for (int i = 0; i < visitorCount; i++) 
            Instantiate(visitorPrefab, RandomPointInBounds(boxCollider.bounds), new Quaternion(0, 0.707f, 0, 0.707f), transform);
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
