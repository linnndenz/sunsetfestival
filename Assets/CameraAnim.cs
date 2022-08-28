using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnim : MonoBehaviour
{
    public float time;
    public Transform camPosParent;
    Transform[] camPoses;

    void Start()
    {
        camPoses = new Transform[camPosParent.childCount];
        for (int i = 0; i < camPosParent.childCount; i++) {
            camPoses[i] = camPosParent.GetChild(i);
        }

        transform.position = camPoses[0].position;
        transform.rotation = camPoses[0].rotation;
    }

    int curr = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            curr = (curr + 1) % camPoses.Length;
            transform.DOMove(camPoses[curr].position, time);
            transform.DORotate(camPoses[curr].localEulerAngles, time);
        }
    }
}
