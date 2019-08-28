using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeUIController : MonoBehaviour
{
    public GameObject mainCamera;
    // 視点UIの対象パネル
    public GameObject modePlane_1;
    public GameObject modePlane_2;

    // 対象ボタンの色 0:通常/1:ヒット
    public Material[] _materials;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Gaze UI Controller is started");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Sceneのみで線が見える
            Debug.DrawLine(ray.origin, hit.point, Color.black);

            // 視点UIの対象かどうかをタグで判定
            if (hit.collider.gameObject.tag == "VRUI")
            {
                Debug.Log("hit");
                if (hit.collider.gameObject == modePlane_1)
                {
                    // ヒットの色
                    modePlane_1.GetComponent<Renderer>().material = _materials[1];
                }

                if (hit.collider.gameObject == modePlane_2)
                {
                    modePlane_2.GetComponent<Renderer>().material = _materials[1];
                }
            }
        }
        else
        {
            // 通常の色
            modePlane_1.GetComponent<Renderer>().material = _materials[0];
            modePlane_2.GetComponent<Renderer>().material = _materials[0];
        }
    }
}
