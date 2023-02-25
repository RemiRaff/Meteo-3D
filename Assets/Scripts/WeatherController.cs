using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

using static FromCurrentForecastweather;

public class WeatherController : MonoBehaviour
{
    // [SerializeField] Pointable _pointable; // Permet de récupérer datas ou méthodes from script "Pointable"
    // Pointable _pointable;

    [Header("Point terrestre")]
    [SerializeField] public float latitude;
    [SerializeField] public float longitude;
    [SerializeField] string ville;

    //public string latitude = "43.700936"; // Nice, Alpes-Maritimes, France
    //public string longitude = "7.268391"; // Nice, Alpes-Maritimes, France

    [Header("UI Current Weather")]
    public TextMeshProUGUI country;
    public TextMeshProUGUI description;
    public TextMeshProUGUI feels_like;
    public TextMeshProUGUI humidity;
    public TextMeshProUGUI location;
    public TextMeshProUGUI mainWeather;
    public TextMeshProUGUI pressure;
    public TextMeshProUGUI temp;
    public TextMeshProUGUI temp_max;
    public TextMeshProUGUI temp_min;
    public TextMeshProUGUI visibility;
    public TextMeshProUGUI windspeed;
    public TextMeshProUGUI windsOrientation;

    [Header("UI Forecast 5 days Weather")]
    public TextMeshProUGUI country_bis;
    public TextMeshProUGUI description_bis;
    public TextMeshProUGUI feels_like_bis;
    public TextMeshProUGUI humidity_bis;
    public TextMeshProUGUI location_bis;
    public TextMeshProUGUI mainWeather_bis;
    public TextMeshProUGUI pressure_bis;
    public TextMeshProUGUI temp_bis;
    public TextMeshProUGUI temp_max_bis;
    public TextMeshProUGUI temp_min_bis;
    public TextMeshProUGUI visibility_bis;
    public TextMeshProUGUI windspeed_bis;
    public TextMeshProUGUI windsOrientation_bis;


    public bool Choix_CurrentWeather = true;
    public bool Choix_ForecastWeather5days = false;
    public bool Choix_LocationWeather = false;

    private const string URL_GetCurrentWeatherData = "http://api.openweathermap.org/data/2.5/weather";

    private TextMeshProUGUI statusText;

    // from "https://openweathermap.org/api/geocoding-api"
    private string URL_current_Forecast_weather = URL_GetCurrentWeatherData + "?lat={lat}&lon={lon}&lang={country}&appid={API key}";

    private string URL_fetch_By_location_name = "http://api.openweathermap.org/geo/1.0/direct?q={city name},{state code},{country code}&limit={limit}&appid={API key}";

    private string fetch_By_zip_post_code = "http://api.openweathermap.org/geo/1.0/zip?zip={zip code},{country code}&appid={API key}";

    // from "https://openweathermap.org/api/geocoding-api#reverse"
    private string reverse_geocoding = "http://api.openweathermap.org/geo/1.0/reverse?lat={lat}&lon={lon}&limit={limit}&appid={API key}";

    public string appID_API_key = "";

    private string cheminAppIdAPIkey, cheminJSON, jsonStrings, URL;

    // [Header("Open Weather")]
    // [SerializeField] private string key;

    private void Start()
    {
        UpdateWeatherData();
    }

    public void UpdateWeatherData()
    {
        if (Choix_CurrentWeather)
        {
            StartCoroutine(GetWeather_CurrentWeatherInformation()); // OK
        }
        else
        {
            if (!Choix_ForecastWeather5days)
            {
                // string URL_current_Forecast_weather = ;

            }
            else
            {
            }
        }
    }

    //public IEnumerator ReadAppIDKeyFile()
    //{
    //    // Lecture du contenu du fichier contenant la clé API. OK
    //    cheminAppIdAPIkey = Application.streamingAssetsPath + "/OpenWeatherAPI";
    //    Debug.Log("chemin OpenWeatherAPI = " + cheminJSON);
    //    string appID_API_key = File.ReadAllText(cheminAppIdAPIkey);
    //    Debug.Log("Lecture de la clé AppIdAPIkey : \n" + appID_API_key);
    //    yield return appID_API_key;
    //}

    //fetchWeatherRequest.Dispose();

    public IEnumerator GetWeather_CurrentWeatherInformation()
    {
        //string myappID_API_key = ReadAPIKey();
        //ReadAPIKey();
        //Debug.Log("MyKey is : \n" + ReadAPIKey());

        //_pointable = GameObject.Find("Manipulate Object").GetComponent<Pointable>();
        //Debug.Log("_pointable = " + _pointable);

        string weatherURL = URL_GetCurrentWeatherData;
        weatherURL += $"?lat={latitude}";
        weatherURL += $"&lon={longitude}";
        weatherURL += $"&lang=fr";
        weatherURL += $"&units=metric";
        weatherURL += $"&APPID={ReadAPIKey()}";
        Debug.Log("URL envoyée : " + weatherURL);

        // attempt to retrieve the geographic data
        using (UnityWebRequest request = UnityWebRequest.Get(weatherURL))
        {
            request.timeout = 1;
            yield return request.SendWebRequest();
            string[] pages = weatherURL.Split('/');
            int page = pages.Length - 1;

            // did the request succeed?
            if (request.result == UnityWebRequest.Result.Success)
            {
                {
                    string jsonResults = request.downloadHandler.text;
                    Debug.Log("From FetchWeatherDataFromApi function => \nSent requested URL : " + pages[page] + " \nReceived :  \n" + jsonResults);

                    // Ecriture du contenu re�u au format JSON dans un fichier, OK
                    cheminJSON = Application.streamingAssetsPath + "/current_Forecast_weather_received.json";
                    jsonStrings = jsonResults;
                    File.WriteAllText(cheminJSON, jsonStrings);

                    // Lecture du contenu reçu se trouvant dans le fichier précédemment créé, OK
                    jsonStrings = File.ReadAllText(cheminJSON);
                    Debug.Log("Lecture du fichier MyJSON_File.json : \n" + jsonStrings);

                    Rootobject myDeserializedClass = JsonUtility.FromJson<Rootobject>(jsonStrings);

                    //Debug.Log("FromCurrentForecastweather function, longitude = \n" + myDeserializedClass.coord.lon);
                    //Debug.Log("FromCurrentForecastweather function, latitude = \n" + myDeserializedClass.coord.lat);
                    //Debug.Log("FromCurrentForecastweather function, Weather ID = \n" + myDeserializedClass.weather[0].id);
                    //Debug.Log("FromCurrentForecastweather function, Weather main = \n" + myDeserializedClass.weather[0].main);
                    //Debug.Log("FromCurrentForecastweather function, Weather description = \n" + myDeserializedClass.weather[0].description);
                    //Debug.Log("FromCurrentForecastweather function, Weather Icon = \n" + myDeserializedClass.weather[0].icon);
                    //Debug.Log("FromCurrentForecastweather function, base = \n" + myDeserializedClass.@base);
                    //Debug.Log("FromCurrentForecastweather function, temp = \n" + myDeserializedClass.main.temp);
                    //Debug.Log("FromCurrentForecastweather function, feels Like = \n" + myDeserializedClass.main.feels_like);
                    //Debug.Log("FromCurrentForecastweather function, temp_min = \n" + myDeserializedClass.main.temp_min);
                    //Debug.Log("FromCurrentForecastweather function, temp_max = \n" + myDeserializedClass.main.temp_max);
                    //Debug.Log("FromCurrentForecastweather function, pressure = \n" + myDeserializedClass.main.pressure);
                    //Debug.Log("FromCurrentForecastweather function, humidity = \n" + myDeserializedClass.main.humidity);
                    //Debug.Log("FromCurrentForecastweather function, Visibilit� = \n" + myDeserializedClass.visibility);
                    //Debug.Log("FromCurrentForecastweather function, Wind Speed = \n" + myDeserializedClass.wind.speed);
                    //Debug.Log("FromCurrentForecastweather function, Wind deg = \n" + myDeserializedClass.wind.deg);
                    //Debug.Log("FromCurrentForecastweather function, Couverture Clouds = \n" + myDeserializedClass.clouds.all);
                    //Debug.Log("FromCurrentForecastweather function, Time of data calculation dt = \n" + myDeserializedClass.dt);
                    //Debug.Log("FromCurrentForecastweather function, sys Type = \n" + myDeserializedClass.sys.type);
                    //Debug.Log("FromCurrentForecastweather function, sys ID = \n" + myDeserializedClass.sys.id);
                    //Debug.Log("FromCurrentForecastweather function, Country code = \n" + myDeserializedClass.sys.country);
                    //Debug.Log("FromCurrentForecastweather function, sunrise = \n" + myDeserializedClass.sys.sunrise);
                    //Debug.Log("FromCurrentForecastweather function, sunset = \n" + myDeserializedClass.sys.sunset);
                    //Debug.Log("FromCurrentForecastweather function, Shift in seconds from UTC TimeZone = \n" + myDeserializedClass.timezone);
                    //Debug.Log("FromCurrentForecastweather function, ID = \n" + myDeserializedClass.id);
                    //Debug.Log("FromCurrentForecastweather function, location = \n" + myDeserializedClass.name);
                    //Debug.Log("FromCurrentForecastweather function, cod = \n" + myDeserializedClass.cod);

                    string getWeatherIcon = "http://openweathermap.org/img/w/" + myDeserializedClass.weather[0].icon + ".png";

                    description.text = "Météo actuelle: \n" + myDeserializedClass.weather[0].description;
                    //Debug.Log("Description.text = " + description.text);

                    temp.text = "Température actuelle :\n" + Mathf.Floor(myDeserializedClass.main.temp) + "C°";
                    //Debug.Log("temp.text = " + temp.text);

                    location.text = "Lieu :\n" + myDeserializedClass.name;
                    //Debug.Log("location.text = " + location.text);

                    country.text = "Pays :\n" + myDeserializedClass.sys.country;
                    //Debug.Log("country.text = " + country.text);

                    mainWeather.text = "2SEE :\n" + myDeserializedClass.weather[0].main;
                    //Debug.Log("mainWeather.text = " + mainWeather.text);

                    feels_like.text = "Température ressentie :\n" + myDeserializedClass.main.feels_like + " C°";
                    //Debug.Log("feels_like.text = " + feels_like.text);

                    temp_min.text = "Température mini :\n" + myDeserializedClass.main.temp_min + " C°";
                    //Debug.Log("temp_min.text = " + temp_min.text);

                    temp_max.text = "Température max :\n" + myDeserializedClass.main.temp_max + " C°";
                    //Debug.Log("temp_max.text = " + temp_max.text);

                    pressure.text = "Pression :\n" + myDeserializedClass.main.pressure + " hPa";
                    // Debug.Log("pressure.text = " + pressure.text);

                    humidity.text = "Humidité :\n" + myDeserializedClass.main.humidity + " %";
                    //Debug.Log("humidity.text = " + humidity.text);

                    windspeed.text = "Vitesse vent :\n" + myDeserializedClass.wind.speed + " Km/h";
                    //Debug.Log("windspeed.text = " + windspeed.text);

                    windsOrientation.text = "Orientation vent :\n" + myDeserializedClass.wind.deg + " degré";
                    //Debug.Log("windsOrientation.text = " + windsOrientation.text);

                    visibility.text = "Visibilité :\n" + myDeserializedClass.visibility + " mètres";
                    //Debug.Log("visibility.text = " + visibility.text);

                    //yield return appID_API_key;
                }
            }
            else
            {
                Debug.LogError($"Failed to get geographic data: {request.downloadHandler.text}");
                //Phase = EPhase.Failed;
            }
        }

        yield return null;
    }

    private string ReadAPIKey()
    {
        // Lecture du contenu du fichier contenant la clé API. OK
        cheminAppIdAPIkey = Application.streamingAssetsPath + "/OpenWeatherAPI";
        Debug.Log("chemin OpenWeatherAPI = " + cheminAppIdAPIkey);
        string appID_API_key = File.ReadAllText(cheminAppIdAPIkey);
        //Debug.Log("Lecture de la clé AppIdAPIkey : \n" + appID_API_key);
        return appID_API_key;
    }

    public void GetLatLong(Vector2 coordLatLong)
    {
        latitude = coordLatLong.x;
        longitude = coordLatLong.y;
    }

    //switch (fetchWeatherRequest.result)
    //{
    //    case UnityWebRequest.Result.ConnectionError:
    //    //Check and print error
    //    //statusText.text = fetchWeatherRequest.error;
    //    case UnityWebRequest.Result.DataProcessingError:
    //        //Debug.LogError(pages[page] + ": Error: " + fetchWeatherRequest.error);
    //        //Check and print error
    //        statusText.text = fetchWeatherRequest.error;
    //        break;
    //    case UnityWebRequest.Result.ProtocolError:
    //        //Debug.LogError(pages[page] + ": HTTP Error: " + fetchWeatherRequest.error);
    //        //Check and print error
    //        statusText.text = fetchWeatherRequest.error;
    //        break;
    //    case UnityWebRequest.Result.Success:
    //        {
    //            Debug.Log(fetchWeatherRequest.downloadHandler.text);
    //            var response = JSON.Parse(fetchWeatherRequest.downloadHandler.text);
    //            location.text = response["name"];
    //            country.text = response["country"];

    //            // mainWeather.text = response["weather"][0]["main"];
    //            description.text = response["weather"][0]["description"];
    //            temp.text = response["main"]["temp"] + " C";
    //            //feels_like.text = "Feels like " + response["main"]["feels_like"] + " C";
    //            temp_min.text = "Min is " + response["main"]["temp_min"] + " C";
    //            temp_max.text = "Max is " + response["main"]["temp_max"] + " C";
    //            pressure.text = "Pressure is " + response["main"]["pressure"] + " Pa";
    //            humidity.text = response["main"]["humidity"] + " % Humidity";
    //            windspeed.text = "Windspeed is " + response["wind"]["speed"] + " Km/h";
    //        }

    //Debug.Log(pages[page] + ":\nReceived: " + fetchWeatherRequest.downloadHandler.text);
    //break;
    //}
    //}

    //private void MyJSON_Deserialize()
    //{
    //    JSONNode weatherInfo = JSON.Parse(results.downloadHandler.text);

    //    currentWeatherText.text = "Current weather: " + weatherInfo["weather"][0]["description"];
    //    tempText.text = "Current temperature: " + Mathf.Floor(weatherInfo["main"][0]) + "�C";

    //    if (weatherInfo["weather"][0]["icon"] == "01d")
    //    {
    //        weatherController.ClearDay();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "01n")
    //    {
    //        weatherController.ClearNight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "02d")
    //    {
    //        weatherController.CloudCover();
    //        weatherController.ClearDay();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "02n")
    //    {
    //        weatherController.CloudCover();
    //        weatherController.ClearNight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "03d")
    //    {
    //        weatherController.CloudsDay();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "03n")
    //    {
    //        weatherController.CloudsNight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "10d")
    //    {
    //        weatherController.RainDay();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "10n")
    //    {
    //        weatherController.RainNight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "09n")
    //    {
    //        weatherController.CloudCover();
    //        weatherController.RainNightLight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "09d")
    //    {
    //        weatherController.CloudCover();
    //        weatherController.RainDayLight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "50d")
    //    {
    //        weatherController.MistDay();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "50n")
    //    {
    //        weatherController.MistNight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "13d")
    //    {
    //        weatherController.SnowDay();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "13n")
    //    {
    //        weatherController.SnowNight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "02d")
    //    {
    //        weatherController.CloudsDayLight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "02n")
    //    {
    //        weatherController.CloudsNightLight();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "04d")
    //    {
    //        weatherController.CloudsDayBroken();
    //    }
    //    else if (weatherInfo["weather"][0]["icon"] == "04n")
    //    {
    //        weatherController.CloudsNightBroken();
    //    }

    //    print(weatherInfo["weather"][0]["description"]);
    //    print(weatherInfo["weather"][0]["icon"]);
    //    print(weatherInfo["weather"][0]["main"]);
    //}
}