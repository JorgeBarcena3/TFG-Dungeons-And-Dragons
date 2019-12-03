﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Informacion del canvas relativa a la baraja
/// </summary>
public class DeckCanvasInfo
{

    /// <summary>
    /// Componente del UI que almacena las imagenes
    /// </summary>
    public Image ImageComponent;

    /// <summary>
    /// Obtiene una referencia al canvas
    /// </summary>
    public GameObject CanvasGameObject;

    /// <summary>
    /// Diccionario que guarda las posiciones de las cartas y si estan ocupadas o no
    /// </summary>
    public List<AnchorInfo> anchorToCards;


    /// <summary>
    /// Obtiene el gameobject donde pinta el canvas
    /// </summary>
    /// <param name="tag">Parametro opcional que contiene el tag a buscar</param>
    public void getCanvasGameobject(string tag = "GameCanvas")
    {
        CanvasGameObject = GameObject.FindGameObjectWithTag(tag);
    }

    /// <summary>
    /// Selecciona el sprite de la baraja
    /// </summary>
    public void setDeckBack(Image ImageComponent, Sprite back, ref GameObject cardPrefab)
    {
        ImageComponent.sprite = back;
        cardPrefab.GetComponent<Image>().sprite = back;
    }
}