using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineGenerator : MonoBehaviour {

	public int width;
	public int height;
	public GameObject minePrefab;

	Tile[,] tileMap;
	Dictionary<GameObject, Coord> objToCoord;

	void Start() {
		tileMap = new Tile[width, height];
		objToCoord = new Dictionary<GameObject, Coord>();

		GenerateTiles(.1f);
	}

	void GenerateTiles(float tileLength) {
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				Vector3 pos = new Vector3((-width/2f + .5f + x) * tileLength, (-height/2f + .5f + y) * tileLength, 0);
				tileMap[x, y] = new Tile(minePrefab, pos, tileLength);
				objToCoord.Add(tileMap[x, y].tileObject, new Coord(x, y));
			}
		}
	}

	public void HitMine(GameObject obj, bool leftClick, bool rightClick) {
		Debug.Log(obj.name);
		if(!objToCoord.ContainsKey(obj))
			return;
		Coord coord = objToCoord[obj];
		Tile tile = tileMap[coord.x, coord.y];
		if(rightClick) {
			if(tile.tileState != TileState.CHECKED) {
				tile.SetState(tile.tileState == TileState.DEFAULT ? TileState.FLAGGED : TileState.DEFAULT);
			}
		} else if(leftClick) {
			if(tile.tileState != TileState.FLAGGED) {
				tile.SetState(TileState.CHECKED);
				if(tile.tileData.isMine) {
					// Game over
				}
			}
		}
	}

	public struct Coord {
		public int x;
		public int y;

		public Coord(int _x, int _y) {
			x = _x;
			y = _y;
		}
	}

	public enum TileState {DEFAULT, CHECKED, FLAGGED};

	public class Tile {
		public Vector3 position;
		public GameObject tileObject;
		public TileState tileState;
		public TileData tileData;

		public Tile(GameObject prefab, Vector3 pos, float scale) {
			position = pos;
			tileObject = Instantiate(prefab, position, Quaternion.identity) as GameObject;
			tileObject.transform.localScale = new Vector3(scale, scale, scale);
			tileState = TileState.DEFAULT;
			tileData = new TileData();
		}

		public void SetState(TileState state) {
			tileState = state;
			foreach(Transform child in tileObject.transform) {
				if(state == TileState.DEFAULT) {
					child.gameObject.SetActive(child.name == "Default");
				} else if(state == TileState.CHECKED) {
					child.gameObject.SetActive(child.name == "Checked");
				} else if(state == TileState.FLAGGED) {
					child.gameObject.SetActive(child.name == "Flagged");
				}
			}
		}
	}

	public class TileData {
		public bool isMine;
		public int tileCount;

		public TileData() {
			isMine = false;
			tileCount = -1;
		}
	}
}
