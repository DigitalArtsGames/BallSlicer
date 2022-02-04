using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _ball;
    [SerializeField] GameObject _ballSliced;
    [SerializeField] Rigidbody _rb;
    [SerializeField] Rigidbody[] _slicedBallRb;
    [SerializeField] float _enemySpeed;
    [SerializeField] SphereCollider _collider;
    [SerializeField] AudioSource _dieSound;

    private GameObject _player;

    public static event System.Action onEnemyDie;

    public static Vector3 DyingEnemyPosImage;

    void Start()
    {
        _rb.AddForce(transform.up * 400);
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
        _dieSound.Play();

        DyingEnemyPosImage = Camera.main.WorldToScreenPoint(transform.position);

        _ball.SetActive(false);
        _ballSliced.SetActive(true);

        _slicedBallRb[0].AddForce(transform.up * 250);
        _slicedBallRb[1].AddForce(transform.right * 200);

        ObjectPooler.Instance.SpawnFromPool("BallSliceParticle", transform.position, transform.rotation);


        Vibrator.Vibrate(100);

        _collider.enabled = false;

        Destroy(gameObject, 2);

        onEnemyDie?.Invoke();


    }

    private void FixedUpdate()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;

        _rb.AddForce(direction * _enemySpeed);
    }

    

}
