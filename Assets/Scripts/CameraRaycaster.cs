using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    [SerializeField]
    private Animator focusAnimator;

    // Update is called once per frame
    void Update()
    {
        //temp
        if (Input.GetKeyDown(KeyCode.I)) {
            //from raycast get trigger string
            focusAnimator.SetTrigger("MagicMirror");
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            focusAnimator.SetTrigger("Entrance");
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            focusAnimator.SetTrigger("Reset");
        }
    }
}
