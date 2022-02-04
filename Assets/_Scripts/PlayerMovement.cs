using UnityEngine;
using DartsGames.Core.Input;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;

    [SerializeField] public GameObject _bowlingPinModel;

    [SerializeField] public Animator _playerAnim;

    [SerializeField] public BladeController blade;
    [SerializeField] public RageMode RageMode;
    
    private float targetPosition;

    [SerializeField] public float _speed;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _damping;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotDamp;
    [SerializeField] private GameObject bladeStick;
    public bool IsRunning = false;

    [SerializeField] Rigidbody[] _boneRbs;

    private void Start()
    {
        Physics.IgnoreLayerCollision(6, 7);
    }
    void Update()
    {
        if (IsRunning)
        {
            _controller.Move(transform.forward * _speed * Time.deltaTime);
            var newX = Mathf.MoveTowards(transform.position.x, targetPosition, _maxSpeed * Time.deltaTime);
            newX = Mathf.Lerp(targetPosition, newX, Mathf.Pow(_damping, Time.deltaTime));
            var move = new Vector3(newX - transform.position.x, 0, 0) + transform.forward * _speed * Time.deltaTime;
            _controller.Move(move);

            move.x = Mathf.Clamp(Vector3.Dot(move, Vector3.right) / move.magnitude, -0.3f, 0.3f) * move.magnitude;
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);



            _bowlingPinModel.transform.rotation = Quaternion.RotateTowards(_bowlingPinModel.transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            _bowlingPinModel.transform.rotation = Quaternion.Lerp(toRotation, _bowlingPinModel.transform.rotation, Mathf.Pow(_rotDamp, Time.deltaTime));

            if (!InputSystem.Instance.isHold) return;

            targetPosition -= InputSystem.Instance.delta.x * (Time.deltaTime * _sensitivity);
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        IsRunning = false;
        blade.enabled = false;
        for (int i = 0; i < _boneRbs.Length; i++)
        {

            _boneRbs[i].isKinematic = false;
        }
        _playerAnim.enabled = false;
        blade.gameObject.SetActive(false);
        bladeStick.gameObject.SetActive(false);

        if (!GameManager.Instance.IsFinished)
        {
            UIScript.Instance.GameOver();

        }
        else
        {
            UIScript.Instance.Win();
        }
    }

}
