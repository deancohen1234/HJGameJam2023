using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Febucci.UI;

public class Entrance : Location
{
    public SeerChallenge[] allChallenges;

    public GameObject dialoguePanel;
    public TextAnimatorPlayer dialogueTextPlayer;

    [Space(10)]

    public GameObject optionsPanel;
    public TextMeshProUGUI dialogueText;

    [Space(10)]

    public TextMeshProUGUI response1Text;
    public TextMeshProUGUI response2Text;
    public TextMeshProUGUI response3Text;

    //only display options once we enter the entrance for the second
    private SeerChallenge currentChallenge;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "";
    }

    public override void Enter() {

        if (currentChallenge == null) {
            AcceptNewClient();
        }
        else {

            dialoguePanel.SetActive(true);
            optionsPanel.SetActive(true);
        }
    }

    public override void Exit() {

        dialoguePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void AcceptNewClient() {
        SelectSeerChallenge();

        SetSeerChallengeText();

        OmenManager.instance.SelectRandomOmen();
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

        dialogueTextPlayer.ShowText(clientResponse);
        optionsPanel.SetActive(false);

        StartCoroutine(_WaitForNewClientSequence());
    }
    
    private void SetSeerChallengeText() {

        if (currentChallenge == null) {
            Debug.LogError("Current Challenge is null!!");
        }

        dialoguePanel.SetActive(true);

        dialogueTextPlayer.ShowText(currentChallenge.openingDialogueText);
        //dialogueText.text = currentChallenge.openingDialogueText;

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
    }

    private IEnumerator _WaitForNewClientSequence() {

        yield return new WaitForSeconds(1f);

        //hide UI panel since client left
        dialoguePanel.SetActive(false);

        yield return new WaitForSeconds(1f);

        AcceptNewClient();
    }
}
