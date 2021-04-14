using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is used to manage the audio of the game.
 */

/*
 * This enum used to identify the correct audio clip 
 */
public enum AUDIOTYPE 
{
    HOP,
    PLUNK,
    SQUASH,
    TIME
    
}
[System.Serializable]
public struct AudioClipData // this struct is used store clip data with its enum 
{
    public AUDIOTYPE audioType;
    public AudioClip audioClip;
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // used for singleton
    public AudioSource SFX_Source; // used to play different audio clip
    public AudioClipData[] audioClipDatas; // audio clip data

    private void Awake()
    {
        // Creating singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySound(AUDIOTYPE audioType) // this function used play correct audio based on the argument passed
    {
        for (int i = 0; i < audioClipDatas.Length; i++) // looping throught all the clip array
        {
            if(audioType == audioClipDatas[i].audioType) // finding the correct clip
            {
                SFX_Source.clip = audioClipDatas[i].audioClip; // loading the clip
                SFX_Source.Play(); // playing the music.
            }
        }
    }
    
}
