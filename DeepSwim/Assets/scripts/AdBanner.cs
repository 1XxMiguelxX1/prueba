using System;
using UnityEngine;
using GoogleMobileAds.Api;

// El nombre del archivo debe ser AdBannnerBehaviour.cs
public class AdBannnerBehaviour : MonoBehaviour 
{
    // For Banner
#if UNITY_ANDROID
    private string adUnitIdBanner = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
    private string adUnitIdBanner = "ca-app-pub-3940256099942544/2934735716";
#else
    private string adUnitIdBanner = "unused";
#endif

    private BannerView bannerView;

    void Start()
    {
        // No es necesario inicializar MobileAds aquí si ya lo haces en un script "Director" o "Manager" principal.
        // Si este es tu único script de anuncios, déjalo. Si tienes otro, es mejor centralizar la inicialización.
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            Debug.Log("Google Mobile Ads initialization complete.");
            CreateBannerView();
        });
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        bannerView = new BannerView(adUnitIdBanner, AdSize.Banner, AdPosition.BottomRight);

        // (AJUSTE IMPORTANTE) Llamamos al método para registrar los eventos.
        ListenToAdEvents();

        var adRequest = new AdRequest();
        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
    }

    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : " + bannerView.GetResponseInfo());
        };

        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : " + error);
        };

        // ... aquí van el resto de tus eventos (OnAdClicked, etc.) ...
    }

    // Es buena práctica limpiar el banner cuando el objeto se destruye.
    private void OnDestroy()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
}