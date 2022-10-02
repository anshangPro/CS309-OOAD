using System.Collections;
using System.Collections.Generic;
using GUI;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get { return _instance; }
    }

    public GameManager gameManager;
    public ButtonManager buttonManager;
    public TextManager textManager;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
        buttonManager = ButtonManager.Instance;
        textManager = TextManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateGUI()
    {
        buttonManager.UpdateButton();
        textManager.UpdateButton();
    }
}