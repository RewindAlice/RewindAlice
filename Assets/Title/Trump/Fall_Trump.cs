using UnityEngine;
using System.Collections;

public class Fall_Trump : MonoBehaviour {

	const int DeathTime = 500; // 消滅時間
	const int RotSpeed_Min = 1;
	const int RotSpeed_Max = 3;
	const int RotSpeed_Rate = 1;
	const float FallSpeed = -0.025f;

	int Timer; // タイマー
	int Speed_x,Speed_y,Speed_z; // 回転速度

	// Use this for initialization
	void Start ()
    {
		Timer = 0;

		// 回転速度を0~2の範囲でランダムで決定
		Speed_x = Random.Range (RotSpeed_Min, RotSpeed_Max) * RotSpeed_Rate;
		Speed_y = Random.Range (RotSpeed_Min, RotSpeed_Max) * RotSpeed_Rate; 
		Speed_z = Random.Range (RotSpeed_Min, RotSpeed_Max) * RotSpeed_Rate;
	}
	
	// Update is called once per frame
	void Update ()
    {

		transform.Rotate(new Vector3(Speed_x,Speed_y,Speed_z)); // 回転
		transform.position += new Vector3 (0.0f, FallSpeed, 0.0f);

		Timer ++;

		if (Timer == DeathTime)
			Destroy (gameObject);
	}
}
