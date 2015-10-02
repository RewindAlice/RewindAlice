using UnityEngine;
using System.Collections;

public class HeartSoldier : BaseGimmick{

    public GameObject alice;
    private Player moveScript;
    public GameObject stage;
    private Stage stageScript;

    public int soldierDirection;
    public int timeCount;
    Vector3 enemyAngle1;
    Vector3 enemyAngle2;
    Vector3 enemyAngle3;
    Vector3 enemyAngle4;
    private GameObject pause;
    private Pause pauseScript;
	// Use this for initialization


    public int arrayPosX;
    public int arrayPosY;
    public int arrayPosZ;
   
    void Start()
    {
        pause = GameObject.Find("Pause");
        alice = GameObject.Find("Alice");
        pauseScript = pause.GetComponent<Pause>();
        moveScript = alice.GetComponent<Player>();
        enemyAngle1 = new Vector3(0, 0, 0);
        enemyAngle2 = new Vector3(0, 90, 0);
        enemyAngle3 = new Vector3(0, 180, 0);
        enemyAngle4 = new Vector3(0, 270, 0);

        arrayPosX = 0;
        arrayPosY = 0;
        arrayPosZ = 0;

        soldierDirection = 1;
        timeCount = 0;
        //moveScript = alice.GetComponent<Player>();
	
	}
	
	// Update is called once per frame
	void Update () {
      if(pauseScript.pauseFlag == false)
      {

      }
        
	
	}

    public override void OnAliceMove(int aliceMoveTime)
    {
        if (timeCount < aliceMoveTime)
        {
            bool flag;
            //修正箇所１
            flag = CaptureDecision();
            if(flag == false)
            {
                soldierDirection += 1;
                timeCount += 1;
                if (soldierDirection == 5)
                {
                    soldierDirection = 1;
                }

                if (soldierDirection == 1)
                {
                    this.transform.localEulerAngles = enemyAngle1;
                }
                if (soldierDirection == 2)
                {
                    this.transform.localEulerAngles = enemyAngle2;
                }
                if (soldierDirection == 3)
                {
                    this.transform.localEulerAngles = enemyAngle3;
                }
                if (soldierDirection == 4)
                {
                    this.transform.localEulerAngles = enemyAngle4;
                }
                //修正箇所１
                CaptureDecision();
            }
           
        }
    }

    public void SetData(int Direction,int PosY,int PosX,int PosZ)
    {

        soldierDirection = Direction;

        if (soldierDirection == 1)
        {
            this.transform.localEulerAngles = enemyAngle1;
        }
        if (soldierDirection == 2)
        {
            this.transform.localEulerAngles = enemyAngle2;
        }
        if (soldierDirection == 3)
        {
            this.transform.localEulerAngles = enemyAngle3;
        }
        if (soldierDirection == 4)
        {
            this.transform.localEulerAngles = enemyAngle4;
        }

        arrayPosX = PosX;
        arrayPosY = PosY;
        arrayPosZ = PosZ;
    }

    public override void OnAliceReturn(int aliceMoveTime)
    {
        if (timeCount >= aliceMoveTime)
        {
            soldierDirection -= 1;
            timeCount -= 1;
            if (soldierDirection == 0)
            {
                soldierDirection = 4;
            }

            if (soldierDirection == 1)
            {
                this.transform.localEulerAngles = enemyAngle1;
            }
            if (soldierDirection == 2)
            {
                this.transform.localEulerAngles = enemyAngle2;
            }
            if (soldierDirection == 3)
            {
                this.transform.localEulerAngles = enemyAngle3;
            }
            if (soldierDirection == 4)
            {
                this.transform.localEulerAngles = enemyAngle4;
            }

        }
    }

    public bool CaptureDecision()
    {
        Vector3 playerArray = moveScript.GetArray();
        Debug.Log(playerArray);
        Debug.Log(arrayPosX);
        Debug.Log(arrayPosY);
        Debug.Log(arrayPosZ);
        if ((playerArray.x == arrayPosX) && (playerArray.y == arrayPosY) && (playerArray.z == arrayPosZ))
        {
            Debug.Log("1ｄ");
        }
        Debug.Log(soldierDirection);
        switch (soldierDirection)
        {
            case 1:
                if ((playerArray.x == arrayPosX) && (playerArray.y == arrayPosY) && (playerArray.z == arrayPosZ + 1))
                {
                    Debug.Log("a");
                    moveScript.gameOverFlag = true;
                    return true;
                }
                break;
            case 2:
                if ((playerArray.x == arrayPosX + 1) && (playerArray.y == arrayPosY) && (playerArray.z == arrayPosZ))
                {

                    Debug.Log("b");
                    moveScript.gameOverFlag = true;
                    return true;
                }
                break;

            case 3:
                if ((playerArray.x == arrayPosX) && (playerArray.y == arrayPosY) && (playerArray.z == arrayPosZ - 1))
                {

                    Debug.Log("c");
                    moveScript.gameOverFlag = true;
                    return true;
                }
                break;

            case 4:
                if ((playerArray.x == arrayPosX - 1) && (playerArray.y == arrayPosY) && (playerArray.z == arrayPosZ))
                {

                    Debug.Log("d");
                    moveScript.gameOverFlag = true;
                    return true;
                }
                break;
        }
        return false;
    }
}
