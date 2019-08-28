using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    // キーボードでの操作用
#if UNITY_EDITOR || UNITY_STANDALONE
    private Vector3 rotate;
#endif

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
#if UNITY_EDITOR || UNITY_STANDALONE
        rotate = transform.rotation.eulerAngles;
        Debug.Log("non-smartphone");
#else
        Input.gyro.enabled = true;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        // PCはキーボード操作で視点変更、スマホはジャイロで視点変更
#if UNITY_EDITOR || UNITY_STANDALONE
        float speed = Time.deltaTime * 100.0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotate.y -= speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotate.y += speed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotate.x -= speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rotate.x += speed;
        }
        transform.rotation = Quaternion.Euler(rotate);
#else
        Quaternion gattitude = Input.gyro.attitude;
        gattitude.x *= -1;
        gattitude.y *= -1;
        transform.localRotation = Quaternion.Euler(90, 0, 0) * gattitude;
#endif
    }
}
