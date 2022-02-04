using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlusOneBehaviour : MonoBehaviour
{
    [SerializeField] private RectTransform rectTrans;
    private Image thisImage;

    // Start is called before the first frame update
    void Start()
    {
        thisImage = GetComponent<Image>();

        var spawnMoveAnimPos = new Vector3(rectTrans.position.x+25, rectTrans.position.y + 50, rectTrans.position.z);

        LeanTween.move(gameObject, spawnMoveAnimPos, 1f).setEaseOutSine();
        LeanTween.value(gameObject, 1, 0, 0.7f).setEaseInCubic().setOnUpdate((float val) =>
        {
            Color c = thisImage.color;
            c.a = val;
            thisImage.color = c;
        }).setOnComplete(() => { DestroyOnCompleted(); });
    }

    private void DestroyOnCompleted()
    {
        Destroy(gameObject);
    }

}
