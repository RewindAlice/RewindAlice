using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // Listを使用するため

public class Player : MonoBehaviour
{
    const int SAVE_NUM = 50;        // 移動情報の保存数
    const float SPEED_W = 0.02f;    // 移動速度（横方向）
    const float SPEED_H = 0.025f;    // 移動速度（縦方向）

    //ドラゴン参照
    const int DEFAULTINVISIBLECOUNT = 5; // 初期不可視時間
    const int DEFAULTHOLDKEYCOUNT = 100; // 初期鍵保持カウント
    public GameObject Stage; // ステージ(仮)
    public bool climbFlag = false; // 梯や蔦に捕まっているかどうか
    public bool invisibleFlag = false; // 不可視状態かどうか
    public int invisibleCount = 0; // 不可視カウント
    public bool touchCheshireFlag = false; // チェシャに触ったかどうか
    public bool getKeyFlag = false; // 鍵を持っているかどうか
    public int getKeyCount = 0; // 鍵の所持時間
    public int getKeyTiming = 0; // 鍵を手に入れたタイミング
    public bool touchKeyFlag = false; // 鍵に触ったかどうか
    public bool timeFlag; // 時間移動フラグ
    public bool timeBackFlag; // 時間巻き戻しフラグ
    //

    // プレイヤーの向き
    public enum PlayerAngle
    {
        NONE,
        FRONT,  // 前
        BACK,   // 後
        LEFT,   // 左
        RIGHT,  // 右
    }

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

    // 配列
    public enum ArrayMove
    {
        NONE,
        PLUS_X,     // X方向にプラス
        MINUS_X,    // X方向にマイナス
        PLUS_Y,     // Y方向にプラス
        MINUS_Y,    // Y方向にマイナス
        PLUS_Z,     // Z方向にプラス
        MINUS_Z,    // Z方向にマイナス
    }

    // アリスの行動
    public enum MoveAction
    {
        NONE,       // 無し
        NEXT,       // 進む
        RETURN,     // 戻る
    }

    // アリスの状態
    public enum Mode
    {
        NORMAL,  // 通常
        SMALL,  // 小さい
        BIG,    // 大きい
    }

    // アリスの動き
    public enum Motion
    {
        WALK_NEXT,      // 歩く（進む）
        WALK_RETURN,    // 歩く（戻る）
        STAY_NEXT,
        STAY_RETURN,
        DROP,           // 落下
        CLIMB1,         // 登る直前
        CLIMB2,         // 登る途中
        CLIMB3,         // 登る２
        CRY,            // 泣く（ゲームオーバー）
    }


    public AliceActionNotifer aliceActionNotifer;  // ギミックホルダー

    public CameraSystem camera;                     // カメラ
    public CameraSystem.CameraAngle cameraAngle;    // カメラの向き
    public PlayerAngle playerAngle;                 // プレイヤーの向き
    public MoveDirection moveDirection;             // プレイヤーの移動方向
    public MoveAction moveAction;                   // 行動
    public Mode mode;                               // 状態
    public bool modeSmallFlag;                      // 小さくなったら切り替えるフラグ
    public bool modeBigFlag;                        // 大きくなったら切り替えるフラグ
    public int modeSmallCount;                      // 小さくなっているターン数
    public int modeBigCount;                        // 大きくなっているターン数
    public int modeSmallTurnCount;                  // 小さくなってからのターン数
    public int modeBigTurnCount;                    // 大きくなってからのターン数
    public int arrayPosX;                           // 配列上での座標Ｘ
    public int arrayPosY;                           // 配列上での座標Ｙ
    public int arrayPosZ;                           // 配列上での座標Ｚ

    public Vector3 moveBeforePosition;     // ボタン入力時の座標
    public MoveDirection saveDirection;     // 移動方向の保存用

    // 移動用フラグ
    public bool moveFlag;           // 移動フラグ
    public bool moveNextFlag;

    // 自動移動用フラグ
    public bool autoMoveFlag;       // 自動移動フラグ
    public MoveDirection autoMove;

    public bool moveFinishFlag;     // 移動完了フラグ

    public bool moveFrontPossibleFlag;  // 前移動可能フラグ
    public bool moveBackPossibleFlag;   // 後移動可能フラグ
    public bool moveLeftPossibleFlag;   // 左移動可能フラグ
    public bool moveRightPossibleFlag;  // 右移動可能フラグ

    // 移動情報保存
    public PlayerAngle[] saveMovePlayerAngle = new PlayerAngle[SAVE_NUM];       // 移動情報を保存する配列（プレイヤーの向き）
    public MoveDirection[] saveMoveDirection = new MoveDirection[SAVE_NUM];     // 移動情報を保存する配列（移動する方向）
    public bool[] saveMoveInput = new bool[SAVE_NUM];                           // 移動情報を保存する配列（キー入力による移動か）
    public int saveCount;                                                       // 移動情報の配列番号
    public int moveCount;                                                       // 移動数のカウント（キー入力の移動のみ）
    public int turnCount;

    // アニメーション関連
    public bool animationWalkNextFlag;      // 進む時の歩きアニメーション用フラグ
    public bool animationWalkReturnFlag;    // 戻る時の歩きアニメーション用フラグ
    public bool animationStayNextFlag;
    public bool animationStayReturnFlag;
    public bool animationDropNextFlag;      // 進む時の落下アニメーション用フラグ
    public bool animationDropReturnFlag;    // 戻る時の落下アニメーション用フラグ
    public bool animationClimb1Flag;    // 進む時の登り１アニメーション用フラグ
    public bool animationClimb2Flag;    // 進む時の登り２アニメーション用フラグ
    public bool animationClimb3Flag;    // 進む時の登り３アニメーション用フラグ
    public bool animationGameOverFlag;

    // 向きに応じた角度
    public Vector3 angleFront;     // 前方向
    public Vector3 angleBack;      // 後方向
    public Vector3 angleLeft;      // 左方向
    public Vector3 angleRight;     // 右方向

    // 移動方向矢印
    public bool arrowDrawFlag = false;
    public GameObject stay;
    public GameObject arrowA;
    public GameObject arrowB;
    public GameObject arrowX;
    public GameObject arrowY;

    // 移動矢印に応じた角度
    Vector3 angleArrowFront;     // 前方向
    Vector3 angleArrowBack;      // 後方向
    Vector3 angleArrowLeft;      // 左方向
    Vector3 angleArrowRight;     // 右方向

    // 登るフラグ
    public bool climbFrontFlag;
    public bool climbBackFlag;
    public bool climbLeftFlag;
    public bool climbRightFlag;
    public bool climbFront2Flag;
    public bool climbBack2Flag;
    public bool climbLeft2Flag;
    public bool climbRight2Flag;

    public bool climbDropFlag;

    //巻き戻ししたかどうか
    public bool returnFlag;

    public int stopCount;  // 待機時のカウント（他の移動に時間を合わせるため）

    public bool gameOverFlag;

    public int climbCount; // 登りアニメーションの段階
    private GameObject pause;
    private Pause pauseScript;
    private GameObject stage;
    private Stage stageScript;

    // 初期化
	void Start ()
    {
        pause = GameObject.Find("Pause");
        pauseScript = pause.GetComponent<Pause>();
        stage = GameObject.Find("Stage");
        stageScript = stage.GetComponent<Stage>();
        aliceActionNotifer = new AliceActionNotifer();
        aliceActionNotifer.GimmickAry = GameObject.FindGameObjectsWithTag("Gimmick");   // TagがGimmickのオブジェクトを保存

        moveAction = MoveAction.NONE;

        moveBeforePosition = new Vector3(0, 0, 0);
        saveDirection = MoveDirection.NONE;

        angleFront = new Vector3(0, 0, 0);
        angleBack = new Vector3(0, 180, 0);
        angleLeft = new Vector3(0, 270, 0);
        angleRight = new Vector3(0, 90, 0);


        returnFlag = false;
        // 移動情報を保存する配列の初期化
        for (int num = 0; num < SAVE_NUM; num++)
        {
            saveMovePlayerAngle[num] = PlayerAngle.NONE;
            saveMoveDirection[num] = MoveDirection.NONE;
            saveMoveInput[num] = false;
        }
        saveCount = 0;
        moveCount = 0;

        moveFlag = false;
        moveNextFlag = false;

        autoMoveFlag = false;
        autoMove = MoveDirection.NONE;

        moveFinishFlag = false;

        moveFrontPossibleFlag = false;
        moveBackPossibleFlag = false;
        moveLeftPossibleFlag = false;
        moveRightPossibleFlag = false;

        animationWalkNextFlag = false;
        animationWalkReturnFlag = false;
        animationStayNextFlag = false;
        animationStayReturnFlag = false;
        animationDropNextFlag = false;
        animationDropReturnFlag = false;
        animationClimb1Flag = false;
        animationClimb2Flag = false;
        animationClimb3Flag = false;
        animationGameOverFlag = false;

        climbCount = 0;

        modeSmallFlag = false;
        modeBigFlag = false;
        modeSmallCount = 0;
        modeBigCount = 0;
        modeSmallTurnCount = 0;
        modeBigTurnCount = 0;

        angleArrowFront = new Vector3(90, 0, 0);
        angleArrowBack = new Vector3(90, 180, 0);
        angleArrowLeft = new Vector3(90, 270, 0);
        angleArrowRight = new Vector3(90, 90, 0);

        climbFrontFlag = false;
        climbBackFlag = false;
        climbLeftFlag = false;
        climbRightFlag = false;
        climbFront2Flag = false;
        climbBack2Flag = false;
        climbLeft2Flag = false;
        climbRight2Flag = false;

        climbDropFlag = false;

        stopCount = 0;
	}

    // 更新
	void Update () 
	{
        if(pauseScript.pauseFlag == false)
        {
            DrawMoveArrow();
            cameraAngle = camera.cameraAngle;   // カメラの向きを取得

            Move();             
        }
                      // アリスの移動
 	}

    // カメラに対応した移動方向に変更
    public void ChangeDirection(MoveDirection direction)
    {
        // 移動方向が
        switch (direction)
        {
            // 前なら
            case MoveDirection.FRONT:
                switch (cameraAngle)
                {
                    case CameraSystem.CameraAngle.FRONT: moveDirection = MoveDirection.BACK; break;
                    case CameraSystem.CameraAngle.BACK: moveDirection = MoveDirection.FRONT; break;
                    case CameraSystem.CameraAngle.LEFT: moveDirection = MoveDirection.RIGHT; break;
                    case CameraSystem.CameraAngle.RIGHT: moveDirection = MoveDirection.LEFT; break;
                }
                break;
            // 後なら
            case MoveDirection.BACK:
                switch (cameraAngle)
                {
                    case CameraSystem.CameraAngle.FRONT: moveDirection = MoveDirection.FRONT; break;
                    case CameraSystem.CameraAngle.BACK: moveDirection = MoveDirection.BACK; break;
                    case CameraSystem.CameraAngle.LEFT: moveDirection = MoveDirection.LEFT; break;
                    case CameraSystem.CameraAngle.RIGHT: moveDirection = MoveDirection.RIGHT; break;
                }
                break;
            // 左なら
            case MoveDirection.LEFT:
                switch (cameraAngle)
                {
                    case CameraSystem.CameraAngle.FRONT: moveDirection = MoveDirection.RIGHT; break;
                    case CameraSystem.CameraAngle.BACK: moveDirection = MoveDirection.LEFT; break;
                    case CameraSystem.CameraAngle.LEFT: moveDirection = MoveDirection.FRONT; break;
                    case CameraSystem.CameraAngle.RIGHT: moveDirection = MoveDirection.BACK; break;
                }
                break;
            // 右なら
            case MoveDirection.RIGHT:
                switch (cameraAngle)
                {
                    case CameraSystem.CameraAngle.FRONT: moveDirection = MoveDirection.LEFT; break;
                    case CameraSystem.CameraAngle.BACK: moveDirection = MoveDirection.RIGHT; break;
                    case CameraSystem.CameraAngle.LEFT: moveDirection = MoveDirection.BACK; break;
                    case CameraSystem.CameraAngle.RIGHT: moveDirection = MoveDirection.FRONT; break;
                }
                break;
            // 上なら
            case MoveDirection.UP:
                moveDirection = MoveDirection.UP;
                break;
            // 下なら
            case MoveDirection.DOWN:
                moveDirection = MoveDirection.DOWN;
                break;
            // 待機なら
            case MoveDirection.STOP:
                moveDirection = MoveDirection.STOP;
                break;
        }

        saveDirection = moveDirection;  // 移動方向の保存用変数に移動方向を入れる
        

        //ドラゴン参照
        if (touchCheshireFlag)
            TouchCheshire();

        if (touchKeyFlag)
            TouchKey();

        // ドアの開閉
        //Stage.GetComponent<Stage>().OpenDoor(getKeyFlag);

        //
    
    }

    // プレイヤーの向きを変更
    public void ChangeAngle()
    {
        // 移動方向が
        switch (moveDirection)
        {
            // 前なら
            case MoveDirection.FRONT:
                playerAngle = PlayerAngle.FRONT;            // アリスの向きを前向きに
                transform.localEulerAngles = angleFront;    // 前方向の角度を指定
                break;
            // 後なら
            case MoveDirection.BACK:
                playerAngle = PlayerAngle.BACK;             // アリスの向きを後向きに
                transform.localEulerAngles = angleBack;     // 後方向の角度を指定
                break;
            // 左なら
            case MoveDirection.LEFT:
                playerAngle = PlayerAngle.LEFT;             // アリスの向きを左向きに
                transform.localEulerAngles = angleLeft;     // 左方向の角度を指定
                break;
            // 右なら
            case MoveDirection.RIGHT:
                playerAngle = PlayerAngle.RIGHT;            // アリスの向きを右向きに
                transform.localEulerAngles = angleRight;    // 右方向の角度を指定
                break;
        }
    }

    // 状態切り替え
    public void ModeChange()
    {
        switch(mode)
        {
            case Mode.NORMAL: transform.localScale = new Vector3(0.7f, 0.7f, 0.7f); break;
            case Mode.SMALL: transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); break;
            case Mode.BIG: transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); break;
        }
    }

    // 移動
    public void Move()
    {
        // アリスの行動が
        switch(moveAction)
        {
            // 何も無しなら
            case MoveAction.NONE:
                break;
            // 進めるなら
            case MoveAction.NEXT:
                MoveNextPosition(); // アリスの移動処理

                // 移動方向が
                switch (moveDirection)
                {
                    // 前なら
                    case MoveDirection.FRONT:
                        if ((animationClimb2Flag == true) && (transform.localPosition.z >= moveBeforePosition.z + 0.7f))
                        {
                            Vector3 position = new Vector3((float)arrayPosX, (float)arrayPosY - 0.5f, (float)arrayPosZ + 1);
                            MoveFinish(position, ArrayMove.PLUS_Z);     // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        else if (transform.localPosition.z >= moveBeforePosition.z + 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, moveBeforePosition.z + 1);
                            MoveFinish(position, ArrayMove.PLUS_Z);     // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        break;
                    // 後なら
                    case MoveDirection.BACK:
                        if ((animationClimb2Flag == true) && (transform.localPosition.z <= moveBeforePosition.z - 0.7f))
                        {
                            Vector3 position = new Vector3((float)arrayPosX, (float)arrayPosY - 0.5f, (float)arrayPosZ - 1);
                            MoveFinish(position, ArrayMove.MINUS_Z);    // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        else if (transform.localPosition.z <= moveBeforePosition.z - 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, moveBeforePosition.z - 1);
                            MoveFinish(position, ArrayMove.MINUS_Z);    // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        break;
                    // 左なら
                    case MoveDirection.LEFT:
                        if ((animationClimb2Flag == true) && (transform.localPosition.x <= moveBeforePosition.x - 0.7f))
                        {
                            Vector3 position = new Vector3((float)arrayPosX - 1, (float)arrayPosY - 0.5f, (float)arrayPosZ);
                            MoveFinish(position, ArrayMove.MINUS_X);    // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        else if (transform.localPosition.x <= moveBeforePosition.x - 1)
                        {
                            Vector3 position = new Vector3(moveBeforePosition.x - 1, transform.localPosition.y, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.MINUS_X);    // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        break;
                    // 右なら
                    case MoveDirection.RIGHT:
                        if ((animationClimb2Flag == true) && (transform.localPosition.x >= moveBeforePosition.x + 0.7f))
                        {
                            Vector3 position = new Vector3((float)arrayPosX + 1, (float)arrayPosY - 0.5f, (float)arrayPosZ);
                            MoveFinish(position, ArrayMove.PLUS_X);     // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        else if (transform.localPosition.x >= moveBeforePosition.x + 1)
                        {
                            Vector3 position = new Vector3(moveBeforePosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.PLUS_X);     // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        break;
                    // 上なら
                    case MoveDirection.UP:
                        if ((animationClimb2Flag == true) && (transform.localPosition.y >= moveBeforePosition.y + 0.5f))
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y + 0.5f, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.PLUS_Y);     // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        else if (transform.localPosition.y >= moveBeforePosition.y + 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y + 1, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.PLUS_Y);     // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        break;
                    // 下なら
                    case MoveDirection.DOWN:
                        if ((climbCount == 2) && (transform.localPosition.y <= moveBeforePosition.y - 0.5f))
                        {
                            ResetAnimation(Motion.CLIMB1);
                            ResetAnimation(Motion.CLIMB2);
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y - 0.5f, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.MINUS_Y);     // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        else if (transform.localPosition.y <= moveBeforePosition.y - 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y - 1, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.MINUS_Y);    // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);   // アリスが移動したことをギミックに伝える
                        }
                        break;
                    // 待機なら
                    case MoveDirection.STOP:
                        if (stopCount == 50)
                        {
                            MoveFinish(transform.localPosition, ArrayMove.NONE);    // 移動完了
                            aliceActionNotifer.NotifyMove(moveCount);               // アリスが移動したことをギミックに伝える
                        }
                        break;
                }
                break;
            // 戻るなら
            case MoveAction.RETURN:
                MoveReturnPosition(); // アリスの移動処理

                // 移動方向が
                switch (moveDirection)
                {
                    // 前なら
                    case MoveDirection.FRONT:
                        if (transform.localPosition.z <= moveBeforePosition.z - 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, moveBeforePosition.z - 1);
                            MoveFinish(position, ArrayMove.MINUS_Z);
                            moveDirection = MoveDirection.FRONT;
                            AgainMove();
                        }
                        break;
                    // 後なら
                    case MoveDirection.BACK:
                        if (transform.localPosition.z >= moveBeforePosition.z + 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, moveBeforePosition.z + 1);
                            MoveFinish(position, ArrayMove.PLUS_Z);
                            moveDirection = MoveDirection.BACK;
                            AgainMove();
                        }
                        break;
                    // 左なら
                    case MoveDirection.LEFT:
                        if (transform.localPosition.x >= moveBeforePosition.x + 1)
                        {
                            Vector3 position = new Vector3(moveBeforePosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.PLUS_X);
                            moveDirection = MoveDirection.LEFT;
                            AgainMove();
                        }
                        break;
                    // 右なら
                    case MoveDirection.RIGHT:
                        if (transform.localPosition.x <= moveBeforePosition.x - 1)
                        {
                            Vector3 position = new Vector3(moveBeforePosition.x - 1, transform.localPosition.y, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.MINUS_X);
                            moveDirection = MoveDirection.RIGHT;
                            AgainMove();
                        }
                        break;
                    // 上なら
                    case MoveDirection.UP:
                        if ((animationClimb2Flag == true) && (transform.localPosition.y >= moveBeforePosition.y + 0.5f))
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y + 0.5f, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.PLUS_Y);     // 移動完了
                            AgainMove();
                        }
                        else if((climbDropFlag == true) && (transform.position.y >= moveBeforePosition.y + 0.5f))
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y + 0.5f, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.PLUS_Y);     // 移動完了
                            AgainMove();
                        }
                        else if (transform.localPosition.y >= moveBeforePosition.y + 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y + 1, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.PLUS_Y);
                            AgainMove();
                        }
                        break;
                    // 下なら
                    case MoveDirection.DOWN:
                        if ((climbCount == 2) && (transform.localPosition.y <= moveBeforePosition.y - 0.5f))
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y - 0.5f, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.MINUS_Y);     // 移動完了
                            AgainMove();
                        }
                        else if(transform.position.y <= moveBeforePosition.y -1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y - 0.5f, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.MINUS_Y);     // 移動完了
                            AgainMove();
                        }
                        else if (transform.localPosition.y <= moveBeforePosition.y - 1)
                        {
                            Vector3 position = new Vector3(transform.localPosition.x, moveBeforePosition.y - 1, transform.localPosition.z);
                            MoveFinish(position, ArrayMove.MINUS_Y);
                            AgainMove();
                        }
                        break;
                    // 待機なら
                    case MoveDirection.STOP:
                        if (stopCount == 50)
                        {
                            MoveFinish(transform.localPosition, ArrayMove.NONE);
                            AgainMove();
                        }
                        break;
                }
                break;
        }
    }

    // プレイヤーを進める
    public void MoveNextPosition()
    {

        // 移動方向が
        switch (moveDirection)
        {
            // 上移動なら
            case MoveDirection.UP:
                transform.Translate(Vector3.up * SPEED_H);
                break;
            // 下移動なら
            case MoveDirection.DOWN:
                transform.Translate(Vector3.down * SPEED_H);
                break;
            // 移動なら
            case MoveDirection.FRONT:
            case MoveDirection.BACK:
            case MoveDirection.LEFT:
            case MoveDirection.RIGHT:
                transform.Translate(Vector3.forward * SPEED_W);
                break;
            // 待機なら
            case MoveDirection.STOP:
                stopCount++;
                break;
        }
    }

    // プレイヤーを戻す
    public void MoveReturnPosition()
    {
        // 移動方向が
        switch (moveDirection)
        {
            // 上移動なら
            case MoveDirection.UP:
                transform.Translate(Vector3.up * SPEED_H);
                break;
            // 下移動なら
            case MoveDirection.DOWN:
                transform.Translate(Vector3.down * SPEED_H);
                break;
            // 移動なら
            case MoveDirection.FRONT:
            case MoveDirection.BACK:
            case MoveDirection.LEFT:
            case MoveDirection.RIGHT:
                transform.Translate(Vector3.back * SPEED_W);
                break;
            // 待機なら
            case MoveDirection.STOP:
                stopCount++;
                break;
        }
    }

    // 前移動
    public void MoveFront()
    {
        moveFlag = true;
        moveBeforePosition = transform.position;    // 移動前の座標に現在の座標を入れる
        ChangeDirection(MoveDirection.FRONT);       // カメラに対応した移動方向に変更
        ChangeAngle();                              // アリスの向きを変更
        moveAction = MoveAction.NEXT;               // アリスの行動を進めるに
    }

    // 後移動
    public void MoveBack()
    {
        moveFlag = true;
        moveBeforePosition = transform.position;    // 移動前の座標に現在の座標を入れる
        ChangeDirection(MoveDirection.BACK);        // カメラに対応した移動方向に変更
        ChangeAngle();                              // アリスの向きを変更
        moveAction = MoveAction.NEXT;               // アリスの行動を進めるに
    }

    // 左移動
    public void MoveLeft()
    {
        moveFlag = true;
        moveBeforePosition = transform.position;    // 移動前の座標に現在の座標を入れる
        ChangeDirection(MoveDirection.LEFT);        // カメラに対応した移動方向に変更
        ChangeAngle();                              // アリスの向きを変更
        moveAction = MoveAction.NEXT;               // アリスの行動を進めるに
    }

    // 右移動
    public void MoveRight()
    {
        moveFlag = true;
        moveBeforePosition = transform.position;    // 移動前の座標に現在の座標を入れる
        ChangeDirection(MoveDirection.RIGHT);       // カメラに対応した移動方向に変更
        ChangeAngle();                              // アリスの向きを変更
        moveAction = MoveAction.NEXT;               // アリスの行動を進めるに
    }

    // 上移動
    public void MoveUp()
    {
        moveFlag = true;
        moveBeforePosition = transform.position;    // 移動前の座標に現在の座標を入れる
        ChangeDirection(MoveDirection.UP);          // カメラに対応した移動方向に変更
        ChangeAngle();                              // アリスの向きを変更
        moveAction = MoveAction.NEXT;               // アリスの行動を進めるに
    }

    // 下移動
    public void MoveDown()
    {
        moveFlag = true;
        moveBeforePosition = transform.position;    // 移動前の座標に現在の座標を入れる
        ChangeDirection(MoveDirection.DOWN);        // カメラに対応した移動方向に変更
        ChangeAngle();                              // アリスの向きを変更
        moveAction = MoveAction.NEXT;               // アリスの行動を進めるに
    }

    // 待機
    public void MoveStop()
    {
        moveFlag = true;
        moveBeforePosition = transform.position;    // 移動前の座標に現在の座標を入れる
        ChangeDirection(MoveDirection.STOP);        // カメラに対応した移動方向に変更
        ChangeAngle();                              // アリスの向きを変更
        moveAction = MoveAction.NEXT;               // アリスの行動を進めるに
        stopCount = 0;
    }

    // 巻き戻し
    public void MoveReturn()
    {
        if (saveCount > 0)
        {
            timeFlag = true;
            timeBackFlag = true;
            moveFlag = true;
            moveBeforePosition = transform.position;            // 移動前の座標に現在の座標を入れる
            playerAngle = saveMovePlayerAngle[saveCount - 1];   // プレイヤーの向きに１つ前の向きを取得
            moveDirection = saveMoveDirection[saveCount - 1];   // 移動方向に１つ前の移動方向を取得

            // キー入力による移動の場合
            if (saveMoveInput[saveCount - 1] == true)
            {
                moveCount--;

                ModeCheck();
                ModeCountUp();

                // 保存されている移動方向が
                switch(saveMoveDirection[saveCount - 1])
                {
                    case MoveDirection.FRONT:
                    case MoveDirection.BACK:
                    case MoveDirection.LEFT:
                    case MoveDirection.RIGHT:
                        ResetPosition();
                        moveBeforePosition = transform.position;
                        SetAnimation(Motion.WALK_RETURN);
                        ResetAnimation(Player.Motion.CLIMB1);
                        ResetAnimation(Player.Motion.CLIMB2);
                        break;
                    case MoveDirection.STOP:
                        SetAnimation(Motion.STAY_RETURN);
                        break;
                }
            }

            // 移動方向が上下の場合はプレイヤーの向きを入れる
            if ((moveDirection == MoveDirection.UP) || (moveDirection == MoveDirection.DOWN))
            {
                switch (playerAngle)
                {
                    case PlayerAngle.FRONT: transform.localEulerAngles = angleFront; break;
                    case PlayerAngle.BACK: transform.localEulerAngles = angleBack; break;
                    case PlayerAngle.LEFT: transform.localEulerAngles = angleLeft; break;
                    case PlayerAngle.RIGHT: transform.localEulerAngles = angleRight; break;
                }
            }
            // 移動方向が待機の場合
            if (moveDirection == MoveDirection.STOP)
            {
                stopCount = 0;
            }

            ChangeAngle();                      // アリスの向きを変更
            moveAction = MoveAction.RETURN;     // アリスの行動を戻るに
            saveCount--;                        // 最後の保存場所を変更

            // 上下の反転
            switch (moveDirection)
            {
                case MoveDirection.UP:
                    moveDirection = MoveDirection.DOWN;
                    if (animationClimb2Flag == true)
                    {
                        ResetAnimation(Player.Motion.CLIMB2);
                    }
                    break;
                case MoveDirection.DOWN:
                    moveDirection = MoveDirection.UP;
                    if (animationClimb1Flag == true)
                    {
                        SetAnimation(Player.Motion.CLIMB2);
                        ResetAnimation(Player.Motion.CLIMB1);
                    }
                    break;
            }
        }
    }

    // 早送り
    public void MoveNext()
    {
        // 移動方向が保存されていれば
        if (saveMoveDirection[saveCount] != MoveDirection.NONE)
        {
            timeFlag = true;
            moveFlag = true;
            moveNextFlag = false;
            moveBeforePosition = transform.position;            // 移動前の座標に現在の座標を入れる
            moveDirection = saveMoveDirection[saveCount];       // 移動方向に移動方向を保存

            // キー入力による移動の場合
            if(saveMoveInput[saveCount] == true)
            {
                ModeCountDown();

                // 保存されている移動方向が
                switch(moveDirection)
                {
                    case MoveDirection.FRONT:
                    case MoveDirection.BACK:
                    case MoveDirection.LEFT:
                    case MoveDirection.RIGHT:
                        if (animationClimb2Flag == true)
                        {
                            transform.position = new Vector3(transform.position.x, (float)arrayPosY - 0.5f, transform.position.z);
                            SetAnimation(Player.Motion.WALK_NEXT);    // 進む時の歩きアニメーションをセット
                            ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                        }
                        else
                        {
                            ResetPosition();                          // アリスの位置をリセット
                            SetAnimation(Motion.WALK_RETURN);
                            ResetAnimation(Player.Motion.CLIMB1);
                            ResetAnimation(Player.Motion.CLIMB2);
                        }
                        break;
                    case MoveDirection.UP:
                        if(animationClimb1Flag == true)
                        {
                            SetAnimation(Player.Motion.CLIMB2);       // 登り途中に
                            ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                        }
                        break;
                }
            }
            // 移動方向が待機の場合
            if (moveDirection == MoveDirection.STOP)
            {
                stopCount = 0;
            }

            // 次の動作がキー入力無しのDOWNの場合（自然落下）
            if((saveMoveDirection[saveCount + 1] == MoveDirection.DOWN) && (saveMoveInput[saveCount + 1] == false))
            {
                moveNextFlag = true;
            }

            ChangeAngle();                                      // アリスの向きを変更
            moveAction = MoveAction.NEXT;                       // アリスの行動を進めるに
            saveCount++;                                        // 最後の保存場所を変更
        }
    }

    // 移動情報の保存（キー入力あり）
    public void SaveInputMove()
    {
        // 早送り移動の場合はリセットしない
        if (moveNextFlag == false)
        {
            //途中から動いたときにも進んだ方向を初期化
            for (int i = saveCount; i < SAVE_NUM; i++)
            {
                saveMovePlayerAngle[i] = PlayerAngle.NONE;
                saveMoveDirection[i] = MoveDirection.NONE;
                saveMoveInput[i] = false;
            }
        }

        saveMovePlayerAngle[saveCount] = playerAngle;   // プレイヤーの向きを保存
        saveMoveDirection[saveCount] = moveDirection;   // 移動情報を保存
        saveMoveInput[saveCount] = true;                // キー入力を真に
        saveCount++;                                    // 最後の保存場所を変更
    }

    // 移動情報の保存（キー入力なし）
    public void SaveMove()
    {
        // 早送り移動の場合はリセットしない
        if (moveNextFlag == false)
        {
            //途中から動いたときにも進んだ方向を初期化
            for (int i = saveCount; i < SAVE_NUM; i++)
            {
                saveMovePlayerAngle[i] = PlayerAngle.NONE;
                saveMoveDirection[i] = MoveDirection.NONE;
                saveMoveInput[i] = false;
            }
        }

        saveMovePlayerAngle[saveCount] = playerAngle;   // プレイヤーの向きを保存
        saveMoveDirection[saveCount] = moveDirection;   // 移動情報を保存
        saveMoveInput[saveCount] = false;               // キー入力を偽に
        saveCount++;                                    // 最後の保存場所を変更
    }

    // 移動完了
    public void MoveFinish(Vector3 position, ArrayMove arrayMove)
    {
        moveFlag = false;                       // 移動フラグを偽に
        moveFinishFlag = true;                  // 移動完了フラグを真に
        transform.localPosition = position;     // 座標を変更
        ChangeArrayPosition(arrayMove);         // 配列上の位置を変更
        moveAction = MoveAction.NONE;           // アリスの行動を無しに

        ModeChange();

        //ドラゴン参照
        InvisibleCount();
        GetKeyCount();
        touchCheshireFlag = false;
        timeFlag = false;
        timeBackFlag = false;

        // アニメーションのリセット
        ResetAnimation(Motion.WALK_NEXT);       // 歩き（進む）
        ResetAnimation(Motion.WALK_RETURN);     // 歩き（戻る）
        ResetAnimation(Motion.STAY_NEXT);
        ResetAnimation(Motion.STAY_RETURN);
    }

    // 配列上の位置を変更する
    public void ChangeArrayPosition(ArrayMove arrayMove)
    {
        switch (arrayMove)
        {
            case ArrayMove.NONE: break;                     // 配列上の座標変化なし
            case ArrayMove.PLUS_X: arrayPosX++; break;      // 配列上の座標Xに１プラス
            case ArrayMove.MINUS_X: arrayPosX--; break;     // 配列上の座標Xに１マイナス
            case ArrayMove.PLUS_Y: arrayPosY++; break;      // 配列上の座標Yに１プラス
            case ArrayMove.MINUS_Y: arrayPosY--; break;     // 配列上の座標Yに１マイナス
            case ArrayMove.PLUS_Z: arrayPosZ++; break;      // 配列上の座標Zに１プラス
            case ArrayMove.MINUS_Z: arrayPosZ--; break;     // 配列上の座標Zに１マイナス
        }
    }

    // 移動数のカウントを増やす
    public void MoveCountUp()
    {
        moveCount++;
    }

    // 移動数のカウントを減らす
    public void MoveCountDown()
    {
        moveCount--;
    }

    // 自動移動設定
    public void AutoMoveSetting(MoveDirection direction)
    {
        autoMoveFlag = true;
        autoMove = direction;
    }

    // 状態のターン数を増やす
    public void ModeCountUp()
    {
        // 小さくなったら切り替えるフラグが真なら
        if(modeSmallFlag == true)
        {
            modeSmallTurnCount--;   // 小さくなってからのターン数を減らす
            modeSmallCount++;       // 小さくなっているターン数を増やす

        }

        // 大きくなったら切り替えるフラグが真なら
        if (modeBigFlag == true)
        {
            modeBigTurnCount--;     // 大きくなってからのターン数を減らす
            modeBigCount++;         // 大きくなっているターン数を増やす

        }
    }

    // 状態のターン数を減らす
    public void ModeCountDown()
    {
        // 小さくなったら切り替えるフラグが真なら
        if(modeSmallFlag == true)
        {
            modeSmallTurnCount++;   // 小さくなってからのターン数を増やす
        }

        // 大きくなったら切り替えるフラグが真なら
        if (modeBigFlag == true)
        {
            modeBigTurnCount++;     // 大きくなってからのターン数を増やす
        }

        // 小さくなっているフラグが真なら
        if (modeSmallFlag == true)
        {
            modeSmallCount--;   // 小さくなっているターン数を減らす

            // 小さくなっているターン数が０で大きくなっているターン数が０以下なら
            if((modeSmallCount == 0) && (modeBigCount <= 0))
            {
                mode = Mode.NORMAL;  // 状態を通常に
            }
        }

        // 大きくなっているフラグが真なら
        if (modeBigFlag == true)
        {
            modeBigCount--;     // 大きくなっているターン数を減らす

            // 大きくなっているターン数が０で小さくなっているターン数が０以下なら
            if ((modeBigCount == 0) && (modeSmallCount <= 0))
            {
                mode = Mode.NORMAL;  // 状態を通常に
            }
        }
    }

    // 状態の確認
    public void ModeCheck()
    {
        // 小さくなっているフラグが真で小さくなってからのターン数が０なら
        if(modeSmallFlag == true && modeSmallTurnCount == 0)
        {
            modeSmallFlag = false;  // 小さくなったら切り替えるフラグを偽に
            modeSmallCount = 0;     // 小さくなっているターン数を０に
            mode = Mode.NORMAL;      // 状態を通常に

            // 大きくなっているターン数が０より大きければ
            if(modeBigCount > 0)
            {
                mode = Mode.BIG;    // 状態を大きいに
            }

            ModeChange();           // 状態の変化
        }

        // 大きくなっているフラグが真で大きくなってからのターン数が０なら
        if (modeBigFlag == true && modeBigTurnCount == 0)
        {
            modeBigFlag = false;    // 大きくなったら切り替えるフラグを偽に
            modeBigCount = 0;       // 大きくなっているターン数を０に
            mode = Mode.NORMAL;      // 状態を通常に

            // 小さくなっているターン数が０より大きければ
            if(modeSmallCount > 0)
            {
                mode = Mode.SMALL;  // 状態を小さいに
            }

            ModeChange();           // 状態の変化
        }

        // 小さくなってからのターン数が３で大きくなっているターン数が０以下なら
        if(modeSmallTurnCount == 3 && modeBigCount <= 0)
        {
            mode = Mode.SMALL;      // 状態を小さいに
            ModeChange();           // 状態の変化
        }

        // 大きくなってからのターン数が３で小さくなっているターン数が０以下なら
        if (modeBigTurnCount == 3 && modeSmallCount <= 0)
        {
            mode = Mode.BIG;        // 状態を大きいに
            ModeChange();           // 状態の変化
        }
    }

    // 移動可能方向矢印表示
    public void DrawMoveArrow()
    {
        // 表示フラグが真なら
        if(arrowDrawFlag == true)
        {
            stay.GetComponent<Renderer>().enabled = true;
            arrowA.GetComponent<Renderer>().enabled = true;     // 矢印Ａの表示フラグを真に
            arrowB.GetComponent<Renderer>().enabled = true;     // 矢印Ｂの表示フラグを真に
            arrowX.GetComponent<Renderer>().enabled = true;     // 矢印Ｘの表示フラグを真に
            arrowY.GetComponent<Renderer>().enabled = true;     // 矢印Ｙの表示フラグを真に
        }
        else
        {
            stay.GetComponent<Renderer>().enabled = false;
            arrowA.GetComponent<Renderer>().enabled = false;     // 矢印Ａの表示フラグを偽に
            arrowB.GetComponent<Renderer>().enabled = false;     // 矢印Ｂの表示フラグを偽に
            arrowX.GetComponent<Renderer>().enabled = false;     // 矢印Ｘの表示フラグを偽に
            arrowY.GetComponent<Renderer>().enabled = false;     // 矢印Ｙの表示フラグを偽に
        }

        // 表示フラグが真なら
        if (arrowDrawFlag == true)
        {
            // カメラアングルが
            switch (cameraAngle)
            {
                case CameraSystem.CameraAngle.FRONT:
                    stay.transform.position = (transform.position + new Vector3(0, 0.001f, 0));
                    arrowA.transform.position = (transform.position + new Vector3(0, 0.1f, 0.5f));
                    arrowB.transform.position = (transform.position + new Vector3(-0.5f, 0.1f, 0));
                    arrowX.transform.position = (transform.position + new Vector3(0.5f, 0.1f, 0));
                    arrowY.transform.position = (transform.position + new Vector3(0, 0.1f, -0.5f));

                    arrowA.transform.localEulerAngles = angleArrowFront;
                    arrowB.transform.localEulerAngles = angleArrowLeft;
                    arrowX.transform.localEulerAngles = angleArrowRight;
                    arrowY.transform.localEulerAngles = angleArrowBack;

                    switch (playerAngle)
                    {
                        case PlayerAngle.FRONT:
                            if (moveFrontPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.BACK:
                            if (moveFrontPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.LEFT:
                            if (moveFrontPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.RIGHT:
                            if (moveFrontPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            break;
                    }
                    break;
                case CameraSystem.CameraAngle.BACK:
                    stay.transform.position = (transform.position + new Vector3(0, 0.001f, 0));
                    arrowA.transform.position = (transform.position + new Vector3(0, 0.1f, -0.5f));
                    arrowB.transform.position = (transform.position + new Vector3(0.5f, 0.1f, 0));
                    arrowX.transform.position = (transform.position + new Vector3(-0.5f, 0.1f, 0));
                    arrowY.transform.position = (transform.position + new Vector3(0, 0.1f, 0.5f));
                    
                    arrowA.transform.localEulerAngles = angleArrowBack;
                    arrowB.transform.localEulerAngles = angleArrowRight;
                    arrowX.transform.localEulerAngles = angleArrowLeft;
                    arrowY.transform.localEulerAngles = angleArrowFront;
                    
                    switch (playerAngle)
                    {
                        case PlayerAngle.FRONT:
                            if (moveFrontPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.BACK:
                            if (moveFrontPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.LEFT:
                            if (moveFrontPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.RIGHT:
                            if (moveFrontPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            break;
                    }
                    break;
                case CameraSystem.CameraAngle.LEFT:
                    stay.transform.position = (transform.position + new Vector3(0, 0.001f, 0));
                    arrowA.transform.position = (transform.position + new Vector3(-0.5f, 0.1f, 0));
                    arrowB.transform.position = (transform.position + new Vector3(0, 0.1f, -0.5f));
                    arrowX.transform.position = (transform.position + new Vector3(0, 0.1f, 0.5f));
                    arrowY.transform.position = (transform.position + new Vector3(0.5f, 0.1f, 0f));

                    arrowA.transform.localEulerAngles = angleArrowLeft;
                    arrowB.transform.localEulerAngles = angleArrowBack;
                    arrowX.transform.localEulerAngles = angleArrowFront;
                    arrowY.transform.localEulerAngles = angleArrowRight;

                    switch (playerAngle)
                    {
                        case PlayerAngle.FRONT:
                            if (moveFrontPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.BACK:
                            if (moveFrontPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.LEFT:
                            if (moveFrontPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.RIGHT:
                            if (moveFrontPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            break;
                    }
                    break;

                case CameraSystem.CameraAngle.RIGHT:
                    stay.transform.position = (transform.position + new Vector3(0, 0.001f, 0));
                    arrowA.transform.position = (transform.position + new Vector3(0.5f, 0.1f, 0));
                    arrowB.transform.position = (transform.position + new Vector3(0, 0.1f, 0.5f));
                    arrowX.transform.position = (transform.position + new Vector3(0, 0.1f, -0.5f));
                    arrowY.transform.position = (transform.position + new Vector3(-0.5f, 0.1f, 0f));

                    arrowA.transform.localEulerAngles = angleArrowRight;
                    arrowB.transform.localEulerAngles = angleArrowFront;
                    arrowX.transform.localEulerAngles = angleArrowBack;
                    arrowY.transform.localEulerAngles = angleArrowLeft;

                    switch (playerAngle)
                    {
                        case PlayerAngle.FRONT:
                            if (moveFrontPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.BACK:
                            if (moveFrontPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.LEFT:
                            if (moveFrontPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            break;
                        case PlayerAngle.RIGHT:
                            if (moveFrontPossibleFlag == true) { arrowA.GetComponent<Renderer>().enabled = true; }
                            else { arrowA.GetComponent<Renderer>().enabled = false; }
                            if (moveBackPossibleFlag == true) { arrowY.GetComponent<Renderer>().enabled = true; }
                            else { arrowY.GetComponent<Renderer>().enabled = false; }
                            if (moveLeftPossibleFlag == true) { arrowB.GetComponent<Renderer>().enabled = true; }
                            else { arrowB.GetComponent<Renderer>().enabled = false; }
                            if (moveRightPossibleFlag == true) { arrowX.GetComponent<Renderer>().enabled = true; }
                            else { arrowX.GetComponent<Renderer>().enabled = false; }
                            break;
                    }
                    break;
            }
        }
    }

    // 登るフラグをリセット
    public void ClimbFlagReset()
    {
        climbFrontFlag = false;
        climbFront2Flag = false;
        climbBackFlag = false;
        climbBack2Flag = false;
        climbLeftFlag = false;
        climbLeft2Flag = false;
        climbRightFlag = false;
        climbRight2Flag = false;
    }

    // 進む時の歩くアニメーション//////////////
    public void AnimationWalkNext(bool flag)
    {
        animationWalkNextFlag = flag;
        GetComponent<Animator>().SetBool("WalkNextFlag", animationWalkNextFlag);
    }

    // 戻る時の歩くアニメーション////////////////
    public void AnimationWalkReturn(bool flag)
    {
        animationWalkReturnFlag = flag;
        GetComponent<Animator>().SetBool("WalkReturnFlag", animationWalkReturnFlag);
    }

    // 進む時の待機アニメーション//////////////
    public void AnimationStayNext(bool flag)
    {
        animationStayNextFlag = flag;
        GetComponent<Animator>().SetBool("StayNextFlag", animationStayNextFlag);
    }

    // 戻る時の歩くアニメーション////////////////
    public void AnimationStayReturn(bool flag)
    {
        animationStayReturnFlag = flag;
        GetComponent<Animator>().SetBool("StayReturnFlag", animationStayReturnFlag);
    }

    // 進む時の落下アニメーション//////////////
    public void AnimationDropNext(bool flag)
    {
        animationDropNextFlag = flag;
        GetComponent<Animator>().SetBool("DropNextFlag", animationDropNextFlag);
    }

    // ゲームオーバー時のアニメーション////////
    public void AnimationGameOver(bool flag)
    {
        animationGameOverFlag = flag;
        GetComponent<Animator>().SetBool("GameOverFlag", animationGameOverFlag);
    }

    // 登る１のアニメーション///////////////
    public void AnimationClimb1(bool flag)
    {
        animationClimb1Flag = flag;
        GetComponent<Animator>().SetBool("Climb1Flag", animationClimb1Flag);
    }

    // 登る２のアニメーション///////////////
    public void AnimationClimb2(bool flag)
    {
        animationClimb2Flag = flag;
        GetComponent<Animator>().SetBool("Climb2Flag", animationClimb2Flag);
    }

    // 進む時の登る３のアニメーション///////////////
    public void AnimationClimb3(bool flag)
    {
        animationClimb3Flag = flag;
        GetComponent<Animator>().SetBool("Climb3Flag", animationClimb3Flag);
    }

    // チェシャに触れたら
    public void TouchCheshire()
    {
        invisibleFlag = true;
        invisibleCount = DEFAULTINVISIBLECOUNT;

        Renderer[] renderChildren = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderChildren.Length; ++i)
        {
            renderChildren[i].enabled = false;
        }
        Stage.GetComponent<Stage>().HydeCheshire(invisibleFlag);
    }

    // 不可視カウント
    void InvisibleCount()
    {
        if (stageScript.presenceCheshireFlag)
        {
            // 時間を巻き戻していないなら不可視カウントを減らす
            if (timeBackFlag == false)
                invisibleCount--;
            // 時間を巻き戻しているなら不可視カウントを増やす
            else
                invisibleCount++;

            if ((invisibleCount > 0) && (invisibleCount <= DEFAULTINVISIBLECOUNT))
                invisibleFlag = true;
            else
                invisibleFlag = false;

            if ((timeBackFlag) && (invisibleCount > DEFAULTINVISIBLECOUNT))
            {
                invisibleFlag = false;
                invisibleCount = 100;
            }
            Renderer[] renderChildren = GetComponentsInChildren<Renderer>();

            for (int i = 0; i < renderChildren.Length; ++i)
            {
                if (invisibleFlag)
                    renderChildren[i].enabled = false;
                else
                    renderChildren[i].enabled = true;
            }
            Stage.GetComponent<Stage>().HydeCheshire(invisibleFlag);
        }
    }

    // 鍵に触れたら
    public void TouchKey()
    {
        getKeyFlag = true;
        if (getKeyTiming <= getKeyCount)
        {
            getKeyTiming = DEFAULTHOLDKEYCOUNT;
            getKeyCount = getKeyTiming;
        }
        Stage.GetComponent<Stage>().OpenDoor(getKeyFlag);
    }

    // 鍵取得カウント
    void GetKeyCount()
    {
        if (stageScript.presenceKeyFlag)
        {
            if (getKeyFlag)
            {
                // 時間を巻き戻していないなら不可視カウントを減らす
                if (timeBackFlag == false)
                    getKeyCount--;
                // 時間を巻き戻しているなら不可視カウントを増やす
                else
                    getKeyCount++;

                if ((timeBackFlag) && (getKeyCount > getKeyTiming))
                {
                    getKeyFlag = false;
                    getKeyTiming = 100;
                    getKeyCount = 100;
                }
            }
            Stage.GetComponent<Stage>().OpenDoor(getKeyFlag);
        }
    }

    // もう一度移動する
    void AgainMove()
    {
        // 前の動作がキー入力無しの移動ならさらに戻る
        if (saveCount >= 0)
        {
            bool input = saveMoveInput[saveCount];              // 保存されているものがキー入力によるものかを判断するための変数
            MoveDirection move = saveMoveDirection[saveCount];  // 保存されている移動方向を入れる変数

            // キー入力による移動でなければ（自動移動の場合）
            if (input == false)
            {
                moveFlag = true;            // 移動フラグを真に
                moveFinishFlag = false;     // 移動完了フラグを偽に
                moveDirection = move;       // 保存されている移動方向を入れる
                MoveReturn();               // 巻き戻し
            }
        }
    }

    // 登り状態のセット
    public void SetClimbPosition(PlayerAngle angle)
    {
        switch (angle)
        {
            case PlayerAngle.FRONT: transform.position = new Vector3((float)arrayPosX, (float)arrayPosY - 0.5f, (float)arrayPosZ + 0.3f); break;
            case PlayerAngle.BACK: transform.position = new Vector3((float)arrayPosX, (float)arrayPosY - 0.5f, (float)arrayPosZ - 0.3f); break;
            case PlayerAngle.LEFT: transform.position = new Vector3((float)arrayPosX - 0.3f, (float)arrayPosY - 0.5f, (float)arrayPosZ); break;
            case PlayerAngle.RIGHT: transform.position = new Vector3((float)arrayPosX + 0.3f, (float)arrayPosY - 0.5f, (float)arrayPosZ); break;
        }
    }

    // 登り状態のリセット
    public void ResetClimbPosition(PlayerAngle angle)
    {
        switch (angle)
        {

            case PlayerAngle.FRONT: transform.position = new Vector3((float)arrayPosX, (float)arrayPosY - 0.5f, (float)arrayPosZ); break;
            case PlayerAngle.BACK: transform.position = new Vector3((float)arrayPosX, (float)arrayPosY - 0.5f, (float)arrayPosZ); break;
            case PlayerAngle.LEFT: transform.position = new Vector3((float)arrayPosX, (float)arrayPosY - 0.5f, (float)arrayPosZ); break;
            case PlayerAngle.RIGHT: transform.position = new Vector3((float)arrayPosX, (float)arrayPosY - 0.5f, (float)arrayPosZ); break;
        }
    }

    // アニメーションのセット
    public void SetAnimation(Motion motion)
    {
        switch(motion)
        {
            case Motion.WALK_NEXT: AnimationWalkNext(true); break;      // 進む時の歩きアニメーションを真に
            case Motion.WALK_RETURN: AnimationWalkReturn(true); break;  // 戻る時の歩きアニメーションを真に
            case Motion.STAY_NEXT: AnimationStayNext(true); break;
            case Motion.STAY_RETURN: AnimationStayReturn(true); break;
            case Motion.DROP: AnimationDropNext(true); break;           // 進む時の落下アニメーションを真に
            case Motion.CLIMB1: AnimationClimb1(true); break;             // 登る時のアニメーションを真に
            case Motion.CLIMB2: AnimationClimb2(true); break;             // 登る時のアニメーションを真に
            case Motion.CLIMB3: AnimationClimb3(true); break;             // 登る時のアニメーションを真に
            case Motion.CRY: AnimationGameOver(true); break;            // ゲームオーバー時のアニメーションを真に
        }
    }

    public void ResetAnimation(Motion motion)
    {
        switch(motion)
        {
            case Motion.WALK_NEXT: AnimationWalkNext(false); break;     // 進む時の歩きアニメーションを偽に
            case Motion.WALK_RETURN: AnimationWalkReturn(false); break; // 戻る時の歩きアニメーションを偽に
            case Motion.STAY_NEXT: AnimationStayNext(false); break;
            case Motion.STAY_RETURN: AnimationStayReturn(false); break;
            case Motion.DROP: AnimationDropNext(false); break;          // 進む時の落下アニメーションを偽に
            case Motion.CLIMB1: AnimationClimb1(false); break;             // 登る時のアニメーションを真に
            case Motion.CLIMB2: AnimationClimb2(false); break;             // 登る時のアニメーションを真に
            case Motion.CLIMB3: AnimationClimb3(false); break;             // 登る時のアニメーションを真に
            case Motion.CRY: AnimationGameOver(false); break;           // ゲームオーバー時のアニメーションを偽に
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector3((float)arrayPosX, (float)arrayPosY - 0.5f, (float)arrayPosZ);
    }

    public Vector3 GetArray()
    {
        return new Vector3(arrayPosX, arrayPosY, arrayPosZ);
    }

    //-------------------
    //アリスが透明か返す
    //-------------------
    public bool GetInvisible()
    {
        return invisibleFlag;
    }
}