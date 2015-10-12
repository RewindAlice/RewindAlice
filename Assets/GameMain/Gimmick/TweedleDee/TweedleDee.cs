using UnityEngine;
using System.Collections;

public class TweedleDee :BaseGimmick
{
    public GameObject stage;
    public Stage stageScript;
    // 移動方向
    public enum MoveDirection
    {
        NONE,
        FRONT,  // 前
        BACK,   // 後
        LEFT,   // 左
        RIGHT,  // 右
        UP,     // 上
        DOWN,   // 下
        STOP,   // 止
    }

    const float MOVE_SPEED = 1.0f;    // 移動速度
    const float SPEED = 0.05f;    // 移動速度

    public bool moveFlag;
    public bool returnFlag;

    public int dumDirection;
    //キャラの歩数計算
    public int timeCount;

    Vector3 enemyAngle1;
    Vector3 enemyAngle2;
    Vector3 enemyAngle3;
    Vector3 enemyAngle4;

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

    //過去の向きを保存
    public int[] beforeDirection;
    public int[] notMoveTrun;

    //------------------------
    //初期化関数
    //------------------------
	void Start () 
    {
        //オブジェクトの検索
        stage = GameObject.Find("Stage");
        stageScript = stage.GetComponent<Stage>();

        //キャラの回転処理
        enemyAngle1 = new Vector3(0, 0, 0);
        enemyAngle2 = new Vector3(0, 90, 0);
        enemyAngle3 = new Vector3(0, 180, 0);
        enemyAngle4 = new Vector3(0, 270, 0);

        //ボタン入力時のオブジェクトの位置
        buttonInputPosition = new Vector3(0, 0, 0);

        //経過したターン数
        timeCount = 0;

        //動けるかどうか
        moveFlag = false;
        //時を戻したときの挙動かどうか
        returnFlag = false;
        
        //過去の向きの保存用変数の初期化
        beforeDirection = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        notMoveTrun = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        //初期の向きを代入
        beforeDirection[0] = dumDirection;
	}

    //------------------------
    //座標・向きの初期化関数
    //------------------------
    public void Initialize(int direction, int x, int y, int z)
    {
        //向きの初期化
        if (direction == 1)
        {
            this.transform.localEulerAngles = enemyAngle1;
            dumDirection = 1;
        }
        else if (direction == 2)
        {
            this.transform.localEulerAngles = enemyAngle2;
            dumDirection = 3;
        }
        else if (direction == 3)
        {
            this.transform.localEulerAngles = enemyAngle3;
            dumDirection = 4;
        }
        else if (direction == 4)
        {
            this.transform.localEulerAngles = enemyAngle4;
            dumDirection = 2;
        }
        

        //座標の初期化
        arrayPosX = x;
        arrayPosY = y;
        arrayPosZ = z;
    }

    //---------------------------------
    //アリスが動いたときに呼ばれる関数
    //---------------------------------
    public override void OnAliceMove(int aliceMoveTime)
    {
        //リターンフラグの初期化
        returnFlag = false;

        //(moveScript)プレイヤーの歩数と(timeCount)歩数を比べる
        if (timeCount < aliceMoveTime)
        {     
            int roopMax = 4;
            for (int i = 0; i < roopMax; i++)
            {
                switch (dumDirection)
                {
                    case 1:
                        if ((stageScript.DumBesideDecision(arrayPosX, arrayPosY, arrayPosZ + 1)) && (stageScript.DumBesideDownDecision(arrayPosX, arrayPosY, arrayPosZ + 1)))
                        {
                            moveFlag = true;
                        }
                        break;
                    case 3:
                        if ((stageScript.DumBesideDecision(arrayPosX, arrayPosY, arrayPosZ - 1)) && (stageScript.DumBesideDownDecision(arrayPosX, arrayPosY, arrayPosZ - 1)))
                        {    
                            moveFlag = true;
                        }
                        break;
                    case 4:
                        if ((stageScript.DumBesideDecision(arrayPosX - 1, arrayPosY, arrayPosZ)) && (stageScript.DumBesideDownDecision(arrayPosX - 1, arrayPosY, arrayPosZ)))
                        {
                            moveFlag = true;
                        }
                        break;
                    case 2:
                        if ((stageScript.DumBesideDecision(arrayPosX + 1, arrayPosY, arrayPosZ)) && (stageScript.DumBesideDownDecision(arrayPosX + 1, arrayPosY, arrayPosZ)))
                        { 
                            moveFlag = true;
                        }
                        break;
                    default:
                        break;
                }

                //もし動くフラグがtrueなら
                if (moveFlag == true)
                {
                    break;
                }
                else
                {
                    //左回転させる
                    dumDirection--;

                    //向きが一周したら最初の向きに戻す
                    if (dumDirection == 0)
                    {
                        dumDirection = 4;
                    }

                    //もし動けなかったら
                    if(i == 3)
                    {
                        notMoveTrun[timeCount + 1] = 1;
                    }
                }
            }

            //現在座標を保存
            buttonInputPosition = this.transform.localPosition;

            //ターン数を進める
            timeCount++;

            //向きを保存
            beforeDirection[timeCount] = dumDirection;
            //向きの変更
            ChangeDirection();
        }
    }


    //アリスが戻った時に呼ばれる関数
    public override void OnAliceReturn(int aliceMoveTime)
    {

        returnFlag = true;
    
        if (timeCount >= aliceMoveTime)
        {
        
                Debug.Log("通せる");
           if(notMoveTrun[timeCount] == 0)
           {
               int roopMax = 4;
               for (int i = 0; i < roopMax; i++)
               {
                   Debug.Log("テスト");
                   //deeの正面から右側を判定します。
                   switch (dumDirection)
                   {
                       case 1://Tweedleの角度０の時
                           if ((stageScript.DumBesideDecision(arrayPosX, arrayPosY, arrayPosZ - 1)) && (stageScript.DumBesideDownDecision(arrayPosX, arrayPosY, arrayPosZ - 1)))
                           {

                               moveFlag = true;
                           }
                           break;
                       case 3://Tweedleの角度180の時

                           if ((stageScript.DumBesideDecision(arrayPosX, arrayPosY, arrayPosZ + 1)) && (stageScript.DumBesideDownDecision(arrayPosX, arrayPosY, arrayPosZ + 1)))
                           {
                               moveFlag = true;

                           }
                           break;
                       case 4://Tweedleの角度270の時

                           if ((stageScript.DumBesideDecision(arrayPosX + 1, arrayPosY, arrayPosZ)) && (stageScript.DumBesideDownDecision(arrayPosX + 1, arrayPosY, arrayPosZ)))
                           {
                               moveFlag = true;

                               Debug.Log("break２");
                           }

                           break;
                       case 2://Tweedleの角度90の時

                           if ((stageScript.DumBesideDecision(arrayPosX - 1, arrayPosY, arrayPosZ)) && (stageScript.DumBesideDownDecision(arrayPosX - 1, arrayPosY, arrayPosZ)))
                           {

                               moveFlag = true;
                           }
                           break;
                       default:
                           break;
                   }
                   if (moveFlag == true)
                   {
                       break;
                   }
                   else
                   {
                       // 回転させる
                       dumDirection++;

                       //向きが一周したら最初の向きに戻す
                       if (dumDirection == 5)
                           dumDirection = 1;
                   }

               }
           }
        }


        notMoveTrun[timeCount] = 0;

        //カウントを戻す
        timeCount -= 1;
 
      
        //仮の保存座標に現在座標に入れる
        buttonInputPosition = this.transform.localPosition;

    }

    //-----------------
	//アップデート関数
    //-----------------
	void Update () 
    {
        //動かす
        Move();
	}

    //---------------
    //移動させる関数
    //---------------
    public void Move()
    {
        switch (returnFlag)
        {
            //ターン数が進むとき
            case false:
                if (moveFlag)
                {
                    //動くスピードを設定
                    transform.Translate(Vector3.forward * SPEED);
                    
                    //以下停止コード
                    switch (dumDirection)
                    {
                        //Z軸を調整する
                        case 1:
                            //目的地に着いたとき
                            if (transform.localPosition.z >= buttonInputPosition.z + 1)
                            {
                                //誤差の調節
                                Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z + 1);
                                //移動を終える
                                MoveFinish(position, ArrayMove.PLUS_Z);
                            }
                            break;
                        case 3:
                            //目的地に着いたとき
                            if (transform.localPosition.z <= buttonInputPosition.z - 1)
                            {
                                //誤差の調節
                                Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z - 1);
                                //移動を終える
                                MoveFinish(position, ArrayMove.MINUS_Z);
                            }
                            break;

                        //X軸を調整する
                        case 4:
                            //目的地に着いたとき
                            if (transform.localPosition.x <= buttonInputPosition.x - 1)
                            {
                                //誤差の調節
                                Vector3 position = new Vector3(buttonInputPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
                                //移動を終える
                                MoveFinish(position, ArrayMove.MINUS_X);
                            }
                            break;
                        case 2:
                            //目的地に着いたとき
                            if (transform.localPosition.x >= buttonInputPosition.x + 1)
                            {
                                //誤差の調節
                                Vector3 position = new Vector3(buttonInputPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                                //移動を終える
                                MoveFinish(position, ArrayMove.PLUS_X);
                            }
                            break;
                    }
                }
                else
                {
                    moveFlag = false;
                }
                break;

            //ターン数が戻るとき
            case true:
                       
                if (moveFlag)
                {
                    //動くスピードを設定
                    transform.Translate(Vector3.back * SPEED);

                    //以下停止コード
                    switch (dumDirection)
                    {
                        //Z軸を調整する
                        case 1:
                            if (transform.localPosition.z <= buttonInputPosition.z - 1)
                            {
                                //誤差の調節
                                Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z - 1);
                                //移動を終える
                                MoveFinish(position, ArrayMove.MINUS_Z);
                              
                            }
                            break;
                        case 3:
                            //目的地に着いたとき
                            if (transform.localPosition.z >= buttonInputPosition.z + 1)
                            {
                                //誤差の調節
                                Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z + 1);
                                //移動を終える
                                MoveFinish(position, ArrayMove.PLUS_Z);
                           
                            }
                            break;

                        //X軸を調整する
                        case 4:
                            //目的地に着いたとき
                            if (transform.localPosition.x >= buttonInputPosition.x + 1)
                            {
                                //誤差の調節
                                Vector3 position = new Vector3(buttonInputPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                                //移動を終える
                                MoveFinish(position, ArrayMove.PLUS_X);
                            }
                            break;
                        case 2:
                            //目的地に着いたとき
                            if (transform.localPosition.x <= buttonInputPosition.x - 1)
                            {
                                //誤差の調節
                                Vector3 position = new Vector3(buttonInputPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
                                //移動を終える
                                MoveFinish(position, ArrayMove.MINUS_X);
                            
                            }
                            break;
                    }
                }
                else
                {
                    moveFlag = false;
                }
                      
                break;
        }
   
    }

    //-------------------
    //移動完了時呼ぶ関数
    //-------------------
    public void MoveFinish(Vector3 position, ArrayMove arrayMove)
    {
        transform.localPosition = position;     // 座標を変更
        ChangeArrayPosition(arrayMove);         // 配列上の位置を変更
        stageScript.DumGimmickDecision(arrayPosX, arrayPosY, arrayPosZ);

        //ターン数が戻った時の処理なら
        if(returnFlag == true)
        {
            //前回の向きの取得
            dumDirection = beforeDirection[timeCount];
            
            //向きの変更
            ChangeDirection();
            
           
        }
        //リターンフラグの初期化
        returnFlag = false;
        //動きを止める
        moveFlag = false;
    }

    //---------------------------
    //配列上の位置を変更する関数
    //---------------------------
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

    //---------------------
    //向きの変更をする関数
    //---------------------
    public void ChangeDirection()
    {
        //変数に応じて回転を代入する
        if (dumDirection == 1)
        {
            this.transform.localEulerAngles = enemyAngle1;
        }
        if (dumDirection == 2)
        {
            this.transform.localEulerAngles = enemyAngle2;
        }
        if (dumDirection == 3)
        {
            this.transform.localEulerAngles = enemyAngle3;
        }
        if (dumDirection == 4)
        {
            this.transform.localEulerAngles = enemyAngle4;
        }
    }

}
