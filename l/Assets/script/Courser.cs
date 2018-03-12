using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Courser : MonoBehaviour
{

    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;

    void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        //Screen.lockCursor = true;
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Translate(h/2, v/2, 0);

       //Vector2 mouseOnScreen = (Vector2)(Input.mousePosition);
       // transform.position = mouseOnScreen /40f;
    }
}