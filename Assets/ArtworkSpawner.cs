using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtworkSpawner : MonoBehaviour
{
    public List<CardBehaviour> CardList = new();
    public int executionCount = 5; 
    public float pause = 0.5f;          

    private void Start()
    {
        StartCoroutine(ExecuteWithIrregularPauses(spawn, executionCount, pause));
    }

    private IEnumerator ExecuteWithIrregularPauses(System.Action taskFunction, int times, float pause)
    {
        for (int i = 0; i < times; i++)
        {
            spawn();

            if (i < times - 1)
            {
                float pauseDuration = pause;
                pause += pause;
                yield return new WaitForSeconds(pauseDuration);
            }
        }
    }
    void spawn()
    {
        int rand = UnityEngine.Random.Range(0, CardList.Count-1);
        float randX = UnityEngine.Random.Range(-0.5f,0.5f);
        float randY = UnityEngine.Random.Range(-0.5f,0.5f);
        float randZ = UnityEngine.Random.Range(-0.5f,0.5f);

        
        GameObject artPiece = Instantiate(CardList[rand].cardProps.presentPrefab, transform, false);
        if(artPiece.GetComponent<AudioSource>() != null) artPiece.GetComponent<AudioSource>().enabled = false;
        Rigidbody rb = artPiece.GetComponent<Rigidbody>();
        artPiece.transform.position = artPiece.transform.position + new Vector3(randX, randY, randZ);
        rb.isKinematic = false;
        rb.transform.Rotate(new Vector3(0,1,0),180 + UnityEngine.Random.Range(-20,20));
        rb.transform.Rotate(new Vector3(1,0,0),-10);

        CardList.Remove(CardList[rand]);
    }


}
