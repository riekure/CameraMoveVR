using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeUIController : MonoBehaviour
{
    public GameObject mainCamera;
    // 視点UIの対象パネル
    public GameObject movePlane_1;
    public GameObject movePlane_2;

    // 対象ボタンの色 0:通常/1:ヒット
    public Material[] _materials;

    public SpriteRenderer movePlaneSprite_1;
    public SpriteRenderer movePlaneSprite_2;

    // ポインタ画像
    public Sprite[] pointer;

    // 確定までの時間(秒)
    public float gazeTimeCount = 3.0f;

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
                if (CheckHitGameObject(hit, movePlane_1) == true)
                {
                    movePlane_1.GetComponent<Renderer>().material = _materials[1];   // ヒットの色
                    if (DrawSpriteFromGazeTimeCount(movePlaneSprite_1) == true)
                    {
                        mainCamera.transform.position = new Vector3(0, 1, 2);
                        mainCamera.transform.rotation = Quaternion.Euler(0, 180f, 0);
                    }
                }
                if (CheckHitGameObject(hit, movePlane_2) == true)
                {
                    movePlane_2.GetComponent<Renderer>().material = _materials[1];   // ヒットの色
                    if (DrawSpriteFromGazeTimeCount(movePlaneSprite_2) == true)
                    {
                        mainCamera.transform.position = new Vector3(-1, 1, 1);
                        mainCamera.transform.rotation = Quaternion.Euler(0, 135f, 0);
                    }
                }
            }
        }
        else
        {
            // 通常の色
            movePlane_1.GetComponent<Renderer>().material = _materials[0];
            movePlane_2.GetComponent<Renderer>().material = _materials[0];

            movePlaneSprite_1.sprite = pointer[0];
            movePlaneSprite_2.sprite = pointer[0];

            gazeTimeCount = 3.0f;
        }
    }

    public bool CheckHitGameObject(RaycastHit hit, GameObject obj)
    {
        bool result = false;
        if (hit.collider.gameObject == obj)
        {
            result = true;
        }
        return result;
    }

    // 注視カウントの値によりポインタ画像を変更
    public bool DrawSpriteFromGazeTimeCount(SpriteRenderer spr)
    {
        bool result = false;

        if (gazeTimeCount > 0)
        {
            int idx = 6 - (int)(gazeTimeCount * 10) / 5;
            spr.sprite = pointer[idx];
            gazeTimeCount -= Time.deltaTime;
        }
        else
        {
            result = true;
            spr.sprite = pointer[0];
        }
        return result;
    }
}
