using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Febucci.UI;

public class Entrance : Location
{
    public SeerChallenge[] allChallenges;
    public MoneyStash moneyStash;

    public GameObject dialoguePanel;
    public TextAnimatorPlayer dialogueTextPlayer;

    [Space(10)]

    public GameObject optionsPanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI clientNameText;

    [Space(10)]

    public TextMeshProUGUI response1Text;
    public TextMeshProUGUI response2Text;
    public TextMeshProUGUI response3Text;

    //only display options once we enter the entrance for the second
    private SeerChallenge currentChallenge;

    private List<SeerChallenge> availableChallenges;

    private bool responseTextComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        availableChallenges = new List<SeerChallenge>(allChallenges);

        dialogueText.text = "";

        if (moneyStash != null) {
            moneyStash.coinCountFinished += CoinCountFinished;
        }
    }

    private void OnDestroy() {
        if (moneyStash != null) {
            moneyStash.coinCountFinished -= CoinCountFinished;
        }
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

        OmenManager.instance.SelectOmenByType(currentChallenge.connectedOmen);
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

            //Add money!
            moneyStash.AddCoins(currentChallenge.reward);
        }
        else {
            clientResponse = currentChallenge.incorrectReadingResponse;
            moneyStash.RemoveCoins(currentChallenge.loss);
        }

        dialogueTextPlayer.ShowText(clientResponse);
        optionsPanel.SetActive(false);

        SetIsLocking(true);

        dialogueTextPlayer.onTextShowed.AddListener(OnReponseShowed);

        //the adding/removing of coins is now going to call CoinCountFinished 
    }

    private void SetSeerChallengeText() {

        if (currentChallenge == null) {
            Debug.LogError("Current Challenge is null!!");
        }

        dialoguePanel.SetActive(true);

        clientNameText.text = currentChallenge.clientName;
        dialogueTextPlayer.ShowText(currentChallenge.openingDialogueText);
        //dialogueText.text = currentChallenge.openingDialogueText;

        response1Text.text = currentChallenge.response1;
        response2Text.text = currentChallenge.response2;
        response3Text.text = currentChallenge.response3;
    }

    private void SelectSeerChallenge() {

        //all challenges complete!
        if (allChallenges.Length == 0) {
            GameManager.instance.WinGame();
            return;
        }
        
        currentChallenge = availableChallenges[Random.Range(0, allChallenges.Length)];
        availableChallenges.Remove(currentChallenge);
    }

    private void CoinCountFinished() {
        StartCoroutine(_WaitForNewClientSequence());
    }

    private void OnReponseShowed() {
        responseTextComplete = true;
    }

    private IEnumerator _WaitForNewClientSequence() {

        yield return new WaitUntil(() => responseTextComplete);

        yield return new WaitForSeconds(1.5f);

        //hide UI panel since client left
        dialoguePanel.SetActive(false);
        responseTextComplete = false;

        yield return new WaitForSeconds(2.5f);

        AcceptNewClient();

        SetIsLocking(false);
    }
}
