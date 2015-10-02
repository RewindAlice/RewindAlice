using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AliceActionNotifer {

    public GameObject[] gimmickAry;

    public GameObject[] GimmickAry
    {
        set { gimmickAry = value; }
    }

    //アリスを動かしたとき
    public void NotifyMove(int aliceMoveTime)
    {
        for (int i = 0; i < gimmickAry.Length; i++)
        {
            gimmickAry[i].GetComponent<BaseGimmick>().OnAliceMove(aliceMoveTime);

        }
    }

    //アリスを戻した時
    public void NotifyReturn(int aliceMoveTime)
    {
        for (int i = 0; i < gimmickAry.Length; i++)
        {
            gimmickAry[i].GetComponent<BaseGimmick>().OnAliceReturn(aliceMoveTime);

        }
    }
}
