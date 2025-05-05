using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject gameOverScene;



    private void Awake()
    {
        gameOverScene.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (playerController.playerHp <= 0)
        {
            gameOverScene.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOverScene.SetActive(true);
    }

    public void Restart()
    {
        var s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.name);
    }

    public void ReturntToMainMenu() 
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
