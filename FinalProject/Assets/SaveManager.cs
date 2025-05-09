using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;
using System;

public class SaveManager : MonoBehaviour
{

    public GameManager gameManager;
    public int gameScore;
    public int level;

    int s1Score;
    int s1Level;
    int s2Score;
    int s2Level;
    int s3Score;
    int s3Level;
    
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        s1Score = PlayerPrefs.GetInt("s1Score", -1);
        s1Level = PlayerPrefs.GetInt("s1Level", -1);
        s2Score = PlayerPrefs.GetInt("s2Score", -1);
        s2Level = PlayerPrefs.GetInt("s2Level", -1);
        s3Score = PlayerPrefs.GetInt("s3Score", -1);
        s3Level = PlayerPrefs.GetInt("s3Level", -1);
        gatherCurrentData();
    }

    // Update is called once per frame
    void Update()
    {
        displaySaveData();
        gatherCurrentData();
    }

    public void setText(GameObject gameObject, String text){
        TextMeshProUGUI textBox = gameObject.GetComponent<TextMeshProUGUI>();
        textBox.text = text;
    }

    void gatherCurrentData(){
        gameScore = gameManager.gameScore;
        level = gameManager.currentLevel;
    }

    public void displaySaveData(){
        if (s1Score == -1){
            setText(s1, "Empty");
        } else {
            setText(s1, saveText(s1Score, s1Level));
        }
        if (s2Score == -1){
            setText(s2, "Empty");
        } else {
            setText(s2, saveText(s2Score, s2Level));
        }
        if (s3Score == -1){
            setText(s3, "Empty");
        } else {
            setText(s3, saveText(s3Score, s3Level));
        }
    }

    public String saveText(int score, int level){
        return "SCORE: "+score+"\nLEVEL: "+level;
    }

    public void clickSave1(){
        s1Score = gameScore;
        s1Level = level;
        PlayerPrefs.SetInt("s1Score", s1Score);
        PlayerPrefs.SetInt("s1Level", s1Level);
        //leaveGame();
    }
    public void clickSave2(){
        s2Score = gameScore;
        s2Level = level;
        PlayerPrefs.SetInt("s2Score", s2Score);
        PlayerPrefs.SetInt("s2Level", s2Level);
        //leaveGame();
    }
    public void clickSave3(){
        s3Score = gameScore;
        s3Level = level;
        PlayerPrefs.SetInt("s3Score", s3Score);
        PlayerPrefs.SetInt("s3Level", s3Level);
        //leaveGame();
    }
    public void leaveGame(){
        SceneManager.LoadScene("StartScreen");
    }
}
