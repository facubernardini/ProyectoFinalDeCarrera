using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using System.Globalization;

public class LocaleSelector : MonoBehaviour
{
    public Button spanish, english, french, german;
    private bool active = false;

    void Start()
    {
        int deviceLanguage = ObtenerIdiomaDispositivo();
        int ID = PlayerPrefs.GetInt("LocaleKey", deviceLanguage);

        ChangeLocale(ID);
        ChangeIconLanguage(ID);
    }

    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;

        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        active = false;
    }

    private void ChangeIconLanguage(int id) 
    {
        if (id == 0)
            english.onClick.Invoke();

        if (id == 1)
            french.onClick.Invoke();  

        if (id == 2)
            german.onClick.Invoke();

        if (id == 3)
            spanish.onClick.Invoke();
    }

    private int ObtenerIdiomaDispositivo()
    {
        string lenguaje = CultureInfo.CurrentCulture.Name.Split('-')[0];

        if (lenguaje == "en")
            return 0;
        
        if (lenguaje == "fr")
            return 1;

        if (lenguaje == "de")
            return 2;

        if (lenguaje == "es")
            return 3;

        return 0;
    }
}