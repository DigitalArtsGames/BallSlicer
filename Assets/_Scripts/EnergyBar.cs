using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Image _energyBar;
    private bool _rageMode;
    private float _energyReduceRate = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Enemy.onEnemyDie += IncreaseEnergy;
        
    }

    private void OnDisable()
    {
        Enemy.onEnemyDie -= IncreaseEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (_energyBar.fillAmount > 0)
        {
            _energyBar.fillAmount -= _energyReduceRate * Time.deltaTime;

        }
        else if(_energyBar.fillAmount <=0 && _rageMode)
        {
            RageModeOff();

        }

    }

    private void IncreaseEnergy()
    {
        if (_rageMode)
        {
            return;
        }
        _energyBar.fillAmount += 0.075f;

        if (_energyBar.fillAmount >= 1)
        {

            RageModeOn();

        }
    }

    private void RageModeOn()
    {
        _rageMode = true;

        _energyReduceRate = 0.2f;

        GameManager.Instance.player._playerAnim.Play("RageMode");
        GameManager.Instance.player.blade.enabled = false;
        GameManager.Instance.player.RageMode.enabled = true;
    }

    private void RageModeOff()
    {
        _rageMode = false;

        _energyReduceRate = 0.1f;
        if (!SwitchToFinalStage.isReachedCenter)
        {
            GameManager.Instance.player._playerAnim.Play("Run");

        }
        else 
        {
            Debug.Log(SwitchToFinalStage.isReachedCenter);
            GameManager.Instance.player._playerAnim.Play("FinishIdle");
        }

        GameManager.Instance.player.blade.enabled = true;
        GameManager.Instance.player.RageMode.enabled = false;
    }
}
