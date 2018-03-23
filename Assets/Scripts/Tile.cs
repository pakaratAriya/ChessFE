using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public TileAvailability tileType = TileAvailability.Available;
    private int x, y;

    public void SetX(int x)
    {
        this.x = x;
    }

    public void SetY(int y)
    {
        this.y = y;
    }

    public int GetX()
    {
        return this.x;
    }

    public int GetY()
    {
        return this.y;
    }
    
    public Vector3 GetPosition()
    {
        return new Vector3(this.x, this.y, 0);
    }

    public void ChangeType(TileAvailability newType = TileAvailability.Available)
    {
        tileType = newType;
    }

    public bool CheckAvailability()
    {
        return tileType == TileAvailability.Available;
    }

    public TileAvailability GetTileType()
    {
        return tileType;
    }

    public bool CheckAllyTile(Character ch)
    {
        return tileType == TileAvailability.AllyTile;
    }

    public bool CheckEnemyTile(Character ch)
    {
        return tileType == TileAvailability.EnemyTile;
    }
}

public enum TileAvailability
{
    Available,
    Unavailable,
    EnemyTile,
    AllyTile
};
