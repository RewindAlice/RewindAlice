using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class ChangeCharacter : MonoBehaviour {


    //キャラクター
    public Sprite flower;
    public Sprite cheshireCat;
    public Sprite alice;
    public Sprite greenCaterpillar;
    public Sprite whiteRabbit;
    public Sprite madHatter;
    public Sprite queenOfHeart;
    public Sprite sisterAlice;

    // シナリオを格納する一行格納する
    public string[] scenarios;

    //出力するテキストのファイルパス
    private string filepath;


    //ステージの番号
    private int stageNumber;

    private int timeCount;

    //イメージ変更用
    public Image image;

    public int charaCount;

    //会話の限界
    private int limitTalk;
    //ヒントなしの話の数
    private int talkNumber;

    private int talkSpeed;

    private GameObject pause;
    private Pause pauseScript;


	// Use this for initialization
	void Start () {

        pause = GameObject.Find("Pause");
        pauseScript = pause.GetComponent<Pause>();
        //オブジェクトのImageを入手
        image = gameObject.GetComponent<Image>();
        charaCount = 0;
        timeCount = 0;
        //ステージの番号を取得
        stageNumber = PlayerPrefs.GetInt("STAGE_NUM");
        if (stageNumber == 1)
        {
            stageNumber = 111;
        }
        else if (stageNumber == 2)
        {
            stageNumber = 112;
        }
        else if (stageNumber == 3)
        {
            stageNumber = 113;
        }
        else if (stageNumber == 4)
        {
            stageNumber = 114;
        }
        else if (stageNumber == 5)
        {
            stageNumber = 115;
        }
        else if (stageNumber == 6)
        {
            stageNumber = 121;
        }
        else if (stageNumber == 7)
        {
            stageNumber = 122;
        }
        else if (stageNumber == 8)
        {
            stageNumber = 123;
        }
        else if (stageNumber == 9)
        {
            stageNumber = 124;
        }
        else if (stageNumber == 10)
        {
            stageNumber = 125;
        }
        else if (stageNumber == 11)
        {
            stageNumber = 131;
        }
        else if (stageNumber == 12)
        {
            stageNumber = 132;
        }
        else if (stageNumber == 13)
        {
            stageNumber = 133;
        }
        else if (stageNumber == 14)
        {
            stageNumber = 134;
        }
        else if (stageNumber == 15)
        {
            stageNumber = 135;
        }

        else if (stageNumber == 16)
        {
            stageNumber = 141;
        }
        else if (stageNumber == 17)
        {
            stageNumber = 142;
        }
        else if (stageNumber == 18)
        {
            stageNumber = 143;
        }
        else if (stageNumber == 19)
        {
            stageNumber = 144;
        }
        else if (stageNumber == 20)
        {
            stageNumber = 145;
        }
        else if (stageNumber == 21)
        {
            stageNumber = 151;
        }
        else if (stageNumber == 22)
        {
            stageNumber = 152;
        }
        else if (stageNumber == 23)
        {
            stageNumber = 153;
        }
        else if (stageNumber == 24)
        {
            stageNumber = 154;
        }
        else if (stageNumber == 25)
        {
            stageNumber = 201;
        }

       
        //ステージの番号によって、取得するパスの変更(Story用)
        if (stageNumber == 111)
        {
            limitTalk = 2;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage1-1character.txt";
        }
        else if (stageNumber == 112)
        {
            limitTalk = 2;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage1-2character.txt";
        }
        else if (stageNumber == 113)
        {
            limitTalk = 3;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage1-3character.txt";
        }
        else if (stageNumber == 114)
        {
            limitTalk = 3;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage1-4character.txt";
        }
        else if (stageNumber == 115)
        {
            limitTalk = 4;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage1-5character.txt";
        }
        else if (stageNumber == 121)
        {
            limitTalk = 2;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage2-1character.txt";
        }
        else if (stageNumber == 122)
        {
            limitTalk = 2;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage2-2character.txt";
        }
        else if (stageNumber == 123)
        {
            limitTalk = 3;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage2-3character.txt";
        }
        else if (stageNumber == 124)
        {
            limitTalk = 4;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage2-4character.txt";
        }
        else if (stageNumber == 125)
        {
            limitTalk = 4;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage2-5character.txt";
        }
        else if (stageNumber == 131)
        {
            limitTalk = 2;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage3-1character.txt";
        }
        else if (stageNumber == 132)
        {
            limitTalk = 3;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage3-2character.txt";
        }
        else if (stageNumber == 133)
        {
            limitTalk = 3;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage3-3character.txt";
        }
        else if (stageNumber == 134)
        {
            limitTalk = 4;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage3-4character.txt";
        }
        else if (stageNumber == 135)
        {
            limitTalk = 4;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage3-5character.txt";
        }
        else if (stageNumber == 141)
        {
            limitTalk = 2;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage4-1character.txt";
        }
        else if (stageNumber == 142)
        {
            limitTalk = 3;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage4-2character.txt";
        }
        else if (stageNumber == 143)
        {
            limitTalk = 3;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage4-3character.txt";
        }
        else if (stageNumber == 144)
        {
            limitTalk = 4;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage4-4character.txt";
        }
        else if (stageNumber == 145)
        {
            limitTalk = 4;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage4-5character.txt";
        }
        else if (stageNumber == 151)
        {
            limitTalk = 2;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage5-1character.txt";
        }
        else if (stageNumber == 152)
        {
            limitTalk = 3;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage5-2character.txt";
        }
        else if (stageNumber == 153)
        {
            limitTalk = 3;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage5-3character.txt";
        }
        else if (stageNumber == 154)
        {
            limitTalk = 4;
            talkNumber = 0;
            filepath = "/GameMain/UI/Text/Character/stage5-4character.txt";
        }

        if (stageNumber == 201)
        {
            limitTalk = 6;
            talkNumber = 0;
            filepath = "/GameMain/UI/stage1-1character.txt";
        }
            
  
        talkSpeed = 600;
      
        this.read();

        if (scenarios[charaCount] == "1")
        {
            image.sprite = flower;
        }
        else if (scenarios[charaCount] == "2")
        {
            image.sprite = cheshireCat;
        }
        else if (scenarios[charaCount] == "3")
        {
            image.sprite = alice;
        }
        else if (scenarios[charaCount] == "4")
        {
            image.sprite = greenCaterpillar;
        }
        else if (scenarios[charaCount] == "5")
        {
            image.sprite = whiteRabbit;
        }
        else if (scenarios[charaCount] == "6")
        {
            image.sprite = madHatter;
        }
        else if (scenarios[charaCount] == "7")
        {
            image.sprite = queenOfHeart;
        }
        else if (scenarios[charaCount] == "8")
        {
            image.sprite = sisterAlice;
        }

        charaCount++;
      

}
	
	// Update is called once per frame
	void Update () {

        if(pauseScript.pauseFlag == false)
        {
            timeCount++;

        }
        

        if (timeCount % talkSpeed == 0)
        {
            if (charaCount == limitTalk)
            {
                charaCount = talkNumber;
            }
          
            if (scenarios[charaCount] == "1")
            {
                image.sprite = flower;
            }
            else if (scenarios[charaCount] == "2")
            {
                image.sprite = cheshireCat;
            }
            else if (scenarios[charaCount] == "3")
            {
                image.sprite = alice;
            }
            else if (scenarios[charaCount] == "4")
            {
                image.sprite = greenCaterpillar;
            }
            else if (scenarios[charaCount] == "5")
            {
                image.sprite = whiteRabbit;
            }
            else if (scenarios[charaCount] == "6")
            {
                image.sprite = madHatter;
            }
            else if (scenarios[charaCount] == "7")
            {
                image.sprite = queenOfHeart;
            }
            else if (scenarios[charaCount] == "8")
            {
                image.sprite = sisterAlice;
            }

            charaCount++;
                
            
            
            
           
        }
        
	
	}

    // テキストの読み込み
    public void read()
    {

        FileInfo fi = new FileInfo(Application.dataPath + "/" + filepath);
        StreamReader sr = new StreamReader(fi.OpenRead());

        int i = 0;
        while (sr.Peek() != -1)
        {
            scenarios[i] = sr.ReadLine();
            i++;
        }
        i = 0;
        sr.Close();
    }
}
