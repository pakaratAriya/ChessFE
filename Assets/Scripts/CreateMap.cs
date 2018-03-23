using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {
    public List<Tile> allTilePrefabs = new List<Tile>();
    public List<Tile> allTile = new List<Tile>();
    private MapManager mapManager;
    public int width = 8;
    public int height = 8;
	// Use this for initialization
	void Start () {
        allTilePrefabs.AddRange(Resources.LoadAll<Tile>("Tiles"));
        mapManager = FindObjectOfType<MapManager>();
        mapManager.createMap = this;
        int count0 = 0;
        for(int i = -width/2; i <= width-width/2; i++)
        {
            for(int j=-height/2; j<=height-height/2; j++)
            {
                int randNum = Random.Range(0, allTilePrefabs.Count);

                count0 = randNum == 0 ? count0 + 1 : count0;
                Tile tile = Instantiate<Tile>(allTilePrefabs[randNum]);
                tile.transform.position = new Vector3(i, j, 0);
                tile.SetX(i);
                tile.SetY(j);
                tile.transform.SetParent(this.transform);
                allTile.Add(tile);
                mapManager.RegisterTile(tile);
            }
        }
	}
	
}


