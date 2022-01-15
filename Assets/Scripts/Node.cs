using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public BuildManager buildManager;
    
    private Renderer _renderer;
    private Color startColor;
    [Header("Optional")]
    public GameObject turret;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        startColor = _renderer.material.color;
        buildManager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        if (!buildManager.CanBuild)
            return;
        if (turret != null)
        {
            Debug.Log("Can not build here"); // todo display on screen
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
            return;
        if (buildManager.HasMoney)
        {
            _renderer.material.color = hoverColor;
        }
        else
        {
            _renderer.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        _renderer.material.color = startColor;
    }
}
