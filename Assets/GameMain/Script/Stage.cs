using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour
{
    const int STAGE_X = 11; // ステージの横幅
    const int STAGE_Y = 5;  // ステージの高さ
    const int STAGE_Z = 11; // ステージの奥行

    // ステージの天球
    public enum Field
    {
        STAGE_1,    // ステージ１
        STAGE_2,    // ステージ２
        STAGE_3,    // ステージ３
        STAGE_4,    // ステージ４
        STAGE_5,    // ステージ５
    }

    // ステージギミック番号
    const int NONE = 0;     // 何も無い
    const int BLOCK = 1;    // 足場ブロック
    const int START = -1;   // スタート地点
    const int GOAL = -2;    // ゴール地点
    const int WATER = 2;    // 水

    const int BLOCK_GRASS_GROUND = 11;      // 森ステージの足場ブロック（１段目）
    const int BLOCK_GRASS_1 = 12;           // 森ステージの足場ブロック（２段目）
    const int BLOCK_GRASS_2 = 13;           // 森ステージの足場ブロック（３段目）
    const int BLOCK_HOUSE_GROUND = 21;      // 家ステージの足場ブロック（１段目）
    const int BLOCK_HOUSE_BOOK = 22;        // 家ステージの足場ブロック（本棚）

    const int BLOCK_SUN_GRASS_GROUND = 31;      // 森ステージの足場ブロック（１段目）
    const int BLOCK_SUN_GRASS_1 = 32;           // 森ステージの足場ブロック（２段目）
    const int BLOCK_SUN_GRASS_2 = 33;           // 森ステージの足場ブロック（３段目）

    const int BLOCK_INSECT_GROUND = 41;     // 暗い森ステージの足場ブロック（１段目）

    const int BLOCK_GARDEN_GROUND = 51;     // 暗い森ステージの足場ブロック（１段目）
    const int BLOCK_GARDEN_FLOWER = 52;     // 暗い森ステージの足場ブロック（１段目）

    const int HOLE1 = 60;   // 穴１
    const int HOLE2 = 61;   // 穴２
    const int HOLE3 = 62;   // 穴３
    const int HOLE4 = 63;   // 穴４
    const int HOLE5 = 64;   // 穴５

    const int TREE1 = 65;   // 木（高さ１）
    const int TREE2 = 66;   // 木（高さ２）

    const int IVY_BLOCK = 70;       // 蔦ブロック
    const int IVY_FRONT = 71;       // 蔦（FRONT）
    const int IVY_BACK = 72;        // 蔦（BACK）
    const int IVY_LEFT = 73;        // 蔦（LEFT）
    const int IVY_RIGHT = 74;       // 蔦（RIGHT）

    const int LADDER_BLOCK = 75;    // 梯子ブロック
    const int LADDER_FRONT = 76;    // 梯子（FRONT）
    const int LADDER_BACK = 77;     // 梯子（BACK）
    const int LADDER_LEFT = 78;     // 梯子（LEFT）
    const int LADDER_RIGHT = 79;    // 梯子（RIGHT）

    const int DOOR_RED_KEY = 80;        // 赤扉（鍵）
    const int DOOR_RED_FRONT = 81;      // 赤扉（FRONT）
    const int DOOR_RED_BACK = 82;       // 赤扉（BACK）
    const int DOOR_RED_LEFT = 83;       // 赤扉（LEFT）
    const int DOOR_RED_RIGHT = 84;      // 赤扉（RIGHT）
    const int DOOR_BLUE_KEY = 85;       // 青扉（鍵）
    const int DOOR_BLUE_FRONT = 86;     // 青扉（FRONT）
    const int DOOR_BLUE_BACK = 87;      // 青扉（BACK）
    const int DOOR_BLUE_LEFT = 88;      // 青扉（LEFT）
    const int DOOR_BLUE_RIGHT = 89;     // 青扉（RIGHT）
    const int DOOR_YELLOW_KEY = 90;     // 黄扉（鍵）
    const int DOOR_YELLOW_FRONT = 91;   // 黄扉（FRONT）
    const int DOOR_YELLOW_BACK = 92;    // 黄扉（BACK）
    const int DOOR_YELLOW_LEFT = 93;    // 黄扉（LEFT）
    const int DOOR_YELLOW_RIGHT = 94;   // 黄扉（RIGHT）
    const int DOOR_GREEN_KEY = 95;      // 緑扉（鍵）
    const int DOOR_GREEN_FRONT = 96;    // 緑扉（FRONT）
    const int DOOR_GREEN_BACK = 97;     // 緑扉（BACK）
    const int DOOR_GREEN_LEFT = 98;     // 緑扉（LEFT）
    const int DOOR_GREEN_RIGHT = 99;    // 緑扉（RIGHT）

    const int FLOWER1 = 100;    // 花１
    const int FLOWER2 = 101;    // 花２
    const int FLOWER3 = 102;    // 花３
    const int BRAMBLE = 103;    // 茨

    const int CHESHIRE_CAT = 105;       // チェシャ猫
    const int MUSHROOM_SMALL = 106;     // キノコ（小さくなる）
    const int MUSHROOM_BIG = 107;       // キノコ（大きくなる）
    const int POTION_SMALL = 108;       // 薬（小さくなる）
    const int POTION_BIG = 109;         // 薬（大きくなる）

    const int SOLDIER_HEART = 110;      // トランプ兵（その場監視）
    const int SOLDIER_SPADE1 = 111;     // トランプ兵（巡回１）
    const int SOLDIER_SPADE2 = 112;     // トランプ兵（巡回２）
    const int TWINS_LEFT_FRONT = 113;         // 双子（LEFT）
    const int TWINS_RIGHT_FRONT = 114;        // 双子（RIGHT）
    const int EGG = 115;                // ハンプティ―
    const int STONE = 116;              // 石
    const int BOX = 117;                // 木箱

    const int TWINS_LEFT_BACK = 120;         // 双子（LEFT）
    const int TWINS_RIGHT_BACK = 126;        // 双子（RIGHT）

    const int TWINS_LEFT_LEFT = 121;         // 双子（LEFT）
    const int TWINS_RIGHT_LEFT = 127;        // 双子（RIGHT）

    const int TWINS_LEFT_RIGHT = 122;         // 双子（LEFT）
    const int TWINS_RIGHT_RIGHT = 128;        // 双子（RIGHT）

    private bool presenceDum = false;
    private bool presenceDee = false;
    private TweedleDum Dum;
    private TweedleDee Dee;
    private bool presenceCheshire = false;
    private bool presenceDoor_Red;
    private bool presenceDoor_Blue;
    private bool presenceDoor_Yellow;
    private bool presenceDoor_Green;

    public GameMain gameMain;
    private GameObject player;
    private Player Player;

    public GameObject fieldStage1;  // ステージ１の天球
    public GameObject fieldStage2;  // ステージ２の天球
    public GameObject fieldStage3;  // ステージ３の天球
    public GameObject fieldStage4;  // ステージ４の天球
    public GameObject fieldStage5;  // ステージ５の天球

    public GameObject block;    // 足場ブロック
    public GameObject start;    // スタート地点
    public GameObject goal;     // ゴール地点
    public GameObject water;    // 水

    //----------------------------
    //ステージブロックオブジェクト
    public GameObject blockGrassGround;           // 森ステージの足場ブロック（１段目）
    public GameObject blockGrass1;                // 森ステージの足場ブロック（２段目）
    public GameObject blockGrass2;                // 森ステージの足場ブロック（３段目）
    public GameObject blockBlack;                 // 家ステージの足場ブロック（黒）
    public GameObject blockWhite;                 // 家ステージの足場ブロック（白）
    public GameObject blockBook;                  // 家ステージの足場ブロック（本棚）
    public GameObject blockSunsetGrassGround;     // 森(ステージ4)ステージの足場ブロック（１段目）
    public GameObject blockSunsetGrass1;          // 森(ステージ4)ステージの足場ブロック（２段目）
    public GameObject blockSunsetGrass2;          // 森(ステージ4)ステージの足場ブロック（３段目）
    public GameObject blockInsect1;               // 暗い森ステージの足場ブロック（茶）
    public GameObject blockInsect2;               // 暗い森ステージの足場ブロック（青）
    public GameObject blockInsect3;               // 暗い森ステージの足場ブロック（赤）
    public GameObject blockInsectGrassBule;       // 暗い森ステージの足場ブロック（青）（２段目以降）
    public GameObject blockInsectGrassRed;        // 暗い森ステージの足場ブロック（赤）（２段目以降）
    //public GameObject blockGardenGrassGround;     // 庭園ステージの足場ブロック(一段目?)
    public GameObject blockGardenGrass;           // 庭園ステージの足場ブロック（一段目）
    public GameObject blockGardenRoseRed;         // 庭園ステージの足場ブロック（赤）（２段目以降）
    public GameObject blockGardenRoseWhite;       // 庭園ステージの足場ブロック（白）（２段目以降）
    //----------------------------
    public GameObject hole;    // 穴

    public GameObject tree;     // 木

    public GameObject ivy;      // 蔦

    public GameObject ladder;       // 梯子

    private Key DoorKey; // 鍵
    private int holdKeyColor; // 所持している鍵の色

    public GameObject doorKey;      // 鍵
    public GameObject doorRed;      // 赤扉
    public GameObject doorBlue;     // 青扉
    public GameObject doorYellow;   // 黄扉
    public GameObject doorGreen;    // 緑扉

    private Door DoorRed; // 赤扉
    private Door DoorBlue; // 青扉
    private Door DoorYellow; // 黄扉
    private Door DoorGreen; // 緑扉

    public GameObject flower1;  // 花１
    public GameObject flower2;  // 花２
    public GameObject flower3;  // 花３
    public GameObject bramble;  // 茨

    public GameObject cheshireCat;      // チェシャ猫
    private Cheshire CheshireCat; // チェシャネコ
    public GameObject mushroomSmall;    // キノコ（小さくなる）
    public GameObject mushroomBig;      // キノコ（大きくなる）
    public GameObject potionSmall;      // 薬（小さくなる）
    public GameObject potionBig;        // 薬（大きくなる）

    public GameObject soldierHeart;     // トランプ兵（ハート）
    public GameObject soldierSpade;     // トランプ兵（スペード）
    public GameObject twinsLeft;        // 双子（LEFT）
    public GameObject twinsRight;       // 双子（RIGHT）
    public GameObject egg;              // ハンプティ―
    //public GameObject tweedleDum; // トゥイードル・ダム


    public bool invisibleFlag = false; // 不可視状態か
    public bool getKeyFlag = false; // 鍵を所持しているか
    public bool holdKeyFlag = false; // 鍵を所持しているか



    const int DIRECTION_1 = 1;  // 向き１
    const int DIRECTION_2 = 2;  // 向き２
    const int DIRECTION_3 = 3;  // 向き３
    const int DIRECTION_4 = 4;  // 向き４

    public bool nextFlag = false;   // ターンを進めるフラグ
    public bool backFlag = false;   // ターンを戻すフラグ
    //

    Field field;                                                                // フィールド
    public int[, ,] stage = new int[STAGE_Y, STAGE_X, STAGE_Z];                 // ステージの配置情報を保存
    GameObject fieldObject;                                                     // ステージ天球のオブジェクト
    GameObject[, ,] stageObject = new GameObject[STAGE_Y, STAGE_X, STAGE_Z];    // ステージのオブジェクト
    public int turnCount;                                                       // ステージのターン数
    public bool goalFlag = false;
    public bool presenceKeyFlag = false;
    public bool presenceCheshireFlag = false;


    // 初期化
    void Start()
    {
        player = GameObject.Find("Alice");
        Player = player.GetComponent<Player>();
    }

    // 更新
    void Update()
    {

    }

    // ステージ生成/////////////
    public void CreateStage()
    {
        Vector3 fieldPosition = new Vector3(5, -0.5f, 5);

        switch (field)
        {
            case Field.STAGE_1: fieldObject = GameObject.Instantiate(fieldStage1, fieldPosition, Quaternion.identity) as GameObject; break;
            case Field.STAGE_2: fieldObject = GameObject.Instantiate(fieldStage2, fieldPosition, Quaternion.identity) as GameObject; break;
            case Field.STAGE_3: fieldObject = GameObject.Instantiate(fieldStage3, fieldPosition, Quaternion.identity) as GameObject; break;
            case Field.STAGE_4: fieldObject = GameObject.Instantiate(fieldStage4, fieldPosition, Quaternion.identity) as GameObject; break;
            case Field.STAGE_5: fieldObject = GameObject.Instantiate(fieldStage5, fieldPosition, Quaternion.identity) as GameObject; break;
        }

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    // ギミックの生成
                    CreateGimmick(x, y, z);
                }
            }
        }
    }

    // ギミック生成//////////////////////////////////
    public void CreateGimmick(int x, int y, int z)
    {
        switch (stage[y, x, z])
        {
            // 何も無い
            case NONE:
                stageObject[y, x, z] = null;
                break;
            // 足場ブロック
            case BLOCK:
                stageObject[y, x, z] = GameObject.Instantiate(block, new Vector3(x, y, z), Quaternion.identity) as GameObject;
                break;
            // 森ステージの足場ブロック（１段目）
            case BLOCK_GRASS_GROUND:
                stageObject[y, x, z] = GameObject.Instantiate(blockGrassGround, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                break;
            // 森ステージの足場ブロック（２段目）
            case BLOCK_GRASS_1:
                stageObject[y, x, z] = GameObject.Instantiate(blockGrass1, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                break;
            // 森ステージの足場ブロック（３段目）
            case BLOCK_GRASS_2:
                stageObject[y, x, z] = GameObject.Instantiate(blockGrass2, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                break;
            // 家ステージの足場ブロック（１段目）
            case BLOCK_HOUSE_GROUND:
                switch (x)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 9:
                        if (z % 2 == 0)
                        {
                            // 家ステージの足場ブロック（黒）
                            stageObject[y, x, z] = GameObject.Instantiate(blockBlack, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                            stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        }
                        else
                        {
                            // 家ステージの足場ブロック（白）
                            stageObject[y, x, z] = GameObject.Instantiate(blockWhite, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                            stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        }
                        break;
                    case 2:
                    case 4:
                    case 6:
                    case 8:
                    case 10:
                        if (z % 2 == 1)
                        {
                            // 家ステージの足場ブロック（黒）
                            stageObject[y, x, z] = GameObject.Instantiate(blockBlack, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                            stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        }
                        else
                        {
                            // 家ステージの足場ブロック（白）
                            stageObject[y, x, z] = GameObject.Instantiate(blockWhite, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                            stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        }
                        break;
                }
                break;
            // 家ステージの足場ブロック（本棚）
            case BLOCK_HOUSE_BOOK:
                stageObject[y, x, z] = GameObject.Instantiate(blockBook, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                break;

            // 森(夕日)ステージの足場ブロック（１段目）
            case BLOCK_SUN_GRASS_GROUND:
                stageObject[y, x, z] = GameObject.Instantiate(blockSunsetGrassGround, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                break;
            // 森(夕日)ステージの足場ブロック（２段目）
            case BLOCK_SUN_GRASS_1:
                stageObject[y, x, z] = GameObject.Instantiate(blockSunsetGrass1, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                break;
            // 森(夕日)ステージの足場ブロック（３段目）
            case BLOCK_SUN_GRASS_2:
                stageObject[y, x, z] = GameObject.Instantiate(blockSunsetGrass2, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                break;
            // 暗い森ステージの足場ブロック
            case BLOCK_INSECT_GROUND:
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        stageObject[y, x, z] = GameObject.Instantiate(blockInsect1, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                        stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        break;
                    case 1:
                        stageObject[y, x, z] = GameObject.Instantiate(blockInsect2, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                        stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        break;
                    case 2:
                        stageObject[y, x, z] = GameObject.Instantiate(blockInsect3, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                        stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        break;
                }
                break;

            case BLOCK_GARDEN_GROUND:
                stageObject[y, x, z] = GameObject.Instantiate(blockGardenGrass, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                break;
            case BLOCK_GARDEN_FLOWER:
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        stageObject[y, x, z] = GameObject.Instantiate(blockGardenRoseRed, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                        stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        break;
                    case 1:
                        stageObject[y, x, z] = GameObject.Instantiate(blockGardenRoseWhite, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                        stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        break;
                }
                break;
            // スタート地点
            case START:
                stageObject[y, x, z] = GameObject.Instantiate(start, new Vector3(x, y, z), Quaternion.identity) as GameObject;
                break;
            // ゴール地点
            case GOAL:
                stageObject[y, x, z] = GameObject.Instantiate(goal, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                break;
            // 水
            case WATER:
                stageObject[y, x, z] = GameObject.Instantiate(water, new Vector3(x, y, z), Quaternion.identity) as GameObject;
                break;
            // 木
            case TREE1:
                stageObject[y, x, z] = GameObject.Instantiate(tree, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                break;
            // 穴
            case HOLE1:
            case HOLE2:
            case HOLE3:
            case HOLE4:
            case HOLE5:
                stageObject[y, x, z] = GameObject.Instantiate(hole, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                break;

            // 蔦ブロック
            case IVY_BLOCK:
                switch (field)
                {
                    case Field.STAGE_1:
                    case Field.STAGE_3:
                    case Field.STAGE_5:
                        switch (y)
                        {
                            case 0:
                                // 森ステージの足場ブロック（１段目）
                                stageObject[y, x, z] = GameObject.Instantiate(blockGrassGround, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                                break;
                            case 1:
                                // 森ステージの足場ブロック（２段目）
                                stageObject[y, x, z] = GameObject.Instantiate(blockGrass1, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                                break;
                            case 2:
                            case 3:
                            case 4:
                                // 森ステージの足場ブロック（３段目）
                                stageObject[y, x, z] = GameObject.Instantiate(blockGrass2, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                                break;
                        }
                        break;
                    case Field.STAGE_4:
                        switch (UnityEngine.Random.Range(0, 3))
                        {
                            case 0:
                                //stageObject[y, x, z] = GameObject.Instantiate(blockInsect1, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                                break;
                            case 1:
                                //stageObject[y, x, z] = GameObject.Instantiate(blockInsect2, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                                break;
                            case 2:
                                //stageObject[y, x, z] = GameObject.Instantiate(blockInsect3, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                                break;
                        }
                        break;
                }
                break;
            // 梯子
            case LADDER_BLOCK:
                switch (field)
                {
                    case Field.STAGE_2:
                        // 家ステージの足場ブロック（本棚）
                        stageObject[y, x, z] = GameObject.Instantiate(blockBook, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                        stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                        break;
                    case Field.STAGE_5:
                        switch (y)
                        {
                            case 0:
                                // 森ステージの足場ブロック（１段目）
                                stageObject[y, x, z] = GameObject.Instantiate(blockGrassGround, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                                break;
                            case 1:
                                // 森ステージの足場ブロック（２段目）
                                stageObject[y, x, z] = GameObject.Instantiate(blockGrass1, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                                break;
                            case 2:
                            case 3:
                            case 4:
                                // 森ステージの足場ブロック（３段目）
                                stageObject[y, x, z] = GameObject.Instantiate(blockGrass2, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
                                break;
                        }
                        break;
                }
                break;
            // 蔦（FRONT）
            case IVY_FRONT:
                Vector3 ivyFrontPosition = new Vector3(x, y - 0.4f, z + 0.5f);
                stageObject[y, x, z] = GameObject.Instantiate(ivy, ivyFrontPosition, Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            // 蔦（BACK）
            case IVY_BACK:
                Vector3 ivyBackPosition = new Vector3(x, y - 0.4f, z - 0.5f);
                stageObject[y, x, z] = GameObject.Instantiate(ivy, ivyBackPosition, Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            // 蔦（LEFT）
            case IVY_LEFT:
                Vector3 ivyLeftPosition = new Vector3(x - 0.5f, y - 0.4f, z);
                stageObject[y, x, z] = GameObject.Instantiate(ivy, ivyLeftPosition, Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            // 蔦（RIGHT）
            case IVY_RIGHT:
                Vector3 ivyRightPosition = new Vector3(x + 0.5f, y - 0.4f, z);
                stageObject[y, x, z] = GameObject.Instantiate(ivy, ivyRightPosition, Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 270, 0);
                break;
            // 梯子（FRONT）
            case LADDER_FRONT:
                stageObject[y, x, z] = GameObject.Instantiate(ladder, new Vector3(x - 0.9f, y - 0.5f, z + 0.45f), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            // 梯子（BACK）
            case LADDER_BACK:
                stageObject[y, x, z] = GameObject.Instantiate(ladder, new Vector3(x + 0.9f, y - 0.5f, z - 0.45f), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            // 梯子（LEFT）
            case LADDER_LEFT:
                stageObject[y, x, z] = GameObject.Instantiate(ladder, new Vector3(x - 0.45f, y - 0.5f, z - 0.9f), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            // 梯子（RIGHT）
            case LADDER_RIGHT:
                stageObject[y, x, z] = GameObject.Instantiate(ladder, new Vector3(x + 0.45f, y - 0.5f, z + 0.9f), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 270, 0);
                break;
            // 鍵
            case DOOR_RED_KEY:
                stageObject[y, x, z] = GameObject.Instantiate(doorKey, new Vector3(x, y - 0.3f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-45, 0, 0);
                stageObject[y, x, z].GetComponent<Key>().Initialize();
                DoorKey = stageObject[y, x, z].GetComponent<Key>();
                holdKeyColor = DOOR_RED_KEY;
                presenceKeyFlag = true;
                break;
            case DOOR_BLUE_KEY:
                stageObject[y, x, z] = GameObject.Instantiate(doorKey, new Vector3(x, y - 0.3f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-45, 0, 0);
                stageObject[y, x, z].GetComponent<Key>().Initialize();
                DoorKey = stageObject[y, x, z].GetComponent<Key>();
                holdKeyColor = DOOR_BLUE_KEY;
                presenceKeyFlag = true;
                break;
            case DOOR_YELLOW_KEY:
                stageObject[y, x, z] = GameObject.Instantiate(doorKey, new Vector3(x, y - 0.3f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-45, 0, 0);
                stageObject[y, x, z].GetComponent<Key>().Initialize();
                DoorKey = stageObject[y, x, z].GetComponent<Key>();
                holdKeyColor = DOOR_YELLOW_KEY;
                presenceKeyFlag = true;
                break;
            case DOOR_GREEN_KEY:
                stageObject[y, x, z] = GameObject.Instantiate(doorKey, new Vector3(x, y - 0.3f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(-45, 0, 0);
                stageObject[y, x, z].GetComponent<Key>().Initialize();
                DoorKey = stageObject[y, x, z].GetComponent<Key>();
                holdKeyColor = DOOR_GREEN_KEY;
                presenceKeyFlag = true;
                break;
            // 赤扉（FRONT）
            case DOOR_RED_FRONT:
                stageObject[y, x, z] = GameObject.Instantiate(doorRed, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 0, 0);
                DoorRed = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Red = true;
                break;
            // 赤扉（BACK）
            case DOOR_RED_BACK:
                stageObject[y, x, z] = GameObject.Instantiate(doorRed, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 180, 0);
                DoorRed = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Red = true;
                break;
            // 赤扉（LEFT）
            case DOOR_RED_LEFT:
                stageObject[y, x, z] = GameObject.Instantiate(doorRed, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, -90, 0);
                DoorRed = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Red = true;
                break;
            // 赤扉（RIGHT）
            case DOOR_RED_RIGHT:
                stageObject[y, x, z] = GameObject.Instantiate(doorRed, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 90, 0);
                DoorRed = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Red = true;
                break;
            // 青扉（FRONT）
            case DOOR_BLUE_FRONT:
                stageObject[y, x, z] = GameObject.Instantiate(doorBlue, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 0, 0);
                DoorBlue = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Blue = true;
                break;
            // 青扉（BACK）
            case DOOR_BLUE_BACK:
                stageObject[y, x, z] = GameObject.Instantiate(doorBlue, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 180, 0);
                DoorBlue = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Blue = true;
                break;
            // 青扉（LEFT）
            case DOOR_BLUE_LEFT:
                stageObject[y, x, z] = GameObject.Instantiate(doorBlue, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, -90, 0);
                DoorBlue = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Blue = true;
                break;
            // 青扉（RIGHT）
            case DOOR_BLUE_RIGHT:
                stageObject[y, x, z] = GameObject.Instantiate(doorBlue, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 90, 0);
                DoorBlue = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Blue = true;
                break;
            // 黄扉（FRONT）
            case DOOR_YELLOW_FRONT:
                stageObject[y, x, z] = GameObject.Instantiate(doorYellow, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 0, 0);
                DoorYellow = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Yellow = true;
                break;
            // 黄扉（BACK）
            case DOOR_YELLOW_BACK:
                stageObject[y, x, z] = GameObject.Instantiate(doorYellow, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 180, 0);
                DoorYellow = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Yellow = true;
                break;
            // 黄扉（LEFT）
            case DOOR_YELLOW_LEFT:
                stageObject[y, x, z] = GameObject.Instantiate(doorYellow, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, -90, 0);
                DoorYellow = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Yellow = true;
                break;
            // 黄扉（RIGHT）
            case DOOR_YELLOW_RIGHT:
                stageObject[y, x, z] = GameObject.Instantiate(doorYellow, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 90, 0);
                DoorYellow = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Yellow = true;
                break;
            // 緑扉（FRONT）
            case DOOR_GREEN_FRONT:
                stageObject[y, x, z] = GameObject.Instantiate(doorGreen, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 0, 0);
                DoorGreen = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Green = true;
                break;
            // 緑扉（BACK）
            case DOOR_GREEN_BACK:
                stageObject[y, x, z] = GameObject.Instantiate(doorGreen, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 180, 0);
                DoorGreen = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Green = true;
                break;
            // 緑扉（LEFT）
            case DOOR_GREEN_LEFT:
                stageObject[y, x, z] = GameObject.Instantiate(doorGreen, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, -90, 0);
                DoorGreen = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Green = true;
                break;
            // 緑扉（RIGHT）
            case DOOR_GREEN_RIGHT:
                stageObject[y, x, z] = GameObject.Instantiate(doorGreen, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(0, 90, 0);
                DoorGreen = stageObject[y, x, z].GetComponent<Door>();
                presenceDoor_Green = true;
                break;
            // 花１
            case FLOWER1:
                stageObject[y, x, z] = GameObject.Instantiate(flower1, new Vector3(x, y - 0.2f, z), Quaternion.identity) as GameObject;
                break;
            // 花２
            case FLOWER2:
                stageObject[y, x, z] = GameObject.Instantiate(flower2, new Vector3(x, y - 0.2f, z), Quaternion.identity) as GameObject;
                break;
            // 花３
            case FLOWER3:
                stageObject[y, x, z] = GameObject.Instantiate(flower3, new Vector3(x, y - 0.2f, z), Quaternion.identity) as GameObject;
                break;
            // 茨
            case BRAMBLE:
                stageObject[y, x, z] = GameObject.Instantiate(bramble, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                break;
            // チェシャ猫
            case CHESHIRE_CAT:
                stageObject[y, x, z] = GameObject.Instantiate(cheshireCat, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<Cheshire>().Initialize();
                CheshireCat = stageObject[y, x, z].GetComponent<Cheshire>();
                presenceCheshire = true;
                break;
            // キノコ（小さくなる）
            case MUSHROOM_SMALL:
                stageObject[y, x, z] = GameObject.Instantiate(mushroomSmall, new Vector3(x, y - 0.4f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(270.0f, 0, 0);
                break;
            // キノコ（大きくなる）
            case MUSHROOM_BIG:
                stageObject[y, x, z] = GameObject.Instantiate(mushroomBig, new Vector3(x, y - 0.4f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].transform.localEulerAngles = new Vector3(270.0f, 0, 0);
                break;
            // 薬（小さくなる）
            case POTION_SMALL:
                stageObject[y, x, z] = GameObject.Instantiate(potionSmall, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                break;
            // 薬（大きくなる）
            case POTION_BIG:
                stageObject[y, x, z] = GameObject.Instantiate(potionBig, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                break;
            // トランプ兵（その場監視）
            case SOLDIER_HEART:
                stageObject[y, x, z] = GameObject.Instantiate(soldierHeart, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<HeartSoldier>().Initialize(1, x, y, z);
                break;
            // トランプ兵（巡回１）
            case SOLDIER_SPADE1:
                stageObject[y, x, z] = GameObject.Instantiate(soldierSpade, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<SpadeSoldier>().Initialize(1, x, y, z);
               
                break;
            // トランプ兵（巡回２）
            case SOLDIER_SPADE2:
                stageObject[y, x, z] = GameObject.Instantiate(soldierSpade, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                //stageObject[y, x, z].GetComponent<SpadeSoldier>().SetPat();
                break;
            // 双子（左）
            case TWINS_LEFT_FRONT:
                stageObject[y, x, z] = GameObject.Instantiate(twinsLeft, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<TweedleDee>().Initialize(DIRECTION_1, x, y, z);
                presenceDee = true;
                Dee = stageObject[y, x, z].GetComponent<TweedleDee>();
                break;
            // 双子（右）
            case TWINS_RIGHT_FRONT:
                stageObject[y, x, z] = GameObject.Instantiate(twinsRight, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<TweedleDum>().Initialize(DIRECTION_1, x, y, z);
                presenceDum = true;
                Dum = stageObject[y, x, z].GetComponent<TweedleDum>();
                break;

            // 双子（左）
            case TWINS_LEFT_BACK:
                stageObject[y, x, z] = GameObject.Instantiate(twinsLeft, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<TweedleDee>().Initialize(DIRECTION_2, x, y, z);
                presenceDee = true;
                Dee = stageObject[y, x, z].GetComponent<TweedleDee>();
                break;
            // 双子（右）
            case TWINS_RIGHT_BACK:
                stageObject[y, x, z] = GameObject.Instantiate(twinsRight, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<TweedleDum>().Initialize(DIRECTION_2, x, y, z);
                presenceDum = true;
                Dum = stageObject[y, x, z].GetComponent<TweedleDum>();
                break;

            // 双子（左）
            case TWINS_LEFT_LEFT:
                stageObject[y, x, z] = GameObject.Instantiate(twinsLeft, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<TweedleDee>().Initialize(DIRECTION_3, x, y, z);
                presenceDee = true;
                Dee = stageObject[y, x, z].GetComponent<TweedleDee>();
                break;
            // 双子（右）
            case TWINS_RIGHT_LEFT:
                stageObject[y, x, z] = GameObject.Instantiate(twinsRight, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<TweedleDum>().Initialize(DIRECTION_3, x, y, z);
                presenceDum = true;
                Dum = stageObject[y, x, z].GetComponent<TweedleDum>();
                break;

            // 双子（左）
            case TWINS_LEFT_RIGHT:
                stageObject[y, x, z] = GameObject.Instantiate(twinsLeft, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<TweedleDee>().Initialize(DIRECTION_4, x, y, z);
                presenceDee = true;
                Dee = stageObject[y, x, z].GetComponent<TweedleDee>();
                break;
            // 双子（右）
            case TWINS_RIGHT_RIGHT:
                stageObject[y, x, z] = GameObject.Instantiate(twinsRight, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                stageObject[y, x, z].GetComponent<TweedleDum>().Initialize(DIRECTION_4, x, y, z);
                presenceDum = true;
                Dum = stageObject[y, x, z].GetComponent<TweedleDum>();
                break;
            // ハンプティ―
            case EGG:
                stageObject[y, x, z] = GameObject.Instantiate(egg, new Vector3(x, y - 0.5f, z), Quaternion.identity) as GameObject;
                break;
            // それ以外
            default:
                stageObject[y, x, z] = null;
                break;
        }
    }

    // スタート位置の取得///////////////
    public Vector3 getStartPosition()
    {
        Vector3 startPosition = new Vector3(0, 0, 0);

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    if (stage[y, x, z] == START)
                    {
                        startPosition = new Vector3(x, y - 0.5f, z);
                    }
                }
            }
        }
        return startPosition;
    }

    // 配列上のスタート座標Ｘの取得///////
    public int getStartArrayPositionX()
    {
        int arrayPosX = 0;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    if (stage[y, x, z] == START)
                    {
                        arrayPosX = x;
                    }
                }
            }
        }
        return arrayPosX;
    }

    // 配列上のスタート座標Ｙの取得///////
    public int getStartArrayPositionY()
    {
        int arrayPosY = 0;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    if (stage[y, x, z] == START)
                    {
                        arrayPosY = y;
                    }
                }
            }
        }
        return arrayPosY;
    }

    // 配列上のスタート座標Ｚの取得///////
    public int getStartArrayPositionZ()
    {
        int arrayPosZ = 0;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    if (stage[y, x, z] == START)
                    {
                        arrayPosZ = z;
                    }
                }
            }
        }
        return arrayPosZ;
    }
    // 選択されたステージを設定
    public void SelectStage(int stageNum)
    {
        switch (stageNum)
        {
            case 0: StageDebug(); break;


            case 1: Stage1(); break;    // ステージ１を設定
            case 2: Stage2(); break;    // ステージ２を設定
            case 3: Stage3(); break;    // ステージ３を設定
            case 4: Stage4(); break;    // ステージ４を設定
            case 5: Stage5(); break;    // ステージ５を設定
            case 6: Stage6(); break;    // ステージ６を設定
            case 7: Stage7(); break;    // ステージ７を設定
            case 8: Stage8(); break;    // ステージ８を設定
            case 9: Stage9(); break;    // ステージ９を設定
            case 10: Stage10(); break;  // ステージ１０を設定
            case 11: Stage11(); break;  // ステージ１１を設定
            case 12: Stage12(); break;  // ステージ１２を設定
            case 13: Stage13(); break;  // ステージ１３を設定
            case 14: Stage14(); break;  // ステージ１４を設定
            case 15: Stage15(); break;  // ステージ１５を設定
            case 16: Stage16(); break;  // ステージ１６を設定
            case 17: Stage17(); break;  // ステージ１７を設定
            case 18: Stage18(); break;  // ステージ１８を設定
            case 19: Stage19(); break;  // ステージ１９を設定
            case 20: Stage20(); break;  // ステージ２０を設定
            case 21: Stage21(); break;  // ステージ２１を設定
            case 22: Stage22(); break;  // ステージ２２を設定
            case 23: Stage23(); break;  // ステージ２３を設定
            case 24: Stage24(); break;  // ステージ２４を設定
            case 25: Stage25(); break;  // ステージ２５を設定
        }
    }


    // ステージ１を設定
    public void Stage1()
    {
        Field stage1Field = Field.STAGE_1;
        int stage1TurnCount = 4;

        int[, ,] stage1 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 11,  0, 11,  0, 11,  0,  0,  0},
                {  0,  0,  0,  0, 11,  0, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 11, 11, 11,  0,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11, 11,  0,  0,  0},
                {  0,  0,  0,  0,  0, 11, 11, 11,  0,  0,  0},
                {  0,  0,  0,  0, 11,  0, 11,  0,  0,  0,  0},
                {  0,  0,  0, 11,  0, 11,  0, 11,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, -1,  0,  0,  0, -2,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage1Field;
        turnCount = stage1TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage1[y, x, z];
                }
            }
        }
    }
    // ステージ２を設定
    public void Stage2()
    {
        Field stage1Field = Field.STAGE_1;
        int stage2TurnCount = 6;

        int[, ,] stage2 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, -1,  0, 74,  0,  0,  0,  0},
                {  0,  0,  0,  0, 65, 12, 70,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, -2,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage1Field;
        turnCount = stage2TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage2[y, x, z];
                }
            }
        }
    }
    // ステージ３を設定
    public void Stage3()
    {
        Field stage1Field = Field.STAGE_1;
        int stage3TurnCount = 7;

        int[, ,] stage3 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, -1,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 12, 70, 72,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, -2,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage1Field;
        turnCount = stage3TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage3[y, x, z];
                }
            }
        }
    }
    // ステージ４を設定
    public void Stage4()
    {
        Field stage1Field = Field.STAGE_1;
        int stage4TurnCount = 6;

        int[, ,] stage4 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, -1,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 12,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 71, 70, 65,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 65,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, -2,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage1Field;
        turnCount = stage4TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage4[y, x, z];
                }
            }
        }
    }
    // ステージ５を設定
    public void Stage5()
    {
        Field stage1Field = Field.STAGE_1;
        int stage5TurnCount = 10;

        int[, ,] stage5 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 11, 11, 11,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 11,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, -1,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 12, 65, 12,  0,  0,  0,  0},
                {  0,  0,  0,  0, 12, 12, 12,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 12,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 13,  0,  0,  0,  0},
                {  0,  0,  0,  0, 71, 70, 13,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 13,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, -2,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage1Field;
        turnCount = stage5TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage5[y, x, z];
                }
            }
        }
    }
    // ステージ６を設定
    public void Stage6()
    {
        Field stage2Field = Field.STAGE_2;
        int stage6TurnCount = 8;

        int[, ,] stage6 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, 21, 21, 21,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 21,  0, 21,  0,  0,  0,  0},
                {  0,  0,  0,  0, 21, 21, 21, 21,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, -1,  0, 79,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 75,  0, -2,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage2Field;
        turnCount = stage6TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage6[y, x, z];
                }
            }
        }
    }
    // ステージ７を設定
    public void Stage7()
    {
        Field stage2Field = Field.STAGE_2;
        int stage7TurnCount = 10;

        int[, ,] stage7 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 21, 21, 21, 21,  0,  0,  0},
                {  0,  0,  0,  0, 21,  0,  0, 21,  0,  0,  0},
                {  0,  0, 21, 21, 21,  0,  0, 21,  0,  0,  0},
                {  0,  0,  0,  0, 21,  0,  0, 21,  0,  0,  0},
                {  0,  0,  0,  0, 21, 21, 21, 21,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目(扉付き)
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 82,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, -1,  0, 80,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0, -2,  0,  0,  0},
                {  0,  0,  0,  0,  0, 86,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage2Field;
        turnCount = stage7TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage7[y, x, z];
                }
            }
        }
    }
    // ステージ８を設定
    public void Stage8()
    {
        Field stage2Field = Field.STAGE_2;
        int stage8TurnCount = 14;

        int[, ,] stage8 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 21, 21, 21, 21, 21,  0,  0,  0},
                {  0,  0,  0,  0,  0, 21,  0, 21,  0,  0,  0},
                {  0,  0,  0,  0,  0, 21,  0, 21,  0,  0,  0},
                {  0,  0,  0,  0,  0, 21,  0, 21,  0,  0,  0},
                {  0,  0,  0, 21, 21, 21, 21, 21,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, -1,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0, 65,  0,  0,  0},
                {  0,  0,  0,  0,  0, 22,  0, 22,  0,  0,  0},
                {  0,  0,  0, -2,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 22,  0, 22,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage2Field;
        turnCount = stage8TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage8[y, x, z];
                }
            }
        }
    }
    // ステージ９を設定
    public void Stage9()
    {
        Field stage2Field = Field.STAGE_2;
        int stage9TurnCount = 15;

        int[, ,] stage9 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, 21, 21, 21, 21, 21, 21,  0,  0,  0},
                {  0,  0, 21,  0, 21,  0, 21, 21,  0,  0,  0},
                {  0,  0, 21, 21, 21,  0, 21, 21,  0,  0,  0},
                {  0,  0, 21,  0, 21,  0, 21, 21,  0,  0,  0},
                {  0,  0, 21, 21, 21, 21, 21, 21,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, 76, 75, 21, 21, 21,  0,  0,  0,  0},
                {  0,  0,  0,  0, 22,  0, 21,  0,  0,  0,  0},
                {  0,  0, -1,  0, 65,  0, 21,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 21,  0,  0,  0,  0},
                {  0,  0, 76, 75, 76, 75, 21, -2,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage2Field;
        turnCount = stage9TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage9[y, x, z];
                }
            }
        }
    }
    // ステージ１０を設定
    public void Stage10()
    {
        Field stage2Field = Field.STAGE_2;
        int stage10TurnCount = 16;

        int[, ,] stage10 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 21, 21, 21, 21, 21, 21,  0,  0},
                {  0,  0,  0, 21,  0,  0, 21,  0,  0,  0,  0},
                {  0, 21, 21, 21, 21, 21, 21, 21, 21,  0,  0},
                {  0,  0,  0,  0, 21,  0, 21,  0,  0,  0,  0},
                {  0,  0,  0,  0, 21, 21, 21, 21, 21,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 76, 75, 65, 76, 75, -2,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0, -1,  0,  0, 65, 65,  0, 21,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 76, 75,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage2Field;
        turnCount = stage10TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage10[y, x, z];
                }
            }
        }
    }

    // ステージ１１を設定
    public void Stage11()
    {
        Field stage3Field = Field.STAGE_3;
        int stage11TurnCount = 6;

        int[, ,] stage11 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 12,102, 12,  0,  0,  0,  0},
                {  0,  0,  0,  0, 12, 12, 12,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, -1,  0, 74,  0,  0,  0,  0},
                {  0,  0,  0,  0, 13,101, 70,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, -2,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage3Field;
        turnCount = stage11TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage11[y, x, z];
                }
            }
        }
    }
    // ステージ１２を設定
    public void Stage12()
    {
        Field stage3Field = Field.STAGE_3;
        int stage12TurnCount = 8;

        int[, ,] stage12 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11,  0, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 12, 12,100,  0,  0,  0,  0},
                {  0,  0,  0,  0, 12,  0, 12,  0,  0,  0,  0},
                {  0,  0,  0,  0,101, 12, 12,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 12,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, -1,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 74,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 71, 70,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 13,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, -2,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage3Field;

        turnCount = stage12TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage12[y, x, z];
                }
            }
        }
    }
    // ステージ１３を設定
    public void Stage13()
    {
        Field stage3Field = Field.STAGE_3;
        int stage13TurnCount = 10;

        int[, ,] stage13 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, 11, 11, 11,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11,  0, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11,  0, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, 12, 12, 12,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,100,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,102,  0, 74,  0,  0,  0,  0},
                {  0,  0,  0,  0,101,  0, 70,  0,  0,  0,  0},
                {  0,  0,  0,  0, 12, 12, 12,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, -1,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 65, -2,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage3Field;
        turnCount = stage13TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage13[y, x, z];
                }
            }
        }
    }
    // ステージ１４を設定
    public void Stage14()
    {
        Field stage3Field = Field.STAGE_3;
        int stage14TurnCount = 12;

        int[, ,] stage14 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11,  0,  0,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11, 11,  0,  0,  0},
                {  0,  0,  0, 11, 11,  0,  0, 11,  0,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11, 11,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, -1,  0, 65,  0,  0,  0,  0,  0},
                {  0,  0,  0,100,  0, 65, 65, 12,  0,  0,  0},
                {  0,  0,  0,100,  0,  0,  0, 12,  0,  0,  0},
                {  0,  0,  0, 65, 12, 12, 12,102,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 13, 13, 72,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, -2,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage3Field;
        turnCount = stage14TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage14[y, x, z];
                }
            }
        }
    }
    // ステージ１５を設定
    public void Stage15()
    {
        Field stage3Field = Field.STAGE_3;
        int stage15TurnCount = 11;

        int[, ,] stage15 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11, 11, 11,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11, 11, 11,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11, 11, 11,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11, 11, 11,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11, 11, 11,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11, 11, 11,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,100,101,100,100, 12, 12,  0,  0},
                {  0,  0,  0,100,101,101,102,100, 12,  0,  0},
                {  0,  0,  0,100,100,101,101,102,101,  0,  0},
                {  0,  0,  0,100,100,102,101,100,102,  0,  0},
                {  0,  0,  0, 12,101,102,100,101,102,  0,  0},
                {  0,  0,  0, 12, 12,102,100,101,100,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0, -2,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, -1,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage3Field;
        turnCount = stage15TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage15[y, x, z];
                }
            }
        }
    }
    // ステージ１６を設定
    public void Stage16()
    {
        Field stage4Field = Field.STAGE_4;
        int stage16TurnCount = 5;

        int[, ,] stage16 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0, 60, 41, 41,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, 41, 60, 41,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0, -2,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, -1,  0,106,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage4Field;
        turnCount = stage16TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage16[y, x, z];
                }
            }
        }
    }
    // ステージ１７を設定
    public void Stage17()
    {
        Field stage4Field = Field.STAGE_4;
        int stage17TurnCount = 14;

        int[, ,] stage17 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 41, 41, 41, 41,  0,  0, 0,  0},
                {  0,  0,  0, 41,  2,  2, 41,  0,  0,  0,  0},
                {  0,  0,  0, 41,  2,  2,  41, 0,  0,  0,  0},
                {  0,  0, 41, 41, 41, 41, 41, 41,  0,  0,  0},
                {  0,  0,  0, 41,  2, 2,  41,  0,  0,  0,  0},
                {  0,  0,  0, 41,  2,  2, 41,  0,  0,  0,  0},
                {  0,  0,  0, 41, 41, 41, 41,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  114,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  -1,  0,  0,  65,  0,  -2,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  65,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage4Field;
        turnCount = stage17TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage17[y, x, z];
                }
            }
        }
    }
    // ステージ１８を設定
    public void Stage18()
    {
        Field stage4Field = Field.STAGE_4;
        int stage18TurnCount = 12;

        int[, ,] stage18 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0, 41, 41,  0,  0,  0,  0, 61, 41,  0,  0},
                {  0, 64,  0,  0,  0,  0,  0,  0, 60,  0,  0},
                {  0,  0,  0, 41, 41, 60, 41,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0, 64,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0, 62, 41,  0,  0},
                {  0,  0,  0,  0,  0, 63,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 41, 61, 41,  0,  0,  0},
                {  0, 41,  0,  0,  0, 41, 41, 62,  0,  0,  0},
                {  0, 41, 63,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,106,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, -1,  0,  0,106,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,106,  0,  0,  0,  0,  0},
                {  0, -2,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage4Field;
        turnCount = stage18TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage18[y, x, z];
                }
            }
        }
    }
    // ステージ１９を設定
    public void Stage19()
    {
        Field stage4Field = Field.STAGE_4;
        int stage19TurnCount = 9;

        int[, ,] stage19 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  60,  0,  0, 61, 41, 41,  0,  0},
                {  0,  41, 41, 41, 61, 0,  0,  0,  41,  0,  0},
                {  0,  0,  0,  62,  0,  0, 62, 41, 41,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  41,  41,  0},
                {  0,  0,  0,  0,  0,  0, 60,  0,  41,  0,  0},
                {  0,  0,  0,  0,  0,  0, 41,  0,  41,  0,  0},
                {  0,  0,  0,  0,  0,  0, 41, 41,  41,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0, 65,  0,  0},
                {  0,  -1, 0,106,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0, 65 , 0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0, -2,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0, 65,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage4Field;
        turnCount = stage19TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage19[y, x, z];
                }
            }
        }
    }
    // ステージ２０を設定
    public void Stage20()
    {
        Field stage4Field = Field.STAGE_4;
        int stage20TurnCount = 24;

        int[, ,] stage20 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 41,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 41, 41, 41, 41,  0,  0,  0,  0},
                {  0,  0, 41, 41,  2,  2, 41, 41, 41, 60,  0},
                {  0,  0,  0, 41,  2,  2, 41,  0,  0,  0,  0},
                {  0,  0,  0, 41, 41, 41, 41,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0, 60, 41, 41, 41, 41,  0,  0,  0,  0,  0},
                {  0,  0, 41,  2,  2, 41, 41, 41,  0,  0,  0},
                {  0, 41, 41, 41, 41, 41,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0, -1,  0,  0,  0,  0,  0,106,  0,  0},
                {  0,  0,  0,  0,  0,  0,113,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 65,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,114,  0, -2,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage4Field;
        turnCount = stage20TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage20[y, x, z];
                }
            }
        }
    }
    // ステージ２１を設定
    public void Stage21()
    {
        Field stage5Field = Field.STAGE_5;
        int stage21TurnCount = 14;

        int[, ,] stage21 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 51,  0,  0, 51, 51, 51,  0},
                {  0, 51, 51, 51, 51, 51, 51, 51,  0, 51,  0},
                {  0,  0,  0,  0,  0,  0,  0, 51, 51, 51,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0, 51,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,110,  0,  0,  0,  0,  0,  0},
                {  0, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,111,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0, -2,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage5Field;
        turnCount = stage21TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage21[y, x, z];
                }
            }
        }
    }
    // ステージ２２を設定
    public void Stage22()
    {
        Field stage5Field = Field.STAGE_5;
        int stage22TurnCount = 7;

        int[, ,] stage22 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 51,  0,  0,  0,  0,  0},
                {  0,  0, 51, 51, 51, 51, 51, 51, 51,  0,  0},
                {  0,  0,  0,  0,  0,  0, 51,  0, 51,  0,  0},
                {  0,  0,  0,  0,  0,  0, 51, 51, 51,  0,  0},
                {  0,  0,  0,  0,  0,  0, 51,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,110,  0,  0,  0,  0,  0},
                {  0,  0, -1,  0,105,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,111,  0,  0},
                {  0,  0,  0,  0,  0,  0, -2,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage5Field;
        turnCount = stage22TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage22[y, x, z];
                }
            }
        }
    }
    // ステージ２３を設定
    public void Stage23()
    {
        Field stage5Field = Field.STAGE_5;
        int stage23TurnCount = 14;

        int[, ,] stage23 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 51,  0, 51, 51, 51,  0,  0,  0},
                {  0, 51, 51, 51, 51, 51, 51, 51, 51,  0,  0},
                {  0,  0,  0, 51,  0, 51, 51, 51,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,110,  0,111,  0,  0,  0,  0,  0},
                {  0, -1,  0,  0,  0,  0, 52,  0, -2,  0,  0},
                {  0,  0,  0,110,  0,  0,  0,111,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage5Field;
        turnCount = stage23TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage23[y, x, z];
                }
            }
        }
    }
    // ステージ２４を設定
    public void Stage24()
    {
        Field stage5Field = Field.STAGE_5;
        int stage24TurnCount = 15;

        int[, ,] stage24 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 51, 51, 51, 51, 51, 51,  0,  0},
                {  0,  0,  0, 51, 51, 51, 51, 51, 51,  0,  0},
                {  0,  0,  0, 51, 51, 51, 51, 51, 51,  0,  0},
                {  0,  0,  0, 51, 51, 51, 51, 51, 51,  0,  0},
                {  0,  0,  0, 51, 51, 51, 51, 51, 51,  0,  0},
                {  0,  0,  0, 51, 51, 51, 51, 51, 51,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 52, 52,  0, 52, 52, 52,  0,  0},
                {  0,  0,  0, 52, 52, 80,  0, 52, 52,  0,  0},
                {  0,  0,  0, 52, 52, 52, 52,  0, 52,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,112, 52,  0,  0},
                {  0,  0,  0, 112,  0, 0, 0,   0, 52,  0,  0},
                {  0,  0,  0,  0,  0,  0, 85, 71, 52,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,110,  0,  0, 81, -2,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0, 89,  0,  0},
                {  0,  0,  0,  0, -1,  0,110,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage5Field;
        turnCount = stage24TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage24[y, x, z];
                }
            }
        }
    }
    // ステージ２５を設定
    public void Stage25()
    {
        Field stage1Field = Field.STAGE_5;
        int stage25TurnCount = 30;

        int[, ,] stage25 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0, 11, 11, 11,100,101,102, 11, 11, 11,  0},
                {  0, 11,  2, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11,101, 11, 11, 60, 11, 11,  0},
                {  0, 11, 11, 11,100, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,113,  0,  0,109,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, -1,  0,  0,108, 12, 12,  0,  0},
                {  0,  0,  0,  0,  0, 11,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 11,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0, 71, 70, 72,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0, 73,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0, 12,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0, 60,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage1Field;
        turnCount = stage25TurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage25[y, x, z];
                }
            }
        }
    }

    //デバッグステージ
    public void StageDebug()
    {
        Field stage1Field = Field.STAGE_1;
        int stageTurnCount = 99;

        int[, ,] stage20 = new int[,,]
        {
            {
                // １段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0, 11, 11, 11, 11, 11, 11, 11, 11, 11,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ２段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 11,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 11, 11, 11, 11,  0,  0,  0,  0},
                {  0,  -1, 0,  0,  0, 0,   0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  SOLDIER_HEART,  0, 11,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0, 11,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ３段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ４段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
            {
                // ５段目
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
            },
        };

        field = stage1Field;
        turnCount = stageTurnCount;

        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    stage[y, x, z] = stage20[y, x, z];
                }
            }
        }
    }

    public void SettingStageGimmick(int stageNum)
    {
        switch (stageNum)
        {
            case 1: Stage1Gimmick(); break;     // ステージ１のギミックを設定
            case 2: Stage2Gimmick(); break;     // ステージ２のギミックを設定
            case 3: Stage3Gimmick(); break;     // ステージ３のギミックを設定
            case 4: Stage4Gimmick(); break;     // ステージ４のギミックを設定
            case 5: Stage5Gimmick(); break;     // ステージ５のギミックを設定
            case 6: Stage6Gimmick(); break;     // ステージ６のギミックを設定
            case 7: Stage7Gimmick(); break;     // ステージ７のギミックを設定
            case 8: Stage8Gimmick(); break;     // ステージ８のギミックを設定
            case 9: Stage9Gimmick(); break;     // ステージ９のギミックを設定
            case 10: Stage10Gimmick(); break;   // ステージ１０のギミックを設定
            case 11: Stage11Gimmick(); break;   // ステージ１１のギミックを設定
            case 12: Stage12Gimmick(); break;   // ステージ１２のギミックを設定
            case 13: Stage13Gimmick(); break;   // ステージ１３のギミックを設定
            case 14: Stage14Gimmick(); break;   // ステージ１４のギミックを設定
            case 15: Stage15Gimmick(); break;   // ステージ１５のギミックを設定
            case 16: Stage16Gimmick(); break;   // ステージ１６のギミックを設定
            case 17: Stage17Gimmick(); break;   // ステージ１７のギミックを設定
            case 18: Stage18Gimmick(); break;   // ステージ１８のギミックを設定
            case 19: Stage19Gimmick(); break;   // ステージ１９のギミックを設定
            case 20: Stage20Gimmick(); break;   // ステージ２０のギミックを設定
            case 21: Stage21Gimmick(); break;   // ステージ２１のギミックを設定
            case 22: Stage22Gimmick(); break;   // ステージ２２のギミックを設定
            case 23: Stage23Gimmick(); break;   // ステージ２３のギミックを設定
            case 24: Stage24Gimmick(); break;   // ステージ２４のギミックを設定
            case 25: Stage25Gimmick(); break;   // ステージ２５のギミックを設定

            case 0: StageDebugGimmick(); break;   // ステージ２５のギミックを設定

        }
    }
    // ステージのギミックを設定///
    public void StageDebugGimmick()
    {
        //stageObject[1, 9, 8].GetComponent<Tree>().SetStartActionTurn(2);
    }

    // ステージのギミックを設定///
    public void Stage1Gimmick()
    {
        //stageObject[1, 7, 3].GetComponent<Tree>().SetStartActionTurn(0);
        //stageObject[1, 7, 4].GetComponent<Tree>().SetStartActionTurn(4);
        //stageObject[1, 7, 5].GetComponent<Tree>().SetStartActionTurn(3);
        //stageObject[1, 7, 6].GetComponent<Tree>().SetStartActionTurn(2);
        //stageObject[1, 7, 7].GetComponent<Tree>().SetStartActionTurn(1);
    }
    public void Stage2Gimmick()
    {
        //高さ　縦　横
        stageObject[1, 5, 4].GetComponent<Tree>().SetStartActionTurn(2);
        stageObject[1, 4, 6].GetComponent<Ivy>().SetStartActionTurn(2);
    }
    public void Stage3Gimmick()
    {
        stageObject[1, 6, 6].GetComponent<Ivy>().SetStartActionTurn(15);
    }
    public void Stage4Gimmick()
    {
        //高さ　縦　横
        stageObject[1, 5, 6].GetComponent<Tree>().SetStartActionTurn(3);
        stageObject[1, 6, 5].GetComponent<Tree>().SetStartActionTurn(1);
        stageObject[1, 5, 4].GetComponent<Ivy>().SetStartActionTurn(2);
    }
    public void Stage5Gimmick()
    {
        //高さ　縦　横
        stageObject[1, 4, 5].GetComponent<Tree>().SetStartActionTurn(3);
        stageObject[2, 5, 4].GetComponent<Ivy>().SetStartActionTurn(6);
    }
    public void Stage6Gimmick()
    {
        //高さ　縦　横
    }
    public void Stage7Gimmick()
    {

    }  //ステージ追加
    public void Stage8Gimmick()
    {
        //高さ　縦　横
        stageObject[1, 5, 7].GetComponent<Tree>().SetStartActionTurn(6);
    }
    public void Stage9Gimmick()
    {

    }
    public void Stage10Gimmick()
    {
        stageObject[1, 5, 4].GetComponent<Tree>().SetStartActionTurn(2);
        stageObject[1, 5, 5].GetComponent<Tree>().SetStartActionTurn(1);
        stageObject[1, 3, 5].GetComponent<Tree>().SetStartActionTurn(4);
        stageObject[1, 3, 3].GetComponent<Ladder>().SetStartActionTurn(10);
        stageObject[1, 3, 6].GetComponent<Ladder>().SetStartActionTurn(15);
    }
    public void Stage11Gimmick()
    {

    }
    public void Stage12Gimmick()
    {
        //高さ　縦　横
        stageObject[2, 4, 6].GetComponent<Ivy>().SetStartActionTurn(10);
        stageObject[2, 5, 5].GetComponent<Ivy>().SetStartActionTurn(3);
    }
    public void Stage13Gimmick()
    {
        //高さ　縦　横
        stageObject[2, 7, 5].GetComponent<Tree>().SetStartActionTurn(5);

        stageObject[1, 5, 6].GetComponent<Ivy>().SetStartActionTurn(15);
    }
    public void Stage14Gimmick()
    {

        //木1
        //高さ　縦　横
        stageObject[1, 6, 3].GetComponent<Tree>().SetStartActionTurn(0);
        //木2
        //高さ　縦　横
        stageObject[1, 3, 5].GetComponent<Tree>().SetStartActionTurn(1);
        //木3
        //高さ　縦　横
        stageObject[1, 4, 5].GetComponent<Tree>().SetStartActionTurn(2);
        //木4
        //高さ　縦　横
        stageObject[1, 4, 6].GetComponent<Tree>().SetStartActionTurn(3);


        stageObject[2, 6, 6].GetComponent<Ivy>().SetStartActionTurn(15);

    }
    public void Stage15Gimmick()
    {


    }
    public void Stage16Gimmick()
    {
        //高さ　縦　横
    }
    public void Stage17Gimmick()
    {
        stageObject[1, 8, 3].GetComponent<Tree>().SetStartActionTurn(2);
    }
    public void Stage18Gimmick()
    {

    }
    public void Stage19Gimmick()
    {
        stageObject[1, 7, 8].GetComponent<Tree>().SetStartActionTurn(6);
        stageObject[1, 2, 8].GetComponent<Tree>().SetStartActionTurn(5);
    }
    public void Stage20Gimmick()
    {
        stageObject[1, 7, 4].GetComponent<Tree>().SetStartActionTurn(13);
    }
    public void Stage21Gimmick()
    {
        //stageObject[1, 6, 7].GetComponent<SpadeSoldier>().SetData(4, 2, true, 1, 6, 7);

        //stageObject[1, 4, 4].GetComponent<HeartSoldier>().SetData(4, 1, 4, 4);
    }
    public void Stage22Gimmick()
    {
        //stageObject[1, 6, 8].GetComponent<SpadeSoldier>().SetData(3, 2, true, 1, 6, 8);

        //stageObject[1, 3, 5].GetComponent<HeartSoldier>().SetData(3, 1, 3, 5);
    }
    public void Stage23Gimmick()
    {
        //stageObject[1, 6, 7].GetComponent<SpadeSoldier>().SetData(3, 2, true, 1, 6, 7);
        //stageObject[1, 4, 5].GetComponent<SpadeSoldier>().SetData(1, 2, true, 1, 4, 5);

        //stageObject[1, 4, 3].GetComponent<HeartSoldier>().SetData(4, 1, 4, 3);
        //stageObject[1, 6, 3].GetComponent<HeartSoldier>().SetData(2, 1, 6, 3);

    }
    public void Stage24Gimmick()
    {
        //stageObject[1, 6, 3].GetComponent<SpadeSoldier>().SetData(1, 4, false, 1, 6, 3);
        //stageObject[1, 5, 7].GetComponent<SpadeSoldier>().SetData(3, 4, false, 1, 5, 7);

        //stageObject[2, 2, 4].GetComponent<HeartSoldier>().SetData(4, 2, 2, 4);
        //stageObject[2, 4, 6].GetComponent<HeartSoldier>().SetData(2, 2, 4, 6);


        stageObject[1, 7, 7].GetComponent<Ivy>().SetStartActionTurn(30);

    }
    public void Stage25Gimmick()
    {
        //高さ　縦　横
        stageObject[1, 8, 4].GetComponent<Ivy>().SetStartActionTurn(30);
        stageObject[1, 8, 6].GetComponent<Ivy>().SetStartActionTurn(30);
        stageObject[1, 9, 5].GetComponent<Ivy>().SetStartActionTurn(30);
    }

    // 前移動可能判定/////////////////////////////////////
    public bool MoveFrontPossibleDecision(Player alice)
    {
        bool flag1;     // 前方向用フラグ
        bool flag2;     // 前下方向用フラグ

        // アリスがステージ外に来ている場合
        if ((alice.arrayPosY == 0) ||
            (alice.arrayPosX == 0) || (alice.arrayPosX == 10) ||
            (alice.arrayPosZ == 0) || (alice.arrayPosZ == 10))
        {
            return false;   // 移動できない
        }
        else
        {
            flag1 = FrontDecision(alice);       // 前方向判定
            flag2 = FrontDownDecision(alice);   // 前下方向判定

            if ((flag1 == true) && (flag2 == true))
            {
                return true;    // 移動できる
            }
            else
            {
                return false;   // 移動できない
            }
        }
    }


    // 後移動可能判定////////////////////////////////////
    public bool MoveBackPossibleDecision(Player alice)
    {
        bool flag1;     // 後方向用フラグ
        bool flag2;     // 後下方向用フラグ

        // アリスがステージ外に来ている場合
        if ((alice.arrayPosY == 0) ||
            (alice.arrayPosX == 0) || (alice.arrayPosX == 10) ||
            (alice.arrayPosZ == 0) || (alice.arrayPosZ == 10))
        {
            return false;   // 移動できない
        }
        else
        {
            flag1 = BackDecision(alice);       // 後方向判定
            flag2 = BackDownDecision(alice);   // 後下方向判定

            if ((flag1 == true) && (flag2 == true))
            {
                return true;    // 移動できる
            }
            else
            {
                return false;   // 移動できない
            }
        }
    }

    // 左移動可能判定////////////////////////////////////
    public bool MoveLeftPossibleDecision(Player alice)
    {
        bool flag1;     // 左方向用フラグ
        bool flag2;     // 左下方向用フラグ

        // アリスがステージ外に来ている場合
        if ((alice.arrayPosY == 0) ||
            (alice.arrayPosX == 0) || (alice.arrayPosX == 10) ||
            (alice.arrayPosZ == 0) || (alice.arrayPosZ == 10))
        {
            return false;   // 移動できない
        }
        else
        {
            flag1 = LeftDecision(alice);       // 左方向判定
            flag2 = LeftDownDecision(alice);   // 左下方向判定

            if ((flag1 == true) && (flag2 == true))
            {
                return true;    // 移動できる
            }
            else
            {
                return false;   // 移動できない
            }
        }
    }

    // 右移動可能判定/////////////////////////////////////
    public bool MoveRightPossibleDecision(Player alice)
    {
        bool flag1;     // 右方向用フラグ
        bool flag2;     // 右下方向用フラグ

        // アリスがステージ外に来ている場合
        if ((alice.arrayPosY == 0) ||
            (alice.arrayPosX == 0) || (alice.arrayPosX == 10) ||
            (alice.arrayPosZ == 0) || (alice.arrayPosZ == 10))
        {
            return false;   // 移動できない
        }
        else
        {
            flag1 = RightDecision(alice);       // 右方向判定
            flag2 = RightDownDecision(alice);   // 右下方向判定

            if ((flag1 == true) && (flag2 == true))
            {
                return true;    // 移動できる
            }
            else
            {
                return false;   // 移動できない
            }
        }
    }

    // 前方向判定//////////////////////
    bool FrontDecision(Player alice)
    {
        bool flag = false;

        int posX = alice.arrayPosX;                     // 配列上の座標Ｘ
        int posY = alice.arrayPosY;                     // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;                     // 配列上の座標Ｚ
        Player.PlayerAngle angle = alice.playerAngle;   // 向き

        switch (angle)
        {
            case Player.PlayerAngle.FRONT: flag = BesideDecision(stage[posY, posX, posZ + 1], posX, posY, posZ + 1, alice); break;
            case Player.PlayerAngle.BACK: flag = BesideDecision(stage[posY, posX, posZ - 1], posX, posY, posZ - 1, alice); break;
            case Player.PlayerAngle.LEFT: flag = BesideDecision(stage[posY, posX - 1, posZ], posX - 1, posY, posZ, alice); break;
            case Player.PlayerAngle.RIGHT: flag = BesideDecision(stage[posY, posX + 1, posZ], posX + 1, posY, posZ, alice); break;
        }

        return flag;
    }

    // 後方向判定/////////////////////
    bool BackDecision(Player alice)
    {
        bool flag = false;

        int posX = alice.arrayPosX;                     // 配列上の座標Ｘ
        int posY = alice.arrayPosY;                     // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;                     // 配列上の座標Ｚ
        Player.PlayerAngle angle = alice.playerAngle;   // 向き

        switch (angle)
        {
            case Player.PlayerAngle.FRONT: flag = BesideDecision(stage[posY, posX, posZ - 1], posX, posY, posZ - 1, alice); break;
            case Player.PlayerAngle.BACK: flag = BesideDecision(stage[posY, posX, posZ + 1], posX, posY, posZ + 1, alice); break;
            case Player.PlayerAngle.LEFT: flag = BesideDecision(stage[posY, posX + 1, posZ], posX + 1, posY, posZ, alice); break;
            case Player.PlayerAngle.RIGHT: flag = BesideDecision(stage[posY, posX - 1, posZ], posX - 1, posY, posZ, alice); break;
        }

        return flag;
    }

    // 左方向判定/////////////////////
    bool LeftDecision(Player alice)
    {
        bool flag = false;

        int posX = alice.arrayPosX;                     // 配列上の座標Ｘ
        int posY = alice.arrayPosY;                     // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;                     // 配列上の座標Ｚ
        Player.PlayerAngle angle = alice.playerAngle;   // 向き

        switch (angle)
        {
            case Player.PlayerAngle.FRONT: flag = BesideDecision(stage[posY, posX - 1, posZ], posX - 1, posY, posZ, alice); break;
            case Player.PlayerAngle.BACK: flag = BesideDecision(stage[posY, posX + 1, posZ], posX + 1, posY, posZ, alice); break;
            case Player.PlayerAngle.LEFT: flag = BesideDecision(stage[posY, posX, posZ - 1], posX, posY, posZ - 1, alice); break;
            case Player.PlayerAngle.RIGHT: flag = BesideDecision(stage[posY, posX, posZ + 1], posX, posY, posZ + 1, alice); break;
        }

        return flag;
    }

    // 右方向判定//////////////////////
    bool RightDecision(Player alice)
    {
        bool flag = false;

        int posX = alice.arrayPosX;                     // 配列上の座標Ｘ
        int posY = alice.arrayPosY;                     // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;                     // 配列上の座標Ｚ
        Player.PlayerAngle angle = alice.playerAngle;   // 向き

        switch (angle)
        {
            case Player.PlayerAngle.FRONT: flag = BesideDecision(stage[posY, posX + 1, posZ], posX + 1, posY, posZ, alice); break;
            case Player.PlayerAngle.BACK: flag = BesideDecision(stage[posY, posX - 1, posZ], posX - 1, posY, posZ, alice); break;
            case Player.PlayerAngle.LEFT: flag = BesideDecision(stage[posY, posX, posZ + 1], posX, posY, posZ + 1, alice); break;
            case Player.PlayerAngle.RIGHT: flag = BesideDecision(stage[posY, posX, posZ - 1], posX, posY, posZ - 1, alice); break;
        }

        return flag;
    }

    // 前下方向判定////////////////////////
    bool FrontDownDecision(Player alice)
    {
        bool flag = false;

        int posX = alice.arrayPosX;                     // 配列上の座標Ｘ
        int posY = alice.arrayPosY;                     // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;                     // 配列上の座標Ｚ
        Player.PlayerAngle angle = alice.playerAngle;   // 向き

        switch (angle)
        {
            case Player.PlayerAngle.FRONT: flag = BesideDownDecision(stage[posY - 1, posX, posZ + 1], posX, posY - 1, posZ + 1); break;
            case Player.PlayerAngle.BACK: flag = BesideDownDecision(stage[posY - 1, posX, posZ - 1], posX, posY - 1, posZ - 1); break;
            case Player.PlayerAngle.LEFT: flag = BesideDownDecision(stage[posY - 1, posX - 1, posZ], posX - 1, posY - 1, posZ); break;
            case Player.PlayerAngle.RIGHT: flag = BesideDownDecision(stage[posY - 1, posX + 1, posZ], posX + 1, posY - 1, posZ); break;
        }

        return flag;
    }

    // 後下方向判定///////////////////////
    bool BackDownDecision(Player alice)
    {
        bool flag = false;

        int posX = alice.arrayPosX;                     // 配列上の座標Ｘ
        int posY = alice.arrayPosY;                     // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;                     // 配列上の座標Ｚ
        Player.PlayerAngle angle = alice.playerAngle;   // 向き

        switch (angle)
        {
            case Player.PlayerAngle.FRONT: flag = BesideDownDecision(stage[posY - 1, posX, posZ - 1], posX, posY - 1, posZ - 1); break;
            case Player.PlayerAngle.BACK: flag = BesideDownDecision(stage[posY - 1, posX, posZ + 1], posX, posY - 1, posZ + 1); break;
            case Player.PlayerAngle.LEFT: flag = BesideDownDecision(stage[posY - 1, posX + 1, posZ], posX + 1, posY - 1, posZ); break;
            case Player.PlayerAngle.RIGHT: flag = BesideDownDecision(stage[posY - 1, posX - 1, posZ], posX - 1, posY - 1, posZ); break;
        }

        return flag;
    }

    // 左下方向判定///////////////////////
    bool LeftDownDecision(Player alice)
    {
        bool flag = false;

        int posX = alice.arrayPosX;                     // 配列上の座標Ｘ
        int posY = alice.arrayPosY;                     // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;                     // 配列上の座標Ｚ
        Player.PlayerAngle angle = alice.playerAngle;   // 向き

        switch (angle)
        {
            case Player.PlayerAngle.FRONT: flag = BesideDownDecision(stage[posY - 1, posX - 1, posZ], posX - 1, posY - 1, posZ); break;
            case Player.PlayerAngle.BACK: flag = BesideDownDecision(stage[posY - 1, posX + 1, posZ], posX + 1, posY - 1, posZ); break;
            case Player.PlayerAngle.LEFT: flag = BesideDownDecision(stage[posY - 1, posX, posZ - 1], posX, posY - 1, posZ - 1); break;
            case Player.PlayerAngle.RIGHT: flag = BesideDownDecision(stage[posY - 1, posX, posZ + 1], posX, posY - 1, posZ + 1); break;
        }

        return flag;
    }

    // 右下方向判定////////////////////////
    bool RightDownDecision(Player alice)
    {
        bool flag = false;

        int posX = alice.arrayPosX;                     // 配列上の座標Ｘ
        int posY = alice.arrayPosY;                     // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;                     // 配列上の座標Ｚ
        Player.PlayerAngle angle = alice.playerAngle;   // 向き

        switch (angle)
        {
            case Player.PlayerAngle.FRONT: flag = BesideDownDecision(stage[posY - 1, posX + 1, posZ], posX + 1, posY - 1, posZ); break;
            case Player.PlayerAngle.BACK: flag = BesideDownDecision(stage[posY - 1, posX - 1, posZ], posX - 1, posY - 1, posZ); break;
            case Player.PlayerAngle.LEFT: flag = BesideDownDecision(stage[posY - 1, posX, posZ + 1], posX, posY - 1, posZ + 1); break;
            case Player.PlayerAngle.RIGHT: flag = BesideDownDecision(stage[posY - 1, posX, posZ - 1], posX, posY - 1, posZ - 1); break;
        }

        return flag;
    }

    // 横判定////////////////////////////////////////////////////////
    bool BesideDecision(int gimmick, int posX, int posY, int posZ, Player alice)
    {
        bool flag = false;

        switch (gimmick)
        {
            case NONE:      // 何も無し
            case BLOCK:     // ブロック
            case START:     // スタート地点
            case GOAL:      // ゴール地点
            case MUSHROOM_SMALL:
            case MUSHROOM_BIG:
            case POTION_SMALL:
            case POTION_BIG:
            case BRAMBLE:
            //ドラゴン参照
            case CHESHIRE_CAT: // チェシャ
            case DOOR_RED_KEY: // 鍵
            case DOOR_BLUE_KEY: // 鍵
            case DOOR_YELLOW_KEY: // 鍵
            case DOOR_GREEN_KEY: // 鍵
            //

            case IVY_FRONT:
            case IVY_BACK:
            case IVY_LEFT:
            case IVY_RIGHT:

            case LADDER_FRONT:
            case LADDER_BACK:
            case LADDER_LEFT:
            case LADDER_RIGHT:
            case SOLDIER_SPADE1:
            case SOLDIER_SPADE2:

            case TWINS_LEFT_FRONT:
            case TWINS_LEFT_BACK:
            case TWINS_LEFT_LEFT:
            case TWINS_LEFT_RIGHT:
            case TWINS_RIGHT_FRONT:
            case TWINS_RIGHT_BACK:
            case TWINS_RIGHT_LEFT:
            case TWINS_RIGHT_RIGHT:
                //case IVY_BLOCK:         // 蔦ブロック
                //case LADDER_BLOCK:      // 梯子ブロック
                flag = true;
                break;
            case BLOCK_GRASS_GROUND:    // 森ステージの足場ブロック（１段目）
            case BLOCK_GRASS_1:         // 森ステージの足場ブロック（２段目）
            case BLOCK_GRASS_2:         // 森ステージの足場ブロック（３段目）
            case BLOCK_HOUSE_GROUND:    // 家ステージの足場ブロック（１段目）
            case BLOCK_INSECT_GROUND:   // 暗い森ステージの足場ブロック（１段目）
            case BLOCK_SUN_GRASS_GROUND:    // 森ステージの足場ブロック（１段目）
            case BLOCK_SUN_GRASS_1:         // 森ステージの足場ブロック（２段目）
            case BLOCK_SUN_GRASS_2:         // 森ステージの足場ブロック（３段目）

            case BLOCK_GARDEN_GROUND:
            case BLOCK_GARDEN_FLOWER:


            case WATER:                 // 水
            case SOLDIER_HEART:

                flag = false;
                break;
            case TREE1:
                if (stageObject[posY, posX, posZ].GetComponent<Tree>().movePossibleFlag == true) { flag = true; }
                else { flag = false; }
                break;

            //ドラゴン参照
            case DOOR_RED_FRONT:
            case DOOR_RED_BACK:
            case DOOR_RED_LEFT:
            case DOOR_RED_RIGHT:
                if (DoorRed.trapFlag)
                    flag = true;
                break;
            case DOOR_BLUE_FRONT:
            case DOOR_BLUE_BACK:
            case DOOR_BLUE_LEFT:
            case DOOR_BLUE_RIGHT:
                if (DoorBlue.trapFlag)
                    flag = true;
                break;
            case DOOR_YELLOW_FRONT:
            case DOOR_YELLOW_BACK:
            case DOOR_YELLOW_LEFT:
            case DOOR_YELLOW_RIGHT:
                if (DoorYellow.trapFlag)
                    flag = true;
                break;
            case DOOR_GREEN_FRONT:
            case DOOR_GREEN_BACK:
            case DOOR_GREEN_LEFT:
            case DOOR_GREEN_RIGHT:
                if (DoorGreen.trapFlag)
                    flag = true;
                break;

            //
            case IVY_BLOCK:         // 蔦ブロック
            case LADDER_BLOCK:      // 梯子ブロック

                if ((stage[alice.arrayPosY, alice.arrayPosX, alice.arrayPosZ] == IVY_FRONT) ||
                    (stage[alice.arrayPosY, alice.arrayPosX, alice.arrayPosZ] == IVY_BLOCK) ||
                    (stage[alice.arrayPosY, alice.arrayPosX, alice.arrayPosZ] == IVY_LEFT) ||
                    (stage[alice.arrayPosY, alice.arrayPosX, alice.arrayPosZ] == IVY_RIGHT))
                {
                    if (stageObject[alice.arrayPosY, alice.arrayPosX, alice.arrayPosZ].GetComponent<Ivy>().movePossibleFlag == true)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                }

                break;
        }

        if (presenceDum)
        {
            if ((posX == Dum.arrayPosX) &&
                (posY == Dum.arrayPosY) &&
                (posZ == Dum.arrayPosZ))
            {
                flag = false;
            }
        }

        if (presenceDee)
        {
            if ((posX == Dee.arrayPosX) &&
                (posY == Dee.arrayPosY) &&
                (posZ == Dee.arrayPosZ))
            {
                flag = false;
            }
        }


        return flag;
    }

    // 横下判定//////////////////////////////////////////////////////////
    bool BesideDownDecision(int gimmick, int posX, int posY, int posZ)
    {
        bool flag = false;

        switch (gimmick)
        {
            case NONE:              // 何も無し
            case BLOCK:             // ブロック
            case BLOCK_GRASS_GROUND:    // 森ステージの足場ブロック（１段目）
            case BLOCK_GRASS_1:         // 森ステージの足場ブロック（２段目）
            case BLOCK_GRASS_2:         // 森ステージの足場ブロック（３段目）
            case BLOCK_HOUSE_GROUND:    // 家ステージの足場ブロック（黒）
            case BLOCK_HOUSE_BOOK:    // 家ステージの足場ブロック（黒）
            case BLOCK_SUN_GRASS_GROUND:    // 森ステージの足場ブロック（１段目）
            case BLOCK_SUN_GRASS_1:         // 森ステージの足場ブロック（２段目）
            case BLOCK_SUN_GRASS_2:         // 森ステージの足場ブロック（３段目）
            case BLOCK_INSECT_GROUND:   // 暗い森ステージの足場ブロック（１段目）
            case BLOCK_GARDEN_GROUND:
            case BLOCK_GARDEN_FLOWER:
            case MUSHROOM_SMALL:
            case MUSHROOM_BIG:
            case POTION_SMALL:
            case POTION_BIG:
            case START:             // スタート地点
            case GOAL:              // ゴール地点
            case WATER:             // 水
            case HOLE1:
            case HOLE2:
            case HOLE3:
            case HOLE4:
            case HOLE5:
            case IVY_BLOCK:
            case IVY_FRONT:
            case IVY_BACK:
            case IVY_LEFT:
            case IVY_RIGHT:
            case LADDER_BLOCK:
            case LADDER_FRONT:
            case LADDER_BACK:
            case LADDER_LEFT:
            case LADDER_RIGHT:

            //ドラゴン参照
            case CHESHIRE_CAT: // チェシャ
            case DOOR_RED_KEY: // 鍵
            case DOOR_BLUE_KEY: // 鍵
            case DOOR_YELLOW_KEY: // 鍵
            case DOOR_GREEN_KEY: // 鍵
                // 扉
                //
                flag = true;
                break;
            // 木
            case TREE1:
                if (stageObject[posY, posX, posZ].GetComponent<Tree>().growCount <= 2) { flag = true; }
                else { flag = false; }
                break;
            // 花
            case FLOWER1:
                if (stageObject[posY, posX, posZ].GetComponentInChildren<Flower1>().movePossibleFlag == true) { flag = true; }
                else { flag = false; }
                break;
            case FLOWER2:
                if (stageObject[posY, posX, posZ].GetComponentInChildren<Flower2>().movePossibleFlag == true) { flag = true; }
                else { flag = false; }
                break;
            case FLOWER3:
                if (stageObject[posY, posX, posZ].GetComponentInChildren<Flower3>().movePossibleFlag == true) { flag = true; }
                else { flag = false; }
                break;
        }

        return flag;
    }

    // ギミックとの判定
    public void GimmickDecision(Player alice)
    {
        int posX = alice.arrayPosX;       // 配列上の座標Ｘ
        int posY = alice.arrayPosY;       // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;       // 配列上の座標Ｚ

        invisibleFlag = false;
        getKeyFlag = false;


        switch (stage[posY, posX, posZ])
        {
            // 何も無し
            case NONE:
                break;
            // ゴール地点
            case GOAL:
                // ゴール処理を書く
                print("ゴール");

                switch (PlayerPrefs.GetInt("STAGE_NUM"))
                {
                    case 1: PlayerPrefs.SetInt("STAMP_NUM", 2); break;
                    case 2: PlayerPrefs.SetInt("STAMP_NUM", 3); break;
                    case 3: PlayerPrefs.SetInt("STAMP_NUM", 4); break;
                    case 4: PlayerPrefs.SetInt("STAMP_NUM", 5); break;
                    case 5: PlayerPrefs.SetInt("STAMP_NUM", 6); break;
                    case 6: PlayerPrefs.SetInt("STAMP_NUM", 9); break;
                    case 7: PlayerPrefs.SetInt("STAMP_NUM", 10); break;
                    case 8: PlayerPrefs.SetInt("STAMP_NUM", 11); break;
                    case 9: PlayerPrefs.SetInt("STAMP_NUM", 12); break;
                    case 10: PlayerPrefs.SetInt("STAMP_NUM", 13); break;
                    case 11: PlayerPrefs.SetInt("STAMP_NUM", 16); break;
                    case 12: PlayerPrefs.SetInt("STAMP_NUM", 17); break;
                    case 13: PlayerPrefs.SetInt("STAMP_NUM", 18); break;
                    case 14: PlayerPrefs.SetInt("STAMP_NUM", 19); break;
                    case 15: PlayerPrefs.SetInt("STAMP_NUM", 20); break;
                    case 16: PlayerPrefs.SetInt("STAMP_NUM", 23); break;
                    case 17: PlayerPrefs.SetInt("STAMP_NUM", 24); break;
                    case 18: PlayerPrefs.SetInt("STAMP_NUM", 25); break;
                    case 19: PlayerPrefs.SetInt("STAMP_NUM", 26); break;
                    case 20: PlayerPrefs.SetInt("STAMP_NUM", 27); break;
                    case 21: PlayerPrefs.SetInt("STAMP_NUM", 30); break;
                    case 22: PlayerPrefs.SetInt("STAMP_NUM", 31); break;
                    case 23: PlayerPrefs.SetInt("STAMP_NUM", 32); break;
                    case 24: PlayerPrefs.SetInt("STAMP_NUM", 33); break;
                }

                CameraFade.StartAlphaFade(Color.black, false, 1.0f, 0.5f, () => { Application.LoadLevel("StageSelectScene"); });
                break;
            // 木
            case TREE1:
                if (stageObject[posY, posX, posZ].GetComponent<Tree>().growCount == 2)
                {
                    alice.AutoMoveSetting(Player.MoveDirection.UP);
                }
                break;
            // 蔦（FRONT）
            case IVY_FRONT:
                if (stageObject[posY, posX, posZ].GetComponent<Ivy>().movePossibleFlag == true)
                {
                    alice.climbFrontFlag = true;
                    alice.playerAngle = Player.PlayerAngle.FRONT;
                    alice.transform.localEulerAngles = alice.angleFront;
                    alice.SetClimbPosition(Player.PlayerAngle.FRONT);   // 登り状態の座標を設定
                    alice.SetAnimation(Player.Motion.CLIMB1);           // 登るアニメーションをセット
                }
                else
                {
                    alice.ResetAnimation(Player.Motion.CLIMB1);
                }
                break;

            case LADDER_FRONT:
                if (stageObject[posY, posX, posZ].GetComponent<Ladder>().movePossibleFlag == true)
                {
                    alice.climbFrontFlag = true;
                    alice.playerAngle = Player.PlayerAngle.FRONT;
                    alice.transform.localEulerAngles = alice.angleFront;
                    alice.SetClimbPosition(Player.PlayerAngle.FRONT);   // 登り状態の座標を設定
                    alice.SetAnimation(Player.Motion.CLIMB1);           // 登るアニメーションをセット
                }
                else
                {
                    alice.ResetAnimation(Player.Motion.CLIMB1);
                }
                break;

            // 蔦（BACK）
            case IVY_BACK:
                if (stageObject[posY, posX, posZ].GetComponent<Ivy>().movePossibleFlag == true)
                {
                    alice.climbBackFlag = true;
                    alice.playerAngle = Player.PlayerAngle.BACK;
                    alice.transform.localEulerAngles = alice.angleBack;
                    alice.SetClimbPosition(Player.PlayerAngle.BACK);    // 登り状態の座標を設定
                    alice.SetAnimation(Player.Motion.CLIMB1);           // 登るアニメーションをセット
                }
                else
                {
                    alice.ResetAnimation(Player.Motion.CLIMB1);
                }
                break;

            case LADDER_BACK:
                if (stageObject[posY, posX, posZ].GetComponent<Ladder>().movePossibleFlag == true)
                {
                    alice.climbBackFlag = true;
                    alice.playerAngle = Player.PlayerAngle.BACK;
                    alice.transform.localEulerAngles = alice.angleBack;
                    alice.SetClimbPosition(Player.PlayerAngle.BACK);    // 登り状態の座標を設定
                    alice.SetAnimation(Player.Motion.CLIMB1);           // 登るアニメーションをセット
                }
                else
                {
                    alice.ResetAnimation(Player.Motion.CLIMB1);
                }
                break;

            // 蔦（LEFT）
            case IVY_LEFT:
                if (stageObject[posY, posX, posZ].GetComponent<Ivy>().movePossibleFlag == true)
                {
                    alice.climbLeftFlag = true;
                    alice.playerAngle = Player.PlayerAngle.LEFT;
                    alice.transform.localEulerAngles = alice.angleLeft;
                    alice.SetClimbPosition(Player.PlayerAngle.LEFT);    // 登り状態の座標を設定
                    alice.SetAnimation(Player.Motion.CLIMB1);           // 登るアニメーションをセット
                }
                else
                {
                    alice.ResetAnimation(Player.Motion.CLIMB1);
                }
                break;

            case LADDER_LEFT:
                if (stageObject[posY, posX, posZ].GetComponent<Ladder>().movePossibleFlag == true)
                {
                    alice.climbLeftFlag = true;
                    alice.playerAngle = Player.PlayerAngle.LEFT;
                    alice.transform.localEulerAngles = alice.angleLeft;
                    alice.SetClimbPosition(Player.PlayerAngle.LEFT);    // 登り状態の座標を設定
                    alice.SetAnimation(Player.Motion.CLIMB1);           // 登るアニメーションをセット
                }
                else
                {
                    alice.ResetAnimation(Player.Motion.CLIMB1);
                }
                break;

            // 蔦（RIGHT）
            case IVY_RIGHT:
                if (stageObject[posY, posX, posZ].GetComponent<Ivy>().movePossibleFlag == true)
                {
                    alice.climbRightFlag = true;
                    alice.playerAngle = Player.PlayerAngle.RIGHT;
                    alice.transform.localEulerAngles = alice.angleRight;
                    alice.SetClimbPosition(Player.PlayerAngle.RIGHT);   // 登り状態の座標を設定
                    alice.SetAnimation(Player.Motion.CLIMB1);           // 登るアニメーションをセット
                }
                else
                {
                    alice.ResetAnimation(Player.Motion.CLIMB1);
                }
                break;

            case LADDER_RIGHT:
                if (stageObject[posY, posX, posZ].GetComponent<Ladder>().movePossibleFlag == true)
                {
                    alice.climbRightFlag = true;
                    alice.playerAngle = Player.PlayerAngle.RIGHT;
                    alice.transform.localEulerAngles = alice.angleRight;
                    alice.SetClimbPosition(Player.PlayerAngle.RIGHT);   // 登り状態の座標を設定
                    alice.SetAnimation(Player.Motion.CLIMB1);           // 登るアニメーションをセット
                }
                else
                {
                    alice.ResetAnimation(Player.Motion.CLIMB1);
                }
                break;

            // 鍵
            case DOOR_RED_KEY:
            case DOOR_BLUE_KEY:
            case DOOR_YELLOW_KEY:
            case DOOR_GREEN_KEY:
                getKeyFlag = true;
                break;
            // 茨
            case BRAMBLE:
                stageObject[posY, posX, posZ].GetComponent<Bramble>().trapFlag = true;
                alice.gameOverFlag = true;
                break;
            // キノコ（小さくなる）
            case MUSHROOM_SMALL:
                // ギミックフラグが真なら
                if (stageObject[posY, posX, posZ].GetComponent<MushroomSmall>().GetGimmickFlag() == true)
                {
                    alice.mode = Player.Mode.SMALL;                                                         // プレイヤーの状態を小さいに
                    alice.modeSmallFlag = true;                                                             // 小さくなったら切り替えるフラグを真に
                    alice.modeSmallCount = 3;                                                               // 小さい状態のターン数に３を設定
                    alice.modeSmallTurnCount = 0;                                                           // 小さい状態になってからのターン数に０を設定
                    alice.ModeChange();                                                                     // 状態の変化を反映
                    stageObject[posY, posX, posZ].GetComponent<MushroomSmall>().gimmickDrawFlag = false;    // 描画フラグを偽に
                    stageObject[posY, posX, posZ].GetComponent<MushroomSmall>().SetGimmickFlag(false);      // ギミックフラグを偽に
                }
                break;
            // キノコ（大きくなる）
            case MUSHROOM_BIG:
                // ギミックフラグが真なら
                if (stageObject[posY, posX, posZ].GetComponent<MushroomBig>().GetGimmickFlag() == true)
                {
                    alice.mode = Player.Mode.BIG;                                                           // プレイヤーの状態を大きいに
                    alice.modeBigFlag = true;                                                               // 大きくなったら切り替えるフラグを真に
                    alice.modeBigCount = 3;                                                                 // 大きい状態のターン数に３を設定
                    alice.modeBigTurnCount = 0;                                                             // 大きい状態になってからのターン数に０を設定
                    alice.ModeChange();                                                                     // 状態の変化を反映
                    stageObject[posY, posX, posZ].GetComponent<MushroomBig>().gimmickDrawFlag = false;      // 描画フラグを偽に
                    stageObject[posY, posX, posZ].GetComponent<MushroomBig>().SetGimmickFlag(false);        // ギミックフラグを偽に
                }
                break;
            // 薬（小さくなる）
            case POTION_SMALL:
                // ギミックフラグが真なら
                if (stageObject[posY, posX, posZ].GetComponent<PotionSmall>().GetGimmickFlag() == true)
                {
                    alice.mode = Player.Mode.SMALL;                                                         // プレイヤーの状態を小さいに
                    alice.modeSmallFlag = true;                                                             // 小さくなったら切り替えるフラグを真に
                    alice.modeSmallCount = 3;                                                               // 小さい状態のターン数に３を設定
                    alice.modeSmallTurnCount = 0;                                                           // 小さい状態になってからのターいン数に０を設定
                    alice.ModeChange();                                                                     // 状態の変化を反映
                    stageObject[posY, posX, posZ].GetComponent<PotionSmall>().gimmickDrawFlag = false;      // 描画フラグを偽に
                    stageObject[posY, posX, posZ].GetComponent<PotionSmall>().SetGimmickFlag(false);        // ギミックフラグを偽に
                }
                break;
            // 薬（大きくなる）
            case POTION_BIG:
                // ギミックフラグが真なら
                if (stageObject[posY, posX, posZ].GetComponent<PotionBig>().GetGimmickFlag() == true)
                {
                    alice.mode = Player.Mode.BIG;                                                           // プレイヤーの状態を大きいに
                    alice.modeBigFlag = true;                                                               // 大きくなったら切り替えるフラグを真に
                    alice.modeBigCount = 3;                                                                 // 大きい状態のターン数に３を設定
                    alice.modeBigTurnCount = 0;                                                             // 大きい状態になってからのターン数に０を設定
                    alice.ModeChange();                                                                     // 状態の変化を反映
                    stageObject[posY, posX, posZ].GetComponent<PotionBig>().gimmickDrawFlag = false;        // 描画フラグを偽に
                    stageObject[posY, posX, posZ].GetComponent<PotionBig>().SetGimmickFlag(false);          // ギミックフラグを偽に
                }
                break;
            // チェシャ猫
            case CHESHIRE_CAT:
                invisibleFlag = true;
                break;
        }

        if (invisibleFlag)
            alice.GetComponent<Player>().TouchCheshire();

        if (getKeyFlag)
            alice.GetComponent<Player>().TouchKey();

        //
    }

    // 足元の判定
    public void FootDecision(Player alice)
    {
        int posX = alice.arrayPosX;       // 配列上の座標Ｘ
        int posY = alice.arrayPosY;       // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;       // 配列上の座標Ｚ

        // アリスが地面についていないなら
        if (alice.arrayPosY >= 1)
        {
            switch (stage[posY - 1, posX, posZ])
            {
                case NONE:              // 何も無い
                case START:             // スタート
                case GOAL:              // ゴール
                case WATER:             // 水
                case MUSHROOM_SMALL:    // キノコ（小さくなる）
                case MUSHROOM_BIG:      // キノコ（大きくなる）
                case POTION_SMALL:      // 薬（小さくなる）
                case POTION_BIG:        // 薬（大きくなる）
                case DOOR_RED_KEY:
                case DOOR_BLUE_KEY:
                case DOOR_YELLOW_KEY:
                case DOOR_GREEN_KEY:
                    alice.AutoMoveSetting(Player.MoveDirection.DOWN);   // 自動下移動を設定
                    alice.SetAnimation(Player.Motion.DROP);             // 進む時の落下アニメーションをセット
                    break;
                case BLOCK_GRASS_GROUND:    // 森ステージの足場ブロック（１段目）
                case BLOCK_GRASS_1:         // 森ステージの足場ブロック（２段目）
                case BLOCK_GRASS_2:         // 森ステージの足場ブロック（３段目）
                case BLOCK_HOUSE_GROUND:    // 家ステージの足場ブロック（黒）
                case BLOCK_INSECT_GROUND:   // 暗い森ステージの足場ブロック（１段目）
                    alice.ResetAnimation(Player.Motion.DROP);           // 進む時の落下アニメーションをリセット
                    break;
                // 穴
                case HOLE1:
                case HOLE2:
                case HOLE3:
                case HOLE4:
                case HOLE5:
                    for (int y = 0; y < STAGE_Y; y++)
                    {
                        for (int x = 0; x < STAGE_X; x++)
                        {
                            for (int z = 0; z < STAGE_Z; z++)
                            {
                                switch (stage[posY - 1, posX, posZ])
                                {
                                    case HOLE1:
                                        if (((alice.mode == Player.Mode.NORMAL) && (alice.modeSmallTurnCount == 3)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 2)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 1)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 0)))
                                        {
                                            if (stage[y, x, z] == HOLE1 && ((y != posY - 1) || (x != posX) || (z != posZ)))
                                            {
                                                alice.transform.position = new Vector3(x, y + 0.5f, z);
                                                alice.arrayPosX = x;
                                                alice.arrayPosY = y + 1;
                                                alice.arrayPosZ = z;
                                                break;
                                            }
                                        }

                                        break;
                                    case HOLE2:
                                        if (((alice.mode == Player.Mode.NORMAL) && (alice.modeSmallTurnCount == 3)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 2)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 1)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 0)))
                                        {
                                            if (stage[y, x, z] == HOLE2 && ((y != posY - 1) || (x != posX) || (z != posZ)))
                                            {
                                                alice.transform.position = new Vector3(x, y + 0.5f, z);
                                                alice.arrayPosX = x;
                                                alice.arrayPosY = y + 1;
                                                alice.arrayPosZ = z;
                                                break;
                                            }
                                        }
                                        break;
                                    case HOLE3:
                                        if (((alice.mode == Player.Mode.NORMAL) && (alice.modeSmallTurnCount == 3)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 2)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 1)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 0)))
                                        {
                                            if (stage[y, x, z] == HOLE3 && ((y != posY - 1) || (x != posX) || (z != posZ)))
                                            {
                                                alice.transform.position = new Vector3(x, y + 0.5f, z);
                                                alice.arrayPosX = x;
                                                alice.arrayPosY = y + 1;
                                                alice.arrayPosZ = z;
                                                break;
                                            }
                                        }
                                        break;
                                    case HOLE4:
                                        if (((alice.mode == Player.Mode.NORMAL) && (alice.modeSmallTurnCount == 3)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 2)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 1)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 0)))
                                        {
                                            if (stage[y, x, z] == HOLE4 && ((y != posY - 1) || (x != posX) || (z != posZ)))
                                            {
                                                alice.transform.position = new Vector3(x, y + 0.5f, z);
                                                alice.arrayPosX = x;
                                                alice.arrayPosY = y + 1;
                                                alice.arrayPosZ = z;
                                                break;
                                            }
                                        }
                                        break;
                                    case HOLE5:
                                        if (((alice.mode == Player.Mode.NORMAL) && (alice.modeSmallTurnCount == 3)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 2)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 1)) ||
                                           ((alice.mode == Player.Mode.SMALL) && (alice.modeSmallTurnCount == 0)))
                                        {
                                            if (stage[y, x, z] == HOLE5 && ((y != posY - 1) || (x != posX) || (z != posZ)))
                                            {
                                                alice.transform.position = new Vector3(x, y + 0.5f, z);
                                                alice.arrayPosX = x;
                                                alice.arrayPosY = y + 1;
                                                alice.arrayPosZ = z;
                                                break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    break;
                //  木
                case TREE1:
                    // 木の成長段階が１以下なら
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Tree>().growCount <= 1)
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);
                        alice.AnimationDropNext(true);
                    }
                    // 木の成長段階が１以下なら
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Tree>().growCount == 3)
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.UP);
                    }
                    break;

                case IVY_BLOCK:
                case LADDER_BLOCK:
                    alice.ResetAnimation(Player.Motion.CLIMB2);
                    break;

                // 蔦（FRONT）
                case IVY_FRONT:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Ivy>().movePossibleFlag == true)
                    {
                        alice.climbFront2Flag = true;
                        alice.playerAngle = Player.PlayerAngle.FRONT;
                        alice.transform.localEulerAngles = alice.angleFront;

                        alice.climbCount = 2;                               // 登りアニメーションの段階を２に
                        alice.SetClimbPosition(Player.PlayerAngle.FRONT);   // 登り状態の座標を設定
                        alice.transform.position += new Vector3(0, -0.5f, 0);
                        alice.SetAnimation(Player.Motion.CLIMB2);
                    }
                    else if(alice.saveMoveDirection[alice.saveCount - 1] == Player.MoveDirection.UP)
                    {
                        alice.climbDropFlag = true;
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);
                        alice.SetAnimation(Player.Motion.DROP);
                    }
                    else
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);   // 自動下移動を設定
                        alice.SetAnimation(Player.Motion.DROP);             // 進む時の落下アニメーションをセット
                    }
                    break;

                case LADDER_FRONT:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Ladder>().movePossibleFlag == true)
                    {
                        alice.climbFront2Flag = true;
                        alice.playerAngle = Player.PlayerAngle.FRONT;
                        alice.transform.localEulerAngles = alice.angleFront;

                        alice.climbCount = 2;                               // 登りアニメーションの段階を２に
                        alice.SetClimbPosition(Player.PlayerAngle.FRONT);   // 登り状態の座標を設定
                        alice.transform.position += new Vector3(0, -0.5f, 0);
                        alice.SetAnimation(Player.Motion.CLIMB2);
                    }
                    else if (alice.saveMoveDirection[alice.saveCount - 1] == Player.MoveDirection.UP)
                    {
                        alice.climbDropFlag = true;
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);
                        alice.SetAnimation(Player.Motion.DROP);
                    }
                    else
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);   // 自動下移動を設定
                        alice.SetAnimation(Player.Motion.DROP);             // 進む時の落下アニメーションをセット
                    }
                    break;

                // 蔦（BACK）
                case IVY_BACK:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Ivy>().movePossibleFlag == true)
                    {
                        alice.climbBack2Flag = true;
                        alice.playerAngle = Player.PlayerAngle.BACK;
                        alice.transform.localEulerAngles = alice.angleBack;

                        alice.climbCount = 2;                               // 登りアニメーションの段階を２に
                        alice.SetClimbPosition(Player.PlayerAngle.BACK);   // 登り状態の座標を設定
                        alice.transform.position += new Vector3(0, -0.5f, 0);
                        alice.SetAnimation(Player.Motion.CLIMB2);
                    }
                    else if (alice.saveMoveDirection[alice.saveCount - 1] == Player.MoveDirection.UP)
                    {
                        alice.climbDropFlag = true;
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);
                        alice.SetAnimation(Player.Motion.DROP);
                    }
                    else
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);   // 自動下移動を設定
                        alice.SetAnimation(Player.Motion.DROP);             // 進む時の落下アニメーションをセット
                    }
                    break;

                case LADDER_BACK:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Ladder>().movePossibleFlag == true)
                    {
                        alice.climbBack2Flag = true;
                        alice.playerAngle = Player.PlayerAngle.BACK;
                        alice.transform.localEulerAngles = alice.angleBack;

                        alice.climbCount = 2;                               // 登りアニメーションの段階を２に
                        alice.SetClimbPosition(Player.PlayerAngle.BACK);   // 登り状態の座標を設定
                        alice.transform.position += new Vector3(0, -0.5f, 0);
                        alice.SetAnimation(Player.Motion.CLIMB2);
                    }
                    else if (alice.saveMoveDirection[alice.saveCount - 1] == Player.MoveDirection.UP)
                    {
                        alice.climbDropFlag = true;
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);
                        alice.SetAnimation(Player.Motion.DROP);
                    }
                    else
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);   // 自動下移動を設定
                        alice.SetAnimation(Player.Motion.DROP);             // 進む時の落下アニメーションをセット
                    }
                    break;

                // 蔦（LEFT）
                case IVY_LEFT:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Ivy>().movePossibleFlag == true)
                    {
                        alice.climbLeft2Flag = true;
                        alice.playerAngle = Player.PlayerAngle.LEFT;
                        alice.transform.localEulerAngles = alice.angleLeft;

                        alice.climbCount = 2;                               // 登りアニメーションの段階を２に
                        alice.SetClimbPosition(Player.PlayerAngle.LEFT);   // 登り状態の座標を設定
                        alice.transform.position += new Vector3(0, -0.5f, 0);
                        alice.SetAnimation(Player.Motion.CLIMB2);
                    }
                    else if (alice.saveMoveDirection[alice.saveCount - 1] == Player.MoveDirection.UP)
                    {
                        alice.climbDropFlag = true;
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);
                        alice.SetAnimation(Player.Motion.DROP);
                    }
                    else
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);   // 自動下移動を設定
                        alice.SetAnimation(Player.Motion.DROP);             // 進む時の落下アニメーションをセット
                    }
                    break;

                case LADDER_LEFT:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Ladder>().movePossibleFlag == true)
                    {
                        alice.climbLeft2Flag = true;
                        alice.playerAngle = Player.PlayerAngle.LEFT;
                        alice.transform.localEulerAngles = alice.angleLeft;

                        alice.climbCount = 2;                               // 登りアニメーションの段階を２に
                        alice.SetClimbPosition(Player.PlayerAngle.LEFT);   // 登り状態の座標を設定
                        alice.transform.position += new Vector3(0, -0.5f, 0);
                        alice.SetAnimation(Player.Motion.CLIMB2);
                    }
                    else if (alice.saveMoveDirection[alice.saveCount - 1] == Player.MoveDirection.UP)
                    {
                        alice.climbDropFlag = true;
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);
                        alice.SetAnimation(Player.Motion.DROP);
                    }
                    else
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);   // 自動下移動を設定
                        alice.SetAnimation(Player.Motion.DROP);             // 進む時の落下アニメーションをセット
                    }
                    break;

                // 蔦（RIGHT）
                case IVY_RIGHT:

                    if (stageObject[posY - 1, posX, posZ].GetComponent<Ivy>().movePossibleFlag == true)
                    {
                        alice.climbRight2Flag = true;
                        alice.playerAngle = Player.PlayerAngle.RIGHT;
                        alice.transform.localEulerAngles = alice.angleRight;

                        alice.climbCount = 2;                               // 登りアニメーションの段階を２に
                        alice.SetClimbPosition(Player.PlayerAngle.RIGHT);   // 登り状態の座標を設定
                        alice.transform.position += new Vector3(0, -0.5f, 0);
                        alice.SetAnimation(Player.Motion.CLIMB2);
                    }
                    else if (alice.saveMoveDirection[alice.saveCount - 1] == Player.MoveDirection.UP)
                    {
                        alice.climbDropFlag = true;
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);
                        alice.SetAnimation(Player.Motion.DROP);
                    }
                    else
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);   // 自動下移動を設定
                        alice.SetAnimation(Player.Motion.DROP);             // 進む時の落下アニメーションをセット
                    }
                    break;

                case LADDER_RIGHT:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Ladder>().movePossibleFlag == true)
                    {
                        alice.climbRight2Flag = true;
                        alice.playerAngle = Player.PlayerAngle.RIGHT;
                        alice.transform.localEulerAngles = alice.angleRight;

                        alice.climbCount = 2;                               // 登りアニメーションの段階を２に
                        alice.SetClimbPosition(Player.PlayerAngle.RIGHT);   // 登り状態の座標を設定
                        alice.transform.position += new Vector3(0, -0.5f, 0);
                        alice.SetAnimation(Player.Motion.CLIMB2);
                    }
                    else if (alice.saveMoveDirection[alice.saveCount - 1] == Player.MoveDirection.UP)
                    {
                        alice.climbDropFlag = true;
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);
                        alice.SetAnimation(Player.Motion.DROP);
                    }
                    else
                    {
                        alice.AutoMoveSetting(Player.MoveDirection.DOWN);   // 自動下移動を設定
                        alice.SetAnimation(Player.Motion.DROP);             // 進む時の落下アニメーションをセット
                    }
                    break;
                case FLOWER1:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Flower1>().GetMovePossible() == false)
                        alice.gameOverFlag = true;
                    break;
                case FLOWER2:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Flower2>().GetMovePossible() == false)
                        alice.gameOverFlag = true;
                    break;
                case FLOWER3:
                    if (stageObject[posY - 1, posX, posZ].GetComponent<Flower3>().GetMovePossible() == false)
                        alice.gameOverFlag = true;
                    break;
            }
        }
        else
        {
            alice.ResetAnimation(Player.Motion.DROP);           // 進む時の落下アニメーションをリセット
        }
    }

    // 配列変更系ギミック
    public void ArrayChangeGimmck()
    {
        for (int x = 0; x < STAGE_X; x++)
        {
            for (int y = 0; y < STAGE_Y; y++)
            {
                for (int z = 0; z < STAGE_Z; z++)
                {
                    switch (stage[y, x, z])
                    {
                        // 木の場合
                        case TREE1:
                            // 成長段階が０か１なら
                            if (stageObject[y, x, z].GetComponent<Tree>().growCount <= 2)
                            {
                                stage[y + 1, x, z] = NONE;      // １つ上の配列を変更
                            }
                            // 成長段階が３なら
                            else if (stageObject[y, x, z].GetComponent<Tree>().growCount == 3)
                            {
                                stage[y + 1, x, z] = TREE2;    // １つ上の配列を変更
                            }
                            break;
                    }
                }
            }
        }
    }

    //アリスの下が穴か見る
    public bool GetFootHOLE(Player alice)
    {
        int posX = alice.arrayPosX;       // 配列上の座標Ｘ
        int posY = alice.arrayPosY;       // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;       // 配列上の座標Ｚ

        if (posY - 1 >= 0)
        {
            if ((stage[posY - 1, posX, posZ] == HOLE1) || (stage[posY - 1, posX, posZ] == HOLE2) ||
                (stage[posY - 1, posX, posZ] == HOLE3) || (stage[posY - 1, posX, posZ] == HOLE4) ||
                ((stage[posY - 1, posX, posZ] == HOLE5)))
            {
                return true;
            }
        }

        return false;
    }

    // 扉の開閉
    public void OpenDoor(bool getKeyFlag)
    {
        if (presenceKeyFlag)
        {
            if (getKeyFlag)
            {
                holdKeyFlag = true;


                // 開錠
                switch (holdKeyColor)
                {
                    case DOOR_RED_KEY:
                        if (presenceDoor_Red)
                            DoorRed.trapFlag = true;
                        break;
                    case DOOR_BLUE_KEY:
                        if (presenceDoor_Blue)
                            DoorBlue.GetComponent<Door>().trapFlag = true;
                        break;
                    case DOOR_YELLOW_KEY:
                        if (presenceDoor_Yellow)
                            DoorYellow.GetComponent<Door>().trapFlag = true;
                        break;
                    case DOOR_GREEN_KEY:
                        if (presenceDoor_Green)
                            DoorGreen.GetComponent<Door>().trapFlag = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                holdKeyFlag = false;

                // 閉錠
                switch (holdKeyColor)
                {
                    case DOOR_RED_KEY:
                        if (presenceDoor_Red)
                            DoorRed.trapFlag = false;
                        break;
                    case DOOR_BLUE_KEY:
                        if (presenceDoor_Blue)
                            DoorBlue.trapFlag = false;
                        break;
                    case DOOR_YELLOW_KEY:
                        if (presenceDoor_Yellow)
                            DoorYellow.trapFlag = false;
                        break;
                    case DOOR_GREEN_KEY:
                        if (presenceDoor_Yellow)
                            DoorGreen.trapFlag = false;
                        break;
                    default:
                        break;
                }

            }
            DoorKey.GetComponent<Key>().Invisible(getKeyFlag);
        }
    }

    // ゴールしているかチェック
    public bool GoalCheck(Player alice)
    {
        int posX = alice.arrayPosX;       // 配列上の座標Ｘ
        int posY = alice.arrayPosY;       // 配列上の座標Ｙ
        int posZ = alice.arrayPosZ;       // 配列上の座標Ｚ

        if (stage[posY, posX, posZ] == GOAL)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // ギミックとの当たり判定
    public TweedleDum.MoveDirection DumGimmickDecision(int posX, int posY, int posZ)
    {
        switch (stage[posY, posX, posZ])
        {
            // 何も無し
            case NONE:
                break;
            // 木
            case TREE1:
                if (stageObject[posY - 1, posX, posZ].GetComponent<Tree>().growCount == 2)
                {
                    return TweedleDum.MoveDirection.UP;
                }
                break;
            default:
                break;
        }
        return TweedleDum.MoveDirection.NONE;
    }
    // 横判定////////////////////////////////////////////////////////
    public bool DumBesideDecision(int posX, int posY, int posZ)
    {
        bool flag = false;

        if ((posY == -1) ||
            (posX == -1) || (posX == 11) ||
            (posZ == -1) || (posZ == 11))
            return false;

        switch (stage[posY, posX, posZ])
        {
            case NONE:      // 何も無し
            case BLOCK:     // ブロック
            case START:     // スタート地点
            case GOAL:      // ゴール地点
            case MUSHROOM_SMALL:
            case MUSHROOM_BIG:
            case POTION_SMALL:
            case POTION_BIG:
            case BRAMBLE:
            //ドラゴン参照
            case CHESHIRE_CAT: // チェシャ
            case DOOR_RED_KEY: // 鍵
            case DOOR_BLUE_KEY: // 鍵
            case DOOR_YELLOW_KEY: // 鍵
            case DOOR_GREEN_KEY: // 鍵
            //
            case IVY_FRONT:
            case IVY_BACK:
            case IVY_LEFT:
            case IVY_RIGHT:
            case LADDER_FRONT:
            case LADDER_BACK:
            case LADDER_LEFT:
            case LADDER_RIGHT:
            case SOLDIER_SPADE1:
            case SOLDIER_SPADE2:
            case TWINS_LEFT_FRONT:
            case TWINS_RIGHT_FRONT:
                flag = true;
                break;


            case BLOCK_GRASS_GROUND:
            case BLOCK_GRASS_1:
            case BLOCK_GRASS_2:
            case BLOCK_HOUSE_GROUND:
            case BLOCK_HOUSE_BOOK:
            case BLOCK_INSECT_GROUND:

            case BLOCK_GARDEN_GROUND:
            case BLOCK_GARDEN_FLOWER:

            case WATER:             // 水
            case SOLDIER_HEART:
            case IVY_BLOCK:         // 蔦ブロック
            case LADDER_BLOCK:      // 梯子ブロック
                flag = false;
                break;
            case TREE1:
                if (stageObject[posY, posX, posZ].GetComponent<Tree>().movePossibleFlag == true) { flag = true; }
                else { flag = false; }
                break;

            //ドラゴン参照
            case DOOR_RED_FRONT:
            case DOOR_RED_BACK:
            case DOOR_RED_LEFT:
            case DOOR_RED_RIGHT:
            case DOOR_BLUE_FRONT:
            case DOOR_BLUE_BACK:
            case DOOR_BLUE_LEFT:
            case DOOR_BLUE_RIGHT:
            case DOOR_YELLOW_FRONT:
            case DOOR_YELLOW_BACK:
            case DOOR_YELLOW_LEFT:
            case DOOR_YELLOW_RIGHT:
            case DOOR_GREEN_FRONT:
            case DOOR_GREEN_BACK:
            case DOOR_GREEN_LEFT:
            case DOOR_GREEN_RIGHT:
                flag = false; // 移動不可
                break;
        }

        // アリス
        if ((posX == Player.arrayPosX) &&
            (posY == Player.arrayPosY) &&
            (posZ == Player.arrayPosZ))
            flag = false;
        return flag;
    }

    // 横下判定//////////////////////////////////////////////////////////
    public bool DumBesideDownDecision(int posX, int posY, int posZ)
    {
        bool flag = false;
        //Debug.Log(posX);
        //Debug.Log(posY);
        //Debug.Log(posZ);
        switch (stage[posY - 1, posX, posZ])
        {
            case BLOCK:             // ブロック
            case BLOCK_GRASS_GROUND:
            case BLOCK_GRASS_1:
            case BLOCK_GRASS_2:
            case BLOCK_HOUSE_GROUND:
            case BLOCK_HOUSE_BOOK:
            case BLOCK_INSECT_GROUND:

            case BLOCK_GARDEN_GROUND:
            case BLOCK_GARDEN_FLOWER:

            case MUSHROOM_SMALL:
            case MUSHROOM_BIG:
            case POTION_SMALL:
            case POTION_BIG:
            case START:             // スタート地点
            case GOAL:              // ゴール地点
            case IVY_BLOCK:
            case IVY_FRONT:
            case IVY_BACK:
            case IVY_LEFT:
            case IVY_RIGHT:
            case LADDER_BLOCK:
            case LADDER_FRONT:
            case LADDER_BACK:
            case LADDER_LEFT:
            case LADDER_RIGHT:

            //ドラゴン参照
            case CHESHIRE_CAT: // チェシャ
            case DOOR_RED_KEY: // 鍵
            case DOOR_BLUE_KEY: // 鍵
            case DOOR_YELLOW_KEY: // 鍵
            case DOOR_GREEN_KEY: // 鍵
                // 扉
                flag = true;
                break;
            // 木
            case TREE1:
                if (stageObject[posY, posX, posZ].GetComponent<Tree>().growCount <= 2)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                break;
            case NONE:              // 何も無し
            case WATER:             // 水
            case HOLE1:
            case HOLE2:
            case HOLE3:
            case HOLE4:
            case HOLE5:
                flag = false;
                break;
        }
        return flag;
    }

    public void HydeCheshire(bool flag)
    {
        if (presenceCheshire)
            CheshireCat.GetComponent<Cheshire>().Hyde(flag);
    }
}