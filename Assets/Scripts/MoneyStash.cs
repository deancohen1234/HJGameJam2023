using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStash : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform spawnTransform;
    public float delayBetweenCoins = 0.2f;

    private Stack<GameObject> coinStack;

    private void Start() {

        coinStack = new Stack<GameObject>();
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

        for (int i = 0; i < numCoins; i++) {

            if (coinStack.Count > 0) {
                GameObject coinToDestroy = coinStack.Pop();

                Destroy(coinToDestroy);
            }
        }
    }

    private IEnumerator _AddCoinSequence(int numCoins) {

        for (int i = 0; i < numCoins; i++) {

            GameObject coin = CreateCoin();
            coinStack.Push(coin);

            yield return new WaitForSeconds(delayBetweenCoins);
        }
    }

    private GameObject CreateCoin() {

        GameObject newCoin = Instantiate(coinPrefab);
        newCoin.transform.position = spawnTransform.position + Random.insideUnitSphere * 0.2f;

        return newCoin;
    }
}
