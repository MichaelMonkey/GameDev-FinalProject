using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingSettings : MonoBehaviour
{
    public Volume volume;
    public Slider contrastSlider;
    public Slider brightnessSlider;

    public Slider recolorSlider;

    ColorAdjustments colorAdjustments;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!volume.profile.TryGet(out colorAdjustments)){
            Debug.LogError("No color adjustments found");
        }
        contrastSlider.value = PlayerPrefs.GetFloat("contrast", 0f);
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness", 0f);
        recolorSlider.value = PlayerPrefs.GetFloat("recolor", 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetContrast(){
        colorAdjustments.contrast.value = contrastSlider.value;
        PlayerPrefs.SetFloat("contrast", contrastSlider.value); 
    }
    public void SetBrightness(){
        colorAdjustments.postExposure.value = brightnessSlider.value;
        PlayerPrefs.SetFloat("brightness", brightnessSlider.value); 
    }
    public void SetRecolor(){
        colorAdjustments.hueShift.value = recolorSlider.value;
        PlayerPrefs.SetFloat("recolor", recolorSlider.value); 
    }
}
