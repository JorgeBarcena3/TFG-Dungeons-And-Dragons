﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Informacion sobre la baraja
/// </summary>
public class DeckInfo : MonoBehaviour
{
    /// <summary>
    /// Lista de Gameobjects que contienen las cartas
    /// </summary>
    public List<GameObject> cardsGameObject = new List<GameObject>();

    /// <summary>
    /// Cartas que pueden salir de la baraja
    /// </summary>
    public List<GameObject> activeCards = new List<GameObject>();

    /// <summary>
    /// Cartas que tiene en la mano actualmente
    /// </summary>
    public List<GameObject> handCards = new List<GameObject>();

    /// <summary>
    /// Cartas que han salido, van al cementerio
    /// </summary>
    public List<GameObject> cementeryCards = new List<GameObject>();

    /// <summary>
    /// Numero de cartas que tendrá en la mano
    /// </summary>
    public int cardsInHand = 5;

    /// <summary>
    /// Numero de cartas que habrá en la baraja
    /// </summary>
    public int cardsInDeck = 30;

    /// <summary>
    /// Contructor con parametros de la infromacion de la cantidad de cartas
    /// </summary>
    /// <param name="_cardsInHands"></param>
    /// <param name="_cardsInDeck"></param>
    public void init(int _cardsInHands, int _cardsInDeck)
    {
        cardsInHand = _cardsInHands;
        cardsInDeck = _cardsInDeck;
    }

    /// <summary>
    /// Crea y posiciona los anchors donde se van a parar las cartas
    /// </summary>
    public void setCardsAnchor(ref List<AnchorInfo> anchorToCards, ref GameObject CanvasGameObject, ref GameObject anchorPrefab)
    {

        anchorToCards = new List<AnchorInfo>(cardsInHand);

        RectTransform canvasComponent = CanvasGameObject.GetComponent<RectTransform>();
        Vector2 sizeCanvas = AuxiliarFuncions.getSizeFromRectTransform(canvasComponent);

        float width = sizeCanvas.x;
        float height = sizeCanvas.y;

        Vector2 CardSize = AuxiliarFuncions.getSizeFromRectTransform(Card.CARD_RECT_TRANSFORM);

        float x = width / (cardsInHand + 1);
        float xOffset = x / (cardsInHand + 1);
        float y = CardSize.y * x / CardSize.x;


        for (int i = 0; i < cardsInHand; ++i)
        {
            Vector3 position = new Vector3((-width / 2) + (x / 2) + (x * i) + ((i + 1) * xOffset), (-height / 2) + (height * 0.05f) + (y / 2), 0);
            GameObject anchor = Instantiate(anchorPrefab, position, Quaternion.identity, CanvasGameObject.transform);
            anchorToCards.Add(new AnchorInfo(false, anchor.transform));
        }

    }

}