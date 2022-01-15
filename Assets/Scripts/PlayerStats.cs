using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Lives;
    public static int Rounds;
    public static int WavesCount;
    public static bool isWin;

    public int startMoney = 400;
    public int startLives = 400;
    
    void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
        isWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
