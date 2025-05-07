using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingSettings : MonoBehaviour
{
    public Volume volume;
    public Slider contrastSlider;
    ColorAdjustments colorAdjustments;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!volume.profile.TryGet(out colorAdjustments)){
            Debug.LogError("No color adjustments found");
        }
        contrastSlider.value = PlayerPrefs.GetFloat("contrast", 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetContrast(){
        colorAdjustments.contrast.value = contrastSlider.value;
        PlayerPrefs.SetFloat("contrast", contrastSlider.value);
    }
}
