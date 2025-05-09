using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
public class ScoreDisplay : MonoBehaviour
{
    int currScore;
    int currDifficulty;
    int currPlayerWarning;
    int currEnemyWarning;
    int highscore;
    int hsBaseScore;
    int hsDifficulty;
    int hsPlayerWarning;
    int hsEnemyWarning;
    public GameObject ScoreLabel;
    public GameObject BaseScore;
    public GameObject DifficultyMultiplier;
    public GameObject PlayerWarning;
    public GameObject EnemyWarning;
    public GameObject FinalMultiplier;
    public TextMeshProUGUI temp;
    public void Start()
    {
        currScore = PlayerPrefs.GetInt("currScore", -1);
        currDifficulty = PlayerPrefs.GetInt("currDifficulty", 0);
        currPlayerWarning = PlayerPrefs.GetInt("currPlayerWarning", 0);
        currEnemyWarning = PlayerPrefs.GetInt("currEnemyWarning", 1);
        if(currScore < 0){
            HideDisplay();
            return;
        }  
        highscore = PlayerPrefs.GetInt("highscore", -1);
        hsBaseScore = PlayerPrefs.GetInt("hsBaseScore", -1);
        hsDifficulty = PlayerPrefs.GetInt("hsDifficulty", 0);
        hsPlayerWarning = PlayerPrefs.GetInt("hsPlayerWarning", 0);
        hsEnemyWarning = PlayerPrefs.GetInt("hsEnemyWarning", 1);
        int newScore = calculateScore();
        if(highscore == -1){
            setNewHighscore(newScore);
        } else if(highscore < newScore){
            setNewHighscore(newScore);
        } else {
            ShowDisplay();
        }
    }
    public void setText(GameObject gameObject, String text){
        TextMeshProUGUI textBox = gameObject.GetComponent<TextMeshProUGUI>();
        textBox.text = text;
    }
    public void addText(GameObject gameObject, String text){
        TextMeshProUGUI textBox = gameObject.GetComponent<TextMeshProUGUI>();
        textBox.text += text;
    }

    public String stringify(int x){
        return ""+x;
    }
    public String stringify(float x){
        return x+"X";
    }

    public void ShowDisplay(){
        GetComponent<Canvas>().enabled = true;
        setText(ScoreLabel, stringify(highscore));
        addText(BaseScore, stringify(hsBaseScore));
        addText(DifficultyMultiplier, stringify(getDifficultyMultiplier(hsDifficulty)));
        addText(PlayerWarning, "+" + stringify(getPlayerWarningyMultiplier(hsPlayerWarning)));
        addText(EnemyWarning, "+" + stringify(getEnemyWarningyMultiplier(hsEnemyWarning)));
        addText(FinalMultiplier, stringify(getMultiplier(hsDifficulty, hsPlayerWarning, hsEnemyWarning)));
        /*
        BaseScore;
    public GameObject DifficultyMultiplier;
    public GameObject PlayerWarning;
    public GameObject EnemyWarning;
    public GameObject FinalMultiplier;
        */
    }
    public void HideDisplay(){
        GetComponent<Canvas>().enabled = false;
    }

    public float getMultiplier(int difficulty, int playerWarning, int enemyWarning){
        float ret = 1f;
        ret *= getDifficultyMultiplier(difficulty) + getPlayerWarningyMultiplier(playerWarning) + getEnemyWarningyMultiplier(enemyWarning);
        return ret;
    }

    public float getDifficultyMultiplier(int difficulty){
        float ret = 1;
        if(difficulty == 1){
            ret *= 2.5f;
        } else if(difficulty == 2){
            ret *= 0.5f;
        } else if(difficulty == 3){
            ret *= 1.25f;
        } 
        return ret;
    }
    public float getPlayerWarningyMultiplier(int playerWarning){
        if(playerWarning == 1){
            return 0.25f;
        }
        return 0f;
    }
    public float getEnemyWarningyMultiplier(int enemyWarning){
        if(enemyWarning == 1){
            return 1.5f;
        }
        return 0f;
    }
    
    public int calculateScore(){
        float multiplier = getMultiplier(currDifficulty, currPlayerWarning, currEnemyWarning);
        int calcScore = (int)(currScore * multiplier);
        return calcScore;
    }

    public void setNewHighscore(int score){
        highscore = score;
        hsBaseScore = currScore;
        hsDifficulty = currDifficulty;
        hsPlayerWarning = currPlayerWarning;
        hsEnemyWarning = currEnemyWarning;
        PlayerPrefs.SetInt("highscore", highscore);
        PlayerPrefs.SetInt("hsBaseScore", hsBaseScore);
        PlayerPrefs.SetInt("hsDifficulty", hsDifficulty);
        PlayerPrefs.SetInt("hsPlayerWarning", hsPlayerWarning);
        PlayerPrefs.SetInt("hsEnemyWarning", hsEnemyWarning);
    }
}

