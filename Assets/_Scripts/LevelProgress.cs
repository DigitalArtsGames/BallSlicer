
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelProgress : MonoBehaviour
{

    [Header("UI references :")]
    [SerializeField] private Image uiFillImage;
    [SerializeField] private TMP_Text uiStartText;
    [SerializeField] private TMP_Text uiEndText;

    [Header("Player & Endline references :")]
    private Transform playerTransform;
    //private Transform endLineTransform;

    private Vector3 endLinePosition;


    private float fullDistance;




    private void Start()
    {
        playerTransform = GameManager.Instance.player.transform;
        endLinePosition = FindObjectOfType<SwitchToFinalStage>().transform.position;
        //endLinePosition = endLineTransform.position;
        fullDistance = GetDistance();
        uiStartText.SetText((SceneManager.GetActiveScene().buildIndex + 1).ToString());
        uiEndText.SetText((SceneManager.GetActiveScene().buildIndex + 2).ToString());

    }


    private float GetDistance()
    {
        return Vector3.Distance(playerTransform.position, endLinePosition);
    }


    private void UpdateProgressFill(float value)
    {
        uiFillImage.fillAmount = value;
    }


    private void Update()
    {
        // check if the player doesn't pass the End Line
        if (playerTransform.position.z <= endLinePosition.z)
        {
            float newDistance = GetDistance();
            float progressValue = Mathf.InverseLerp(fullDistance, 0f, newDistance);

            UpdateProgressFill(progressValue);
        }
    }

}
