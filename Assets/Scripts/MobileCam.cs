using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCam : MonoBehaviour
{
    public bool froze;
    public bool isFreeMove;
    [Header("��Բ����ת�ٶ�")] public float roSpeed;
    [Header("�ƽ���Զ�ٶ�")] public float pullSpeed;
    [Header("����ҡ�ٶ�")] public float updownPanSpeed;
    [Header("�������ٶ�")] public float updownMoveSpeed;

    Transform movePoint;
    Transform cam;
    struct InitTransform
    {
        public Vector3 thisPo;
        public Vector3 thisRo;
        public Vector3 childPo;
        public Vector3 childRo;
        public Vector3 camPo;
        public Vector3 camRo;
    }
    InitTransform initTransform;
    void Start()
    {
        movePoint = transform.GetChild(0);
        cam = movePoint.GetChild(0);

        initTransform = new InitTransform();
        initTransform.thisPo = transform.localPosition;
        initTransform.thisRo = transform.localEulerAngles;
        initTransform.childPo = movePoint.localPosition;
        initTransform.childRo = movePoint.localEulerAngles;
        initTransform.camPo = cam.localPosition;
        initTransform.camRo = cam.localEulerAngles;

    }

    void Update()
    {
        if (froze) return;

        //�����ƶ�
        if (isFreeMove) {
            FreeMove();
        } else {//�����ƶ�
            //��Բ����ת transform.localEulerAngles.y
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                transform.localEulerAngles += new Vector3(0, Time.deltaTime * roSpeed, 0);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                transform.localEulerAngles += new Vector3(0, -Time.deltaTime * roSpeed, 0);
            }

            //���� movePoint.localPosition.x
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                movePoint.localPosition += new Vector3(Time.deltaTime * pullSpeed, 0, 0);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                movePoint.localPosition += new Vector3(-Time.deltaTime * pullSpeed, 0, 0);
            }

            //����ҡ movePoint.localEulerAngles.z
            if (Input.GetKey(KeyCode.J) && (movePoint.localEulerAngles.z > 330 || movePoint.localEulerAngles.z < 60)) {
                movePoint.localEulerAngles += new Vector3(0, 0, -Time.deltaTime * updownPanSpeed);
            }
            if (Input.GetKey(KeyCode.K) && (movePoint.localEulerAngles.z < 30 || movePoint.localEulerAngles.z > 300)) {
                movePoint.localEulerAngles += new Vector3(0, 0, Time.deltaTime * updownPanSpeed);
            }

        }

        //������ transform.localPosition.y
        if (Input.GetKey(KeyCode.Q)) {
            transform.localPosition += new Vector3(0, -Time.deltaTime * updownMoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.localPosition += new Vector3(0, Time.deltaTime * updownMoveSpeed, 0);
        }

        //����
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetCamTransform();
        }
    }


    public void ResetCamTransform()
    {
        transform.localPosition = initTransform.thisPo;
        transform.localEulerAngles = initTransform.thisRo;

        movePoint.localPosition = initTransform.childPo;
        movePoint.localEulerAngles = initTransform.childRo;

        cam.localPosition = initTransform.camPo;
        cam.localEulerAngles = initTransform.camRo;
    }

    [Header("�����ٶ�")]
    public int freeRoSpeed = 5;
    public int freeMoveSpeed = 5;

    float axisX2 = 0;
    float axisY2 = 0;

    public void FreeMove()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * freeMoveSpeed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * freeMoveSpeed;
        cam.Translate(x, 0, y);

        float axisX = Input.GetAxis("Mouse X");
        float axisY = Input.GetAxis("Mouse Y");
        axisX2 += axisX;
        axisY2 += axisY;

        //����ͷ��������
        var rotation = Quaternion.Euler(-axisY2 * freeRoSpeed, axisX2 * freeRoSpeed, 0);
        cam.rotation = rotation;
    }
}
