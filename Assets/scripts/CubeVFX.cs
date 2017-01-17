using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVFX : MonoBehaviour {
	Animator animator_item;
	public Transform particles_item;


	void Start () {
		animator_item = GetComponent<Animator> ();
	}

	public void fadeInAnim(){
		animator_item.SetTrigger ("trg_fadeIn");
		particles_item.GetComponent<SpawnParticles> ().triggerParticles ();
	}



}
