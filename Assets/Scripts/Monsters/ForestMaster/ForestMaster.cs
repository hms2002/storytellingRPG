using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMaster : Monster
{
    public readonly GameObject rootTectaclesPrefab;
    private RootTectacles _rootTectacle;
    public RootTectacles rootTectacle
    { get
        {
            return _rootTectacle;
        }
        private set { }
    }

    private void Awake()
    {
        MAX_HP = 100;
        encounterText = "숲의 주인이 땅을 짚으며 일어나자, 거대한 그림자가 드리웠다.";
    }

    private void Start()
    {
        SpawnRootTectacle();
    }

    public void SpawnRootTectacle()
    {
        if (_rootTectacle == null) return;
        _rootTectacle = Instantiate(_rootTectacle).GetComponent<RootTectacles>();
        _rootTectacle.Init(this);
        FightManager.fightManager.AddMonster(_rootTectacle);
    }
}