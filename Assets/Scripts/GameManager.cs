using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private MapManager mapManager;
    private void Awake()
    {
        if(FindObjectOfType<MapManager>() == null)
        {
            mapManager = gameObject.AddComponent<MapManager>();
        }
        if(FindObjectOfType<CreateMap>() == null)
        {
            GameObject go = new GameObject("CreateMap");
            go.AddComponent<CreateMap>();
        }
        if (FindObjectOfType<TurnManager>() == null)
        {
            gameObject.AddComponent<TurnManager>();
            TurnManager.mapManager = mapManager;
        }
    }
}
