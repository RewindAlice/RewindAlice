using UnityEngine;
using System.Collections;

public class Key : BaseGimmick
{
     private GameObject pause;
    private Pause pauseScript;

	private Renderer renderer;

	public int gimmickActionCount;  // ギミックに触れてからのターン数
	public bool gimmickDrawFlag;    // ギミックを表示するかのフラグ
	public int moveCount;

	void Start()
	{ 
        pause = GameObject.Find("Pause");
        pauseScript = pause.GetComponent<Pause>();

		renderer = GetComponentInChildren<Renderer>();
		gimmickFlag = true;     //ギミックが有効かどうか
		startActionTurn = 0;    //ギミックが動き出すターン数
		gimmickCount = 0;       //ギミックが動いた回数

		gimmickActionCount = 0;     // ギミックに触れてからのターン数を０で初期化
		gimmickDrawFlag = true;     // ギミックを表示するかのフラグを真に
		moveCount = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (pauseScript.pauseFlag == false)
		{
			// 描画フラグが
			switch (gimmickDrawFlag)
			{
				// 真なら
				case true:
					renderer.enabled = true;    // 描画する
					break;
				// 偽なら
				case false:
					renderer.enabled = false;   // 描画しない
					break;
			}
		} 	
	}

	//アリスが動いたときに呼ばれる関数
	public override void OnAliceMove(int aliceMoveCount)
	{
		// 保存されている移動数よりアリスの移動数が多かったら
		if (moveCount < aliceMoveCount)
		{
			// ギミックが有効でなければ（既に触れていれば）
			if (gimmickFlag == false)
			{
				gimmickActionCount++;   // ギミックに触れてからのターン数を増やす
			}
			moveCount++;
		}
	}

	//アリスが戻った時に呼ばれる関数
	public override void OnAliceReturn(int aliceMoveCount)
	{
		// 保存されている移動数よりアリスの移動数が多かったら
		if (moveCount == aliceMoveCount)
		{
			// ギミックが有効でなければ（既に触れていれば）
			if (gimmickFlag == false)
			{
				gimmickActionCount--;   // ギミックに触れてからのターン数を減らす

				// ギミックに触れてからのターン数が０より小さくなったら
				if (gimmickActionCount == -1)
				{
					gimmickActionCount = 0;     // ギミックに触れてからのターン数を０
					gimmickFlag = true;
					gimmickDrawFlag = true;
				}
			}
			moveCount--;
		}
	}

	//有効になるターンを入れる
	public virtual void SetStartActionTurn(int actionTurn)
	{
		startActionTurn = actionTurn;
	}

	// ギミックフラグの設定
	public void SetGimmickFlag(bool flag)
	{
		gimmickFlag = flag;
	}

	// ギミックフラグの取得
	public bool GetGimmickFlag()
	{
		return gimmickFlag;
	}

	public void Initialize()
	{

	}

	public void Invisible(bool flag)
	{
		//Renderer[] renderChildren = GetComponentsInChildren<Renderer>();

		//for (int i = 0; i < renderChildren.Length; ++i)
		//{
		//	if (flag)
		//		renderChildren[i].enabled = false;
		//	else
		//		renderChildren[i].enabled = true;
		//}
	}
}