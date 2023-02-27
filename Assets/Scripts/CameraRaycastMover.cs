using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycastMover : MonoBehaviour
{
    [SerializeField]
    private Animator focusAnimator;
    [SerializeField]
    private LayerMask raycastMask;

    private Camera mainCamera;

    private const string RESETTAGNAME = "Reset";

    private void Start() {
        mainCamera = GetComponent<Camera>();
    }

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

        if (Input.GetMouseButtonDown(0)) {
            CameraRaycast();
        }
    }

    private void CameraRaycast() {

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastMask)) 
        {
            Transform objectHit = hit.transform;

            GotoPosition(objectHit.gameObject.tag);
        }
        else 
        {
            GotoPosition(RESETTAGNAME);
        }
    }

    private void GotoPosition(string positionTag) {
        focusAnimator.SetTrigger(positionTag);
    }


}
