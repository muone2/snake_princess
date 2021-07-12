using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefebs;
    public float width;
    public float height;

    private Vector2 coinPosition;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.coinMakeReady == true)
            CoinSpawn();

    }

    public void CoinSpawn()
    {
        while(true)
        {
            coinPosition = new Vector2(Random.Range(-width, width), Random.Range(-height, height));
            float a = player.transform.position.x - coinPosition.x;
            float b = player.transform.position.y - coinPosition.y;
            if ((a > 3 || a < -3) && (b > 3 || b < -3))
                break;
        }

        Instantiate(coinPrefebs, coinPosition, Quaternion.identity);
        GameManager.instance.coinMakeReady = false;
    }


}
