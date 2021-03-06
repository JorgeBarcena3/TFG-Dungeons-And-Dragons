﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


/// <summary>
/// Clase que guarda la informacion de una carta
/// </summary>
public class Card : MonoBehaviour
{

    /// <summary>
    /// Colores de las cartas
    /// </summary>
    public static Dictionary<string, Color> CardsColor = new Dictionary<string, Color>()
        {
            { "GIVENMANA",         new Color(0.68f, 0.93f, 0.98f) },
            { "ATTACKACTION",      new Color(1, 0.82f, 0.82f) },
            { "ATTACKANDMOVEMENT", new Color(1, 0.95f, 0.81f) },
            { "TELEPORT",          new Color(0.45f, 1, 0.70f) },
            { "MOVEMENT",          new Color(0.69f, 0.99f, 0.69f) },
            { "DEALCARDSACTION",   new Color(0.5f, 0.79f, 1)},
            { "TEMPLATE",          new Color(0,0,0) },
        };


    /// <summary>
    /// Tipo de carta
    /// </summary>
    public ATTACKTYPE type { get; private set; }

    /// <summary>
    /// Sprite de la carta en cuestion
    /// </summary>
    private Sprite sprite;

    /// <summary>
    /// Informacion relativa a cada carta
    /// </summary>
    [HideInInspector]
    public InfoCard info;

    /// <summary>
    /// Componente del UI que almacena las imagenes
    /// </summary>
    private Image ImageComponent;

    /// <summary>
    /// Baraja a la que pertenece
    /// </summary>
    private Deck deck;

    /// <summary>
    /// En caso de que esté en la mano del jugador, en que posicion está
    /// </summary>
    public int? indexPosition = null;

    /// <summary>
    /// Gameobject que almacena el background de la carta
    /// </summary>
    public GameObject background;

    /// <summary>
    /// Gameobject que almacena el front de la carta
    /// </summary>
    public GameObject front;

    /// <summary>
    /// Completa la HUD de la carta
    /// </summary>
    public HUDCard HUDCard;

    /// <summary>
    /// Tamaño de la carta
    /// </summary>
    public static RectTransform CARD_RECT_TRANSFORM { get; set; }

    /// <summary>
    /// Tamaño de la carta
    /// </summary>
    public static Vector2 ORIGINAL_SIZE { get; set; }

    /// <summary>
    /// Coloca un sprite en el gameobject de la carta
    /// </summary>
    public void setSprite(Sprite sprt)
    {
        ImageComponent = GetComponent<Image>();
        ImageComponent.sprite = sprt;

    }

    /// <summary>
    /// Instanciamos una carta en la posicion X
    /// </summary>
    /// <param name="position">Posicion donde vamos a instanciar la carta</param>
    public static GameObject instantiateCard(GameObject prefab, RectTransform position, Transform _parent, Deck _deck, InfoCard info = null)
    {
        GameObject cardGameobject = Instantiate(prefab, position.position, Quaternion.identity, _parent);
        Card cardComponent = cardGameobject.AddComponent<Card>();
        cardComponent.deck = _deck;
        cardComponent.HUDCard = cardGameobject.GetComponent<HUDCard>();

        cardComponent.background = cardComponent.gameObject.transform.GetChild(0).gameObject;
        ((RectTransform)cardComponent.background.transform).sizeDelta = Card.CARD_RECT_TRANSFORM.sizeDelta;

        cardComponent.front = cardComponent.gameObject.transform.GetChild(1).gameObject;
        ((RectTransform)cardComponent.front.transform).sizeDelta = new Vector2(0, 0);

        if (info == null)
        { 
            selectCardAction(cardGameobject);
        }
        else
        {
            setCardAction(cardGameobject, info);
        }

        return cardGameobject;
    }

    /// <summary>
    /// Determina la accion de la carta
    /// </summary>
    /// <param name="cardGameobject"></param>
    /// <param name="info"></param>
    private static void setCardAction(GameObject cardGameobject, InfoCard info)
    {
        Card card = cardGameobject.GetComponent<Card>();


        switch (info.Card_kind)
        {
            case ATTACKTYPE.ATTACKACTION:
                cardGameobject.AddComponent<AttackAction>();

                break;
            case ATTACKTYPE.ATTACKANDMOVEMENT:
                cardGameobject.AddComponent<AttackAndMovementAction>();

                break;
            case ATTACKTYPE.GIVENMANA:
                cardGameobject.AddComponent<GivenManaAction>();

                break;
            case ATTACKTYPE.MOVEMENT:
                cardGameobject.AddComponent<MovementAction>();

                break;
            case ATTACKTYPE.TELEPORT:
                cardGameobject.AddComponent<TeleportAction>();

                break;
            case ATTACKTYPE.DEALCARDSACTION:
                cardGameobject.AddComponent<DealCardsAction>();

                break;
        }

        card.info = info;

        cardGameobject.GetComponent<CardAction>().setActor(GameManager.Instance.player.gameObject);

        card.HUDCard.fillInfo(cardGameobject.GetComponent<Card>().info);

    }



    /// <summary>
    /// Instanciamos una carta en la posicion X
    /// </summary>
    /// <param name="position">Posicion donde vamos a instanciar la carta</param>
    public static GameObject instantiateCard(GameObject prefab, Transform position, Transform _parent, Deck _deck)
    {


        GameObject cardGameobject = Instantiate(prefab, position.position, Quaternion.identity, _parent);
        Card cardComponent = cardGameobject.AddComponent<Card>();
        cardComponent.deck = _deck;
        cardComponent.HUDCard = cardGameobject.GetComponent<HUDCard>();

        cardComponent.background = cardComponent.gameObject.transform.GetChild(0).gameObject;
        ((RectTransform)cardComponent.background.transform).sizeDelta = new Vector2(0, 0);

        cardComponent.front = cardComponent.gameObject.transform.GetChild(1).gameObject;
        ((RectTransform)cardComponent.front.transform).sizeDelta = new Vector2(0, 0);

        cardGameobject.transform.localScale = Vector3.zero;

        return cardGameobject;
    }

    /// <summary>
    /// Determina la accion que va a hacer una carta
    /// </summary>
    /// <param name="cardGameobject"></param>
    private static void selectCardAction(GameObject cardGameobject)
    {
        int random = UnityEngine.Random.Range(0, 27);
        Card card = cardGameobject.GetComponent<Card>();

        if (random < 5)
        {

            cardGameobject.AddComponent<GivenManaAction>();

            int power = Random.Range(2, 5);
            card.info = new InfoCard(
                ATTACKTYPE.GIVENMANA,
                01,
                "Recuperación de Maná",
                "Cuando utilices esta carta se te recuperaran " + power + " puntos de maná, ue podras utilizar durante este turno",
                power,
                power,
                0,
                1,
                ATTACKTYPE.GIVENMANA.ToString()
                );

        }
        else if (random < 10)
        {
            cardGameobject.AddComponent<AttackAction>();

            int power = Random.Range(1, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.ATTACKACTION,
                01,
                "Ataque a distancia",
                "Cuando utilices esta carta podrás matar cualquier enemigo (sin moverte de la casilla) que se encuentre en el rango de " + power + " casillas de distancia. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.ATTACKACTION.ToString()
                );

        }
        else if (random < 15)
        {

            cardGameobject.AddComponent<AttackAndMovementAction>();

            int power = Random.Range(1, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.ATTACKANDMOVEMENT,
                01,
                "Ataque y movimiento",
                "Cuando utilices esta carta podrás matar cualquier enemigo (moviéndote a su casilla) que se encuentre en el rango de " + power + " casillas de distancia. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.ATTACKANDMOVEMENT.ToString()
                );

        }
        else if (random < 20)
        {
            cardGameobject.AddComponent<TeleportAction>();

            int power = Random.Range(2, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.TELEPORT,
                01,
                "Teleportación",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.TELEPORT.ToString()
                );

        }
        else if (random < 25)
        {
            cardGameobject.AddComponent<MovementAction>();

            int power = Random.Range(1, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.MOVEMENT,
                01,
                "Movimiento",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.MOVEMENT.ToString()
                );
        }
        else
        {
            cardGameobject.AddComponent<DealCardsAction>();

            int power = 2;
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.DEALCARDSACTION,
                01,
                "Repartir Cartas",
                "Cuando utilices esta carta se te repartirán cartas hasta tener la mano completa. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power,
                0,
                1,
                ATTACKTYPE.DEALCARDSACTION.ToString()
                );
        }

        cardGameobject.GetComponent<CardAction>().setActor(GameManager.Instance.player.gameObject);

        card.HUDCard.fillInfo(cardGameobject.GetComponent<Card>().info);

    }

    /// <summary>
    /// Detectamos si hemos hecho click en una carta
    /// </summary>
    void OnMouseDown()
    {
        if (
            !GameManager.Instance.deck.inCardAction &&
            GameManager.Instance.state == States.INGAME &&
            GameManager.Instance.turn == TURN.PLAYER &&
            indexPosition != null
            && !deck.infoBackground.activeSelf
            )
            deck.ClickOnCard(this.gameObject);
    }

    /// <summary>
    /// Da la vuelta a la carta en cuestion
    /// </summary>
    public void FlipCard(bool flipped = true)
    {
        if (flipped)
        {
            front.SetActive(true);
            ((RectTransform)front.transform).sizeDelta = Card.CARD_RECT_TRANSFORM.sizeDelta;
            ((RectTransform)background.transform).sizeDelta = new Vector2(0, 0);
        }
        else
        {
            front.SetActive(false);
            ((RectTransform)background.transform).sizeDelta = Card.CARD_RECT_TRANSFORM.sizeDelta;
            ((RectTransform)front.transform).sizeDelta = new Vector2(0, 0);
        }
    }

    /// <summary>
    /// Seleccionamos un arte de una carta
    /// </summary>
    public void SetCardArt(Sprite spr)
    {
        Material myMaterial = Instantiate(front.GetComponent<Image>().material);
        myMaterial.SetTexture("_MyTexture", spr.texture);
        front.GetComponent<Image>().material = myMaterial;
    }


}
