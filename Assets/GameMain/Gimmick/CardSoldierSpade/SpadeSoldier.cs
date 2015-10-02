using UnityEngine;
using System.Collections;

public class SpadeSoldier : BaseGimmick {

    const float MOVE_SPEED = 1.0f;    // 移動速度
    const float SPEED = 0.05f;    // 移動速度

    public bool modePatrol;
    public int modeRun;
    public int runNumber; //ステージの端っこについたら
   
    public bool startFlag;

    public bool moveing;
    public bool returnFlag;

    //アリスの情報を仕入れる
    public GameObject alice;
    private Player moveScript;
    //public GameObject stage;
    //private Stage stageScript;
    //追加
    public int soldierDirection;
    //トランプ兵の歩数計算
    public int timeCount;

    Vector3 enemyAngle1;
    Vector3 enemyAngle2;
    Vector3 enemyAngle3;
    Vector3 enemyAngle4;


    bool EndFlag;
    public enum ArrayMove
    {
        PLUS_X,     // X方向にプラス
        MINUS_X,    // X方向にマイナス
        PLUS_Y,     // Y方向にプラス
        MINUS_Y,    // Y方向にマイナス
        PLUS_Z,     // Z方向にプラス
        MINUS_Z,    // Z方向にマイナス
    }

    public int arrayPosX;                           // 配列上での座標Ｘ
    public int arrayPosY;                           // 配列上での座標Ｙ
    public int arrayPosZ;                           // 配列上での座標Ｚ

    public Vector3 buttonInputPosition;  // ボタン入力時の座標


    //追加(黒い人)
    public int backDirection;//１個前を保存する
    //回転して進んだ時の建てます
    public bool rotFlag1;
    public bool rotFlag2;
    public bool rotFlag3;
    public bool rotFlag4;
    //規定ルートを周回した数
    public int rotRoopCount;
    //定数
    const int SQUARE = 4;


    //四角の１辺の長さ(ここをかえるとトランプ兵の場所を変えることができます)
    public int one_Side;



    private GameObject pause;
    private Pause pauseScript;
    // Use this for initialization
    void Start()
    {
        pause = GameObject.Find("Pause");
        alice = GameObject.Find("Alice");
        pauseScript = pause.GetComponent<Pause>();
        moveScript = alice.GetComponent<Player>();
        //トランプ兵の回転処理
        enemyAngle1 = new Vector3(0, 0, 0);
        enemyAngle2 = new Vector3(0, 90, 0);
        enemyAngle3 = new Vector3(0, 180, 0);
        enemyAngle4 = new Vector3(0, 270, 0);

        startFlag = false;
        EndFlag = false;
        buttonInputPosition = new Vector3(0, 0, 0);
        
        //移動制御
        soldierDirection = 1;

        timeCount = 0;
        //moveScript = alice.GetComponent<Player>();

        arrayPosX = 0;
        arrayPosY = 0;
        arrayPosZ = 0;

        returnFlag = false;
        moveing = false;
        //追加(黒人(偽))
        backDirection = 1;
        //方向を変えて移動したときに保存する
        rotFlag1 = false;
        rotFlag2 = false;
        rotFlag3 = false;
        rotFlag4 = false;

        //１つの辺あたりの長さを決める
        one_Side = 2;


        //runNumber = 4;
        modePatrol = false;
        //modeRun = 1;
       
        if(modeRun == 1)
        {
            this.transform.localEulerAngles = enemyAngle1;
            soldierDirection = 1;
        }
        else if (modeRun == 2)
        {
            this.transform.localEulerAngles = enemyAngle2;
            soldierDirection = 2;
        }
        else if (modeRun == 3)
        {
            this.transform.localEulerAngles = enemyAngle3;
            soldierDirection = 3;
        }
        else if (modeRun == 4)
        {
            this.transform.localEulerAngles = enemyAngle4;
            soldierDirection = 4;
        }
	}


    public void SetData(int Direction,int sideNumber,bool pat,int PosY,int PosX, int PosZ)
    {
        if (Direction == 1)
        {
            this.transform.localEulerAngles = enemyAngle1;
            soldierDirection = 1;
            backDirection =1;
            modeRun = 1;
        }
        else if (Direction == 2)
        {
            this.transform.localEulerAngles = enemyAngle2;
            soldierDirection = 2;
            backDirection =2;
            modeRun = 2;
        }
        else if (Direction == 3)
        {
            this.transform.localEulerAngles = enemyAngle3;
            soldierDirection = 3;
            backDirection =3;
            modeRun = 3;
        }
        else if (Direction == 4)
        {
            this.transform.localEulerAngles = enemyAngle4;
            soldierDirection = 4;
            backDirection =4;
            modeRun = 4;
        }

        one_Side = sideNumber;
        modePatrol = pat;

        arrayPosX = PosX;
        arrayPosY = PosY;
        arrayPosZ = PosZ;
    }

    //アリスが動いたときに呼ばれる関数
    public override void OnAliceMove(int aliceMoveTime)
    {
        bool flag;
        //修正箇所１
        flag = CaptureDecision();
        if(flag == false)
        {
            returnFlag = false;

            if (modePatrol == true)
            {

                //(moveScript)プレイヤーの歩数と(timeCount)トランプの歩数を比べる
                if (timeCount < aliceMoveTime)
                {
                    moveing = true;
                    //this.transform.Translate(Vector3.forward * MOVE_SPEED);

                    //仮の保存座標に現在座標を入れる
                    buttonInputPosition = this.transform.localPosition;

                    //
                    if ((startFlag == true) && (timeCount % one_Side == 0))
                    {
                        ////端っこにして回転していないとき
                        //if (((rotFlag1 == false) && (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                        // (rotFlag2 == false) && (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 2) ||
                        // (rotFlag3 == false) && (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 3) ||
                        // (rotFlag4 == false) && (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 4)))
                        if ((timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                           (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 2) ||
                           (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 3) ||
                           (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 4))
                        {
                            soldierDirection++;

                            //向きが一周したら最初の向きに戻す
                            if (soldierDirection == 5)
                            {
                                soldierDirection = 1;
                            }


                            //回転が終わって次に入力された時
                            switch (backDirection)
                            {
                                case 1: rotFlag1 = true; break;
                                case 2: rotFlag2 = true; break;
                                case 3: rotFlag3 = true; break;
                                case 4: rotFlag4 = true; break;
                            }
                        }

                    }
                    else
                    {
                        startFlag = true;
                    }
                    //一個前を回転を保存する
                    backDirection = soldierDirection;
                    //カウントを進める
                    timeCount += 1;

                    //トランプ兵が規定ルートを一周したら
                    if ((rotFlag1 == true) &&
                    (rotFlag2 == true) &&
                    (rotFlag3 == true) &&
                    (rotFlag4 == true))
                    {
                        //周回した数を足し移動したフラグを消す
                        rotRoopCount += 1;
                        rotFlag1 = false;
                        rotFlag2 = false;
                        rotFlag3 = false;
                        rotFlag4 = false;

                    }

                    //向きが一周したら最初の向きに戻す
                    if (soldierDirection == 5)
                    {
                        soldierDirection = 1;
                    }

                    //変数に応じて回転を代入する
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
            else if (modePatrol == false)
            {


                if (timeCount < aliceMoveTime)
                {
                    moveing = true;
                    //this.transform.Translate(Vector3.forward * MOVE_SPEED);

                    //カウントを進める
                    timeCount += 1;
                    //仮の保存座標に現在座標に入れる
                    buttonInputPosition = this.transform.localPosition;

                }



            }      
        }
        else
        {
            EndFlag = true;
        }
       

    }

    //アリスが戻った時に呼ばれる関数
    public override void OnAliceReturn(int aliceMoveTime)
    {

        if (EndFlag == true)
        {
            EndFlag = false;
        }
        else
        {
            returnFlag = true;

            //トランプ兵が動くフラグ

            if (modePatrol == true)
            {
                moveing = true;

                //仮の保存座標に現在座標に入れる
                buttonInputPosition = this.transform.localPosition;

                if (timeCount >= aliceMoveTime)
                {
                    moveing = true;
                    timeCount -= 1;

                    if ((rotRoopCount >= 1) &&
                       (rotFlag1 == false) &&
                       (rotFlag2 == false) &&
                       (rotFlag3 == false) &&
                       (rotFlag4 == false) &&
                       (timeCount - (one_Side * 4 * rotRoopCount) == 1))
                    {
                        rotRoopCount -= 1;
                        rotFlag1 = true;
                        rotFlag2 = true;
                        rotFlag3 = true;
                        rotFlag4 = true;


                    }


                    backDirection = soldierDirection;

                }

            }
            else if (modePatrol == false)
            {

                if (timeCount >= aliceMoveTime)
                {
                    moveing = true;
                    buttonInputPosition = this.transform.localPosition;

                    if (modeRun == 1 || modeRun == 3)
                    {
                        if ((startFlag == true) && (timeCount % one_Side == 0))
                        {
                            soldierDirection -= 2;

                        }
                        else
                        {
                            startFlag = true;
                        }

                        if (soldierDirection <= 0)
                        {
                            soldierDirection = 3;
                        }

                        if (soldierDirection == 1)
                        {
                            this.transform.localEulerAngles = enemyAngle1;
                        }

                        if (soldierDirection == 3)
                        {
                            this.transform.localEulerAngles = enemyAngle3;
                        }
                    }
                    else if (modeRun == 2 || modeRun == 4)
                    {
                        if ((startFlag == true) && (timeCount % one_Side == 0))
                        {
                            soldierDirection -= 2;
                        }
                        else
                        {
                            startFlag = true;
                        }

                        if (soldierDirection <= 0)
                        {
                            soldierDirection = 4;
                        }

                        if (soldierDirection == 2)
                        {
                            this.transform.localEulerAngles = enemyAngle2;
                        }

                        if (soldierDirection == 4)
                        {
                            this.transform.localEulerAngles = enemyAngle4;
                        }
                    }

                    timeCount -= 1;
                }
            }

            if (timeCount == 0)
            {
                startFlag = false;
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if(pauseScript.pauseFlag == false)
        {
            Move();
  
        }

        
	}

    //実際に動かす時
    public void Move()
    {
 
		//----------------------
		switch (moveing)
		{
            case true:

                switch (returnFlag)
                {
                    case false://進むとき
                        transform.Translate(Vector3.forward * SPEED);
                        //以下停止コード
                        switch (soldierDirection)
                        {
                            //Z軸を調整する
                            case 1:
                                //目的地に着いたとき
                                if (transform.localPosition.z >= buttonInputPosition.z + 1)
                                {
                                    Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z + 1);
                                    
                                    if(modePatrol == true)
                                    {

                                    }
                                    else
                                    {
                                        if ((startFlag == true) && (timeCount % one_Side == 0))
                                        {
                                            soldierDirection += 2;
                                           
                                        }
                                        else
                                        {
                                            startFlag = true;
                                        }
                                        if (soldierDirection >= 5)
                                        {
                                            soldierDirection = 1;
                                        }

                                        if (soldierDirection == 1)
                                        {
                                            this.transform.localEulerAngles = enemyAngle1;
                                        }

                                        if (soldierDirection == 3)
                                        {
                                            this.transform.localEulerAngles = enemyAngle3;
                                        }
                                    }
                                    

                                    MoveFinish(position, ArrayMove.PLUS_Z);
                                }
                                break;
                            case 3:
                                //目的地に着いたとき
                                if (transform.localPosition.z <= buttonInputPosition.z - 1)
                                {
                                    Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z - 1);
                                   

                                    if (modePatrol == true)
                                    {

                                    }
                                    else
                                    {
                                        if ((startFlag == true) && (timeCount % one_Side == 0))
                                        {
                                            soldierDirection += 2;
                                           
                                        }
                                        else
                                        {
                                            startFlag = true;
                                        }
                                        if (soldierDirection >= 5)
                                        {
                                            soldierDirection = 1;
                                        }

                                        if (soldierDirection == 1)
                                        {
                                            this.transform.localEulerAngles = enemyAngle1;
                                        }

                                        if (soldierDirection == 3)
                                        {
                                            this.transform.localEulerAngles = enemyAngle3;
                                        }
                                       

                                    }
                                    //移動終了時に呼ばれる
                                    MoveFinish(position, ArrayMove.MINUS_Z);
                                }
                                break;

                            //X軸を調整する
                            case 4:
                                //目的地に着いたとき
                                if (transform.localPosition.x <= buttonInputPosition.x - 1)
                                {
                                    Vector3 position = new Vector3(buttonInputPosition.x - 1, transform.localPosition.y, transform.localPosition.z);

                                    if (modePatrol == true)
                                    {

                                    }
                                    else
                                    {
                                        if ((startFlag == true) && (timeCount % one_Side == 0))
                                        {
                                            soldierDirection += 2; 
                                        }
                                        else
                                        {
                                            startFlag = true;
                                        }

                                        if (soldierDirection >= 5)
                                        {
                                            soldierDirection = 2;
                                        }

                                        if (soldierDirection == 2)
                                        {
                                            this.transform.localEulerAngles = enemyAngle2;
                                        }

                                        if (soldierDirection == 4)
                                        {
                                            this.transform.localEulerAngles = enemyAngle4;
                                        }
                                    }
                                    MoveFinish(position, ArrayMove.MINUS_X);
                                }
                                break;
                            case 2:
                                //目的地に着いたとき
                                if (transform.localPosition.x >= buttonInputPosition.x + 1)
                                {
                                    Vector3 position = new Vector3(buttonInputPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                                    


                                    if (modePatrol == true)
                                    {

                                    }
                                    else
                                    {
                                        if ((startFlag == true) && (timeCount % one_Side == 0))
                                        {
                                            
                                            soldierDirection += 2;
                                        }
                                        else
                                        {
                                            startFlag = true;
                                        }

                                        if (soldierDirection >= 5)
                                        {
                                            soldierDirection = 2;
                                        }

                                        if (soldierDirection == 2)
                                        {
                                            this.transform.localEulerAngles = enemyAngle2;
                                        }

                                        if (soldierDirection == 4)
                                        {
                                            this.transform.localEulerAngles = enemyAngle4;
                                        }

                                        
                                    }
                                    //移動終了
                                    MoveFinish(position, ArrayMove.PLUS_X);
                                }
                                break;
                        }
                        break;

                    case true://戻るとき

                        //実際の移動部分
                        transform.Translate(Vector3.back * SPEED);
                        //以下停止コード
                        switch (soldierDirection)
                        {
                            case 1:


                                if (transform.localPosition.z <= buttonInputPosition.z - 1)
                                {
                                    Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z - 1);
                                    MoveFinish(position, ArrayMove.MINUS_Z);
                                   
                                    if(modePatrol == true)
                                    {
                                        if ((startFlag == true) && (timeCount % one_Side == 0))
                                        {
                                            ////端っこにいて回転した後だった場合
                                            //if (((rotFlag1 == true)&&(timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                                            //  (rotFlag2 == true) &&  (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side *2) ||
                                            //  (rotFlag3 == true) && (timeCount -  (one_Side * SQUARE * rotRoopCount) == one_Side *3) ||
                                            //  (rotFlag4 == true) && (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side *4)))
                                            if ((timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 2) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 3) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 4)) //
                                            {
                                                soldierDirection--;

                                                //回転変数が一周したら
                                                if (soldierDirection == 0)
                                                {
                                                    soldierDirection = 4;
                                                }
                                                //現在の回転を指示する変数をみて対象の回転フラグを立てる
                                                switch (soldierDirection)
                                                {
                                                    case 1: rotFlag1 = false; break;
                                                    case 2: rotFlag2 = false; break;
                                                    case 3: rotFlag3 = false; break;
                                                    case 4: rotFlag4 = false; break;
                                                }
                                            }

                                        }
                                        //変数に応じて回転を代入する
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
                                    else
                                    {
                                       

                                    }
                                    //moveDirection = MoveDirection.FRONT;
                                }

                               
                                break;
                            case 3:
                                if (transform.localPosition.z >= buttonInputPosition.z + 1)
                                {
                                    

                                    Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z + 1);
                                    MoveFinish(position, ArrayMove.PLUS_Z);
                                    if (modePatrol == true)
                                    {
                                        if ((startFlag == true) && (timeCount % one_Side == 0))
                                        {
                                            ////端っこにいて回転した後だった場合
                                            //if (((rotFlag1 == true)&&(timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                                            //  (rotFlag2 == true) &&  (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side *2) ||
                                            //  (rotFlag3 == true) && (timeCount -  (one_Side * SQUARE * rotRoopCount) == one_Side *3) ||
                                            //  (rotFlag4 == true) && (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side *4)))
                                            if ((timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 2) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 3) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 4)) //
                                            {
                                                soldierDirection--;

                                                //回転変数が一周したら
                                                if (soldierDirection == 0)
                                                {
                                                    soldierDirection = 4;
                                                }
                                                //現在の回転を指示する変数をみて対象の回転フラグを立てる
                                                switch (soldierDirection)
                                                {
                                                    case 1: rotFlag1 = false; break;
                                                    case 2: rotFlag2 = false; break;
                                                    case 3: rotFlag3 = false; break;
                                                    case 4: rotFlag4 = false; break;
                                                }
                                            }

                                        }
                                        //変数に応じて回転を代入する
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
                                    else
                                    {
                                        

                                    }
                                    //moveDirection = MoveDirection.BACK;

                                }
                                
                                break;
                            case 4:
                               
                                if (transform.localPosition.x >= buttonInputPosition.x + 1)
                                {
                                 

                                    Vector3 position = new Vector3(buttonInputPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                                    MoveFinish(position, ArrayMove.PLUS_X);
                                    //moveDirection = MoveDirection.LEFT;
                                    if (modePatrol == true)
                                    {
                                        if ((startFlag == true) && (timeCount % one_Side == 0))
                                        {
                                            ////端っこにいて回転した後だった場合
                                            //if (((rotFlag1 == true)&&(timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                                            //  (rotFlag2 == true) &&  (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side *2) ||
                                            //  (rotFlag3 == true) && (timeCount -  (one_Side * SQUARE * rotRoopCount) == one_Side *3) ||
                                            //  (rotFlag4 == true) && (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side *4)))
                                            if ((timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 2) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 3) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 4)) //
                                            {
                                                soldierDirection--;

                                                //回転変数が一周したら
                                                if (soldierDirection == 0)
                                                {
                                                    soldierDirection = 4;
                                                }
                                                //現在の回転を指示する変数をみて対象の回転フラグを立てる
                                                switch (soldierDirection)
                                                {
                                                    case 1: rotFlag1 = false; break;
                                                    case 2: rotFlag2 = false; break;
                                                    case 3: rotFlag3 = false; break;
                                                    case 4: rotFlag4 = false; break;
                                                }
                                            }

                                        }
                                        //変数に応じて回転を代入する
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
                                    else
                                    {
                                       
                                    }
                                   
                                }

                                break;
                            case 2:
                                if (transform.localPosition.x <= buttonInputPosition.x - 1)
                                {
                                    Debug.Log("Return right");

                                    Vector3 position = new Vector3(buttonInputPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
                                    MoveFinish(position, ArrayMove.MINUS_X);
                                    if (modePatrol == true)
                                    {
                                        if ((startFlag == true) && (timeCount % one_Side == 0))
                                        {
                                            ////端っこにいて回転した後だった場合
                                            //if (((rotFlag1 == true)&&(timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                                            //  (rotFlag2 == true) &&  (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side *2) ||
                                            //  (rotFlag3 == true) && (timeCount -  (one_Side * SQUARE * rotRoopCount) == one_Side *3) ||
                                            //  (rotFlag4 == true) && (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side *4)))
                                            if ((timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 2) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 3) ||
                                               (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 4)) //
                                            {
                                                soldierDirection--;

                                                //回転変数が一周したら
                                                if (soldierDirection == 0)
                                                {
                                                    soldierDirection = 4;
                                                }
                                                //現在の回転を指示する変数をみて対象の回転フラグを立てる
                                                switch (soldierDirection)
                                                {
                                                    case 1: rotFlag1 = false; break;
                                                    case 2: rotFlag2 = false; break;
                                                    case 3: rotFlag3 = false; break;
                                                    case 4: rotFlag4 = false; break;
                                                }
                                            }

                                        }
                                        //変数に応じて回転を代入する
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
                                    else
                                    {
                                      
                                    }
                                    
                                   // moveDirection = MoveDirection.RIGHT;
                                }

                                break;
                            case 0:
                                break;
                        }
                        
                        
                        break;

                }
                break;
            //移動が許可されてないとき
            case false:
                break;
		}
		//--------------------
	}
  

    // 移動完了
    public void MoveFinish(Vector3 position, ArrayMove arrayMove)
    {
        transform.localPosition = position;     // 座標を変更
        ChangeArrayPosition(arrayMove);         // 配列上の位置を変更
        moveing = false;
        //修正箇所２
        CaptureDecision();
        //moveAction = MoveAction.NONE;           // アリスの行動を無しに
        //moveFinishFlag = true;                  // 移動完了フラグを真に
    }

    // 配列上の位置を変更する
    public void ChangeArrayPosition(ArrayMove arrayMove)
    {
        switch (arrayMove)
        {
            case ArrayMove.PLUS_X: arrayPosX++; break;      // 配列上の座標Xに１プラス
            case ArrayMove.MINUS_X: arrayPosX--; break;     // 配列上の座標Xに１マイナス
            case ArrayMove.PLUS_Y: arrayPosY++; break;      // 配列上の座標Yに１プラス
            case ArrayMove.MINUS_Y: arrayPosY--; break;     // 配列上の座標Yに１マイナス
            case ArrayMove.PLUS_Z: arrayPosZ++; break;      // 配列上の座標Zに１プラス
            case ArrayMove.MINUS_Z: arrayPosZ--; break;     // 配列上の座標Zに１マイナス
        }
    }

    public void SetPat(){
        modePatrol = true;
    }


    //
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
