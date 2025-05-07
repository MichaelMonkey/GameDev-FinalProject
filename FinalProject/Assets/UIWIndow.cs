using UnityEngine;

public class UIWIndow : MonoBehaviour
{
    public void OpenWindow(){
        GetComponent<Canvas>().enabled = true;
    }
    public void CloseWindow(){
        GetComponent<Canvas>().enabled = false;
    }
}
