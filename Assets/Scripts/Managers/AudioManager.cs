using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	AudioMixer _mixer;
	private int masterVol, musicVol, sfxVol;
	
	private void Awake()
	{
		_mixer = Resources.Load("MainMixer") as AudioMixer;
		LoadAudioPrefs();
	}
	
	public void SetMaster(int value)
	{
		masterVol = value;
		SetMixer();
		SaveAudioPrefs();
	}
	public void SetMusic(int value)
	{
		musicVol = value;
		SetMixer();
		SaveAudioPrefs();
	}
	public void SetSFX(int value)
	{
		sfxVol = value;
		SetMixer();
		SaveAudioPrefs();
	}
	
	void SetMixer()
	{
		_mixer.SetFloat("MasterVol", masterVol);
		_mixer.SetFloat("MusicVol", musicVol);
		_mixer.SetFloat("SFXVol", sfxVol);
	}
	
	void LoadAudioPrefs()
	{
		masterVol = PlayerPrefs.GetInt("MasterVol", 0);
		musicVol = PlayerPrefs.GetInt("MusicVol", 0);
		sfxVol = PlayerPrefs.GetInt("SFXVol", 0);
		SetMixer();
	}
	
	void SaveAudioPrefs()
	{
		PlayerPrefs.SetInt("MasterVol", masterVol);
		PlayerPrefs.SetInt("MusicVol", musicVol);
		PlayerPrefs.SetInt("SFXVol", sfxVol);
	}
}
