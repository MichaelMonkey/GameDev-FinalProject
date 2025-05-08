using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingLoader : MonoBehaviour
{
    
    public Volume volume;

    ColorAdjustments colorAdjustments;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!volume.profile.TryGet(out colorAdjustments)){
            Debug.LogError("No color adjustments found");
        }
        colorAdjustments.contrast.value = PlayerPrefs.GetFloat("contrast", 0f);
        colorAdjustments.postExposure.value = PlayerPrefs.GetFloat("brightness", 0f);
        colorAdjustments.hueShift.value = PlayerPrefs.GetFloat("recolor", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
