using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Entrance : Location
{
    public SeerChallenge[] allChallenges;

    public GameObject uiPanel;
    public GameObject optionsPanel;
    public TextMeshProUGUI dialogueText;

    public TextMeshProUGUI response1Text;
    public TextMeshProUGUI response2Text;
    public TextMeshProUGUI response3Text;

    //only display options once we enter the entrance for the second
    private SeerChallenge currentChallenge;
    private int optionsDisplayCounter;

    // Start is called before the first frame update
    void Start()
    {
        optionsDisplayCounter = 0;
    }

    public override void Enter() {

        AcceptNewClient();
    }

    public override void Exit() {

        uiPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void AcceptNewClient() {
        SelectSeerChallenge();

        SetSeerChallengeText();

        uiPanel.SetActive(true);

        if (optionsDisplayCounter >= 1) {

            optionsPanel.SetActive(true);
        }

        optionsDisplayCounter++;

        OmenManager.instance.SelectRandomOmen();
    }

    public void ResetOptions() {
        optionsDisplayCounter = 0;
    }

    public void SubmitReading(int responseIndex) {
        if (currentChallenge == null) {
            Debug.LogError("Current Challenge empty!");
            return;
        }

        bool correctResponse = currentChallenge.IsCorrectResponse(responseIndex);

        string clientResponse = "";
        if (correctResponse) {
            clientResponse = currentChallenge.correctReadingResponse;
        }
        else {
            clientResponse = currentChallenge.incorrectReadingResponse;
        }

        dialogueText.text = clientResponse;
        optionsPanel.SetActive(false);

        StartCoroutine(_WaitForNewClientSequence());
    }
    
    private void SetSeerChallengeText() {

        if (currentChallenge == null) {
            Debug.LogError("Current Challenge is null!!");
        }

        dialogueText.text = currentChallenge.openingDialogueText;

        response1Text.text = currentChallenge.response1;
        response2Text.text = currentChallenge.response2;
        response3Text.text = currentChallenge.response3;
    }

    private void SelectSeerChallenge() {

        if (allChallenges.Length == 0) {
            Debug.LogError("Seer Challenges Empty!");
            return;
        }
        
        currentChallenge = allChallenges[Random.Range(0, allChallenges.Length)];

        //setup all the dialogue text
        dialogueText.text = "Omg, I have been waiting for someone to tell my future. I have been growing a new squash and I REALLY want it to not die";
    }

    private IEnumerator _WaitForNewClientSequence() {

        ResetOptions();

        yield return new WaitForSeconds(1f);

        //hide UI panel since client left
        uiPanel.SetActive(false);

        yield return new WaitForSeconds(1f);

        AcceptNewClient();
    }
}
