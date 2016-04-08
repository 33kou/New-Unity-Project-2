using UnityEngine;
using System.Collections;

public class fix : MonoBehaviour
{
    public GameObject wallPrehub;
    public GameObject floorPrehub;
    static private int CHECKPOINT_NUMBER = 4;
    private double[,] vector2_original = new double[CHECKPOINT_NUMBER, 2];
    private int[,] vector2_fixed = new int[CHECKPOINT_NUMBER, 2];
    private Vector2[] vec2 = new Vector2[CHECKPOINT_NUMBER];
    private int index = 0;
    // Use this for initialization
    void Start()
    {
        setOriginalVector2();
        setFixedVector2(vector2_original);
    }


    int[,] sortRect2DVectors(int[,] vectors_2d)
    { //左上、左下、右下、右上に並べる
        int x1, x2, x3, x4;
        int y1, y2, y3, y4;
        int tempx, tempy;
        for (int i = 0; i < CHECKPOINT_NUMBER - 1; i++)
        {
            for (int j = 1; j < CHECKPOINT_NUMBER; j++)
            {
                if (vectors_2d[i, 0] > vectors_2d[j, 0])
                {
                    tempx = vectors_2d[i, 0];
                    tempy = vectors_2d[i, 1];
                    vectors_2d[i, 0] = vectors_2d[i + 1, 0];
                    vectors_2d[i, 1] = vectors_2d[i + 1, 1];
                    vectors_2d[i + 1, 0] = tempx;
                    vectors_2d[i + 1, 1] = tempy;
                }
            }
        }
        if (vectors_2d[0, 1] < vectors_2d[1, 1])
        {
            tempx = vectors_2d[0, 0];
            tempy = vectors_2d[0, 1];
            vectors_2d[0, 0] = vectors_2d[1, 0];
            vectors_2d[0, 1] = vectors_2d[1, 1];
            vectors_2d[1, 0] = tempx;
            vectors_2d[1, 1] = tempy;
        }
        if (vectors_2d[2, 1] > vectors_2d[3, 1])
        {
            tempx = vectors_2d[2, 0];
            tempy = vectors_2d[2, 1];
            vectors_2d[2, 0] = vectors_2d[3, 0];
            vectors_2d[2, 1] = vectors_2d[3, 1];
            vectors_2d[3, 0] = tempx;
            vectors_2d[3, 1] = tempy;
        }



        int minx_index = getMinXIndex(vectors_2d);
        int maxx_index = getMaxXIndex(vectors_2d);
        int miny_index = getMinYIndex(vectors_2d);
        int maxy_index = getMaxYIndex(vectors_2d);

        x1 = x2 = vectors_2d[minx_index, 0];
        x3 = x4 = vectors_2d[maxx_index, 0];
        y1 = y4 = vectors_2d[maxy_index, 1];
        y2 = y3 = vectors_2d[miny_index, 1];
        int[,] vectors_sorted = { { x1, y1 }, { x2, y2 }, { x3, y3 }, { x4, y4 } };
        return vectors_sorted;
    }

    void display2dVector(int[,] v2)
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(v2[i, 0] + "," + v2[i, 1]);
        }
    }

    int[,] RotationalTransfer2D(int[,] vector2_fixed, float angle)
    {
        for (int i = 0; i < CHECKPOINT_NUMBER; i++)
        {
            int x = vector2_fixed[i, 0];
            int y = vector2_fixed[i, 1];
            vector2_fixed[i, 0] = (int)(x * Mathf.Cos(angle) - y * Mathf.Sin(angle));
            vector2_fixed[i, 1] = (int)(x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
        }
        return vector2_fixed;

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
        //return vector2_fixed;

    }

    int getMinXIndex(int[,] vector)
    {
        int minX = 300;
        int temp_index = 0;
        for (int index = 0; index < CHECKPOINT_NUMBER; index++)
        {
            if (minX > vector[index, 1])
            {
                minX = vector[index, 1];
                temp_index = index;
            }
        }
        return temp_index;
    }
    int getMaxXIndex(int[,] vector)
    {
        int maxX = -1;
        int temp_index = 0;
        for (int index = 0; index < CHECKPOINT_NUMBER; index++)
        {
            if (maxX < vector[index, 1])
            {
                maxX = vector[index, 1];
                temp_index = index;
            }
        }
        return temp_index;
    }
    int getMaxYIndex(int[,] vector)
    {
        int maxY = -1;
        int temp_index = 0;
        for (int index = 0; index < CHECKPOINT_NUMBER; index++)
        {
            if (maxY < vector[index, 0])
            {
                maxY = vector[index, 0];
                temp_index = index;

            }
        }
        return temp_index;
    }
    int getMinYIndex(int[,] vector)
    {
        int minY = 500;
        int temp_index = 0;
        for (int index = 0; index < CHECKPOINT_NUMBER; index++)
        {
            if (minY > vector[index, 0])
            {
                minY = vector[index, 0];
                temp_index = index;

            }
        }
        return temp_index;
    }


    float getAngle()
    {
        int min_x_index = getMinXIndex(vector2_fixed);
        int max_y_index = getMaxYIndex(vector2_fixed);
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
