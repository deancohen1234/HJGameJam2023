using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct OmenTypePair {
    public OmenType type;
    public GameObject omenObject;
}

//Singleton
public class OmenManager : MonoBehaviour
{
    public static OmenManager instance;

    [SerializeField]
    private GameObject[] allOmens;

    [SerializeField, NonReorderable]
    private OmenTypePair[] allOmenPairs;

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

    public void SelectOmenByType(OmenType type) {

        for (int i = 0; i < allOmenPairs.Length; i++) {
            if (allOmenPairs[i].type == type) {

                currentOmen = allOmenPairs[i].omenObject;
                break;
            }
        }

        if (currentOmen == null) {
            Debug.LogError("No Omen found!!!");
        }
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
