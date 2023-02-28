using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class ChoixMeteo : MonoBehaviour
{
    //public WeatherController _weatherController;
    //[SerializeField] WeatherController _weatherController; // Permet de récupérer datas ou méthodes from script "WeatherController"
    public string choosenMTO;

    ToggleGroup toggleGroup;
    //public GameObject ChoixMeteoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public void Submit()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        choosenMTO = toggle.GetComponentInChildren<Text>().text;
        Debug.Log("From ChoixMeteo.cs] La MTO choisie à partir du GameObject nommé '" + toggle.name + "' est : " + choosenMTO);
        switch (choosenMTO)
        {
            case "Météo actuelle":
                {
                    //toggle.gameObject.SetActive(false);
                    //_weatherController = GameObject.Find("UI Current Weather").GetComponent<WeatherController>();
                    Debug.Log("[From ChoixMeteo.cs] Choix in Submit() = " + choosenMTO);

                    //if
                    //Activer la page d'affichage des données météo reçues correspondantes.
                }
                break;
            case "Choix météo sur 5 jours":
                {
                    //Activer la page d'affichage des données météo reçues correspondantes.
                    Debug.Log("test" + toggle.name);
                }
                break;
            case "Météo Lieu Saisi":
                {
                    //Activer la page d'affichage des données météo reçues correspondantes.
                    Debug.Log("test" + toggle.name);
                }
                break;
        }
    }
}
