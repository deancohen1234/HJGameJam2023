using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SeerGame/SeerChallenge", fileName = "SeerChallengee")]
public class SeerChallenge : ScriptableObject
{
    [TextArea]
    public string openingDialogueText;

    [TextArea]
    public string response1;
    [TextArea]
    public string response2;
    [TextArea]
    public string response3;

    [TextArea]
    public string correctReadingResponse;
    [TextArea]
    public string incorrectReadingResponse;

    //NOT 0 based
    public int correctResponse = 0;

    public OmenType connectedOmen;

    public int reward = 10;
    public int loss = 6;

    public bool IsCorrectResponse(int responseIndex) {
        return (responseIndex == correctResponse);
    }
}
