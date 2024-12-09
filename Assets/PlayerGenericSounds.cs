using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenericSounds : MonoBehaviour
{
	[SerializeField] AudioSource source;
	[SerializeField] AudioClip[] clips;
	public void PlaySound(int soundId)
	{
		source.PlayOneShot(clips[soundId]);
	}
}
