using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entrance : Location
{
    public GameObject uiPanel;
    public GameObject optionsPanel;
    public TextMeshProUGUI text;

    //only display options once we enter the entrance for the second
    private int optionsDisplayCounter;

    // Start is called before the first frame update
    void Start()
    {
        optionsDisplayCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Enter() {

        uiPanel.SetActive(true);
        text.text = "Omg, I have been waiting for someone to tell my future. I have been growing a new squash and I REALLY want it to not die";

        if (optionsDisplayCounter >= 1) {

            optionsPanel.SetActive(true);

        }

        optionsDisplayCounter++;

        OmenManager.instance.SelectRandomOmen();
    }

    public override void Exit() {

        uiPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void ResetOptions() {
        optionsDisplayCounter = 0;
    }
}
