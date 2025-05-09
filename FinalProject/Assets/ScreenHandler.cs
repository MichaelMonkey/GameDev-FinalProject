using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenHandler : MonoBehaviour
{
    public void StartGame(){
        PlayerPrefs.SetInt("StartScore", 0);
        PlayerPrefs.SetInt("StartLevel", 0);
        SceneManager.LoadScene("GameScene");
    }
    public void StartGame(int score, int level){
        PlayerPrefs.SetInt("StartScore", score);
        PlayerPrefs.SetInt("StartLevel", level);
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame(){
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
