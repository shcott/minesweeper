using UnityEngine;
using System.Collections;

public class MineGenerator : MonoBehaviour {

	public int width;
	public int height;
	public GameObject minePrefab;

	Mine[,] mineMap;

	void Start () {
		mineMap = new Mine[width, height];

		GenerateMines(.1f);
	}

	void GenerateMines(float mineLength) {
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				Vector3 pos = new Vector3((-width/2f + .5f + x) * mineLength, (-height/2f + .5f + y) * mineLength, 0);
				mineMap[x, y] = new Mine(minePrefab, pos, mineLength);
			}
		}
	}

	public class Mine {
		public Vector3 position;
		public GameObject mineObject;

		public Mine(GameObject prefab, Vector3 pos, float scale) {
			position = pos;
			mineObject = Instantiate(prefab, position, Quaternion.identity) as GameObject;
			mineObject.transform.localScale = new Vector3(scale, scale, scale);
		}
	}
}
