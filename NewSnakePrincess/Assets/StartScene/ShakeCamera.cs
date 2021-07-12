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
        InvokeRepeating("StartShake", 0f, 0.005f);   // 0초 후에 0.005초마다 한 번씩 반복.
                                                     // (딜레이 기능은 안 쓴다고 보면 됨) 
        InvokeRepeating("ResetCamera", 0f, 0.2f);  //멀리 안 가게 계속 위치 초기화

    }
    public void StartShake()
    {
        float posX = ((Random.value * 2) - 1) * shakeRange;  //(-1에서 1사이의 값이 된다)
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
