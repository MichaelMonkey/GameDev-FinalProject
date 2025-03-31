using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenHandler : MonoBehaviour
{

    public void StartGame(){
        SceneManager.LoadScene("GameScene");
    }
}
