using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinImageBehaviour : MonoBehaviour
{
    [SerializeField] private float _tweenTime;
    [SerializeField] private RectTransform rectTrans;

    public static event System.Action onCoinImageReachCorner;

    // Start is called before the first frame update
    void Start()
    {
        var spawnMoveAnimPos = new Vector3(rectTrans.position.x, rectTrans.position.y + 150, rectTrans.position.z);

        LeanTween.scale(gameObject, Vector3.one * 1.5f, _tweenTime).setEaseOutElastic();
        LeanTween.move(gameObject, spawnMoveAnimPos, 0.4f).setEaseOutCubic().setOnComplete(()=> { AfterFirstMoveComplete(); });
    }



    private void AfterFirstMoveComplete()
    {
        LeanTween.move(gameObject, UIScript.Instance.CoinCountImage.transform.position, 0.7f).setEaseInQuint().setOnComplete(()=> { AfterCompleteAnimation(); });

    }

    private void AfterCompleteAnimation()
    {
        Destroy(gameObject);

        onCoinImageReachCorner?.Invoke();
    }
}
