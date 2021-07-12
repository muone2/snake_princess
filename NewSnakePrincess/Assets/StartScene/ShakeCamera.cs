using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public Camera mainCamera;
    public float shakeRange;
    public float shakeTime;
    Vector3 cameraPosA;

    // Start is called before the first frame update
    void Start()
    {
        Shake();
        cameraPosA = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("StopShake", shakeTime);
    }

    public void Shake()
    {
        InvokeRepeating("StartShake", 0f, 0.005f);   // 0�� �Ŀ� 0.005�ʸ��� �� ���� �ݺ�.
                                                     // (������ ����� �� ���ٰ� ���� ��) 
        InvokeRepeating("ResetCamera", 0f, 0.2f);  //�ָ� �� ���� ��� ��ġ �ʱ�ȭ

    }
    public void StartShake()
    {
        float posX = ((Random.value * 2) - 1) * shakeRange;  //(-1���� 1������ ���� �ȴ�)
        float posY = ((Random.value * 2) - 1) * shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += posX;
        cameraPos.y += posY;
        mainCamera.transform.position = cameraPos;
    }

    public void StopShake()
    {
        CancelInvoke("StartShake");
        CancelInvoke("ResetCamera");
        ResetCamera();
    }
    public void ResetCamera()
    {
        mainCamera.transform.position = cameraPosA;
    }

}
