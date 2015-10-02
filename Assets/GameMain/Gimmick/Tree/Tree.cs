using UnityEngine;
using System.Collections;

public class Tree : BaseGimmick
{
    //public bool gimmickFlag;      // ギミックが有効か判断するフラグ
    //public int startActionTurn;   // ギミックを動かし始めるターン数
    //public int gimmickCount;      // ギミックが有効になってからのターン数
    private GameObject pause;
    private Pause pauseScript;
    public int growCount;           // 成長段階
    public bool movePossibleFlag;   // 移動可能フラグ
    public int moveCount;

	// 初期化
	void Start ()
    {
        pause = GameObject.Find("Pause");
        pauseScript = pause.GetComponent<Pause>();
        gimmickFlag = false;        // ギミックが有効か判断するフラグに偽を保存
        startActionTurn = 1;        // ギミックを動かし始めるターン数を１に
        gimmickCount = 0;           // ギミックが有効になってからのターン数を０に
        growCount = 0;              // 初期の成長段階
        movePossibleFlag = true;    // 移動可能フラグを真に
        moveCount = 0;
	}
	
	// 更新
	void Update ()
    {
        if (pauseScript.pauseFlag == false)
        {

        }   
	}

    //アリスが動いたときに呼ばれる関数
    public override void OnAliceMove(int aliceMoveCount)
    {
        // ギミックを動かし始めるターン数とアリスの移動数が同じになったら
        if (startActionTurn == aliceMoveCount)
        {
            gimmickFlag = true;     // ギミックを有効にする
        }

        // 保存されている移動数よりアリスの移動数が多かったら
        if(moveCount < aliceMoveCount)
        {
            // ギミックが有効なら
            if (gimmickFlag == true)
            {
                // ギミックが有効になってからのターン数が
                switch (gimmickCount)
                {
                    case 0:
                        growCount = 1;              // 成長段階を１に
                        movePossibleFlag = true;    // 移動可能フラグを真に
                        break;
                    case 1:
                        growCount = 2;              // 成長段階を２に
                        movePossibleFlag = false;   // 移動可能フラグを偽に
                        break;
                    case 2:
                        growCount = 3;              // 成長段階を３に
                        movePossibleFlag = false;   // 移動可能フラグを偽に
                        break;
                }

                gimmickCount++;
                GetComponent<Animator>().SetInteger("GrowCount", growCount);
            }
            moveCount++;
        }
    }

    //アリスが戻った時に呼ばれる関数
    public override void OnAliceReturn(int aliceMoveCount)
    {
        // ギミックを動かし始めるターン数がアリスの移動数より多かったら
        if (startActionTurn > aliceMoveCount)
        {
            gimmickFlag = false;    // ギミックを有効にする
        }

        // ギミックが有効なら
        if (gimmickFlag == true)
        {
            // 保存されている移動数よりアリスの移動数が同じなら
            if (moveCount == aliceMoveCount)
            {
                gimmickCount--;

                // ギミックが有効になってからのターン数が
                switch (gimmickCount)
                {
                    case 0:
                        growCount = 0;              // 成長段階を０に
                        movePossibleFlag = true;    // 移動可能フラグを真に
                        gimmickFlag = false;        // ギミックを非有効にする
                        break;
                    case 1:
                        growCount = 1;              // 成長段階を１に
                        movePossibleFlag = true;    // 移動可能フラグを真に
                        break;
                    case 2:
                        growCount = 2;              // 成長段階を２に
                        movePossibleFlag = false;   // 移動可能フラグを偽に
                        break;
                }
                GetComponent<Animator>().SetInteger("GrowCount", growCount);
            }
            moveCount--;
        }
    }
}
