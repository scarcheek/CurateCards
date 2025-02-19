using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject options, main, credits;
    [SerializeField] string mainScene;

    private Animator anim;

    public void Start(){
        anim = GetComponent<Animator>();
        main.SetActive(true);
        options.SetActive(false);
        credits.SetActive(false);
    }

    public void Reset(){
        main.SetActive(true);
        options.SetActive(false);
        credits.SetActive(false);
        anim.Play("Idle", -1, 0f);
    }

    public void Play(){
        SceneManager.LoadSceneAsync(mainScene);
    }

    public void Options(){
        main.SetActive(false);
        options.SetActive(true);
    }

    public void Credits(){
        main.SetActive(false);
        credits.SetActive(true);
        anim.Play("Credits", -1, 0f);
    }

    public void Quit(){
        Application.Quit();
    }

}
