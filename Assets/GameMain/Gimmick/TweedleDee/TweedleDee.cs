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
    public int runNumber; //ステージの端っこについたら
    public bool startFlag;
    public bool moving;
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

    //追加(黒い人)
    public int backDirection;//１個前を保存する
    //回転して進んだ時に建てます
    public bool rotFlag1;
    public bool rotFlag2;
    public bool rotFlag3;
    public bool rotFlag4;

    //規定ルートを周回した数
    public int rotRoopCount;

    //定数
    const int SQUARE = 4;

    //四角の１辺の長さ(ここをかえるとキャラの場所を変えることができます)
    public int one_Side;

    //Tweedle部分追加

    public int targetBreakCount;

    public bool only_Turn = false;

    // 自動移動用フラグ
    public bool autoMoveFlag;       // 自動移動フラグ
    public MoveDirection autoMove;
    //回転歩数を保存
    public int[] directionPoint;
    //回転した数
    public int directionCount;

	// Use this for initialization
	void Start () 
    {
        stage = GameObject.Find("Stage");
        stageScript = stage.GetComponent<Stage>();

        //キャラの回転処理
        enemyAngle1 = new Vector3(0, 0, 0);
        enemyAngle2 = new Vector3(0, 90, 0);
        enemyAngle3 = new Vector3(0, 180, 0);
        enemyAngle4 = new Vector3(0, 270, 0);

        startFlag = false;
        buttonInputPosition = new Vector3(0, 0, 0);

        //移動制御
        //	dumDirection = 1;
        timeCount = 0;

        returnFlag = false;
        moving = false;
        moveFlag = false;
        //追加(黒人(偽))
        backDirection = 1;
        //方向を変えて移動したときに保存する
        rotFlag1 = false;
        rotFlag2 = false;
        rotFlag3 = false;
        rotFlag4 = false;

        //１つの辺あたりの長さを決める
        one_Side = 90;

        //テスト
        directionPoint = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        directionCount = 0;
       
	}

    public void Initialize(int direction, int x, int y, int z)
    {
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
        arrayPosX = x;
        arrayPosY = y;
        arrayPosZ = z;
    }

    //アリスが動いたときに呼ばれる関数
    public override void OnAliceMove(int aliceMoveTime)
    {
      
        returnFlag = false;
        moving = true;

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
                            moveFlag = true;
                        break;
                    case 4:
                        if ((stageScript.DumBesideDecision(arrayPosX - 1, arrayPosY, arrayPosZ)) && (stageScript.DumBesideDownDecision(arrayPosX - 1, arrayPosY, arrayPosZ)))
                            moveFlag = true;
                        break;
                    case 2:
                        if ((stageScript.DumBesideDecision(arrayPosX + 1, arrayPosY, arrayPosZ)) && (stageScript.DumBesideDownDecision(arrayPosX + 1, arrayPosY, arrayPosZ)))
                            moveFlag = true;
                        break;
                    default:
                        break;
                }
                if (moveFlag == true)
                {
                    break;
                }
                else
                {//障害物があったら回転してない方向に行こうとする

                    //回転させる
                    dumDirection--;
                    //現在の歩数を代入しましょう
                    directionPoint[directionCount] = timeCount;
                    //回転した回数を増加
                    directionCount++;

                    Debug.Log("test01");
                    //向きが一周したら最初の向きに戻す
                    if (dumDirection == 0)
                        dumDirection = 4;
                }
            }



            //仮の保存座標に現在座標を入れる
            buttonInputPosition = this.transform.localPosition;

            if ((startFlag == true) && (timeCount % one_Side == 0))
            {

                ////端っこに居て回転していないとき
                //if ((timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
                //    (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 2) ||
                //    (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 3) ||
                //    (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 4))
                //{
                //    dumDirection++;
                //}
            }
            startFlag = true;

            //一個前の回転を保存する
            backDirection = dumDirection;
            //カウントを進める
            timeCount++;
            //向きが一周したら最初の向きに戻す
            if (dumDirection == 5)
                dumDirection = 1;

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


    //アリスが戻った時に呼ばれる関数
    public override void OnAliceReturn(int aliceMoveTime)
    {

        returnFlag = true;
        //キャラが動くフラグ
        moving = true;


        if (timeCount >= aliceMoveTime)
        {
            //歩数をとる
            int tmp = directionCount - 1;
            if (tmp <= 0)
            {
                tmp = 0;
            }

            if (directionPoint[tmp] == timeCount)
            {
                Debug.Log("通せる");
                only_Turn = false;
                int roopMax = 4;
                for (int i = 0; i < roopMax; i++)
                {


                    if (only_Turn == false)
                    {
                        Debug.Log("テスト");
                        //deeの正面から右側を判定します。
                        switch (dumDirection)
                        {
                            case 1://Tweedleの角度０の時
                                if ((stageScript.DumBesideDecision(arrayPosX + 1, arrayPosY, arrayPosZ)) && (stageScript.DumBesideDownDecision(arrayPosX + 1, arrayPosY, arrayPosZ)))
                                {

                                    moveFlag = true;
                                }
                                break;
                            case 3://Tweedleの角度180の時
                               
                                if ((stageScript.DumBesideDecision(arrayPosX - 1, arrayPosY, arrayPosZ)) && (stageScript.DumBesideDownDecision(arrayPosX - 1, arrayPosY, arrayPosZ)))
                                {
                                    moveFlag = true;

                                }
                                break;
                            case 4://Tweedleの角度270の時

                                if ((stageScript.DumBesideDecision(arrayPosX, arrayPosY, arrayPosZ + 1)) && (stageScript.DumBesideDownDecision(arrayPosX, arrayPosY, arrayPosZ + 1)))
                                {
                                    moveFlag = true;

                                    Debug.Log("break２");
                                }

                                break;
                            case 2://Tweedleの角度90の時
                             
                                if ((stageScript.DumBesideDecision(arrayPosX, arrayPosY, arrayPosZ - 1)) && (stageScript.DumBesideDownDecision(arrayPosX, arrayPosY, arrayPosZ - 1)))
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
                            //一回しか回転できなくする
                            only_Turn = true;
                            //回転座標を戻す
                            directionCount--;
                            Debug.Log("てスrp");
                            //向きが一周したら最初の向きに戻す
                            if (dumDirection == 5)
                                dumDirection = 1;
                        }
                    }
                }
            }




            //if ((startFlag == true) && (timeCount % one_Side == 0))
            //{
            //    //端っこにいて回転した後だった場合
            //    if ((timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side) ||
            //        (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 2) ||
            //        (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 3) ||
            //        (timeCount - (one_Side * SQUARE * rotRoopCount) == one_Side * 4))
            //    {
            //        dumDirection--;

            //        //回転変数が一周したら
            //        if (dumDirection == 0)
            //        {
            //            dumDirection = 4;
            //        }
            //    }
            //}
            //else
            //{
            //    startFlag = true;
            //}

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

            backDirection = dumDirection;
            //カウントを戻す
            timeCount -= 1;

            //this.transform.Translate(Vector3.forward * -MOVE_SPEED);
            //仮の保存座標に現在座標に入れる
            buttonInputPosition = this.transform.localPosition;

        }

        if (timeCount == 0)
        {
            startFlag = false;
        }
    }


	// Update is called once per frame
	void Update () 
    {
        Move();
	}


    // 自動移動設定
    public void AutoMoveSetting(MoveDirection direction)
    {
        autoMoveFlag = true;
        autoMove = direction;
    }


    //実際に動かす時
    public void Move()
    {
        //----------------------
        switch (moving)
        {
            case true:
               
                switch (returnFlag)
                {
                    case false://進むとき
                        if (moveFlag)
                        {
                           
                            transform.Translate(Vector3.forward * SPEED);
                            //以下停止コード
                            switch (dumDirection)
                            {
                                //Z軸を調整する
                                case 1:
                                    //目的地に着いたとき
                                    if (transform.localPosition.z >= buttonInputPosition.z + 1)
                                    {
                                        Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z + 1);
                                        MoveFinish(position, ArrayMove.PLUS_Z);
                                    }
                                    break;
                                case 3:
                                    //目的地に着いたとき
                                    if (transform.localPosition.z <= buttonInputPosition.z - 1)
                                    {
                                        Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z - 1);
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
                                        MoveFinish(position, ArrayMove.MINUS_X);
                                    }
                                    break;
                                case 2:
                                    //目的地に着いたとき
                                    if (transform.localPosition.x >= buttonInputPosition.x + 1)
                                    {
                                        Vector3 position = new Vector3(buttonInputPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                                        //移動終了
                                        MoveFinish(position, ArrayMove.PLUS_X);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            moving = false;
                            moveFlag = false;
                        }
                        break;

                    case true://戻るとき
                        //実際の移動部分
                        transform.Translate(Vector3.back * SPEED);
                        //以下停止コード

                        if (transform.localPosition.z <= buttonInputPosition.z - 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z - 1);
                            MoveFinish(position, ArrayMove.MINUS_Z);
                            //moveDirection = MoveDirection.FRONT;
                        }

                        if (transform.localPosition.z >= buttonInputPosition.z + 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, buttonInputPosition.z + 1);
                            MoveFinish(position, ArrayMove.PLUS_Z);
                            //moveDirection = MoveDirection.BACK;
                        }
                        if (transform.localPosition.x >= buttonInputPosition.x + 1)
                        {
                            Vector3 position = new Vector3(buttonInputPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.PLUS_X);
                        }
                        //moveDirection = MoveDirection.LEFT;
                        if (transform.localPosition.x <= buttonInputPosition.x - 1)
                        {

                            Vector3 position = new Vector3(buttonInputPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.MINUS_X);
                            // moveDirection = MoveDirection.RIGHT;
                        }
                        break;
                }
                break;
            //移動が許可されてないとき
            case false:
                break;
        }
    }

    // 移動完了
    public void MoveFinish(Vector3 position, ArrayMove arrayMove)
    {
        transform.localPosition = position;     // 座標を変更
        ChangeArrayPosition(arrayMove);         // 配列上の位置を変更
        moving = false;
        stageScript.DumGimmickDecision(arrayPosX, arrayPosY, arrayPosZ);
        //moveAction = MoveAction.NONE;           // アリスの行動を無しに
        //moveFinishFlag = true;                  // 移動完了フラグを真に
        moveFlag = false;
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

}
