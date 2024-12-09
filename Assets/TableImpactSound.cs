using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TableImpactSound : MonoBehaviour
{
	AudioSource source;
	[SerializeField] AudioClip[] metalSounds;
	
	void Awake()
	{
		source = GetComponent<AudioSource>();
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Metal"))
		{
			Debug.Log("BAM");
			int soundId = Random.Range(0,metalSounds.Length);
			source.pitch = Random.Range(1f, 1.5f);
			source.PlayOneShot(metalSounds[soundId]);
		}
	}
}
