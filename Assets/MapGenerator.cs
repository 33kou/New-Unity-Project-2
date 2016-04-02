using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public GameObject wallPrehub;
	public GameObject floorPrehub;
	private int defaultx_max=30;
	private int defaultz_max=19;
	private int defaulty_max=8;

	// Use this for initialization
	void Start () {
		
		CreateMapWall ();
		CreateMapFloor ();
	}

	void CreateMapWall(){
		for (int dy = 0; dy <= defaulty_max; dy++) {
			for (int dx = 0; dx <= defaultx_max; dx++) {
				Instantiate (wallPrehub, new Vector3 (dx, dy, 0), Quaternion.identity);
				Instantiate (wallPrehub, new Vector3 (dx, dy, defaultz_max), Quaternion.identity);
			}
		}
		for (int dy = 0; dy <= defaulty_max; dy++) {
			for(int dz=0;dz<=defaultz_max;dz++){
				Instantiate (wallPrehub, new Vector3 (0, dy, dz), Quaternion.identity);
				Instantiate (wallPrehub, new Vector3 (defaultx_max, dy, dz), Quaternion.identity);
			}

		}
	}

	void CreateMapFloor(){
		for (int dz = 0; dz <= defaultz_max; dz++) {
			for (int dx = 0; dx <= defaultx_max; dx++) {
				Instantiate (floorPrehub, new Vector3 (dx, 0, dz), Quaternion.identity);
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
