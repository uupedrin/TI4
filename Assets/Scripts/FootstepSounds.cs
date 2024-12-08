using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSounds : MonoBehaviour
{
	[Header("Audios")]
	[SerializeField] AudioClip[] floorSounds;
	[SerializeField] AudioClip[] woodSounds;
	[SerializeField] AudioClip[] carpetSounds;
	[SerializeField] AudioClip[] metalSounds;
	AudioSource _source;
	
	[Header("Setup")]
	[SerializeField] float rayRange;
	[SerializeField] LayerMask layers;
	[SerializeField] float floorVolume;
	[SerializeField] float fallVolume;
	
	void Awake()
	{
		_source = GetComponent<AudioSource>();
	}
	
	void Update()
	{
		Debug.DrawRay(transform.position, transform.up * rayRange * -1, Color.green);
	}
	
	public void PlayFootstep(float state)
	{
		if(Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hit, rayRange, layers))
		{
			float volume = state == 0 ? floorVolume : fallVolume;
			_source.pitch = Random.Range(.8f, 1f);
			
			if(hit.collider.CompareTag("Floor"))
			{
				int soundId = Random.Range(0,floorSounds.Length);
				PlaySound(floorSounds[soundId], volume);
			}
			else if(hit.collider.CompareTag("Wood"))
			{
				int soundId = Random.Range(0,woodSounds.Length);
				PlaySound(woodSounds[soundId], volume);
			}
			else if(hit.collider.CompareTag("Carpet"))
			{
				int soundId = Random.Range(0,carpetSounds.Length);
				PlaySound(carpetSounds[soundId], volume);
			}
			else if(hit.collider.CompareTag("Metal"))
			{
				int soundId = Random.Range(0,metalSounds.Length);
				PlaySound(metalSounds[soundId], volume * .5f);
			}
			
		}
	}
	
	void PlaySound(AudioClip clip, float vol)
	{
		_source.volume = vol;
		_source.PlayOneShot(clip);
	}
}
