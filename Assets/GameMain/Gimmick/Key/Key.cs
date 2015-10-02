using UnityEngine;
using System.Collections;

public class Key : BaseGimmick
{
     private GameObject pause;
    private Pause pauseScript;
	// Use this for initialization
	void Start()
	{ 
        pause = GameObject.Find("Pause");
        pauseScript = pause.GetComponent<Pause>();
	}

	// Update is called once per frame
	void Update()
	{
         if(pauseScript.pauseFlag ==false)
        {

        }   
	
	}

	public void Initialize()
	{

	}

	public void Invisible(bool flag)
	{
		Renderer[] renderChildren = GetComponentsInChildren<Renderer>();

		for (int i = 0; i < renderChildren.Length; ++i)
		{
			if (flag)
				renderChildren[i].enabled = false;
			else
				renderChildren[i].enabled = true;
		}
	}
}