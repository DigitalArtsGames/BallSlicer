using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DartsGames.Core;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : Singleton<UIScript>
{
    public TMP_Text ComboText;
    public TMP_Text ComboTextSlice;
    public TMP_Text ComboTextX;
    public Image CoinImage;
    public Image CoinCountImage;
    public Image PlusOneImage;
    public TMP_Text CoinCountText;
    [SerializeField] TMP_Text _coinCountByLevel;

    public GameObject GameOverPanel;
    public GameObject LevelFinishedPanel;
    public GameObject LevelProgressBar;
    // Start is called before the first frame update
    void Start()
    {
        Enemy.onEnemyDie += DyingReward;
        BladeController.onRotateFinish += ComboReward;
        CoinCountText.text = PlayerPrefs.GetInt("MaxCoin").ToString();
    }

    private void OnDisable()
    {
        Enemy.onEnemyDie -= DyingReward;
        BladeController.onRotateFinish -= ComboReward;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void ComboReward()
    {

        if (CoinCounter.Instance.IsComboCounting == false && CoinCounter.Instance.ComboCount >= 3)
        {

            LeanTween.scale(ComboText.gameObject, Vector3.one, 1f).setEaseOutElastic().setOnComplete(() => { ComboTextComplete(); });
        }
    }

    private void ComboTextComplete()
    {

        LeanTween.scale(ComboText.gameObject, Vector3.zero, 1f).setEaseOutQuint();

        
    }

    private void DyingReward()
    {

        var coinImage = Instantiate(CoinImage, Enemy.DyingEnemyPosImage, Quaternion.identity);
        var plusOneImage = Instantiate(PlusOneImage, Enemy.DyingEnemyPosImage, Quaternion.identity);
        plusOneImage.transform.SetParent(transform);
        coinImage.transform.SetParent(transform);
    }


    public void GameOver()
    {
        if (GameOverPanel.activeSelf == false)
        {
            StartCoroutine(GameOverPanelDelay());
        }
    }

    public void Win()
    {
        if (LevelFinishedPanel.activeSelf == false)
        {
            LevelFinishedPanel.SetActive(true);
            LeanTween.scale(LevelFinishedPanel, Vector3.one, 0.4f).setEaseInCubic();
            _coinCountByLevel.text = CoinCounter.Instance.CoinCount.ToString();
            
            PlayerPrefs.SetInt("MaxCoin", CoinCounter.Instance.MaxCoin);
        }
    }

    IEnumerator GameOverPanelDelay()
    {
        yield return new WaitForSeconds(1);
        GameOverPanel.SetActive(true);
        LevelProgressBar.SetActive(false);
    }
}
