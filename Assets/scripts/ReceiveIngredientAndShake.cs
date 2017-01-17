using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveIngredientAndShake : MonoBehaviour {
	public GameObject bubblePrefab;
	public GameObject shakeBasePrefab;
	GameObject bubble;
	GameObject instantiatedGO;
	GameObject shakeBase;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void InstantiateIt(GameObject thatGameObject){

		if (instantiatedGO != null) Destroy (instantiatedGO);
		if (bubble != null) Destroy (bubble);
		if (shakeBase != null) Destroy (shakeBase);
		Vector3 targetPos = new Vector3 (Random.Range (-5.0f, 5.0f), Random.Range (-0.0f, 10.0f), Random.Range (-5.0f, 5.0f));
		print ("targetPos: " + targetPos);
		instantiatedGO = Instantiate (thatGameObject, Vector3.zero, Quaternion.identity);
		bubble = Instantiate (bubblePrefab, targetPos, Quaternion.identity);
		shakeBase = Instantiate (shakeBasePrefab, Vector3.zero, Quaternion.identity);
		StartCoroutine (ParentIt ());

	}


	IEnumerator ParentIt(){
		yield return new WaitForEndOfFrame ();
		if (instantiatedGO != null) {
			instantiatedGO.transform.parent = shakeBase.transform;
			shakeBase.transform.parent = bubble.transform;

			bubble.GetComponent<Animator>().SetTrigger ("trg_bubbleFadeIn");
			yield return new WaitForSeconds (0.7f);
			// Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + GetComponent<Renderer>().bounds.size.y/2 + instantiatedGO.GetComponent<Renderer>().bounds.size.y/2, transform.position.z);
			instantiatedGO.GetComponent<CubeVFX> ().fadeInAnim ();
		}

	}


}
