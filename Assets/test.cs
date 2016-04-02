using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    public GameObject wallPrehub;
    public GameObject floorPrehub;
    static private int CHECKPOINT_NUMBER = 4;
    private double[,] vector2_original = new double[CHECKPOINT_NUMBER, 2];
    private int[,] vector2_fixed = new int[CHECKPOINT_NUMBER, 2];
    private Vector2[] vec2 = new Vector2[CHECKPOINT_NUMBER];
	private Vector2[] vec2_angle_fix = new Vector2[CHECKPOINT_NUMBER];
    private int index = 0;
    // Use this for initialization
    void Start()
    {
        setOriginalVector2();
        setFixedVector2(vector2_original);
		float angle = getAngle ();
		Debug.Log("angle="+angle);
		for(int i=0;i<CHECKPOINT_NUMBER;i++) vec2_angle_fix[i]=RotationalTransfer2D (vec2[i], angle);
		display2dVector(vec2_angle_fix);
		vec2_angle_fix = sortRect2DVectors (vec2_angle_fix);
		display2dVector(vec2);

		Input.location.lastData.latitude;
    }


	Vector2[] sortRect2DVectors(Vector2[] vectors_2d)
    { //左上、左下、右下、右上に並べる
		Vector2 temp;
        for (int i = 0; i < CHECKPOINT_NUMBER - 1; i++)
        {
            for (int j = 1; j < CHECKPOINT_NUMBER; j++)
            {
                if (vectors_2d[i].x > vectors_2d[j].x)
                {
					temp = vectors_2d[i];
					vectors_2d[i]= vectors_2d[j];
					vectors_2d[j] = temp;
                }
            }
        }
        if (vectors_2d[0].y < vectors_2d[1].y)
        {
			temp = vectors_2d[0];
			vectors_2d[0]= vectors_2d[1];
			vectors_2d[1] = temp;
        }
        if (vectors_2d[2].y > vectors_2d[3].y)
        {
			temp = vectors_2d[2];
			vectors_2d[2]= vectors_2d[3];
			vectors_2d[3] = temp;
        }
		return vectors_2d;
    }

	void display2dVector(Vector2[] v2)
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(v2[i].x + "," + v2[i].y);
        }
    }

	Vector2 RotationalTransfer2D(Vector2 vec2, float angle)
    {
        for (int i = 0; i < CHECKPOINT_NUMBER; i++)
        {
			int x = (int)vec2.x;
			int y = (int)vec2.y;
			vec2.x= (int)(x * Mathf.Cos(angle) - y * Mathf.Sin(angle));
			vec2.y = (int)(x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
        }
		return vec2;

    }
    void setOriginalVector2()
    {
        setOriginalVector2(38.23357181, 140.32735831);
        setOriginalVector2(38.2333015, 140.32735831);
        setOriginalVector2(38.23357181, 140.3270153);
        setOriginalVector2(38.2333015, 140.3270153);
    }
    private void setOriginalVector2(double x, double y)
    {

        vector2_original[index, 0] = x;
        vector2_original[index, 1] = y;
        index++;
    }

    void setFixedVector2(double[,] vector2_original)
    {

        for (int index = 0; index < CHECKPOINT_NUMBER; index++)
        {
            double x = vector2_original[index, 0];
            double y = vector2_original[index, 1];
            string stringy = y.ToString();
            string stringx = x.ToString();
            int xlength = stringx.Length;
            int ylength = stringy.Length;
            int fixedx = int.Parse(stringx.Substring(xlength - 4));
            int fixedy = int.Parse(stringy.Substring(ylength - 4));
            vec2[index].x = fixedx;
            vec2[index].y = fixedy;
        }

    }

	int getMinXIndex(Vector2[] vector)
    {
        int minX = 300;
        int temp_index = 0;
        for (int index = 0; index < CHECKPOINT_NUMBER; index++)
        {
            if (minX > vector[index].y)
            {
				minX = (int)vector[index].y;
                temp_index = index;
            }
        }
        return temp_index;
    }
	int getMaxXIndex(Vector2[] vector)
    {
        int maxX = -1;
        int temp_index = 0;
        for (int index = 0; index < CHECKPOINT_NUMBER; index++)
        {
            if (maxX < vector[index].y)
            {
				maxX = (int)vector[index].y;
                temp_index = index;
            }
        }
        return temp_index;
    }
	int getMaxYIndex(Vector2[] vector)
    {
        int maxY = -1;
        int temp_index = 0;
        for (int index = 0; index < CHECKPOINT_NUMBER; index++)
        {
            if (maxY < vector[index].x)
            {
				maxY = (int)vector[index].x;
                temp_index = index;

            }
        }
        return temp_index;
    }
	int getMinYIndex(Vector2[] vector)
    {
        int minY = 500;
        int temp_index = 0;
        for (int index = 0; index < CHECKPOINT_NUMBER; index++)
        {
            if (minY > vector[index].x)
            {
				minY =(int) vector[index].x;
                temp_index = index;

            }
        }
        return temp_index;
    }


    float getAngle()
    {
		int min_x_index = getMinXIndex(vec2);
		int max_y_index = getMaxYIndex(vec2);
        Vector2 a = new Vector2(0, 1);
        Vector2 b = new Vector2(vector2_fixed[max_y_index, 0] - vector2_fixed[min_x_index, 0],
            vector2_fixed[max_y_index, 1] - vector2_fixed[min_x_index, 1]);
        float angle = Vector2.Angle(a, b);
        return angle;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
