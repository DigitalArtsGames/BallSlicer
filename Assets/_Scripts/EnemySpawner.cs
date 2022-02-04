using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private ParticleSystem _spawnParticle;
    [SerializeField] AudioSource _spawnSound;
    [SerializeField] public float _spawnRate = 1f;
    private float _nextSpawn;

    private bool _activateSpawning;

    // Start is called before the first frame update
    void Start()
    {
        _nextSpawn = Time.time;
        SwitchToFinalStage.onPlayerFinish += IncreaseCoroutine;

    }

    private void OnDisable()
    {
        SwitchToFinalStage.onPlayerFinish -= IncreaseCoroutine;

    }

    void Update()
    {
        if (_activateSpawning)
        {
            SpawnEnemy();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _activateSpawning = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _activateSpawning = false;

        }
    }

    private void SpawnEnemy()
    {
        var randomIndex = Random.Range(0, 4);
        if (Time.time > _nextSpawn)
        {
            _nextSpawn = Time.time + _spawnRate;
            Instantiate(_enemyPrefab[randomIndex], _spawnPos.position, _spawnPos.rotation);
            _spawnParticle.Play();
            _spawnSound.Play();
        }
    }


    private void IncreaseCoroutine()
    {
        StartCoroutine(IncreaseRate());
    }

    IEnumerator IncreaseRate()
    {
        yield return new WaitForSeconds(3);
        if (_spawnRate>1)
        {
            _spawnRate-= 0.5f;

        }
        Debug.Log("oer");
        StartCoroutine(IncreaseRate());
    }
}
