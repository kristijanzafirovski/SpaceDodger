using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sound : MonoBehaviour
{
    [SerializeField]
    private Sprite SoundOn;
    [SerializeField]
    private Sprite SoundOff;
    [SerializeField]
    private Button button;
    private int SoundSetting;
    private AudioSource BckgMusic;

    public void Start()
    {
        BckgMusic = GetComponent<AudioSource>();
        SoundSetting = PlayerPrefs.GetInt("SoundSetting",1);
        if (SoundSetting == 1)
        {
            button.image.sprite = SoundOn;
            SoundSetting = 1;
            BckgMusic.Play();
        }
        else
        {
            BckgMusic.Pause();
            button.image.sprite = SoundOff;
            SoundSetting = 0;
        }
    }
    
    public void ChangeImage()
    {
        if (button.image.sprite == SoundOn)
        {
            button.image.sprite = SoundOff;
            SoundSetting = 0;
            BckgMusic.Pause();
        }
        else
        {
            button.image.sprite = SoundOn;
            SoundSetting = 1;
            BckgMusic.Play();   
        }
        PlayerPrefs.SetInt("SoundSetting", SoundSetting);
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("SoundSetting", SoundSetting);
        PlayerPrefs.Save();
    }

}