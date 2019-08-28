using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeUIController : MonoBehaviour
{
    public GameObject mainCamera;

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
        }

        // 視点UIの対象かどうかをタグで判定
        if (hit.collider.gameObject.tag == "VRUI")
        {
            Debug.Log("hit");
        }
    }
}
