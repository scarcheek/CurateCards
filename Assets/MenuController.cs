using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject options, main;
    [SerializeField] string mainScene;
    public void Start(){
        main.SetActive(true);
        options.SetActive(false);
    }

    public void Play(){
        SceneManager.LoadScene(mainScene);
    }

    public void Options(){
        main.SetActive(false);
        options.SetActive(true);
    }

    public void Quit(){
        Application.Quit();
    }

    void Update(){
    }
}
