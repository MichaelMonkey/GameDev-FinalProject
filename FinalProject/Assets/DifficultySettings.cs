using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class DifficultySettings : MonoBehaviour
{
    public TMP_Dropdown difficultyDropdown;
    public Toggle playerWarningToggle;
    public Toggle enemyWarningToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int currentIndex = PlayerPrefs.GetInt("difficulty", 0);
        difficultyDropdown.value = currentIndex;
        
        int playerWarning = PlayerPrefs.GetInt("playerWarning", 0);
        if(playerWarning == 0){
            playerWarningToggle.isOn = false;
        } else {
            playerWarningToggle.isOn = true;
        }

        int enemyWarning = PlayerPrefs.GetInt("enemyWarning", 1);
        if(enemyWarning == 0){
            enemyWarningToggle.isOn = false;
        } else {
            enemyWarningToggle.isOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDifficulty(){
        int currentIndex = difficultyDropdown.value;
        PlayerPrefs.SetInt("difficulty", currentIndex);
    }
    
    public void SetPlayeryWarning(bool isOn){
        int warning = 0;
        if(isOn){
            warning = 1;
        }
        PlayerPrefs.SetInt("playerWarning", warning);
    }

    public void SetEnemyWarning(bool isOn){
        int warning = 0;
        if(isOn){
            warning = 1;
        }
        PlayerPrefs.SetInt("enemyWarning", warning);
    }
}
