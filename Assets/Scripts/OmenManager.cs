using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton
public class OmenManager : MonoBehaviour
{
    public static OmenManager instance;

    [SerializeField]
    private GameObject[] allOmens;

    private GameObject currentOmen;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    //return name of random omen selected
    public string SelectRandomOmen() {

        currentOmen = allOmens[Random.Range(0, allOmens.Length)];

        return GetOmenName(currentOmen);
    }

    public GameObject GetCurrentOmen() {

        return currentOmen;
    }

    public bool HasActiveOmen() {
        return currentOmen != null;
    }

    private string GetOmenName(GameObject omen) {
        return omen.name;
    }
}
