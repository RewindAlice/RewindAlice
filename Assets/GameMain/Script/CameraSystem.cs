﻿using UnityEngine;
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
	}
	
	// 更新
	void Update () 
	{
        // カメラをプレイヤーに追従させる
        transform.position = new Vector3(alice.transform.position.x + offset.x, alice.transform.position.y + offset.y, alice.transform.position.z + offset.z);
        
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
                    }
                    break;
            }
		}

        transform.eulerAngles = new Vector3(0, currentRotationY, 0);    // カメラの角度に現在の角度を設定

        // 目標の角度に到達したら
        if (currentRotationY == targetRotationY)
        {
            flag = false;
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
}
