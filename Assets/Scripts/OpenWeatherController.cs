using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OpenWeatherController : MonoBehaviour
{
    [SerializeField] private string key;
    // Start is called before the first frame update
    void Start()
    {
        ReadKey();
        // call
        // https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={API key}
        // https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid={API key}
        // units: metric
        // lang: fr
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReadKey()
    {
        string json = File.ReadAllText(Application.dataPath + "/Data/OpenWeatherKey.json");
        OpenWeatherKey data = JsonUtility.FromJson<OpenWeatherKey>(json);
        key = data.key;
    }
}
