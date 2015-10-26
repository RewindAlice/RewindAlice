using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Result : MonoBehaviour {

    //ゴールしたことを伝える
    public GameObject cameraSyatem;
    public CameraSystem resultCamera;

	// Use this for initialization
	void Start () 
    {
        GetComponent<Image>().enabled = false;
        cameraSyatem = GameObject.Find("Camera");
        resultCamera = cameraSyatem.GetComponent<CameraSystem>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (resultCamera.EndCameraMove())
        {
            GetComponent<Image>().enabled = true;
        }
	}
 
}
