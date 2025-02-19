using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlowRot : MonoBehaviour
{
    [SerializeField] float rotSpeed = 1;

    void Update(){
        this.transform.Rotate(new Vector3(0,1,0),rotSpeed*Time.deltaTime);
    }
}
