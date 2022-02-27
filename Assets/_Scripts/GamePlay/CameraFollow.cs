using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float cameraZ;
    private Animator anim;

    private float time;
    [SerializeField]
    private float cameraLerpSpeed = 1;

    void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (time < 1)
        {
            time += Time.deltaTime * cameraLerpSpeed;
            cameraZ = Mathf.Lerp(transform.position.z, -3f, time);
        }
        else
        {
        cameraZ = Ball.GetZ() - 3f;

        }

        transform.position = new Vector3(0, 2.2f, cameraZ);
    }

    public void Flash()
    {
        anim.SetTrigger("Flash");
        cameraZ = 0;
        time = 0;
    }
}
