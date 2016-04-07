using UnityEngine;
using System.Collections;

public class scripts : MonoBehaviour {
	public GameObject wallPrehub;
	public GameObject floorPrehub;
	static private int CHECKPOINT_NUMBR=4;
	private double[,] vector2_original=new double[CHECKPOINT_NUMBR,2];
	private int[,] vector2_fixed=new int[CHECKPOINT_NUMBR,2];
	private int index=0;
	// Use this for initialization
	void Start () {
		setOriginalVector2();
		vector2_fixed = getFixedVector2 (vector2_original);
		float angle = getAngle ();
		int[,] clear_vector2 = RotationalTransfer2D (vector2_fixed, angle);
		display2dVector(vector2_fixed);
		display2dVector(clear_vector2);
		}
		
	void display2dVector(int[,] v2){
		for(int i=0;i<4;i++){
			Debug.Log (v2[i, 0] + ","+v2[i,1]);
		}
	}

	int[,] RotationalTransfer2D(int[,] vector2_fixed,float angle){
		for (int i = 0; i < CHECKPOINT_NUMBR; i++) {
			int x = vector2_fixed [i, 0];
			int y = vector2_fixed [i, 1];
			vector2_fixed [i, 0] =(int)(x*Mathf.Cos (angle) - y* Mathf.Sin (angle));
			vector2_fixed [i, 1] =(int)(x* Mathf.Sin (angle) + y* Mathf.Cos (angle));
		}
		return vector2_fixed;

	}
	void setOriginalVector2(){
		setOriginalVector2(38.23357181, 140.32735831);
		setOriginalVector2(38.2333015, 140.32735831);
		setOriginalVector2(38.23357181, 140.3270153);
		setOriginalVector2(38.2333015, 140.3270153);
	}
	void setOriginalVector2(double x,double y){
		
		vector2_original [index,0] = x;
		vector2_original [index,1] = y;
		index++;
	}

	int[,] getFixedVector2(double[,] vector2_original){
		int[,] vector2_fixed=new int[CHECKPOINT_NUMBR,2];
		for (int index = 0; index < CHECKPOINT_NUMBR; index++) {
			double x=vector2_original [index, 0];
			double y=vector2_original [index, 1];
			string stringy = y.ToString ();
			string stringx = x.ToString ();
			int xlength = stringx.Length;
			int ylength = stringy.Length;
			int fixedx = int.Parse (stringx.Substring (xlength - 4));
			int fixedy = int.Parse (stringy.Substring (ylength - 4));
			vector2_fixed [index, 0] = fixedx;
			vector2_fixed [index, 1] = fixedy;
		}
		return vector2_fixed;

	}

	int getMinXIndex(int[,] vector){
		int minX=300;
		int temp_index=0;
		for (int index = 0; index < CHECKPOINT_NUMBR; index++) {
			if (minX > vector [index, 1]) {
				minX = vector [index, 1];
				temp_index = index;
			}
		}
		return temp_index;
	}
	int getMaxXIndex(int[,] vector){
		int maxX=-1;
		int temp_index=0;
		for (int index = 0; index < CHECKPOINT_NUMBR; index++) {
			if (maxX < vector [index, 1]) {
				maxX = vector [index, 1];
				temp_index = index;
			}
		}
		return temp_index;
	}
	int getMaxYIndex(int[,] vector){
		int maxY=-1;
		int temp_index=0;
		for (int index = 0; index < CHECKPOINT_NUMBR; index++) {
			if (maxY < vector [index, 0]) {
				maxY = vector [index, 0];
				temp_index = index;

			}
		}
		return temp_index;
	}
	int getMinYIndex(int[,] vector){
		int minY=500;
		int temp_index=0;
		for (int index = 0; index < CHECKPOINT_NUMBR; index++) {
			if (minY > vector [index, 0]) {
				minY = vector [index, 0];
				temp_index = index;

			}
		}
		return temp_index;
	}


	float getAngle(){
		int min_x_index = getMinXIndex (vector2_fixed);
		int max_y_index = getMaxYIndex (vector2_fixed);
		Vector2 a = new Vector2 (0, 1);
		Vector2 b = new Vector2(vector2_fixed[max_y_index,0]-vector2_fixed[min_x_index,0],
			vector2_fixed[max_y_index,1]-vector2_fixed[min_x_index,1]);
		float angle = Vector2.Angle (a, b);
		return angle;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
