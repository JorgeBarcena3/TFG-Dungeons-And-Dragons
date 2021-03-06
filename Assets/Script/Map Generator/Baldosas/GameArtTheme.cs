﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Determina el tema que se va a usar en la partida
/// </summary>
public class GameArtTheme : Singelton<GameArtTheme>
{
    /// <summary>
    /// Lista de temas creados
    /// </summary>
    public List<Theme> Themes;
    /// <summary>
    /// Mateial de los detalles
    /// </summary>
    public Material material;

    /// <summary>
    /// El indice del tema que se va a utilizar
    /// </summary>
    [HideInInspector]
    public Theme currentTheme;

    /// <summary>
    /// Detalles spawneados en el mundo
    /// </summary>
    private List<GameObject> details = new List<GameObject>();

    /// <summary>
    /// Determina el tema que se va a usar
    /// </summary>
    public void ChooseTheme()
    {
        currentTheme = Themes[Random.Range(0, Themes.Count)];
    }

    /// <summary>
    /// Añade decoracion al mapa
    /// </summary>
    /// <param name="spriteBoard"></param>
    public void AddDecoration(List<GameObject> spriteBoard)
    {

        if (currentTheme.floorDetails.Count + currentTheme.wallDetails.Count > 0)
        {

            GameObject baseObj = new GameObject("DetailSprite");
            baseObj.AddComponent<SpriteRenderer>();
            bool new_detail = false;
            foreach (var item in spriteBoard)
            {
                Tile tile = item.GetComponent<TileWalkable>();

                if (tile && currentTheme.floorDetails.Count > 0)
                {
                    float random = Random.Range(0.0f, 1.0f);
                    if (random < currentTheme.percentajeDetails)
                    {
                        GameObject detail = Instantiate(baseObj, item.transform.position + new Vector3(0, 0, -0.1f), Quaternion.identity, GameManager.Instance.worldGenerator.transform);
                        detail.GetComponent<SpriteRenderer>().sprite = currentTheme.floorDetails[Random.Range(0, currentTheme.floorDetails.Count)];
                        detail.GetComponent<SpriteRenderer>().color = new Color(1,1,1, 0);
                        details.Add(detail);
                        new_detail = true;
                    }

                }
                else if (!tile && currentTheme.wallDetails.Count > 0)
                {
                    tile = item.GetComponent<TileUnwalkable>();

                    float random = Random.Range(0.0f, 1.0f);
                    if (random < currentTheme.percentajeDetails)
                    {
                        GameObject detail = Instantiate(baseObj, item.transform.position + new Vector3(0, 0.18f, -1.5f), Quaternion.identity, GameManager.Instance.worldGenerator.transform);
                        detail.GetComponent<SpriteRenderer>().sprite = currentTheme.wallDetails[Random.Range(0, currentTheme.wallDetails.Count)];
                        detail.GetComponent<SpriteRenderer>().color = new Color(1,1,1, 0);

                        details.Add(detail);
                        new_detail = true;
                    }
                }

                if (new_detail)
                {
                    new_detail = false;
                    SpriteRenderer sprite = details.Last().GetComponent<SpriteRenderer>();
                    sprite.enabled = false;
                    StartCoroutine(SetActiveDetail(sprite, 4));


                }


            }


        }
    }
    /// <summary>
    /// Activa visualmente los detalles del mapa pasados un tiempo
    /// </summary>
    /// <param name="sprite">SpriteRender que se quiere activar</param>
    /// <param name="time">Tiempo que tarda en activar el spriteRender</param>
    /// <returns></returns>
    private IEnumerator SetActiveDetail(SpriteRenderer sprite, float time)
    {
        float delta = 0.0f;
        while (delta < time)
        {
            delta += Time.deltaTime;
            yield return null;
        }
        sprite.enabled = true;
        yield return null;

        GameManager.Instance.StartCoroutine(AuxiliarFuncions.FadeIn(sprite, time));

    }
}

/// <summary>
/// Tema que vamos a utilizar
/// </summary>
[System.Serializable]
public class Theme
{
    /// <summary>
    /// Nombre del tema
    /// </summary>
    public string Name;

    /// <summary>
    /// Paredes
    /// </summary>
    public Sprite wall;

    /// <summary>
    /// Suelo
    /// </summary>
    public Sprite floor;

    /// <summary>
    /// Porcentaje de detalles
    /// </summary>
    [Range(0, 1)]
    public float percentajeDetails = 0.1f;

    /// <summary>
    /// Detalles del tema
    /// </summary>
    public List<Sprite> floorDetails;

    /// <summary>
    /// Detalles del tema
    /// </summary>
    public List<Sprite> wallDetails;
}