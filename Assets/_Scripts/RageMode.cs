using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageMode : MonoBehaviour
{
    [SerializeField] private BoxCollider[] _bladeCollider;

    [SerializeField] private MeleeWeaponTrail[] _trails;

    [SerializeField] private ParticleSystem _rageParticle;

    [SerializeField] private AudioSource _bladeSound;

    private bool rotating;
    [SerializeField] private float _spawnRate = 0.5f;
    private float _nextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        _nextSpawn = Time.time;

    }

    private void OnEnable()
    {

        StartCoroutine(enableBlades());
        _rageParticle.Play();
        rotating = true;

    }


    private void OnDisable()
    {
        for (int i = 0; i < _bladeCollider.Length; i++)
        {
            //_trails[i].Emit = true;
            _bladeCollider[i].enabled = false;
            _trails[i].Emit = false;
            _rageParticle.Stop();
            

        }
        rotating = false;
        LeanTween.scaleX(gameObject, Vector3.one.x, 0.3f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, -1000 * Time.deltaTime, 0);

        if (Time.time > _nextSpawn && rotating)
        {
            _nextSpawn = Time.time + _spawnRate;
            _bladeSound.Play();
        }
    }

    IEnumerator enableBlades()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _bladeCollider.Length; i++)
        {
            //_trails[i].Emit = true;
            _bladeCollider[i].enabled = true;
            _trails[i].Emit = true;
        }

        LeanTween.scaleX(gameObject, 5, 0.8f);

    }
}
