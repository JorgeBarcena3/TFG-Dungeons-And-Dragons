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
    public static GameObject instantiateCard(GameObject prefab, RectTransform position, Transform _parent, Deck _deck)
    {
        GameObject cardGameobject = Instantiate(prefab, position.position, Quaternion.identity, _parent);
        Card cardComponent = cardGameobject.AddComponent<Card>();
        cardComponent.deck = _deck;
        cardComponent.HUDCard = cardGameobject.GetComponent<HUDCard>();

        cardComponent.background = cardComponent.gameObject.transform.GetChild(0).gameObject;
        ((RectTransform)cardComponent.background.transform).sizeDelta = Card.CARD_RECT_TRANSFORM.sizeDelta;

        cardComponent.front = cardComponent.gameObject.transform.GetChild(1).gameObject;
        ((RectTransform)cardComponent.front.transform).sizeDelta = new Vector2(0, 0);

        selectCardAction(cardGameobject);

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
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(0.68f, 0.93f, 0.98f);
            card.type = ATTACKTYPE.GIVENMANA;
            card.SetCardArt(card.deck.cardArt[(int)card.type]);

            int power = Random.Range(2, 5);
            card.info = new InfoCard(
                ATTACKTYPE.GIVENMANA,
                01,
                "Recuperación de Maná",
                "Cuando utilices esta carta se te recuperaran " + power + " puntos de maná, ue podras utilizar durante este turno",
                power,
                power);

        }
        else if (random < 10)
        {
            cardGameobject.AddComponent<AttackAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 0.82f, 0.82f);
            cardGameobject.GetComponent<Card>().type = ATTACKTYPE.ATTACKACTION;
            card.SetCardArt(card.deck.cardArt[(int)card.type]);

            int power = Random.Range(1, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.ATTACKACTION,
                01,
                "Ataque a distancia",
                "Cuando utilices esta carta podrás matar cualquier enemigo (sin moverte de la casilla) que se encuentre en el rango de " + power + " casillas de distancia. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power
                );

        }
        else if (random < 15)
        {

            cardGameobject.AddComponent<AttackAndMovementAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 0.95f, 0.81f);
            cardGameobject.GetComponent<Card>().type = ATTACKTYPE.ATTACKANDMOVEMENT;
            card.SetCardArt(card.deck.cardArt[(int)card.type]);

            int power = Random.Range(1, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.ATTACKACTION,
                01,
                "Ataque y movimiento",
                "Cuando utilices esta carta podrás matar cualquier enemigo (moviéndote a su casilla) que se encuentre en el rango de " + power + " casillas de distancia. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power
                );

        }
        else if (random < 20)
        {
            cardGameobject.AddComponent<TeleportAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(0.45f, 1, 0.70f);
            cardGameobject.GetComponent<Card>().type = ATTACKTYPE.TELEPORT;
            card.SetCardArt(card.deck.cardArt[(int)card.type]);

            int power = Random.Range(2, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.ATTACKACTION,
                01,
                "Teleportación",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power
                );

        }
        else if (random < 25)
        {
            cardGameobject.AddComponent<MovementAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(0.69f, 0.99f, 0.69f);
            cardGameobject.GetComponent<Card>().type = ATTACKTYPE.MOVEMENT;
            card.SetCardArt(card.deck.cardArt[(int)card.type]);

            int power = Random.Range(1, 4);
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.MOVEMENT,
                01,
                "Movimiento",
                "Cuando utilices esta carta podrás moverte a cualquier casilla que se encuentre en el rango de " + power + ". El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power
                );
        }
        else
        {
            cardGameobject.AddComponent<DealCardsAction>();
            cardGameobject.transform.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.79f, 1);
            cardGameobject.GetComponent<Card>().type = ATTACKTYPE.DEALCARDSACTION;
            card.SetCardArt(card.deck.cardArt[(int)card.type]);

            int power = 2;
            cardGameobject.GetComponent<Card>().info = new InfoCard(
                ATTACKTYPE.DEALCARDSACTION,
                01,
                "Repartir Cartas",
                "Cuando utilices esta carta se te repartirán cartas hasta tener la mano completa. El coste de maná de esta carta sera de " + power + " puntos.",
                power,
                power
                );
        }

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
