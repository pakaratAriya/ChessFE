using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    private Dictionary<Vector3, Tile> allTiles = new Dictionary<Vector3, Tile>();
    public CreateMap createMap;
    public List<Tile> tiles = new List<Tile>();

    public void RegisterTile(Tile tile)
    {
        allTiles[tile.GetPosition()] = tile;
        tiles.Add(tile);
    }

    public List<Tile> GetAllTiles()
    {
        return tiles;
    }

    public bool CheckTileAvailable(Vector3 checkPos)
    {
        return allTiles.ContainsKey(checkPos) && allTiles[checkPos].CheckAvailability();
    }

    public void LeaveTile(Vector3 pos)
    {
        if(allTiles[pos] != null)
        {
            allTiles[pos].ChangeType();
        }
    }

    public void BookTile(Vector3 pos, int teamNum)
    {
        if (allTiles.ContainsKey(pos))
        {
            if (teamNum == TurnManager.turnTeam)
            {
                allTiles[pos].ChangeType(TileAvailability.AllyTile);
            } else
            {
                allTiles[pos].ChangeType(TileAvailability.EnemyTile);
            }   
        }
    }

    public bool CheckReachableTile(Vector3 currentPos,Vector3 toPos, int num = 2)
    {
        return Mathf.Abs(Mathf.RoundToInt(currentPos.x - toPos.x)) + Mathf.Abs(Mathf.RoundToInt(currentPos.y - toPos.y)) <= num;
    }

    public void EnableAvalability(Character ch,Vector3 currentPos, int num = 2)
    {
        for(int i = Mathf.RoundToInt(currentPos.x) - num; i <= Mathf.RoundToInt(currentPos.x) + num; i++)
        {
            for(int j = Mathf.RoundToInt(currentPos.y) - num; j <= Mathf.RoundToInt(currentPos.y) + num; j++)
            {
                if(Mathf.Abs(Mathf.RoundToInt(currentPos.x - i)) + Mathf.Abs(Mathf.RoundToInt(currentPos.y - j)) <= num)
                {
                    if(allTiles.ContainsKey(new Vector3(i, j, 0)))
                    {
                        if (allTiles[new Vector3(i, j, 0)].CheckAvailability())
                            allTiles[new Vector3(i, j, 0)].GetComponent<SpriteRenderer>().color = Color.black;
                        if (allTiles[new Vector3(i, j, 0)].CheckAllyTile(ch))
                            allTiles[new Vector3(i, j, 0)].GetComponent<SpriteRenderer>().color = Color.blue;
                        if (allTiles[new Vector3(i, j, 0)].CheckEnemyTile(ch))
                            allTiles[new Vector3(i, j, 0)].GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }
        }
    }
    public void DisableAvalability()
    {
        foreach(Tile tile in createMap.allTile)
        {
            tile.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
