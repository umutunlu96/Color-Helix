using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject finishLine;
    private GameObject[] walls1, walls2;
    public Color[] colors;
    [HideInInspector]
    public Color hitColor, failColor;

    private int wallsSpawnNumber = 11;
    private float z = 7;
    private int wallsCount = 0;
    private bool colorBump;
    public int score;

    void Awake()
    {
        instance = this;
        GenerateColors();
        SetUpLevelPrefs();
    }



    void Start()
    {
        GenerateLevel();
    }

    void Update()
    {
        SumUpWalls();
    }

    void SetUpLevelPrefs()
    {
        if (!PlayerPrefs.HasKey("Level"))
            PlayerPrefs.SetInt("Level", 1);
    }

    public void GenerateLevel()
    {
        GenerateColors();

        if (PlayerPrefs.GetInt("Level", 1) >= 1 && PlayerPrefs.GetInt("Level") <= 4)
            wallsSpawnNumber = 12;
        else if (PlayerPrefs.GetInt("Level", 1) >= 5 && PlayerPrefs.GetInt("Level") <= 10)
            wallsSpawnNumber = 13;
        else
            wallsSpawnNumber = 14;

        z = 7;
        DelateWalls();
        colorBump = false;
        SpawnWalls();
    }

    void GenerateColors()
    {
        hitColor = colors[Random.Range(0, colors.Length)];

        failColor = colors[Random.Range(0, colors.Length)];

        while (hitColor == failColor)
            failColor = colors[Random.Range(0, colors.Length)];

        Ball.SetColor(hitColor);
    }

    void SpawnWalls()
    {
        for (int i = 0; i < wallsSpawnNumber; i++)
        {
            GameObject wall;

            if (Random.value <= .2f && !colorBump && PlayerPrefs.GetInt("Level") >= 3)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
            }
            else if(i>= wallsSpawnNumber -1 && !colorBump && PlayerPrefs.GetInt("Level") >= 3)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
            }
            else if (Random.value <= .2f && PlayerPrefs.GetInt("Level") >= 3)
            {
                wall = Instantiate(Resources.Load("Walls") as GameObject, transform.position, Quaternion.identity);
            }
            else
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
            }

            wall.transform.SetParent(GameObject.Find("Helix").gameObject.transform);
            wall.transform.localPosition = new Vector3(0, 0, z);
            float randomRotation = Random.Range(0, 360);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));
            z += 7;

            if (i <= wallsSpawnNumber)
                finishLine.transform.position = new Vector3(0, 0, z);
        }

    }

    private void DelateWalls()
    {
        walls2 = GameObject.FindGameObjectsWithTag("Fail");
        if (walls2.Length > 1)
        {
            for (int i = 0; i < walls2.Length; i++)
            {
                Destroy(walls2[i].transform.parent.gameObject);
            }
        }
        Destroy(GameObject.FindGameObjectWithTag("ColorBump"));
    }

    private void SumUpWalls()
    {
        walls1 = GameObject.FindGameObjectsWithTag("Wall1");

        if (walls1.Length > wallsCount)
            wallsCount = walls1.Length;
        if (wallsCount > walls1.Length)
        {
            wallsCount = walls1.Length;
            if (GameObject.Find("Ball").GetComponent<Ball>().perfectStar)
            {
                GameObject.Find("Ball").GetComponent<Ball>().perfectStar = false;
                score += PlayerPrefs.GetInt("Level") * 2;
            }
            else
            {
                score += PlayerPrefs.GetInt("Level");
            }
        }
    }

    public float GetFinishLineDistance()
    {
        return finishLine.transform.position.z;
    }

}
