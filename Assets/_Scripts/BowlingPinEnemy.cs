using UnityEngine;

public class BowlingPinEnemy : MonoBehaviour
{
    [SerializeField] GameObject _pinObject;
    [SerializeField] GameObject _slicedPinObject;
    [SerializeField] Rigidbody _rb;
    [SerializeField] Rigidbody[] _slicedPinObjectRb;
    [SerializeField] float _enemySpeed;
    [SerializeField] CapsuleCollider _collider;

    private GameObject _player;


    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade"))
        {
            Die();


        }
    }

    private void Die()
    {
        _pinObject.SetActive(false);
       // _ballSliced.SetActive(true);

        /*_slicedBallRb[0].AddForce(transform.up * 150);
        _slicedBallRb[1].AddForce(transform.right * 100);*/

        //ObjectPooler.Instance.SpawnFromPool("BallSliceParticle", transform.position, transform.rotation);

        Vibrator.Vibrate(100);

        _collider.enabled = false;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;

        var targetPosition = _player.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

        _rb.velocity = direction * _enemySpeed;
    }

}