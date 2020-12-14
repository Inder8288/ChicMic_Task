using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChicMicTask1;

public class MouseLook : MonoBehaviour
{
    public float mouseSpeed=0.2f;
    public float offset = 1.5f;

    private float mouseX;
    private float mouseY;
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
      /*  mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        transform.rotation =Quaternion.Slerp(transform.rotation,Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y+mouseSpeed * mouseX, 0),0.5f);
        transform.position =new Vector3(playerPos.position.x,playerPos.position.y,playerPos.position.z-offset);*/
    
    }
}
