using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject wallFragment;
    private GameObject wall1, wall2;
    private GameObject perfectStar;

    private float rotationZ;
    private float rotationZMax = 180;

    private bool smallWall;

    void Awake()
    {
        wallFragment = Resources.Load("WallFragment") as GameObject;
        perfectStar = Resources.Load("PerfectStar") as GameObject;
    }

    private void Start()
    {
        SpawnWallFragments();
    }

    private void SpawnWallFragments()
    {
        wall1 = new GameObject();
        wall2 = new GameObject();

        wall1.name = "Wall-1";
        wall2.name = "Wall-2";

        wall1.tag = "Wall1";
        wall2.tag = "Fail";

        wall1.transform.SetParent(transform);
        wall2.transform.SetParent(transform);

        wall2.AddComponent<BoxCollider>();
        wall2.GetComponent<BoxCollider>().size = new Vector3(.9f, 1.85f, .2f);
        wall2.GetComponent<BoxCollider>().center = new Vector3(.46f, 0, 0);

        if (Random.value <= .2f && PlayerPrefs.GetInt("Level") >= 6)
            smallWall = true;

        if (smallWall)
            rotationZMax = 90;
        else
            rotationZMax = 180;

        for (int i = 0; i < 100; i++)
        {
            GameObject WallF = Instantiate(wallFragment, Vector3.zero, Quaternion.Euler(0, 0, rotationZ));
            rotationZ += 3.6f;

            if (rotationZ <= rotationZMax)
            {
                WallF.transform.SetParent(wall1.transform);
                WallF.gameObject.tag = "Hit";
            }
            else
                WallF.transform.SetParent(wall2.transform);
        }


        if (smallWall)
        {
            GameObject wallFragmentChild = wall1.transform.GetChild(14).gameObject;
            AddStar(wallFragmentChild);
        }
        else
        {
            GameObject wallFragmentChild = wall1.transform.GetChild(25).gameObject;
            AddStar(wallFragmentChild);
        }

        wall1.transform.localPosition = Vector3.zero;
        wall2.transform.localPosition = Vector3.zero;

        wall1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wall2.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }


    void AddStar(GameObject wallFragmentChild)
    {
        GameObject star = Instantiate(perfectStar, transform.position, Quaternion.Euler(0,0,90));
        star.transform.SetParent(wallFragmentChild.transform);
        star.transform.localPosition = new Vector3(.05f, .75f, -.06f);
    }

    void Update()
    {
        
    }
}
