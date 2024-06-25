using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [Header("Please Enter Your Time to beat here")]

    [Tooltip("Enter your time to beat here")]
    public float TimeToBeat;

    [Header("Timer")]
    public float TimeElpased = 0.0f;

    [Header("Please Drag your 3 coins into here in the order they should be collected in")]
    public List<Coin> Coins;


    [Header("Dont Worry about")]
    Player player;
    public int Score = 0;
    public bool LevelStarted = false;
    public  TextMeshProUGUI TimerText;
    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        bool enoughcoins = Coins.Count < 3 ? false: true;
        Debug.Assert(enoughcoins, "The Score Manager is missing the coins. Please drag them into the script component. Please have them in order");

        player = FindObjectOfType<Player>();

        if(endScreen== null)
            Debug.Log("ScoreManager requires a reference to the end screen. Please drag the end screen into the score manager inspector");

    }

    // Update is called once per frame
    void Update()
    {
        if (LevelStarted == true)
        {
            TimeElpased += Time.deltaTime;
            TimerText.text = TimeElpased.ToString("F2");

            if(TimeElpased >= TimeToBeat) 
            {
                TimerText.color = Color.red;
            }
        }
    }

    public void OnLevelStart()
    {
        LevelStarted = true;
    }

    public void CheckForCoin()
    {
        if (player == null)
            return;

        if(player.GetActiveTile() != null) 
        {
            foreach(Coin coin in Coins) 
            {
                if (player.GetActiveTile().GetPosition() == coin.CoinLocation) 
                {
                    if(coin.Collected == false)
                    {
                        Score++;
                        coin.Collected = true;
                        coin.PlayCoinSound();
                        //coin.gameObject.SetActive(false);
                        Debug.Log("Hit Coin");
                    }
                }

            }
        }
    }
}
