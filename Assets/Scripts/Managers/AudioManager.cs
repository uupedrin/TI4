using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
	AudioMixer _mixer;
	private float masterVol, musicVol, sfxVol;
	[SerializeField] Slider masterSlider;
	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;
	
	private void Awake()
	{
		_mixer = Resources.Load("MainMixer") as AudioMixer;
	}
	
	void Start()
	{
		LoadAudioPrefs();
	}
	
	public void SetMaster(float value)
	{
		masterVol = value;
		SetMixer();
		SaveAudioPrefs();
	}
	public void SetMusic(float value)
	{
		musicVol = value;
		SetMixer();
		SaveAudioPrefs();
	}
	public void SetSFX(float value)
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
		masterVol = PlayerPrefs.GetFloat("MasterVol", 0);
		musicVol = PlayerPrefs.GetFloat("MusicVol", 0);
		sfxVol = PlayerPrefs.GetFloat("SFXVol", 0);
		masterSlider.value = masterVol;
		musicSlider.value = musicVol;
		sfxSlider.value = sfxVol;
		SetMixer();
	}
	
	void SaveAudioPrefs()
	{
		PlayerPrefs.SetFloat("MasterVol", masterVol);
		PlayerPrefs.SetFloat("MusicVol", musicVol);
		PlayerPrefs.SetFloat("SFXVol", sfxVol);
	}
}
