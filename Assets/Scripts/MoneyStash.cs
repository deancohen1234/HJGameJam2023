using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStash : MonoBehaviour
{
    public delegate void CoinsCountFinished();
    public delegate void NoCoinsRemaining();

    public GameObject coinPrefab;
    public Transform spawnTransform;
    public float delayBetweenCoins = 0.2f;
    public int startingCoins = 10;

    public CoinsCountFinished coinCountFinished;

    private Stack<GameObject> coinStack;

    private void Start() {

        coinStack = new Stack<GameObject>();

        StartCoroutine(_AddCoinSequence(startingCoins));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            AddCoins(5);
        }
    }

    public void AddCoins(int numCoins) {

        StartCoroutine(_AddCoinSequence(numCoins));
    }

    public void RemoveCoins(int numCoins) {

        StartCoroutine(_RemoveCoinSequence(numCoins));
    }

    private IEnumerator _AddCoinSequence(int numCoins) {

        for (int i = 0; i < numCoins; i++) {

            GameObject coin = CreateCoin();
            coinStack.Push(coin);

            yield return new WaitForSeconds(delayBetweenCoins);
        }

        if (coinCountFinished != null) {
            coinCountFinished();
        }
    }

    private IEnumerator _RemoveCoinSequence(int numCoins) {

        for (int i = 0; i < numCoins; i++) {

            if (coinStack.Count > 0) {
                GameObject coinToDestroy = coinStack.Pop();
                Destroy(coinToDestroy);
            }
            else {
                //no coins left, you lose :(
                GameManager.instance.EndGame();
                yield break;
            }

            yield return new WaitForSeconds(delayBetweenCoins);
        }

        if (coinCountFinished != null) {
            coinCountFinished();
        }
    }

    private GameObject CreateCoin() {

        GameObject newCoin = Instantiate(coinPrefab);
        newCoin.transform.position = spawnTransform.position + Random.insideUnitSphere * 0.2f;

        return newCoin;
    }
}
