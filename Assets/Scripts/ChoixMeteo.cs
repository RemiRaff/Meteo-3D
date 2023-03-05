using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChoixMeteo : MonoBehaviour
{
    public WeatherController _weatherController;
    public string choosenMTO;
    public GameObject objectToFind1, objectToFind2; //, objectToFind3;
    private const string objectName1 = "UI Current Weather";
    private const string objectName2 = "UI Forecast  Meteo";
    //private const string objectName3 = "UI Typed Town Name";

    private ToggleGroup toggleGroup;

    // Start is called before the first frame update
    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    private void Update()
    {
    }

    private void Awake()
    {
        // On désactive les trois panneaux d'affichages latéraux.
        DeactivateUI();
    }

    private void DeactivateUI()
    {
        objectToFind1.gameObject.SetActive(false);
        objectToFind2.gameObject.SetActive(false);
        //objectToFind3.gameObject.SetActive(false);
    }

    private void Submit()
    {
        UnityEngine.UI.Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        choosenMTO = toggle.GetComponentInChildren<Text>().text;
        //Debug.Log("From ChoixMeteo.cs] La MTO choisie à partir du GameObject nommé '" + toggle.name + "' est : " + choosenMTO);
        DeactivateUI();
        switch (choosenMTO)
        {
            case "Météo actuelle":
                {
                    //Activer la page d'affichage des données météo reçues correspondantes.
                    objectToFind1.gameObject.SetActive(true);
                }
                break;

            case "Choix météo sur 5 jours":
                {
                    //Activer la page d'affichage des données météo reçues correspondantes.
                    objectToFind2.gameObject.SetActive(true);
                }
                break;

            //case "Météo Lieu Saisi": //Still WIP
            //    {
            //        //Activer la page d'affichage des données météo reçues correspondantes.
            //        objectToFind3.gameObject.SetActive(true);
            //    }
            //    break;
        }
    }
}