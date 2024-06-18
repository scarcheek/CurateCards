using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawnPlaneScript : MonoBehaviour
{
    [Header("Components")]
    public BoxCollider standArea;
    [SerializeField] private GameObject visitorPrefab;
    [Header("Properties")]
    [SerializeField] private int visitorAmountToSpawn;
    [SerializeField] private float verticalOffset;

    [SerializeField] private List<GameObject> visitors = new List<GameObject>();

    public static VisitorSpawnPlaneScript instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        standArea = GetComponent<BoxCollider>();
        SpawnVisitorAmount(visitorAmountToSpawn);
    }

    private void CheckVisitorAmount()
    {
        if (visitors.Count == 0)
            EventManager.EmitRunFailed("All visitors left the park :(");
    }

    private void Awake()
    {
        EventManager.CurationDone += CheckVisitorAmount;
        EventManager.StartDay += OnStartDay;
    }

    private void OnStartDay()
    {
        if (DayScoreManager.instance != null) SpawnVisitorAmount((int)DayScoreManager.instance.GetTodaysScoreToAchieve()/10);
    }

    private void SpawnVisitorAmount(int amount)
    {
        for (int i = 0; i < visitorAmountToSpawn; i++)
        {
            SpawnAndAddVisitor();
        }
    }

    public void SpawnAndAddVisitor()
    {
        // This Quaternion is equal to Euler Rotation Y -90, used this as it is more efficient than always using Quternion.fromEuler
        GameObject visitor = Instantiate(
            visitorPrefab,
            RandomPointOutBounds(),
            new Quaternion(0, 0.707f, 0, 0.707f),
            transform);
        visitor.GetComponentInChildren<VisitorScript>().standPos = RandomPointInBounds(standArea.bounds, transform.position.y + verticalOffset);
        visitors.Add(visitor);
    }

    public Vector3 RandomPointOutBounds() => RandomPointOutBounds(standArea.bounds, transform.position.y + verticalOffset);
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
