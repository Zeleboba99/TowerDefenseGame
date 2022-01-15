using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour
{
    public TurretBlueprint[] turrets;
    private BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        foreach (var turret in turrets)
        {
            SetCostToUi(turret);
        }
    }
    
    public void SelectTurretByIndex(int index)
    {
        buildManager.SelectTurretToBuild(turrets[index]);
    }
    
    private void SetCostToUi(TurretBlueprint turret)
    {
        turret.CostText.text = "$" + turret.cost;
    }
}
