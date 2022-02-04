using System.Collections;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    [SerializeField] ParticleSystem _particle;


    private void OnEnable()
    {
        _particle.Play();
        StartCoroutine(SetFalseAfterFinish());
    }

    IEnumerator SetFalseAfterFinish()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

}
