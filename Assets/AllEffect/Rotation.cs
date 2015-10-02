using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    const float speed_x = 0.1f;
    const float newAngle_y = 90;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.position += new Vector3(speed_x, 0, 0);

        //gameObject.transform.Rotate(new Vector3(0f, -5.0f, 0f));

        gameObject.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime, Space.World);

        //gameObject.transform.position += new Vector3(0, 1, 0);

        
	}
}
