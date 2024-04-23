using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;
    public GameObject particlePrefab;
    int[,] map;
    GameObject[,] field;
    string debugText = "";
    public GameObject clearText;
    bool isClear = false;

    public int kMaxPower;
    private int isPower;



    public enum ObjectType
    {
        kTypeWall = 1,
        kTypePlayer,
        kTypeBox,
        kTypeGoal,
    }
    // Start is called before the first frame update
    void Start()
    {
        isPower = kMaxPower;
        Screen.SetResolution(1280, 720, false);
        map = new int[,] {
            {1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,1 },
            {1,0,0,0,3,0,0,3,3,0,0,1 },
            {1,4,0,3,2,3,0,3,0,0,4,1 },
            {1,0,0,0,3,0,0,3,3,0,0,1 },
            {1,0,0,0,0,0,0,3,0,0,0,1 },
            {1,0,0,0,0,0,0,0,0,0,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1},

        };

        field = new GameObject
        [
            map.GetLength(0),
            map.GetLength(1)
        ];

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == (int)ObjectType.kTypeWall)
                {
                    //GameObject instance
                    field[y, x] = Instantiate(
                        wallPrefab,
                        new Vector3(x - map.GetLength(1) / 2, map.GetLength(0) - y, 0),
                        Quaternion.identity);
                }
                else if (map[y, x] == (int)ObjectType.kTypePlayer)
                {
                    //GameObject instance
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(x - map.GetLength(1) / 2, map.GetLength(0) - y, 0),
                        Quaternion.identity);
                }
                else if (map[y, x] == (int)ObjectType.kTypeBox)
                {
                    //GameObject instance
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x - map.GetLength(1) / 2, map.GetLength(0) - y, 0),
                        Quaternion.identity);
                }
                else if (map[y, x] == (int)ObjectType.kTypeGoal)
                {
                    //GameObject instance
                    Instantiate(
                       goalPrefab,
                       new Vector3(x - map.GetLength(1) / 2, map.GetLength(0) - y, 0.01f),
                       Quaternion.identity);
                }

            }
        }
        //GameObject instance=Instantiate(
        //    playerPrefab,
        //    new Vector3(0,0,0),
        //    Quaternion.identity);
        //Debug.Log("Hello World");
        // PrintArray();
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (!isClear)
        {


            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector2Int playerIndex = GetPlayerIndex();
                SpownParticle(playerIndex);
                isPower = kMaxPower;
                MoveNumber("Player", playerIndex, new Vector2Int(playerIndex.x + 1, playerIndex.y), isPower);
                //PrintArray();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector2Int playerIndex = GetPlayerIndex();
                SpownParticle(playerIndex);
                isPower = kMaxPower;
                MoveNumber("Player", playerIndex, new Vector2Int(playerIndex.x - 1, playerIndex.y), isPower);

                // PrintArray();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector2Int playerIndex = GetPlayerIndex();
                SpownParticle(playerIndex);
                isPower = kMaxPower;

                MoveNumber("Player", playerIndex, new Vector2Int(playerIndex.x, playerIndex.y - 1), isPower);

                //PrintArray();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector2Int playerIndex = GetPlayerIndex();
                SpownParticle(playerIndex);
                isPower = kMaxPower;
                MoveNumber("Player", playerIndex, new Vector2Int(playerIndex.x, playerIndex.y + 1), isPower);

                // PrintArray();
            }

            if (isCleard())
            {
                isClear = true;
                clearText.SetActive(true);
                Debug.Log("clear");
            }
        }


    }

    //void PrintArray()
    //{
    //    string debugText = "";
    //    for (int y = 0; y < map.GetLength(0); y++)
    //    {
    //        for (int x = 0; x < map.GetLength(1); x++)
    //        {
    //            debugText += map[y, x].ToString() + ",";
    //        }
    //        debugText += "\n";
    //    }
    //    //for (int i = 0; i < map.Length; i++)
    //    //{
    //    //    debugText += map[i].ToString() + ",";
    //    //}
    //    Debug.Log(debugText);
    //}
    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }
    //int GetPlayerIndex()
    //{

    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        if (map[i] == 1)
    //        {
    //            return i;
    //        }
    //    }
    //    return -1;
    //}

    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo,int power)
    {

        Debug.Log(tag + ", moveFrome" + moveFrom + ", moveTo" + moveTo+", power"+power);
        if (power-- < 0) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Wall") { return false; }
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velocity,power);
            if (!success) { return false; }
        }
        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x - field.GetLength(1) / 2, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;

        return true;
        //if (moveTo < 0 || moveTo >= map.Length)
        //{
        //    return false;
        //}
        //if (map[moveTo] == 2)
        //{
        //    int velocity = moveTo - moveFrom;

        //    bool success = MoveNumber(2, moveTo, moveTo + velocity);
        //    if (!success)
        //    {
        //        return false;
        //    }
        //}
        //map[moveTo] = number;
        //map[moveFrom] = 0;
        //return true;
    }

    bool isCleard()
    {
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {

                if (map[y, x] == (int)ObjectType.kTypeGoal)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                return false;
            }

        }
        return true;

    }

    void SpownParticle(Vector2Int moveFrom)
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(
            particlePrefab,
           new Vector3(field[moveFrom.y, moveFrom.x].transform.position.x, field[moveFrom.y, moveFrom.x].transform.position.y, 0),
           Quaternion.identity);
        }

    }
}

