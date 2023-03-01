using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class ChoixMeteo : MonoBehaviour
{
    public WeatherController _weatherController;
    //[SerializeField] WeatherController _weatherController; // Permet de récupérer datas ou méthodes from script "WeatherController"
    public string choosenMTO;
    public GameObject objectToFind1, objectToFind2;
    private const string objectName1 = "UI Current Weather";
    private const string objectName2 = "UI Forecast  Meteo";

    ToggleGroup toggleGroup;
    //[SerializeField] private Canvas ChoixMeteoPrefab;

    //public GameObject ChoixMeteoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    private void Update()
    {
        //switch (objectToFind1.name)
        //{
        //    case objectName1:
        //        {
        //            objectToFind1.gameObject.SetActive(false);
        //            print("objectToFind1 name is : " + objectToFind1.name);
        //        }
        //        break;
        //    case objectName2:
        //        {
        //            objectToFind1.gameObject.SetActive(false);
        //            print("objectToFind2 name is : " + objectToFind2.name);
        //        }
        //        break;
        //}
    }

    private void Awake()
    {
        // On désactive les deux panneaux d'affichages.
        DeactivatebothUI();
    }

    private void DeactivatebothUI()
    {
        objectToFind1.gameObject.SetActive(false);
        objectToFind2.gameObject.SetActive(false);
    }

    private void Submit()
    {
        UnityEngine.UI.Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        choosenMTO = toggle.GetComponentInChildren<Text>().text;
        Debug.Log("From ChoixMeteo.cs] La MTO choisie à partir du GameObject nommé '" + toggle.name + "' est : " + choosenMTO);
        DeactivatebothUI();
        switch (choosenMTO)
        {
            case "Météo actuelle":
                {
                    objectToFind1.gameObject.SetActive(true);
                    Debug.Log("[From ChoixMeteo.cs] Choix in Submit() = " + choosenMTO);

                }
                break;
            case "Choix météo sur 5 jours":
                {
                    objectToFind2.gameObject.SetActive(true);
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
