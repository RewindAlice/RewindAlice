using UnityEngine;
using System.Collections;

public class Door : BaseGimmick
{
    const int DIR_N = 1; // 北
    const int DIR_E = 2; //  東
    const int DIR_W = 3; // 西
    const int DIR_S = 4; //  南

    public int dir; // 向き
    public bool truthFlag; // 本物かどうか
    //public bool gimmickFlag;        // ギミックが有効か判断するフラグ
    public bool trapFlag;           // アニメーションを切り替えるフラグ
    public int unlockTime;

    // 初期化
    void Start()
    {
        trapFlag = false;
    }

    // 更新
    void Update()
    {
        GetComponent<Animator>().SetBool("openFlag", trapFlag);
        Debug.Log(trapFlag);
    }
}