using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerPrefsLoader : MonoBehaviour
{
    
    public Volume volume;

    ColorAdjustments colorAdjustments;

    public int difficultySetting;
    public int playerHealthMult;
    public int playerWarning;
    public int enemyHealthMult;
    public int enemyWarning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!volume.profile.TryGet(out colorAdjustments)){
            Debug.LogError("No color adjustments found");
        }
        colorAdjustments.contrast.value = PlayerPrefs.GetFloat("contrast", 0f);
        colorAdjustments.postExposure.value = PlayerPrefs.GetFloat("brightness", 0f);
        colorAdjustments.hueShift.value = PlayerPrefs.GetFloat("recolor", 0f);
        
        difficultySetting = PlayerPrefs.GetInt("difficulty", 0);
        if(difficultySetting == 1){
            playerHealthMult = 1;
            enemyHealthMult = 2;
        } else if (difficultySetting == 2){
            playerHealthMult = 2;
            enemyHealthMult = 1;
        } else if (difficultySetting == 3){
            playerHealthMult = 2;
            enemyHealthMult = 2;
        } else {
            playerHealthMult = 1;
            enemyHealthMult = 1;
        }
        playerWarning = PlayerPrefs.GetInt("playerWarning", 0);
        enemyWarning = PlayerPrefs.GetInt("enemyWarning", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void outputScoreMultipliers(int playerScore){
        PlayerPrefs.SetInt("currScore", playerScore);
        PlayerPrefs.SetInt("currDifficulty", difficultySetting);
        PlayerPrefs.SetInt("currPlayerWarning", playerWarning);
        PlayerPrefs.SetInt("currEnemyWarning", enemyWarning);
    }
}
