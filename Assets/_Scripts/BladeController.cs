using System.Collections;
using UnityEngine;
using DartsGames.Core.Input;

public class BladeController : MonoBehaviour
{
    public static event System.Action onRotateFinish;

    [SerializeField] private Material _bladeMaterial;

    [SerializeField] private BoxCollider[] _bladeCollider;

    [SerializeField] private float _scaleRate;

    [SerializeField] private GameObject _bowlingPinGuy;

    [SerializeField] private AudioSource _bladeSound;
    private bool _rotating;

    private Vector3 _initialScale;

    [SerializeField] private MeleeWeaponTrail[] _trails;

    private int[] _randomBetweenTwoRotation = { -360, 360 };

    private void Start()
    {

        _initialScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        InputSystem.Instance.onMouseUp += StartRotate;
        

    }
    private void OnEnable()
    {
        if (InputSystem.Instance != null)
        {
            InputSystem.Instance.onMouseUp += StartRotate;

        }
       
    }

    private void OnDisable()
    {

        InputSystem.Instance.onMouseUp -= StartRotate;
    }


    private void StartRotate()
    {
        if (!_rotating)
        {
            for (int i = 0; i < _trails.Length; i++)
            {
                _bladeCollider[i].enabled = true;
                _trails[i].Emit = true;

            }
            var color = _bladeMaterial.color;
            color.a = 255f;
            //_bladeMaterial.EnableKeyword("_EMISSION");

            StartCoroutine(Rotate(0.35f));
            StartCoroutine(ScaleDownLerp());
        }
    }

    private void Update()
    {
        transform.rotation = _bowlingPinGuy.transform.rotation;

        if (!InputSystem.Instance.isHold) return;

        transform.localScale += new Vector3(_scaleRate, 0, 0) * Time.deltaTime;

    }

    public IEnumerator Rotate(float duration)
    {
        _rotating = true;
        CoinCounter.Instance.IsComboCounting = true;
        CoinCounter.Instance.ComboCount = 0;
        _bladeSound.Play();
        var randomIndex = Random.Range(0, _randomBetweenTwoRotation.Length);
        var startRotation = transform.eulerAngles.y;
        var endRotation = startRotation + _randomBetweenTwoRotation[randomIndex];
        var t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            var yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % _randomBetweenTwoRotation[randomIndex];
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            _bowlingPinGuy.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }

        RotateFinish();

        onRotateFinish?.Invoke();

    }

    private IEnumerator ScaleDownLerp()
    {
        //var t = 0f;
        yield return new WaitForSeconds(0.15f);
        LeanTween.scaleX(gameObject, 1, 0.3f);
    }

    private void RotateFinish()
    {
        _rotating = false;

        var color = _bladeMaterial.color;
        color.a = 130;
        //_bladeMaterial.DisableKeyword("_EMISSION");

        for (int i = 0; i < _trails.Length; i++)
        {
            _trails[i].Emit = false;
            _bladeCollider[i].enabled = false;

        }
    }

    
}
