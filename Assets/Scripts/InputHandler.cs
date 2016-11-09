using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	Camera cam;
	MineGenerator mineGen;

	void Start() {
		cam = Camera.main;
		mineGen = GetComponent<MineGenerator>();
	}

	void Update () {
		bool leftClick = Input.GetButtonDown("Fire1");
		bool rightClick = Input.GetButtonDown("Fire2");
		if(leftClick || rightClick) {
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit)) {
				GameObject obj = hit.transform.gameObject;
				if(obj != null)
					mineGen.HitMine(obj, leftClick, rightClick);
			}
		}
	}
}
