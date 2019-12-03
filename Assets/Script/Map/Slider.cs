﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Funcion que permite mover una superficie. Requiere el componente de boxColider2D
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Slider : MonoBehaviour
{

    /// <summary>
    /// Gameobject del mapa
    /// </summary>
    private GameObject map;

    /// <summary>
    /// Componente BoxCollider2D
    /// </summary>
    private BoxCollider2D boxCollider;

    /// <summary>
    /// Posicion del click
    /// </summary>
    private Vector3 mousePosition;

    /// <summary>
    /// Posicion del mapa
    /// </summary>
    private Vector3 mapPosition;

    /// <summary>
    /// Funcion que inicializa el componente
    /// </summary>
    private void init()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Activa la funcion de scroll
    /// </summary>
    public void activate(SpriteRenderer sprite, Vector2 mapSize, GameObject _map)
    {
        init();
        map = _map;
        mapPosition = map.transform.position;
        adaptBoxCollider(sprite, mapSize);
    }

    /// <summary>
    /// Adaptar la box collider a todos sus hijos
    /// </summary>
    private void adaptBoxCollider(SpriteRenderer spr, Vector2 mapSize)
    {

        float sizeX = spr.size.x * mapSize.x;
        float sizeY = spr.size.y * mapSize.y - (spr.size.y * mapSize.y * 23 / 40);

        boxCollider.size = new Vector2(sizeX, sizeY);

        boxCollider.offset = new Vector2(spr.size.x * (mapSize.x / 2), -spr.size.y * (mapSize.y / 23 / 40));

    }

    /// <summary>
    /// Mover el mapa en una direccion
    /// </summary>
    private void moveMap()
    {

    }

    /// <summary>
    /// Cuando haces click por primera vez
    /// </summary>
    private void OnMouseDown()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        mapPosition = map.transform.position;
        
    }

    /// <summary>
    /// Determina cuando se esta arrastrando el raton
    /// </summary>
    void OnMouseDrag()
    {
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 diference = mousePosition - currentMousePos;

        map.transform.position += new Vector3(diference.x, diference.y, map.transform.position.z);

        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        Debug.Log(diference);
    }


}
