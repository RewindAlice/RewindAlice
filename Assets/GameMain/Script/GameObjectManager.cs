using UnityEngine;
using System.Collections;

public class GameObjectManager : MonoBehaviour
{
    public GameMain gameMain;
    public CameraSystem camera;
    public Player alice;

	void Start ()
    {
        //カメラ左回転
        gameMain.cameraLeftTurnEvent += camera.TurnLeft;

        //カメラ右回転
        gameMain.cameraRightTurnEvent += camera.TurnRight;

        // キー入力によるプレイヤーの前移動
        gameMain.inputPlayerMoveFrontEvent += alice.MoveFront;      // 前移動
        gameMain.inputPlayerMoveFrontEvent += alice.SaveInputMove;  // 移動情報の保存
        gameMain.inputPlayerMoveFrontEvent += alice.ModeCountDown;

        // キー入力によるプレイヤーの後移動
        gameMain.inputPlayerMoveBackEvent += alice.MoveBack;        // 後移動
        gameMain.inputPlayerMoveBackEvent += alice.SaveInputMove;   // 移動情報の保存
        gameMain.inputPlayerMoveBackEvent += alice.ModeCountDown;

        // キー入力によるプレイヤーの左移動
        gameMain.inputPlayerMoveLeftEvent += alice.MoveLeft;        // 左移動
        gameMain.inputPlayerMoveLeftEvent += alice.SaveInputMove;   // 移動情報の保存
        gameMain.inputPlayerMoveLeftEvent += alice.ModeCountDown;

        // キー入力によるプレイヤーの右移動
        gameMain.inputPlayerMoveRightEvent += alice.MoveRight;      // 右移動
        gameMain.inputPlayerMoveRightEvent += alice.SaveInputMove;  // 移動情報の保存
        gameMain.inputPlayerMoveRightEvent += alice.ModeCountDown;

        // キー入力によるプレイヤーの上移動
        gameMain.inputPlayerMoveUpEvent += alice.MoveUp;            // 上移動
        gameMain.inputPlayerMoveUpEvent += alice.SaveInputMove;     // 移動情報の保存
        gameMain.inputPlayerMoveUpEvent += alice.ModeCountDown;

        // キー入力によるプレイヤーの下移動
        gameMain.inputPlayerMoveDownEvent += alice.MoveDown;        // 下移動
        gameMain.inputPlayerMoveDownEvent += alice.SaveInputMove;   // 移動情報の保存
        gameMain.inputPlayerMoveDownEvent += alice.ModeCountDown;

        // キー入力によるプレイヤーの待機
        gameMain.inputPlayerMoveStopEvent += alice.MoveStop;        // 待機
        gameMain.inputPlayerMoveStopEvent += alice.SaveInputMove;   // 移動情報の保存
        gameMain.inputPlayerMoveStopEvent += alice.ModeCountDown;

        // キー入力によるプレイヤーの巻き戻し
        gameMain.inputPlayerMoveReturnEvent += alice.MoveReturn;    // 巻き戻し

        // キー入力によるプレイヤーの早送り
        gameMain.inputPlayerMoveNextEvent += alice.MoveNext;        // 早送り

        // プレイヤーの前移動
        gameMain.playerMoveFrontEvent += alice.MoveFront;
        gameMain.playerMoveFrontEvent += alice.SaveMove;

        // プレイヤーの後移動
        gameMain.playerMoveBackEvent += alice.MoveBack;
        gameMain.playerMoveBackEvent += alice.SaveMove;

        // プレイヤーの左移動
        gameMain.playerMoveLeftEvent += alice.MoveLeft;
        gameMain.playerMoveLeftEvent += alice.SaveMove;

        // プレイヤーの右移動
        gameMain.playerMoveRightEvent += alice.MoveRight;
        gameMain.playerMoveRightEvent += alice.SaveMove;

        // プレイヤーの上移動
        gameMain.playerMoveUpEvent += alice.MoveUp;
        gameMain.playerMoveUpEvent += alice.SaveMove;

        // プレイヤーの下移動
        gameMain.playerMoveDownEvent += alice.MoveDown;
        gameMain.playerMoveDownEvent += alice.SaveMove;

        // プレイヤーの待機
        gameMain.playerMoveStopEvent += alice.MoveStop;
        gameMain.playerMoveStopEvent += alice.SaveMove;
    }

	void Update ()
    {
	
	}
}
