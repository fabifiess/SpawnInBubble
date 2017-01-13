using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveIngredient : MonoBehaviour {
	public GameObject bubblePrefab;
	GameObject bubble;
	GameObject instantiatedGO;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void InstantiateIt(GameObject thatGameObject){

		if (instantiatedGO != null) Destroy (instantiatedGO);
		if (bubble != null) Destroy (bubble);
		instantiatedGO = Instantiate (thatGameObject, new Vector3 (0f ,6f ,0f), Quaternion.identity);
		bubble = Instantiate (bubblePrefab, instantiatedGO.transform.position, Quaternion.identity);
		StartCoroutine (ParentIt ());

	}


	IEnumerator ParentIt(){
		yield return new WaitForEndOfFrame ();
		if (instantiatedGO != null) {
			instantiatedGO.transform.parent = bubble.transform;

			bubble.GetComponent<Animator>().SetTrigger ("trg_bubbleFadeIn");
			yield return new WaitForSeconds (0.7f);
			// Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + GetComponent<Renderer>().bounds.size.y/2 + instantiatedGO.GetComponent<Renderer>().bounds.size.y/2, transform.position.z);
			instantiatedGO.GetComponent<CubeVFX> ().fadeInAnim ();
		}

	}


}
