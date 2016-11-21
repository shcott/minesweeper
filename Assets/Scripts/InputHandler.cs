using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	Camera cam;
	MapGenerator mapGen;

	void Start() {
		cam = Camera.main;
		mapGen = GetComponent<MapGenerator>();
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
					mapGen.HitTile(obj, leftClick, rightClick);
			}
		}
	}
}
