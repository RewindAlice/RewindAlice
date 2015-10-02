using UnityEngine;
using System.Collections;

public class Spawn_Trump : MonoBehaviour {

	const int Interval = 120;
	const int SpawnPos_xMin = -7;
    const int SpawnPos_xMax = 7;
	const int SpawnPos_y = 5;
	const int SpawnPos_z = -2;

	float Timer;

	public GameObject Trump;
	// Use this for initialization
	void Start ()
    {
		Spawn();
	}
	
	// Update is called once per frame
	void Update ()
    {
		Timer ++;

		if (Timer == Interval)
        {
			Spawn (); //スポーン
			Timer = 0;
			
		}
	}
	
	void Spawn ()
    {
		float x = Random.Range (SpawnPos_xMin, SpawnPos_xMax);
		Vector3 pos = new Vector3 (x, SpawnPos_y, SpawnPos_z);
		GameObject.Instantiate(Trump, pos, Quaternion.identity);
	}    
}
