using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
 


public class PostProcessing : MonoBehaviour
{
 
    [SerializeField]
    public PostProcessVolume volume;
    private float _bloomIntensityValue=0;
    private float _bloomDiffusionValue=0;
 
    private bool cBloom = false;
    [SerializeField]
    private Color[] _colors;
    // Update is called once per frame
    void Update()
    {
        if(cBloom)
        {
            // these values can be integrated into function to allow for continual update
            //on various waves.
            _bloomIntensityValue = 25;
            _bloomDiffusionValue = 5;
            ChangeBloomIntensitySettings();
             
            ChangeBloomDiffusionSettings();
             

        }
 
    }

   
    public void ChangeBloomIntensitySettings()
    {
        GameObject gameObject = GameObject.Find("Post Process Volume");
        //gets game object
        //this is the global post processing object.
        volume = gameObject.GetComponent<PostProcessVolume>();
        //Currenlty just set for bloom but could be used for any setting. 
        Bloom bloom;
        volume.profile.TryGetSettings(out bloom);
        bloom.intensity.value = _bloomIntensityValue;
    }
    public void ChangeBloomDiffusionSettings()
    {
        GameObject gameObject = GameObject.Find("Post Process Volume");
        //gets game object
        volume = gameObject.GetComponent<PostProcessVolume>();
        Bloom bloom;
        volume.profile.TryGetSettings(out bloom);
        bloom.diffusion.value = _bloomDiffusionValue;
    }

    public void ChangeBloomColor()
    {
        GameObject gameObject = GameObject.Find("Post Process Volume");
        //gets game object
        //this is the global post processing object.
        volume = gameObject.GetComponent<PostProcessVolume>();
        //Currenlty just set for bloom but could be used for any setting. 
        Bloom bloom;

        volume.profile.TryGetSettings(out bloom);
        //#0071FF

        //#FF1B1B
        if (GameValues.wave == 0) GameValues.wave = 1;
         Color color = _colors[GameValues.wave-1];
        var colorParameter = new UnityEngine.Rendering.PostProcessing.ColorParameter();
        colorParameter.value = color;
        bloom.color.Override(colorParameter);
         
    }

    public void SetChangeBloom(bool state)
    {
        cBloom = state;
    }
}
