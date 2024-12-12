using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveAndShake : MonoBehaviour
{
    [SerializeField] private Transform button;
    [SerializeField] private Camera cam;
    [SerializeField] private double radius;
    [SerializeField] private float shudder;

    private List<Vector3> startPos = new();
    [SerializeField] private List<Vector3> movement = new();
    [SerializeField] private List<Vector3> endColor = new();
    [SerializeField] private List<GameObject> bodypart = new();
    Vector3 bPos;

    
    
    private void Start()
    {
        bPos = button.position;
        foreach (GameObject g in bodypart)
        {
            startPos.Add(g.transform.localPosition);
        }

        Debug.Log("movemnt list: " + movement.Count);
        Debug.Log("color list: " + endColor.Count);
        Debug.Log("bodypart list: " + bodypart.Count);
    }

    private void Update()
    {
        Vector3 mPos = Input.mousePosition;
        double distance = Vector3.Distance(mPos, bPos);

        //Debug.Log("Mouse position: " + mPos  +"\nDist: " + distance);
        
        
        if (distance < radius)
        {
            float ratio = (float)(distance / radius);
            for (int i = 0; i < bodypart.Count; i++){

                float rx = Random.Range(-shudder * (1-ratio), shudder * (1-ratio));
                float ry = Random.Range(-shudder * (1-ratio), shudder * (1-ratio));

                bodypart[i].transform.localPosition = Vector3.Lerp((startPos[i] + movement[i]), startPos[i], ratio);
                bodypart[i].transform.localPosition += new Vector3(rx,ry,0f);
                Vector3 cVector = Vector3.Lerp(endColor[i], new Vector3(1f, 1f, 1f), ratio);
                bodypart[i].GetComponent<SVGImage>().color = new Color(cVector.x, cVector.y, cVector.z, 1);
            }
        }
    }

    private void move(GameObject obj)
    {

    }
}
