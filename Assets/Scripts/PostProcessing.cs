using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
 
public class PostProcessing : MonoBehaviour
{
 
    [SerializeField]
    public PostProcessVolume volume;
    private float BloomIntensityValue=0;
    private float BloomDiffusionValue=0;
 
    private bool cBloom = false;
 
    // Update is called once per frame
    void Update()
    {
        if(cBloom)
        {
            // these values can be integrated into function to allow for continual update
            //on various waves.
            BloomIntensityValue = 25;
            BloomDiffusionValue = 5;
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
        bloom.intensity.value = BloomIntensityValue;
    }
    public void ChangeBloomDiffusionSettings()
    {
        GameObject gameObject = GameObject.Find("Post Process Volume");
        //gets game object
        volume = gameObject.GetComponent<PostProcessVolume>();
        Bloom bloom;
        volume.profile.TryGetSettings(out bloom);
        bloom.diffusion.value = BloomDiffusionValue;
    }

    public void SetChangeBloom(bool state)
    {
        cBloom = state;
    }
}
