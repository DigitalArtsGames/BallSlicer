using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchToFinalStage : MonoBehaviour
{
    [SerializeField] Transform _center;
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera finishStageCam;
    [SerializeField] ParticleSystem[] _confettis;
    [SerializeField] AudioSource _winSound;
    private EnemySpawner[] enemySpawners;
    public static event System.Action onPlayerFinish;
    public static bool isReachedCenter = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().enabled = false;
            other.GetComponent<PlayerMovement>()._bowlingPinModel.transform.rotation = Quaternion.identity;
            other.gameObject.LeanMove(_center.position, 3).setOnComplete(()=> { ReachCenter(); });
            playerCam.Priority = 0;
            finishStageCam.Priority = 1;
            GameManager.Instance.IsFinished = true;
            PlayParticles();
            _winSound.Play();
            onPlayerFinish?.Invoke();
        }
    }

    private void ReachCenter()
    {
        GameManager.Instance.player._playerAnim.Play("FinishIdle");
        isReachedCenter = true;
    }

    private void PlayParticles()
    {
        for (int i = 0; i < _confettis.Length; i++)
        {
            _confettis[i].Play();
        }
    }

}
