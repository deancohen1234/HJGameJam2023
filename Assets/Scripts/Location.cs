using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Location : MonoBehaviour
{
    public string triggerName;

    private bool isActive;

    public virtual void Enter() {
        isActive = true;
    }
    public virtual void Exit() {
        isActive = false;
    }

    public bool IsActive() {
        return isActive;
    }
}
