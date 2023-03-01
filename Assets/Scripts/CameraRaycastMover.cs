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
    private Location currentLocation;

    private string lastFocusState;

    private const string RESETTAGNAME = "Reset";

    private void Start() {
        mainCamera = GetComponent<Camera>();

        lastFocusState = RESETTAGNAME;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            CameraRaycast();
        }
    }

    private void CameraRaycast() {

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        if (isOverUI) {
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastMask)) 
        {
            Transform objectHit = hit.transform;

            Debug.Log("Hit: " + objectHit.gameObject.name);


            Location location = objectHit.GetComponent<Location>();
            if (location != null) {
                GotoLocation(location);
            }
            else {
                GotoLocation(objectHit.gameObject.tag);
            }
        }
        else 
        {
            GotoLocation(RESETTAGNAME);
        }
    }

    private void GotoLocation(Location location) {

        //don't go into a location if we are already here
        if (location.triggerName == lastFocusState) {
            return;
        }

        //enter this new location
        location.Enter();

        //if there is a current location exit it
        if (currentLocation != null) {
            if (currentLocation.Equals(location)) {
                //don't accidently buffer a location if we are already here
                return;
            }

            currentLocation.Exit();
        }

        currentLocation = location;

        focusAnimator.SetTrigger(location.triggerName);

        lastFocusState = location.triggerName;
    }

    private void GotoLocation(string triggerName) {

        //don't go into a location if we are already here
        if (triggerName == lastFocusState) {
            return;
        }

        if (currentLocation != null) {
            currentLocation.Exit();
            currentLocation = null;
        }

        focusAnimator.SetTrigger(triggerName);
        lastFocusState = triggerName;
    }


}
