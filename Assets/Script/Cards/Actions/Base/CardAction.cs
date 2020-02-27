﻿using System.Linq;
using UnityEngine;

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
    public virtual void clickOnTile(Tile tile) { }

    /// <summary>
    /// Determina si la accion se puede o no hacer
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

        GameManager GM = GameManager.GetInstance();

        GM.deck.inCardAction = false;
        GM.player.playerInfo.useMana(this.gameObject.GetComponent<Card>().info.Cost);

        if ( GM.turnManager.isIATurn() )
        {
            GM.turn = TURN.IA;
        }

        GM.player.refreshPlayerData();

    }
}