﻿using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

/// <summary>
/// Autenticacion de firebase
/// </summary>
public class GooglePlayServicesSocialManager : Singelton<GooglePlayServicesSocialManager>
{

    /// <summary>
    /// Current User
    /// </summary>
    public Firebase.Auth.FirebaseUser user = null;

    /// <summary>
    /// Inicializamos la autorizacion
    /// </summary>
    public void init(Firebase.Auth.FirebaseUser _User)
    {
        user = _User;
    }

    /// <summary>
    /// Desbloquea un logro
    /// </summary>
    public void UnlockAchievement(string id, float progress)
    {
        if (user != null)
            Social.ReportProgress(id, progress, (bool success) =>
            {
                print("Logro desbloqueado");
            });
    }

    /// <summary>
    /// Desbloquea un logro
    /// </summary>
    public void IncrementAchievement(string id, int steps)
    {
        if (user != null)
            PlayGamesPlatform.Instance.IncrementAchievement(id, steps, (bool success) =>
        {
            print("Logro avanzado " + steps + " pasos. " + success);
        });
    }

    /// <summary>
    /// Guarda una score
    /// </summary>
    public void ReportScore(string id, int score)
    {
        if (user != null)
            Social.ReportScore(score, id, (bool success) =>
            {
                print("Score posteada correctamente");
            });
    }

    /// <summary>
    /// Muestra los logros
    /// </summary>
    public void ShowAchievementsUI()
    {
        if (user != null)
            Social.ShowAchievementsUI();
    }

    /// <summary>
    /// Muestra las puntuaciones
    /// </summary>
    public void ShowLeaderboardUI()
    {
        if (user != null)
            Social.ShowLeaderboardUI();
    }






}
