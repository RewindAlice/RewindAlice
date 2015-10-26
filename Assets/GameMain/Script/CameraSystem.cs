using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour
{
    const int CAMERA_FRONT = 0;
    const int CAMERA_BACK = 0;
    const int CAMERA_LEFT = 0;
    const int CAMERA_RIGHT = 0;

    // 向き
    public enum CameraAngle
    {
        FRONT,  // 前
        BACK,   // 後
        LEFT,   // 左
        RIGHT,  // 右
    }

    // 回転方向
    public enum Turn
    {
        LEFT,   // 左
        RIGHT,  // 右
    }

    public Player alice;                // 追従対象
    public CameraAngle cameraAngle;     // 追従対象に対してのアングル
    public int currentRotationY;        // 現在の角度
    public int targetRotationY;         // 目的の角度
    public int inputKeyRotationY;       // キー入力時の角度
    public bool flag;                   // キー入力フラグ
    Vector3 offset;                     // カメラと対象の距離
    Turn turn;                          // カメラの回転方向

    public GameObject camera;

    public Camera mapCamera;

    public bool clearFlag;
    public int clearY;
    public int clearTY;

    public float cameraXZ;
    public float cameraY;
    public int time;

    public float cameraRX;

    //（クリア）回転開始時に一回だけ通す
    private bool onceDirection;
    //（クリア）回転終了判定
    private bool cycleEnd;

    // 左回転時の方向設定
    void RotationLeft()
    {
        // キー入力時の角度が
        switch (inputKeyRotationY)
        {
            //case 0: cameraAngle = CameraAngle.LEFT; break;        // 0度(BACK)ならLEFTを設定
            //case 90: cameraAngle = CameraAngle.FRONT; break;      // 90度(LEFT)ならFRONTを設定
            //case 180: cameraAngle = CameraAngle.RIGHT; break;     // 180度(FRONT)ならRIGHTを設定
            //case 270: cameraAngle = CameraAngle.BACK; break;      // 270度(RIGHT)ならBACKを設定

            case 330: cameraAngle = CameraAngle.LEFT; break;        // 0度(BACK)ならLEFTを設定
            case 60: cameraAngle = CameraAngle.FRONT; break;      // 90度(LEFT)ならFRONTを設定
            case 150: cameraAngle = CameraAngle.RIGHT; break;     // 180度(FRONT)ならRIGHTを設定
            case 240: cameraAngle = CameraAngle.BACK; break;      // 270度(RIGHT)ならBACKを設定
        }
    }

    // 右回転時の方向設定
    void RotationRight()
    {
        // キー入力時の角度が
        switch (inputKeyRotationY)
        {
            //case 0: cameraAngle = CameraAngle.RIGHT; break;       // 0度(BACK)ならRIGHTを設定
            //case 90: cameraAngle = CameraAngle.BACK; break;       // 90度(LEFT)ならBACKを設定
            //case 180: cameraAngle = CameraAngle.LEFT; break;      // 180度(FRONT)ならLEFTを設定
            //case 270: cameraAngle = CameraAngle.FRONT; break;     // 270度(RIGHT)ならFRONTを設定

            case 330: cameraAngle = CameraAngle.RIGHT; break;       // 0度(BACK)ならRIGHTを設定
            case 60: cameraAngle = CameraAngle.BACK; break;       // 90度(LEFT)ならBACKを設定
            case 150: cameraAngle = CameraAngle.LEFT; break;      // 180度(FRONT)ならLEFTを設定
            case 240: cameraAngle = CameraAngle.FRONT; break;     // 270度(RIGHT)ならFRONTを設定
        }
    }

	// 初期化
	void Start ()
    {
        cameraAngle = CameraAngle.BACK;                                   // 初期アングルを(BACK)で初期化
        inputKeyRotationY = 0;                                      // キー入力時の角度を0で初期化
        targetRotationY = 0;                                        // 目的の角度を0で初期化
        currentRotationY = 330;                                       // 現在の角度を0で初期化
        offset = transform.position - alice.transform.position;     // 追従対象との距離の初期化
        turn = Turn.LEFT;
        flag = false;

        clearFlag = false;
        clearY = 0;
        clearTY = 0;
        onceDirection = false;

        camera = GameObject.Find("Main Camera");

        cameraXZ = -4;
        cameraY = 2;
        time = 0;
        cameraRX = 15;
	}
	
	// 更新
	void Update () 
	{
        // 回転する
		if (flag == true) 
		{
            switch(turn)
            {
                // 回転方向が左なら
                case Turn.LEFT:
                    RotationLeft();
                    switch (cameraAngle)
                    {
                        case CameraAngle.FRONT: targetRotationY = 150; break;
                        case CameraAngle.BACK: targetRotationY = 330; break;
                        case CameraAngle.LEFT: targetRotationY = 60; break;
                        case CameraAngle.RIGHT: targetRotationY = 240; break;
                    }

                    if (currentRotationY != targetRotationY)
                    {
                        currentRotationY++;
                        mapCamera.transform.Rotate(0, 0, -1);
                        if (currentRotationY == 360)
                        {
                            currentRotationY = 0;
                        }
                    }

                    break;
                // 回転方向が右なら
                case Turn.RIGHT:
                    RotationRight();
                    switch (cameraAngle)
                    {
                        case CameraAngle.FRONT: targetRotationY = 150; break;
                        case CameraAngle.BACK: targetRotationY = 330; break;
                        case CameraAngle.LEFT: targetRotationY = 60; break;
                        case CameraAngle.RIGHT: targetRotationY = 240; break;
                    }

                    if (currentRotationY != targetRotationY)
                    {
                        if (currentRotationY == 0)
                        {
                            currentRotationY = 360;
                        }

                        currentRotationY--;

                        mapCamera.transform.Rotate(0, 0, 1);
                    }
                    break;
            }

            
		}


        if (clearFlag == false)
        {
            // カメラをプレイヤーに追従させる
            transform.position = new Vector3(alice.transform.position.x + offset.x, alice.transform.position.y + offset.y, alice.transform.position.z + offset.z);

           
           
            transform.eulerAngles = new Vector3(0, currentRotationY, 0);    // カメラの角度に現在の角度を設定
        }

        // 目標の角度に到達したら
        if (currentRotationY == targetRotationY)
        {
            flag = false;
        }

        //クリアしたら
        if (clearFlag == true)
        {
            if (onceDirection == false)
            {
                clearY = (int)transform.eulerAngles.y;
                clearTY = clearY;
                onceDirection = true;
            }
            //アリスの向きを取得
            int direction = alice.GetDirection();
            
           //アリスの向きによって回転させる
            switch (direction)
            {
                case 1:
                    if (clearY < 136 || clearY > 314)
                    {
                        if (clearTY != 135)
                        {
                            clearTY += 2;
                        }

                        if (clearTY == 360)
                        {
                            clearTY = 1;
                        }
                    }
                    else
                    {
                        if (clearTY != 135)
                        {
                            clearTY -= 2;
                        }

                        if (clearTY == 0)
                        {
                            clearTY = 359;
                        }
                    }
                    if(clearTY == 135)
                    {
                        cycleEnd = true;
                    }


                    break;

                case 2:

                    if (clearY < 45 || clearY > 224)
                    {
                        if (clearTY != 225)
                        {
                            clearTY -= 2;
                        }

                        if (clearTY == 0)
                        {
                            clearTY = 359;
                        }
                    }
                    else
                    {
                        if (clearTY != 225)
                        {
                            clearTY += 2;
                        }

                        if (clearTY == 360)
                        {
                            clearTY = 1;
                        }
                    }
                    if (clearTY == 225)
                    {
                        cycleEnd = true;
                    }
                    break;

                case 3:
                    if (clearY < 136 || clearY > 314)
                    {
                        if (clearTY != 315)
                        {
                            clearTY -= 2;
                        }

                        if (clearTY == 0)
                        {
                            clearTY = 359;
                        }
                    }
                    else
                    {
                        if (clearTY != 315)
                        {
                            clearTY += 2;
                        }

                        if (clearTY == 360)
                        {
                            clearTY = 1;
                        }
                    }
                    if (clearTY == 315)
                    {
                        cycleEnd = true;
                    }
                    break;
                case 4:
                    if (clearY < 45 || clearY > 224)
                    {
                        if (clearTY != 45)
                        {
                            clearTY += 2;
                        }

                        if (clearTY == 360)
                        {
                            clearTY = 1;
                        }
                    }
                    else
                    {
                        if (clearTY != 45)
                        {
                            clearTY -= 2;
                        }

                        if (clearTY == 0)
                        {
                            clearTY = 359;
                        }
                    }
                    if (clearTY == 45)
                    {
                        cycleEnd = true;
                    }
                    break;
            }
          
            //カメラを近づける
            if(time < 60)
            {
                cameraXZ += 0.05f;

                cameraRX -= 0.25f;

                if(time<50)
                {
                    cameraY -= 0.025f;
                }
                else
                {
                    cameraY -= 0.035f;
                }
                time++;
                
            }
            

            transform.eulerAngles = new Vector3(0, clearTY, 0);    // カメラの角度に現在の角度を設定
            

            //ズーム
            camera.transform.localPosition = new Vector3(cameraXZ, cameraY, cameraXZ);     // 座標を変更
            camera.transform.localEulerAngles = new Vector3(cameraRX, 45.0f, 0.0f);


        }

        

       
	}

    // 左回転
    public void TurnLeft()
    {
        // キー入力がされていなければ
        if(flag == false)
        {
            flag = true;                                        // キー入力フラグを真に
            inputKeyRotationY = (int)transform.eulerAngles.y;   // キー入力時の角度に現在の角度を設定
            turn = Turn.LEFT;                                   // 回転方向に左回転を設定
        }
    }

    // 右回転
    public void TurnRight()
    {
        // キー入力がされていなければ
        if(flag == false)
        {
            flag = true;                                        // キー入力フラグを真に
            inputKeyRotationY = (int)transform.eulerAngles.y;   // キー入力時の角度に現在の角度を設定
            turn = Turn.RIGHT;                                  // 回転方向に右回転を設定
        }
    }
    //リザルト用のカメラ移動が終わった時を送る
    public bool EndCameraMove()
    {
        if(cycleEnd)
        {
            return true;
        }
        return false;
    }

 
}
