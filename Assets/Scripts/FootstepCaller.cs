using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FootstepCaller : MonoBehaviour
{
	Animator _animator;
	AnimationClip animClip;
	[SerializeField] FootstepSounds ftSound;
	AnimationEvent animEvent;
	float state; //0 - Running | 1 - Falling
	void Awake()
	{
		_animator = GetComponent<Animator>();
		animEvent = new AnimationEvent();
		
		RunAnimationEvents();
		FallAnimationEvents();
	}
	
	void RunAnimationEvents()
	{
		animClip = GetClipByName("JanitorRig|Run Cycle");
		animEvent.floatParameter = 0;
		animEvent.functionName = nameof(CallFootstepSound);
		animEvent.time = animClip.length * .06f;
		animClip.AddEvent(animEvent);
		animEvent.time = animClip.length * .52f;
		animClip.AddEvent(animEvent);
	}
	
	void FallAnimationEvents()
	{
		animClip = GetClipByName("JanitorRig|Jump Cycle End");
		animEvent.floatParameter = 1;
		animEvent.functionName = nameof(CallFootstepSound);
		animEvent.time = animClip.length * .9f;
		animClip.AddEvent(animEvent);
	}
	
	public void CallFootstepSound(float param)
	{
		ftSound.PlayFootstep(param);
	}
	
	AnimationClip GetClipByName(string name)
	{
		RuntimeAnimatorController controller = _animator.runtimeAnimatorController;

		if (controller != null)
		{
			AnimationClip[] clips = controller.animationClips;
			foreach (AnimationClip clip in clips)
			{
				if (clip.name == name)
				{
					return clip;
				}
			}
		}

		return null;
	}
}
