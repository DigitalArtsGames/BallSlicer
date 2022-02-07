using DartsGames.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] public PlayerMovement player;
    [SerializeField] GameObject GameManagerCanvas;
    [SerializeField] AudioSource music;
    public bool IsFinished;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void StartGame()
    {
        player._speed = 4;
        player.IsRunning = true;
        player._playerAnim.Play("Run");
        GameManagerCanvas.SetActive(false);
        music.Play();


    }
}
