using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private string GameSceneName;
    public void OnStartClicked()
    {
        Debug.Log("Loading Main scene");
        SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Single);
    }
}
