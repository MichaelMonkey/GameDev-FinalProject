using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthBarDisplay : MonoBehaviour
{
    public GameObject BarPrefab;
    public List<GameObject> bars;
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

    public void intializeHealthBar(int maxHealth, GameObject barPrefab, Color fadedC, Color activeC, Vector3 position, int rescale){
        BarPrefab = barPrefab;
        fadedColor = fadedC;
        activeColor = activeC;
        bars = new List<GameObject>();
        for(int i = 0; i < maxHealth; i++){
            GameObject newBar = Instantiate(BarPrefab, position, Quaternion.identity, this.transform);
            newBar.transform.localPosition = Vector3.zero;
            newBar.transform.localScale = new Vector3(3f/rescale, 1, 1);
            bars.Add(newBar);
        }
    }

    public void displayHealthBar(int maxHealth, int currentHealth){
        if(bars.Count <= 0){
            return;
        }
        Color barColor;
        for(int i = 0; i < maxHealth; i++){
            if(i < currentHealth){
                barColor = activeColor;
            } else {
                barColor = fadedColor;
            }
            RawImage rawImage = bars[i].GetComponent<RawImage>();
            rawImage.color = barColor;
        }
    }

}
