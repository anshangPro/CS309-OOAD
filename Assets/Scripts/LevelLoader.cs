using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public static LevelLoader Instance { get; private set; }
    
    private static readonly int Start = Animator.StringToHash("Start");
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // private void Update()
    // {
    //     if (loadNextScene)
    //     {
    //         LoadNextScene();
    //     }
    // }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene("SampleScene"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger(Start);
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
