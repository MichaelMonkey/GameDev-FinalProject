using UnityEngine;
using TMPro;
using System;


public class SaveViewer : MonoBehaviour
{
    public ScreenHandler screenHandler;
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
        displaySaveData();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void setText(GameObject gameObject, String text){
        TextMeshProUGUI textBox = gameObject.GetComponent<TextMeshProUGUI>();
        textBox.text = text;
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
        screenHandler.StartGame(s1Score, s1Level);
    }
    public void clickSave2(){
        screenHandler.StartGame(s2Score, s2Level);
    }
    public void clickSave3(){
        screenHandler.StartGame(s3Score, s3Level);
    }
}
