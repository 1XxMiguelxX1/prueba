using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using System;

public class AdsControler : MonoBehaviour
{
    // --- Variables para Banner ---
#if UNITY_ANDROID
    private string adUnitIdBanner = "ca-app-pub-3940256099942544/6300978111"; // Cambiar por el real cuando esté listo
#elif UNITY_IPHONE
    private string adUnitIdBanner = "ca-app-pub-3940256099942544/2934735716";
#else
    private string adUnitIdBanner = "unused";
#endif
    private BannerView bannerView;

    // --- (NUEVO) Variables para Intersticial ---
#if UNITY_ANDROID
    private string adUnitIdInterstitial = "ca-app-pub-3940256099942544/1033173712"; // Cambiar por el real cuando esté listo
#elif UNITY_IPHONE
    private string adUnitIdInterstitial = "ca-app-pub-3940256099942544/4411468910";
#else
    private string adUnitIdInterstitial = "unused";
#endif
    private InterstitialAd interstitialAd;


    void Start()
    {
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            Debug.Log("Google Mobile Ads initialization complete.");
            // Una vez inicializado, creamos el banner y cargamos el primer intersticial.
            CreateBannerView();
            LoadInterstitialAd(); // (NUEVO) Cargamos el primer anuncio intersticial.
        });
    }

    #region Banner Ads
    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // --- LA LÍNEA CORREGIDA ---
        // Creamos el banner en la posición que tenías originalmente.
        bannerView = new BannerView(adUnitIdBanner, AdSize.Banner, AdPosition.BottomRight);

        // Registramos los eventos antes de cargar el anuncio.
        ListenToBannerEvents();

        var adRequest = new AdRequest();
        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
    }

    private void ListenToBannerEvents()
    {
        // ... Tu código de eventos para el banner no necesita cambios ...
        // Raised when an ad is loaded into the banner view.
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // ... (etc. todos tus demás eventos de banner) ...
    }



    #endregion

    #region Interstitial Ads (NUEVO)

    /// <summary>
    /// Carga un anuncio Intersticial para tenerlo listo.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Si ya hay un anuncio cargado, no hacemos nada.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading Interstitial ad.");
        var adRequest = new AdRequest();

        // El método Load es estático. Se le pasa el ID, la petición y un callback.
        InterstitialAd.Load(adUnitIdInterstitial, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            // Si hay un error, lo registramos y salimos.
            if (error != null || ad == null)
            {
                Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
                return;
            }

            Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());

            // Si se carga correctamente, guardamos la referencia y registramos sus eventos.
            interstitialAd = ad;
            ListenToInterstitialEvents();
        });
    }

    /// <summary>
    /// Muestra el anuncio Intersticial si ya está cargado.
    /// </summary>
    public void ShowInterstitialAd()
    {
        // Solo intentamos mostrar el anuncio si existe (no es nulo) y si está listo (CanShowAd).
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    private void ListenToInterstitialEvents()
    {
        // Se dispara cuando se estima que el anuncio generó ingresos.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Se dispara cuando se registra una impresión del anuncio.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Se dispara cuando se hace clic en el anuncio.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // (LÍNEA CORREGIDA) Se dispara cuando el anuncio, ya cargado, falla en mostrarse.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to show full screen content with error: " + error.GetMessage());

            // BUENA PRÁCTICA: Si el anuncio falló en mostrarse, es probable que ya no sea válido.
            // Intentamos cargar uno nuevo inmediatamente.
            LoadInterstitialAd();
        };
        // Se dispara cuando el anuncio se muestra en pantalla completa.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Se dispara cuando el anuncio se cierra. ¡Este es el más importante!
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            // Después de que el usuario cierra un anuncio, cargamos el siguiente.
            LoadInterstitialAd();
        };
    }

    #endregion

    // (NUEVO) Limpieza al destruir el objeto
    private void OnDestroy()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }



    public static AdsControler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: para que no se destruya al cambiar de escena.
        }
    }

    void Update()
    {
        
    }
}
