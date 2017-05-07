using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    public GameObject crossPrefab;          // Player One
    public GameObject circlePrefab;         // Player Two
    public Transform marksParent;           // Parent to hold the marks


    private int[] board = new int[9];       // Array to hold the values of the board
    private int playerTurn = 0;             // Player One begins to place a mark
    private float markSpawnSpeed = 10f;      // Time it takes to place the marks on the board
    private List<GameObject> marksToSpawn = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        InstantiateBoard();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Cell"))
                {
                    int cellNumber = System.Int32.Parse(hit.transform.name);

                    if (board[cellNumber] == -1)
                    {
                        board[cellNumber] = playerTurn;
                        PlaceMark(hit.transform.position + Vector3.up * Camera.main.transform.position.y);

                        if (!BoardIsFull())
                        {
                            CheckForWinner();
                        }
                        else
                        {
                            Reset();
                        }
                    }
                }
            }
        }
    }


    private void FixedUpdate()
    {
        if (marksToSpawn.Count > 0)
        {
            foreach (GameObject markToSpawn in marksToSpawn)
            {
                Vector3 desiredPosition = new Vector3(markToSpawn.transform.position.x, 0f, markToSpawn.transform.position.z);
                markToSpawn.transform.position = Vector3.Lerp(markToSpawn.transform.position, desiredPosition, markSpawnSpeed * Time.deltaTime);

                if (markToSpawn.transform.position == desiredPosition)
                    marksToSpawn.Remove(markToSpawn);
            }
        }
    }


    private void InstantiateBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            // No mark placed yet
            board[i] = -1;
        }
    }


    private void Reset()
    {
        InstantiateBoard();
        playerTurn = 0;

        for (int i = 0; i < marksParent.childCount; i++)
        {
            Destroy(marksParent.GetChild(i).gameObject);
        }

        marksToSpawn = new List<GameObject>();
    }


    private void PlaceMark(Vector3 position)
    {
        switch (playerTurn)
        {
            case 0:
                marksToSpawn.Add(Instantiate(crossPrefab, position, Quaternion.identity, marksParent) as GameObject);
                break;

            case 1:
                marksToSpawn.Add(Instantiate(circlePrefab, position, Quaternion.identity, marksParent) as GameObject);
                break;
        }

        playerTurn = (playerTurn + 1) % 2;
    }


    private void CheckForWinner()
    {
        if (RowCheck(0) || ColumnCheck(0) || DiagonalCheck(0))
        {
            Debug.Log("Player One won the game!");
            Reset();
        }

        if (RowCheck(1) || ColumnCheck(1) || DiagonalCheck(1))
        {
            Debug.Log("Player Two won the game!");
            Reset();
        }
    }


    private bool RowCheck(int player)
    {
        return board[0] == player && board[1] == player && board[2] == player ||
               board[3] == player && board[4] == player && board[5] == player ||
               board[6] == player && board[7] == player && board[8] == player;
    }


    private bool ColumnCheck(int player)
    {
        return board[0] == player && board[3] == player && board[6] == player ||
               board[1] == player && board[4] == player && board[7] == player ||
               board[2] == player && board[5] == player && board[8] == player;
    }


    private bool DiagonalCheck(int player)
    {
        return board[0] == player && board[4] == player && board[8] == player ||
               board[2] == player && board[4] == player && board[6] == player;
    }


    private bool BoardIsFull()
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == -1)
                return false;
        }

        return true;
    }
}
