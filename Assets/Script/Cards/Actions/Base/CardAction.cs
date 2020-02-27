﻿using UnityEngine;

/// <summary>
/// Se encarga de unificar la accion que se debe hacer
/// </summary>
public abstract class CardAction : MonoBehaviour
{

    /// <summary>
    /// Radio de vecinos
    /// </summary>
    [HideInInspector]
    public int radioVecinos;

    /// <summary>
    /// Realiza la accion 
    /// </summary>
    public abstract void DoAction(GameObject player);

    /// <summary>
    /// Determina si hemos hecho click o no en una tile
    /// </summary>
    public abstract void clickOnTile(Tile tile);

    /// <summary>
    /// Determina si hemos hecho click o no en una tile
    /// </summary>
    public abstract bool checkAction(GameObject player);


    /// <summary>
    /// Determina el radio de accion de la carta
    /// </summary>
    /// <returns></returns>
    public string setRadio()
    {
        radioVecinos = this.gameObject.GetComponent<Card>().info.Power;
        return radioVecinos.ToString();
    }

    /// <summary>
    /// Finalizamos el turno
    /// </summary>
    protected virtual void finishTurn()
    {
        GameManager.GetInstance().deck.inCardAction = false;

        GameManager.GetInstance().player.currentMovesTurn++;

        if (GameManager.GetInstance().player.currentMovesTurn >= GameManager.GetInstance().player.maxMovesPerTurn)
        {
            GameManager.GetInstance().player.currentMovesTurn = 0;
            GameManager.GetInstance().turn = TURN.IA;
        }
    }
}