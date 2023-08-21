using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IdleManager : MonoBehaviour
{
    [HideInInspector]
    public int length;

    [HideInInspector]
    public int strength;

    [HideInInspector]
    public int offlineEarnings;

    [HideInInspector]
    public int lengthCost;

    [HideInInspector]
    public int strengthCost;


    [HideInInspector]
    public int offlineEarningsCost;


    [HideInInspector]
    public int wallet;


    [HideInInspector]
    public int totalGain;

    private int[] cost = new int[]
    {
        120,
        151,
        197,
        250,
        324,
        414,
        537,
        687,
        892,
        1145,
        1484,
        1911,
        2479,
        3196,
        4148,
        5359,
        6954,
        9000,
        11687
    };
    public static IdleManager instance;
    void Awake()
    {
        if (IdleManager.instance)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
        else
            IdleManager.instance = this;

        length = -PlayerPrefs.GetInt("Length", 30);
        strength = PlayerPrefs.GetInt("Strength", 3);
        offlineEarnings = PlayerPrefs.GetInt("OfflineEarnings", 3);
        lengthCost = cost[-length / 10 - 3];
        strengthCost = cost[strength - 3];
        offlineEarningsCost = cost[offlineEarnings - 3];
        wallet = PlayerPrefs.GetInt("Wallet", 0);

    }
    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            DateTime now = DateTime.Now;
            PlayerPrefs.SetString("Date", string.Empty);
        }
        else
        {
            string @string = PlayerPrefs.GetString("Date", string.Empty);
            if (@string != string.Empty)
            {
                DateTime d = DateTime.Parse(@string);
                totalGain = (int)((DateTime.Now - d).TotalMinutes * offlineEarnings + 1.0);
            }
        }
    }
    private void OnApplicationQuit()
    {
        OnApplicationPause(true);
    }
    void BuyLength()
    {
        length -= 10;
        wallet -= lengthCost;
        lengthCost = cost[-length / 10 - 3];
        PlayerPrefs.SetInt("Length", -length);
        PlayerPrefs.SetInt("Wallet", wallet);
    }
    void BuyStrength()
    {
        strength++;
        wallet -= strengthCost;
        strengthCost = cost[strength - 3];
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Wallet", wallet);
    }
    void BuyOfflineEarnings()
    {
        offlineEarnings++;
        wallet -= offlineEarningsCost;
        offlineEarningsCost = cost[offlineEarnings - 3];
        PlayerPrefs.SetInt("OfflineEarnings", offlineEarnings);
        PlayerPrefs.SetInt("Wallet", wallet);

    }
    void CollectMoney()
    {
        wallet += totalGain;
        PlayerPrefs.SetInt("Wallet", wallet);

    }
    void CollectDoubleMoney()
    {
        wallet += totalGain *2 ;
        PlayerPrefs.SetInt("Wallet", wallet);

    }
    void Update()
    {

    }
}
