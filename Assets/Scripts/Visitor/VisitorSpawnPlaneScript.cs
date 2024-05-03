using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawnPlaneScript : MonoBehaviour
{
    [SerializeField] private int visitorCount;
    [SerializeField] private GameObject visitorPrefab;
    [SerializeField] private BoxCollider standArea;

    // Start is called before the first frame update
    void Start()
    {
        standArea = GetComponent<BoxCollider>();
        for (int i = 0; i < visitorCount; i++)
        {
            // This Quaternion is equal to Euler Rotation Y -90, used this as it is more efficient than always using Quternion.fromEuler
            GameObject visitor = Instantiate(
                visitorPrefab,
                RandomPointOutBounds(standArea.bounds, transform.position.y),
                new Quaternion(0, 0.707f, 0, 0.707f),
                transform);
            visitor.GetComponentInChildren<VisitorScript>().standPos = RandomPointInBounds(standArea.bounds, transform.position.y + 1);
        }
    }
    public static Vector3 RandomPointOutBounds(Bounds bounds, float height)
    {
        Vector3 newVector;
        do
        {
            newVector = new(
            Random.Range(bounds.min.x * 5, bounds.max.x),
            height,
            Random.Range(bounds.min.z * 5, bounds.max.z * 5));
        } while (bounds.Contains(newVector));
        return newVector;
    }
    public static Vector3 RandomPointInBounds(Bounds bounds, float height)
    {
        return new(
            Random.Range(bounds.min.x, bounds.max.x),
            height,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
