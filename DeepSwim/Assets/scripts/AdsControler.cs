using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsController : MonoBehaviour
{
    public static AdsController instance;

    // --- ID de Unidades de Anuncios ---
#if UNITY_ANDROID
    private string interstitialUnitId = "ca-app-pub-3940256099942544/1033173712";
    private string rewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
    private string interstitialUnitId = "ca-app-pub-3940256099942544/4411468910";
    private string rewardedUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
    private string interstitialUnitId = "unused";
    private string rewardedUnitId = "unused";
#endif

    // --- Variables de Anuncios ---
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    // --- Estado de Inicialización ---
    // Esta variable pública nos dirá si el SDK está listo.
    public bool IsInitialized { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Inicializamos el SDK de Google Mobile Ads UNA SOLA VEZ aquí.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Debug.Log("AdsController: Google Mobile Ads initialization complete.");
            IsInitialized = true; // Marcamos como listo.

            // Precargamos los anuncios para que estén listos cuando se necesiten.
            LoadInterstitialAd();
            LoadRewardedAd();
        });
    }

    // --- Lógica de Anuncio Intersticial ---
    public void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();
        InterstitialAd.Load(interstitialUnitId, adRequest, (ad, error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Interstitial ad failed to load with error: " + error);
                return;
            }
            interstitialAd = ad;
            RegisterInterstitialEventHandlers(interstitialAd);
        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            LoadInterstitialAd(); // Intentar cargarlo de nuevo.
        }
    }

    private void RegisterInterstitialEventHandlers(InterstitialAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad closed.");
            // Volvemos a cargar otro anuncio para la próxima vez.
            LoadInterstitialAd();
        };
    }

    // --- Lógica de Anuncio Recompensado ---
    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        var adRequest = new AdRequest();
        RewardedAd.Load(rewardedUnitId, adRequest, (ad, error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load with error: " + error);
                return;
            }
            rewardedAd = ad;
        });
    }

    public void ShowRewardedAd(Action<Reward> onUserEarnedReward)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            Debug.Log("Showing rewarded ad.");
            rewardedAd.Show(onUserEarnedReward);
        }
        else
        {
            Debug.LogError("Rewarded ad is not ready yet.");
            LoadRewardedAd(); // Intentar cargarlo de nuevo.
        }
    }
}