using UnityEngine;
using System.Collections;

public class  GameMain :  MonoBehaviour
{
    //Cameraの移動関数ポインタ
    public delegate void OnCameraMove();
    public event OnCameraMove cameraLeftTurnEvent;      // Cameraの左回転関数
    public event OnCameraMove cameraRightTurnEvent;     // Cameraの右回転関数

    //Playerの移動関数ポインタ
    public delegate void OnPlayerMove();
    public event OnPlayerMove inputPlayerMoveFrontEvent;    // キー入力によるPlayerの前移動関数
    public event OnPlayerMove inputPlayerMoveBackEvent;     // キー入力によるPlayerの後移動関数
    public event OnPlayerMove inputPlayerMoveLeftEvent;     // キー入力によるPlayerの左移動関数
    public event OnPlayerMove inputPlayerMoveRightEvent;    // キー入力によるPlayerの右移動関数
    public event OnPlayerMove inputPlayerMoveUpEvent;       // キー入力によるPlayerの上移動関数
    public event OnPlayerMove inputPlayerMoveDownEvent;     // キー入力によるPlayerの下移動関数
    public event OnPlayerMove inputPlayerMoveStopEvent;     // キー入力によるPlayerの待機関数
    public event OnPlayerMove inputPlayerMoveReturnEvent;   // キー入力によるPlayerの巻き戻し
    public event OnPlayerMove inputPlayerMoveNextEvent;     // キー入力によるPlayerの早送り
    public event OnPlayerMove playerMoveFrontEvent;         // Playerの前移動関数
    public event OnPlayerMove playerMoveBackEvent;          // Playerの後移動関数
    public event OnPlayerMove playerMoveLeftEvent;          // Playerの左移動関数
    public event OnPlayerMove playerMoveRightEvent;         // Playerの右移動関数
    public event OnPlayerMove playerMoveUpEvent;            // Playerの上移動関数
    public event OnPlayerMove playerMoveDownEvent;          // Playerの下移動関数
    public event OnPlayerMove playerMoveStopEvent;          // Playerの待機関数

    public CameraSystem camera;
    public Player alice;            // アリス
    public Stage stage;             // ステージ

    public int stageTurnNum;

    private int stageNumber;

    public WatchHandAnimation watchHand;

    bool flag = false;
    private GameObject pause;
    private Pause pauseScript;

    public bool moveNow;
    public int stayTime;
	// 初期化
	void Start ()
    {

        moveNow = false;
        stayTime = 0;
        pause = GameObject.Find("Pause");
        pauseScript = pause.GetComponent<Pause>();
        //ステージの番号を取得
        stageNumber = PlayerPrefs.GetInt("STAGE_NUM");
        //stageNumber = 25;
        
        CameraFade.StartAlphaFade(Color.black, true, 1.0f, 0.5f);
        StartSetting(); // ゲーム開始前の設定
	}
	
	// 更新
    void Update()
    {
       if(pauseScript.pauseFlag == false)
       {
           if (flag == false)
           {
               flag = true;
               stage.SettingStageGimmick(stageNumber);
           }

           if (Input.GetKey(KeyCode.Joystick1Button6))
           {
               Debug.Log("Button Back Push");
           }

           if (Input.GetKey(KeyCode.Joystick1Button7))
           {
               Debug.Log("Button START Push");
           }

           if (Input.GetKey(KeyCode.Joystick1Button8))
           {
               Debug.Log("L Stick Push Push");
           }

           if (Input.GetKey(KeyCode.Joystick1Button9))
           {
               Debug.Log("R Stick Push");
           }

           float TrigerInput = 0.0f;
           TrigerInput = Input.GetAxis("Triger");
           if (TrigerInput < -0.07f)
           {
               TrigerInput = 0.0f;
               Debug.Log("R Triger");
           }
           else if (TrigerInput > 0.07f)
           {
               TrigerInput = 0.0f;
               Debug.Log("L Triger");
           }

           //float VerticalKeyInput = Input.GetAxis("VerticalKey");
           //if (VerticalKeyInput < 0.0f)
           //{
           //    Debug.Log("Up Key");
           //}
           //else if (VerticalKeyInput > 0.0f)
           //{
           //    Debug.Log("Down Key");
           //}

           //AliceMovePossibleDecision();    // アリスの移動可能範囲の判定

           // アリスが移動中でなければ
           if (alice.moveFlag == false)
           {
               CameraOperation();  // カメラ操作
           }

           // カメラ回転中でなければ
           if (camera.flag == false)
           {
               AliceMovePossibleDecision();    // アリスの移動可能範囲の判定
               AliceMove();                    // アリスの移動

               if (alice.moveFinishFlag == true)
               {
                   //巻き戻し移動後、アリスの下が穴であったとき、
                   if (stage.GetFootHOLE(alice) == true && alice.returnFlag == true)
                   {
                       //処理しないでフラグを戻す
                       alice.returnFlag = false;
                       alice.moveFinishFlag = false;
                       alice.arrowDrawFlag = false;
                   }
                   else
                   {
                       alice.arrowDrawFlag = false;
                       stage.ArrayChangeGimmck();
                       print("ギミック判定");
                       stage.GimmickDecision(alice);
                       print("足元判定");
                       stage.FootDecision(alice);
                       alice.moveFinishFlag = false;
                       alice.returnFlag = false;
                   }
               }
           }
       }
       
    }

    // ゲーム開始前の設定
    void StartSetting()
    {
        StageSetting();     // ステージの設定
        AliceSetting();     // アリスの設定
    }

    // ステージの設定
    void StageSetting()
    {
        stage.SelectStage(stageNumber);     // 選択されたステージを設定
        stage.CreateStage();                // ステージを生成
        stageTurnNum = stage.turnCount;     // ターン数の取得
    }

    // アリスの設定
    void AliceSetting()
    {
        alice.transform.position = stage.getStartPosition();    // アリスのゲーム上の座標を取得
        alice.arrayPosX = stage.getStartArrayPositionX();       // アリスの配列上の座標Ｘを取得
        alice.arrayPosY = stage.getStartArrayPositionY();       // アリスの配列上の座標Ｘを取得
        alice.arrayPosZ = stage.getStartArrayPositionZ();       // アリスの配列上の座標Ｘを取得
        alice.playerAngle = Player.PlayerAngle.FRONT;           // アリスの初期の向きをFRONTに設定
        alice.turnCount = stage.turnCount;                      // ステージのターン数を取得
    }

    // カメラ操作
    void CameraOperation()
    {
        float HorizontalKeyInput = Input.GetAxis("HorizontalKey");

        // 矢印右を押したら(カメラ移動左回転)
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (HorizontalKeyInput < -0.9f))
        {
            alice.arrowDrawFlag = false;
            HorizontalKeyInput = -1.0f;
            CameraLeftTrunMove();
        }
        // 矢印左を押したら(カメラ移動右回転)
        if ((Input.GetKeyDown(KeyCode.RightArrow)) || (HorizontalKeyInput > 0.9f))
        {
            alice.arrowDrawFlag = false;
            HorizontalKeyInput = 1.0f;
            CameraRightTrunMove();
        }
    }

    // アリスの移動可能範囲の判定
    void AliceMovePossibleDecision()
    {
        alice.moveFrontPossibleFlag = stage.MoveFrontPossibleDecision(alice);   // 前移動可能判定
        alice.moveBackPossibleFlag = stage.MoveBackPossibleDecision(alice);     // 後移動可能判定
        alice.moveLeftPossibleFlag = stage.MoveLeftPossibleDecision(alice);     // 左移動可能判定
        alice.moveRightPossibleFlag = stage.MoveRightPossibleDecision(alice);   // 右移動可能判定
    }

    // アリスの移動処理
    void AliceMove()
    {
        // 残りターン数が０より多い、移動フラグが偽、移動完了フラグが偽なら
        if (((alice.turnCount - alice.moveCount > 0) || (alice.autoMoveFlag == true)) && (alice.moveFlag == false) && (alice.moveFinishFlag == false))
        {
            if (alice.gameOverFlag == true)
            {
                alice.AnimationGameOver(true);
            }
            else
            {
                alice.AnimationGameOver(false);
            }

            // 自動移動フラグが立っていれば
            if (alice.autoMoveFlag == true)
            {
                // 自動移動の方向が
                switch (alice.autoMove)
                {
                    // 無しなら
                    case Player.MoveDirection.NONE: break;
                    // 前移動なら
                    case Player.MoveDirection.FRONT: if (alice.moveFlag == false) { PlayerMoveFront(); } break;
                    // 後移動なら
                    case Player.MoveDirection.BACK: if (alice.moveFlag == false) { PlayerMoveBack(); } break;
                    // 左移動なら
                    case Player.MoveDirection.LEFT: if (alice.moveFlag == false) { PlayerMoveLeft(); } break;
                    // 右移動なら
                    case Player.MoveDirection.RIGHT: if (alice.moveFlag == false) { PlayerMoveRight(); } break;
                    // 上移動なら
                    case Player.MoveDirection.UP: if (alice.moveFlag == false) { PlayerMoveUp(); } break;
                    // 下移動なら
                    case Player.MoveDirection.DOWN: if (alice.moveFlag == false) { PlayerMoveDown(); } break;
                    // 停止なら
                    case Player.MoveDirection.STOP: if (alice.moveFlag == false) { PlayerMoveStop(); } break;
                }

                alice.autoMoveFlag = false;
                alice.autoMove = Player.MoveDirection.NONE;
            }
            else
            {
                if (moveNow == false)
                {
                    alice.arrowDrawFlag = true;

                    float TrigerInput = 0.0f;
                    TrigerInput = Input.GetAxis("Triger");

                    // 前移動
                    if ((Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Joystick1Button3))) && (MoveFrontPossible() == true) && alice.gameOverFlag == false)
                    {
                        moveNow = true;
                        // 蔦一段目（登り）
                        if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbBackFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbFrontFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbRightFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbLeftFlag == true)))
                        {
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.SetAnimation(Player.Motion.CLIMB2);       // 登り途中に
                            alice.ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveUp();                            // キー入力ありの上移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // 蔦二段目（登り）
                        else if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbBack2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbFront2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbRight2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbLeft2Flag == true)))
                        {
                            alice.transform.position = new Vector3(alice.transform.position.x, (float)alice.arrayPosY - 0.5f, alice.transform.position.z);
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.SetAnimation(Player.Motion.WALK_NEXT);    // 進む時の歩きアニメーションをセット
                            alice.ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveFront();                         // キー入力ありの前移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // 蔦二段目（降り）
                        else if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbFront2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbBack2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbLeft2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbRight2Flag == true)))
                        {
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.ResetAnimation(Player.Motion.CLIMB2);       // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveDown();                          // キー入力ありの前移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // それ以外（通常の移動）
                        else
                        {
                            alice.climbCount = 0;
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.ResetPosition();                          // アリスの位置をリセット
                            alice.SetAnimation(Player.Motion.WALK_NEXT);    // 進む時の歩きアニメーションをセット
                            alice.ResetAnimation(Player.Motion.CLIMB1);
                            alice.ResetAnimation(Player.Motion.CLIMB2);
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveFront();                         // キー入力ありの前移動
                            alice.MoveCountUp();                            // 移動のカウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                    }
                    // 後移動
                    else if ((Input.GetKeyDown(KeyCode.S) || (Input.GetKeyDown(KeyCode.Joystick1Button0))) && (MoveBackPossible() == true) && alice.gameOverFlag == false)
                    {
                        moveNow = true;
                        // 蔦一段目（登り）
                        if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbFrontFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbBackFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbLeftFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbRightFlag == true)))
                        {
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.SetAnimation(Player.Motion.CLIMB2);       // 登り途中に
                            alice.ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveUp();                            // キー入力ありの上移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // 蔦二段目（登り）
                        else if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbFront2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbBack2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbLeft2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbRight2Flag == true)))
                        {
                            alice.transform.position = new Vector3(alice.transform.position.x, (float)alice.arrayPosY - 0.5f, alice.transform.position.z);
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.SetAnimation(Player.Motion.WALK_NEXT);    // 進む時の歩きアニメーションをセット
                            alice.ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveBack();                          // キー入力ありの後移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // 蔦二段目（降り）
                        else if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbBack2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbFront2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbRight2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbLeft2Flag == true)))
                        {
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.ResetAnimation(Player.Motion.CLIMB2);     // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveDown();                          // キー入力ありの前移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // それ以外（通常の移動）
                        else
                        {
                            alice.climbCount = 0;
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.ResetPosition();                          // アリスの位置をリセット
                            alice.SetAnimation(Player.Motion.WALK_NEXT);    // 進む時の歩きアニメーションをセット
                            alice.ResetAnimation(Player.Motion.CLIMB1);
                            alice.ResetAnimation(Player.Motion.CLIMB2);
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveBack();                          // キー入力ありの後移動
                            alice.MoveCountUp();                            // 移動のカウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                    }
                    // 左移動
                    else if ((Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.Joystick1Button2))) && (MoveLeftPossible() == true) && alice.gameOverFlag == false)
                    {
                        moveNow = true;
                        // 蔦一段目（登り）
                        if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbRightFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbLeftFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbFrontFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbBackFlag == true)))
                        {
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.SetAnimation(Player.Motion.CLIMB2);       // 登り途中に
                            alice.ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveUp();                            // キー入力ありの上移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // 蔦二段目（登り）
                        else if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbRight2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbLeft2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbFront2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbBack2Flag == true)))
                        {
                            alice.transform.position = new Vector3(alice.transform.position.x, (float)alice.arrayPosY - 0.5f, alice.transform.position.z);
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.SetAnimation(Player.Motion.WALK_NEXT);    // 進む時の歩きアニメーションをセット
                            alice.ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveLeft();                          // キー入力ありの左移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // 蔦二段目（降り）
                        else if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbLeft2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbRight2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbBack2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbFront2Flag == true)))
                        {
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.ResetAnimation(Player.Motion.CLIMB2);     // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveDown();                          // キー入力ありの下移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // それ以外（通常の移動）
                        else
                        {
                            alice.climbCount = 0;
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.ResetPosition();                          // アリスの位置をリセット
                            alice.SetAnimation(Player.Motion.WALK_NEXT);    // 進む時の歩きアニメーションをセット
                            alice.ResetAnimation(Player.Motion.CLIMB1);
                            alice.ResetAnimation(Player.Motion.CLIMB2);
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveLeft();                          // キー入力ありの左移動
                            alice.MoveCountUp();                            // 移動のカウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                    }
                    // 右移動
                    else if ((Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.Joystick1Button1))) && (MoveRightPossible() == true) && alice.gameOverFlag == false)
                    {
                        moveNow = true;
                        // 蔦一段目（登り）
                        if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbLeftFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbRightFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbBackFlag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbFrontFlag == true)))
                        {
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.SetAnimation(Player.Motion.CLIMB2);       // 登り途中に
                            alice.ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveUp();                            // キー入力ありの上移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // 蔦二段目（登り）
                        else if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbLeft2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbRight2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbBack2Flag == true)) ||
                            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbFront2Flag == true)))
                        {
                            alice.transform.position = new Vector3(alice.transform.position.x, (float)alice.arrayPosY - 0.5f, alice.transform.position.z);
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.SetAnimation(Player.Motion.WALK_NEXT);    // 進む時の歩きアニメーションをセット
                            alice.ResetAnimation(Player.Motion.CLIMB1);       // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveRight();                         // キー入力ありの右移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // 蔦二段目（降り）
                        else if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbRight2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbLeft2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbFront2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbBack2Flag == true)))
                        {
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.ResetAnimation(Player.Motion.CLIMB2);     // 登り途中に
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveDown();                          // キー入力ありの下移動
                            alice.MoveCountUp();                            // 移動カウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                        // それ以外（通常の移動）
                        else
                        {
                            alice.climbCount = 0;
                            alice.ClimbFlagReset();                         // 登りフラグをリセット
                            alice.ResetPosition();                          // アリスの位置をリセット
                            alice.SetAnimation(Player.Motion.WALK_NEXT);    // 進む時の歩きアニメーションをセット
                            alice.ResetAnimation(Player.Motion.CLIMB1);
                            alice.ResetAnimation(Player.Motion.CLIMB2);
                            alice.arrowDrawFlag = false;                    // 移動方向矢印を消す
                            InputPlayerMoveRight();                         // キー入力ありの右移動
                            alice.MoveCountUp();                            // 移動のカウントを増やす
                            watchHand.NextTurn();                           // 時計の針を進める
                        }
                    }
                    // 待機
                    else if ((Input.GetKeyDown(KeyCode.X) && alice.gameOverFlag == false) || (TrigerInput < -0.07f))
                    {
                        print("アリス待機");
                        alice.ClimbFlagReset();         // 登りフラグを消す
                        alice.arrowDrawFlag = false;    // 移動方向矢印を消す
                        InputPlayerMoveStop();          // キー入力ありの待機
                        alice.MoveCountUp();            // 移動のカウントを増やす
                        watchHand.NextTurn();           // 時計の針を進める
                    }
                    // 巻き戻し
                    else if (Input.GetKeyDown(KeyCode.Q) || (Input.GetKeyDown(KeyCode.Joystick1Button4)))
                    {
                        // アリスの移動数が０より大きいなら
                        if (alice.moveCount > 0)
                        {
                            moveNow = true;
                            //巻き戻しを押した時、下に穴があった時には、しょりをする、左側の分は丹羽君
                            if (alice.saveMoveDirection[alice.saveCount] == Player.MoveDirection.NONE || stage.GetFootHOLE(alice))
                            {
                                stage.FootDecision(alice);
                            }

                            alice.aliceActionNotifer.NotifyReturn(alice.moveCount); // アリスが戻ったことをギミックに伝える
                            alice.returnFlag = true;
                            alice.gameOverFlag = false;
                            alice.ClimbFlagReset();
                            alice.arrowDrawFlag = false;
                            InputPlayerMoveReturn();
                            watchHand.BackTurn();   // 時計の針を戻す
                        }
                    }
                    // 早送り
                    else if (Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.Joystick1Button5)))
                    {
                        moveNow = true;
                        // 保存されている移動情報がNONEでなければ
                        if (alice.saveMoveDirection[alice.saveCount] != Player.MoveDirection.NONE)
                        {
                            alice.ClimbFlagReset();
                            alice.arrowDrawFlag = false;
                            InputPlayerMoveNext();
                            alice.MoveCountUp();
                            watchHand.NextTurn();   // 時計の針を進める
                        }
                    }
                }
                else
                {
                    stayTime++;

                    if (stayTime == 20)
                    {
                        stayTime = 0;
                        moveNow = false;
                    }
                }
            }
        }
        else if((alice.turnCount - alice.moveCount == 0) && (stage.goalFlag == false))
        {
            alice.gameOverFlag = true;
            if(alice.gameOverFlag == true)
            {
                alice.AnimationGameOver(true);
            }

            if (Input.GetKeyDown(KeyCode.Q) || (Input.GetKeyDown(KeyCode.Joystick1Button4)))
            {
                // アリスの移動数が０より大きいなら
                if (alice.moveCount > 0)
                {
                    //巻き戻しを押した時、下に穴があった時には、しょりをする、左側の分は丹羽君
                    if (alice.saveMoveDirection[alice.saveCount] == Player.MoveDirection.NONE || stage.GetFootHOLE(alice))
                    {
                        stage.FootDecision(alice);
                    }

                    alice.aliceActionNotifer.NotifyReturn(alice.moveCount); // アリスが戻ったことをギミックに伝える
                    alice.returnFlag = true;
                    alice.ClimbFlagReset();
                    alice.arrowDrawFlag = false;
                    InputPlayerMoveReturn();
                    watchHand.BackTurn();

                    alice.gameOverFlag = false;
                }
            }
        }
    }


    // 前移動可能か？
    public bool MoveFrontPossible()
    {
        // 前移動可能な条件を満たしていれば
        if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbBackFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbFrontFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbRightFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbLeftFlag == true)))
        {
            return true;    // 移動できる
        }
        else
        {
            return false;   // 移動できない
        }
    }

    // 後移動可能か？
    public bool MoveBackPossible()
    {
        // 後移動可能な条件を満たしていれば
        if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbFrontFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbBackFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbLeftFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbRightFlag == true)))
        {
            return true;    // 移動できる
        }
        else
        {
            return false;   // 移動できない
        }
    }

    // 左移動可能か？
    public bool MoveLeftPossible()
    {
        // 左移動可能な条件を満たしていれば
        if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbRightFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbLeftFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbFrontFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbBackFlag == true)))
        {
            return true;    // 移動できる
        }
        else
        {
            return false;   // 移動できない
        }
    }

    // 右移動可能か？
    public bool MoveRightPossible()
    {
        // 右移動可能な条件を満たしていれば
        if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.FRONT) && (alice.moveFrontPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.BACK) && (alice.moveBackPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.LEFT) && (alice.moveRightPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.playerAngle == Player.PlayerAngle.RIGHT) && (alice.moveLeftPossibleFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbLeftFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbRightFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbBackFlag == true)) ||
            ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbFrontFlag == true)))
        {
            return true;    // 移動できる
        }
        else
        {
            return false;   // 移動できない
        }
    }

    // 蔦、梯子一段目の判定
    public bool Climb1Decision(Player.MoveDirection direction)
    {
        bool flag = false;

        switch(direction)
        {
            // 前移動
            case Player.MoveDirection.FRONT:
                if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbBackFlag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbFrontFlag == true)) ||
                    ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbRightFlag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbLeftFlag == true)))
                {
                    flag = true;
                }
                break;
            // 後移動
            case Player.MoveDirection.BACK:
                if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbFrontFlag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbBackFlag == true)) ||
                    ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbLeftFlag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbRightFlag == true)))
                {
                    flag = true;
                }
                break;
            // 左移動
            case Player.MoveDirection.LEFT:
                if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbRightFlag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbLeftFlag == true)) ||
                    ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbFrontFlag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbBackFlag == true)))
                {
                    flag = true;
                }
                break;
            // 右移動
            case Player.MoveDirection.RIGHT:
                if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbLeftFlag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbRightFlag == true)) ||
                    ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbBackFlag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbFrontFlag == true)))
                {
                    flag = true;
                }
                break;
        }

        return flag;
    }

    // 蔦、梯子二段目の判定
    public bool Climb2Decision(Player.MoveDirection direction, int num)
    {
        bool flag = false;

        // 登る
        if(num == 0)
        {
            switch (direction)
            {
                // 前移動
                case Player.MoveDirection.FRONT:
                    if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbBack2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbFront2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbRight2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbLeft2Flag == true)))
                    {
                        flag = true;
                    }
                    break;
                // 後移動
                case Player.MoveDirection.BACK:
                    if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbFront2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbBack2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbLeft2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbRight2Flag == true)))
                    {
                        flag = true;
                    }
                    break;
                // 左移動
                case Player.MoveDirection.LEFT:
                    if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbRight2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbLeft2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbFront2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbBack2Flag == true)))
                    {
                        flag = true;
                    }
                    break;
                // 右移動
                case Player.MoveDirection.RIGHT:
                    if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbLeft2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbRight2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbBack2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbFront2Flag == true)))
                    {
                        flag = true;
                    }
                    break;
            }
        }
        // 降りる
        else if(num == 1)
        {
            switch (direction)
            {
                // 前移動
                case Player.MoveDirection.FRONT:
                    if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbFront2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbBack2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbLeft2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbRight2Flag == true)))
                    {
                        flag = true;
                    }
                    break;
                // 後移動
                case Player.MoveDirection.BACK:
                    if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbBack2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbFront2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbRight2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbLeft2Flag == true)))
                    {
                        flag = true;
                    }
                    break;
                // 左移動
                case Player.MoveDirection.LEFT:
                    if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbLeft2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbRight2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbBack2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbFront2Flag == true)))
                    {
                        flag = true;
                    }
                    break;
                // 右移動
                case Player.MoveDirection.RIGHT:
                    if (((alice.cameraAngle == CameraSystem.CameraAngle.FRONT) && (alice.climbRight2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.BACK) && (alice.climbLeft2Flag == true)) ||
                        ((alice.cameraAngle == CameraSystem.CameraAngle.LEFT) && (alice.climbFront2Flag == true)) || ((alice.cameraAngle == CameraSystem.CameraAngle.RIGHT) && (alice.climbBack2Flag == true)))
                    {
                        flag = true;
                    }
                    break;
            }
        }

        return flag;
    }

    //カメラの左回転
    public void CameraLeftTrunMove(){ cameraLeftTurnEvent(); }

    //カメラの左回転
    public void CameraRightTrunMove(){ cameraRightTurnEvent(); }

    // キー入力によるプレイヤーの前移動
    public void InputPlayerMoveFront() { inputPlayerMoveFrontEvent(); }

    // キー入力によるプレイヤーの後移動
    public void InputPlayerMoveBack() { inputPlayerMoveBackEvent(); }

    // キー入力によるプレイヤーの左移動
    public void InputPlayerMoveLeft() { inputPlayerMoveLeftEvent(); }

    // キー入力によるプレイヤーの右移動
    public void InputPlayerMoveRight() { inputPlayerMoveRightEvent(); }

    // キー入力によるプレイヤーの上移動
    public void InputPlayerMoveUp() { inputPlayerMoveUpEvent(); }

    // キー入力によるプレイヤーの下移動
    public void InputPlayerMoveDown() { inputPlayerMoveDownEvent(); }

    // キー入力によるプレイヤーの待機
    public void InputPlayerMoveStop() { inputPlayerMoveStopEvent(); }

    //巻き戻し移動
    public void InputPlayerMoveReturn() { inputPlayerMoveReturnEvent(); }

    // 早送り移動
    public void InputPlayerMoveNext() { inputPlayerMoveNextEvent(); }

    // プレイヤーの前移動
    public void PlayerMoveFront() { playerMoveFrontEvent(); }

    // プレイヤーの後移動
    public void PlayerMoveBack() { playerMoveBackEvent(); }

    // プレイヤーの左移動
    public void PlayerMoveLeft() { playerMoveLeftEvent(); }

    // プレイヤーの右移動
    public void PlayerMoveRight() { playerMoveRightEvent(); }

    // プレイヤーの上移動
    public void PlayerMoveUp() { playerMoveUpEvent(); }

    // プレイヤーの下移動
    public void PlayerMoveDown() { playerMoveDownEvent(); }

    // プレイヤーの待機
    public void PlayerMoveStop() { playerMoveStopEvent(); }
}
