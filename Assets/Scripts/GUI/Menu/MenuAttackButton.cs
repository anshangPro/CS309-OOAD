using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class MenuAttackButton : MonoBehaviour, IClickable
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    bool IClickable.IsClicked()
    {
        return true;
    }
}