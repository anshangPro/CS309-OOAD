using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestShootTextPro : MonoBehaviour
{
    ShootTextProController shootTextProController;

    void Start()
    {
        shootTextProController = GetComponent<ShootTextProController>();
    }


    void Update()
    {
        #region obsolete
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shootTextProController.DelayMoveTime = 0.4f;
            shootTextProController.textAnimationType = TextAnimationType.Burst;
            shootTextProController.CreatShootText("+12345", transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shootTextProController.DelayMoveTime = 0.0f;
            shootTextProController.textAnimationType = TextAnimationType.Normal;
            shootTextProController.CreatShootText("+678910", transform);
        }
        #endregion
    }
}
