using UnityEngine;
using System.Collections;

public class TestButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		int x = 20;
		int y = 20;
		// ボタンを表示する
		if (GUI.Button (new Rect (x, y, 250, 250), "Button")) 
		{
			transform.position += new Vector3(1, 0, 0);
			Debug.Log("Button is clicked.");
		}
	}
}
