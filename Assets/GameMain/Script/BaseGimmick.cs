using UnityEngine;
using System.Collections;

public class BaseGimmick : MonoBehaviour
{

    //ギミックが有効かどうか
    protected bool gimmickFlag;

    //ギミックが動き出すターン数
    public int startActionTurn;

    //ギミックが動いた回数
    protected int gimmickCount;

	// Use this for initialization
	void Start ()
    {
        gimmickFlag = false;
        startActionTurn = 0;
        gimmickCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //アリスが動いたときに呼ばれる関数
    public virtual void OnAliceMove(int aliceMoveTime)
    {

    }

    //アリスが戻った時に呼ばれる関数
    public virtual void OnAliceReturn(int aliceMoveTime)
    {

    }

    //有効になるターンを入れる
    public virtual void SetStartActionTurn(int actionTurn)
    {
        startActionTurn = actionTurn;
    }
}
