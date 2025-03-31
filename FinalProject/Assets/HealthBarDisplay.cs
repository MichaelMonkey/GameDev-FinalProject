using System;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using TMPro;
//using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : MonoBehaviour
{
    public GameObject BarPrefab;
    public GameObject[] bars;
    public Color fadedColor;
    public Color activeColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void intializeHealthBar(int maxHealth, GameObject barPrefab, Color fadedC, Color activeC, Vector3 position){
        BarPrefab = barPrefab;
        fadedColor = fadedC;
        activeColor = activeC;
        bars = new GameObject[maxHealth];
        for(int i = 0; i < maxHealth; i++){
            GameObject newBar = Instantiate(BarPrefab, position, Quaternion.identity, this.transform);
            bars[i] = newBar;
        }
    }

    public void displayHealthBar(int maxHealth, int currentHealth){
        Color barColor;
        for(int i = 0; i < maxHealth; i++){
            if(i < currentHealth){
                barColor = activeColor;
            } else {
                barColor = fadedColor;
            }
            RawImage rawImage = bars[i].GetComponent<RawImage>();
            rawImage.color = barColor;
            //barImage = bars[i].GetComponent<Image>();
        }
    }

}
