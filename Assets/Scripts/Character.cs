using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    public state characterState;
    private MapManager mapManager;
    public int moveNum = 1;
    private Vector3 currentPos;
    public int team = 1;

    // Use this for initialization

    void Start () {
        characterState = state.start;
        mapManager = FindObjectOfType<MapManager>();
        currentPos = transform.position;
        if(team != 1)
        {
            characterState = state.finished;
        }
    }
	
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePos = ray.GetPoint(25);
        Vector3 toPos = new Vector3(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y), 0);
        if (!Input.GetMouseButton(0) && characterState == state.moving)
        {
            EndTurn();
        }
        if (characterState == state.moving)
        {
            OnMovingState(toPos);
        }
        else
        {
            mapManager.BookTile(transform.position,team);
        }

        AdjustColor();
	}

    /// <summary>
    /// Turn off the map routing. Set character state to finished. Check if all team members have already played.
    /// </summary>
    private void EndTurn()
    {
        mapManager.DisableAvalability();
        characterState = state.finished;
        TurnManager.HasAllPlay(team);
    }

    /// <summary>
    /// Show the map routing. Move the character to the mouse position if possible.
    /// </summary>
    /// <param name="toPos">the mouse position</param>
    private void OnMovingState(Vector3 toPos)
    {
        mapManager.EnableAvalability(this, currentPos, moveNum);
        if (mapManager.CheckTileAvailable(toPos) && mapManager.CheckReachableTile(currentPos, toPos, moveNum))
        {
            mapManager.LeaveTile(transform.position);
            transform.position = toPos;
        }
    }

    /// <summary>
    /// Change the color 
    /// </summary>
    private void AdjustColor()
    {
        if (characterState == state.moving)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (characterState == state.finished)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (characterState == state.start)
        {
            if (Input.GetMouseButton(0))
            {
                characterState = state.moving;
                currentPos = transform.position;
            }
        }
    }


}

public enum state
{
    start,
    moving,
    finished
};
