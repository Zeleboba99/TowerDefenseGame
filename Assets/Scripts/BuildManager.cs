using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one build manager");
        }
        instance = this;
    }

    private TurretBlueprint turretToBuild;

    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }
    
    public bool HasMoney
    {
        get { return PlayerStats.Money >= turretToBuild.cost; }
    }
   
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enougt gold");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = Instantiate(turretToBuild.prefab, node.transform.position, Quaternion.identity);
        node.turret = turret;
    }
}
