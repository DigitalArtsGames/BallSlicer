using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DartsGames.Core;

public class CoinCounter : Singleton<CoinCounter>
{
    public int ComboCount;
    public bool IsComboCounting;
    public int CoinCount;
    public int MaxCoin;

    void Start()
    {
        MaxCoin = PlayerPrefs.GetInt("MaxCoin");
        Enemy.onEnemyDie += DyingReward;
        BladeController.onRotateFinish += ComboReward;
        CoinImageBehaviour.onCoinImageReachCorner += IncreaseVoin;

        UIScript.Instance.CoinCountText.text = MaxCoin.ToString();

    }

    private void OnDisable()
    {
        Enemy.onEnemyDie -= DyingReward;
        BladeController.onRotateFinish -= ComboReward;
        CoinImageBehaviour.onCoinImageReachCorner -= IncreaseVoin;

    }

    private void ComboReward()
    {
        IsComboCounting = false;

        if (IsComboCounting == false && ComboCount >= 3)
        {
            MaxCoin += (int)Mathf.Pow(ComboCount, 2);
            CoinCount += (int)Mathf.Pow(ComboCount, 2);

            UIScript.Instance.ComboText.text = ComboCount.ToString();

        }
    }


    private void DyingReward()
    {
        ComboCount++;

    }

    private void IncreaseVoin()
    {
        CoinCount++;
        MaxCoin++;
        UIScript.Instance.CoinCountText.text = MaxCoin.ToString();
    }
}
