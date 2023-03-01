using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Location : MonoBehaviour
{
    public string triggerName;

    private bool isActive;

    //cannot move from this location
    private bool isLocking;

    public virtual void Enter() {
        isActive = true;
    }
    public virtual void Exit() {
        isActive = false;
    }

    public bool IsActive() {
        return isActive;
    }

    public bool IsLocking() {
        return isLocking;
    }

    public void SetIsLocking(bool _isLocking) {

        this.isLocking = _isLocking;
    }
}
