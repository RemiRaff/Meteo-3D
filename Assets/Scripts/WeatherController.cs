using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using File = System.IO.File;
using Unity.VisualScripting;
using static System.Net.WebRequestMethods;

public class WeatherController : MonoBehaviour
{
    // [SerializeField] Pointable _pointable; // Permet de récupérer datas ou méthodes from script "Pointable"
    //Pointable _pointable;

    [Header("Point terrestre")]
    [SerializeField] public float latitude = 43.700936f;
    //[SerializeField] public float latitude;
    [SerializeField] public float longitude = 7.268391f;
    //[SerializeField] public float longitude;
    [SerializeField] string ville, cityName, stateCode, countryCode, limit;

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
    public TextMeshProUGUI temp_min;
    public TextMeshProUGUI temp_max;
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
    public TextMeshProUGUI temp_min_bis;
    public TextMeshProUGUI temp_max_bis;
    public TextMeshProUGUI visibility_bis;
    public TextMeshProUGUI windspeed_bis;
    public TextMeshProUGUI windsOrientation_bis;

    public bool Choix_CurrentWeather = false;
    public bool Choix_ForecastWeather5days = true;
    public bool Choix_LocationWeather = false;
    public string appID_API_key = "", weatherURL;

    private TextMeshProUGUI statusText;
    private const string URL_GetCurrentWeatherData = "http://api.openweathermap.org/data/2.5/weather";
    private const string URL_ForecastWeather5days = "http://api.openweathermap.org/data/2.5/forecast?lat=";
    private const string URL_fetch_Coordinates_by_location_name = "http://api.openweathermap.org/geo/1.0/direct?q=";
    private string fetch_By_zip_post_code = "http://api.openweathermap.org/geo/1.0/zip?zip={zip code},{country code}&appid={API key}";
    //string getWeatherIcon = "http://openweathermap.org/img/w/" + myDeserializedClass.weather[0].icon + ".png";

    // from "https://openweathermap.org/api/geocoding-api#reverse"
    private string reverse_geocoding = "http://api.openweathermap.org/geo/1.0/reverse?lat={lat}&lon={lon}&limit={limit}&appid={API key}";

    private string cheminAppIdAPIkey, cheminJSON, jsonStrings;
    private int timestamps = 3;

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
            weatherURL = URL_GetCurrentWeatherData;
            weatherURL += $"?lat={latitude}";
            weatherURL += $"&lon={longitude}";
            weatherURL += $"&lang=fr";
            weatherURL += $"&units=metric";
            weatherURL += $"&appid={ReadAPIKey()}";
            Debug.Log("URL à envoyer Choix_CurrentWeather : " + weatherURL);

            StartCoroutine(GetWeather_Informations(weatherURL, Choix_CurrentWeather)); // OK
        }
        else
        {
            if (Choix_ForecastWeather5days)
            {
                // 5days forecast : http://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&cnt=3&appid={API key}
                string weatherURL = URL_ForecastWeather5days;
                weatherURL += $"{latitude}";
                weatherURL += $"&lon={longitude}";
                weatherURL += $"&lang=fr";
                weatherURL += $"&units=metric";
                weatherURL += $"&cnt={timestamps}";
                weatherURL += $"&appid={ReadAPIKey()}";
                Debug.Log("URL à envoyer Choix_ForecastWeather5days : " + weatherURL);

                StartCoroutine(GetWeather_Informations(weatherURL, Choix_ForecastWeather5days)); // OK
            }
            else
            {
                if (Choix_LocationWeather)
                {
                    cityName = "Paris"; // test
                    countryCode = "fr"; // test
                    limit = "5"; // test
                    string weatherURL = URL_fetch_Coordinates_by_location_name;
                    weatherURL += $"{cityName}";
                    //weatherURL += $",{stateCode}";
                    //weatherURL += $",{countryCode}";
                    weatherURL += $"&limit={limit}";
                    weatherURL += $"&appid={ReadAPIKey()}";
                    Debug.Log("URL à envoyer Choix_LocationWeather : " + weatherURL);

                    StartCoroutine(GetWeather_Informations(weatherURL, Choix_LocationWeather)); // OK
                }
            }
        }
    }

    //fetchWeatherRequest.Dispose();

    public IEnumerator GetWeather_Informations(string url2use, bool choix)
    {
        //_pointable = GameObject.Find("Manipulate Object").GetComponent<Pointable>();
        //Debug.Log("_pointable = " + _pointable);

        Debug.Log("Inside GetWeather_Informations, URL à envoyer : url2use = " + url2use);

        // attempt to retrieve the geographic data
        using (UnityWebRequest request = UnityWebRequest.Get(url2use))
        {
            request.timeout = 1;
            yield return request.SendWebRequest();
            string[] pages = url2use.Split('/');
            int page = pages.Length - 1;

            // did the request succeed?
            if (request.result == UnityWebRequest.Result.Success)
            {
                {
                    string jsonResults = request.downloadHandler.text;
                    Debug.Log("From GetWeather_Informations => \nSent requested URL : " + pages[page] + " \nReceived :  \n" + jsonResults);

                    // Ecriture du contenu re�u au format JSON dans un fichier, OK
                    cheminJSON = Application.streamingAssetsPath + "/current_Forecast_weather_received.json";
                    jsonStrings = jsonResults;
                    File.WriteAllText(cheminJSON, jsonStrings);

                    // Lecture du contenu reçu se trouvant dans le fichier précédemment créé, OK
                    jsonStrings = File.ReadAllText(cheminJSON);
                    Debug.Log("Lecture du fichier MyJSON_File.json : \n" + jsonStrings);
                }
            }
            else
            {
                Debug.LogError($"Failed to get geographic data: {request.downloadHandler.text}");
                //Phase = EPhase.Failed;
            }

            if (choix == Choix_CurrentWeather)
            {
                Debug.Log("infos Choix_CurrentWeather : " + choix);
                Choix_CurrentWeather.Rootobject CurrentWeatherClass = JsonUtility.FromJson<Choix_CurrentWeather.Rootobject>(jsonStrings);
                //Debug.Log("FromCurrentForecastweather function, longitude = \n" + CurrentWeatherClass.coord.lon);
                //Debug.Log("FromCurrentForecastweather function, latitude = \n" + CurrentWeatherClass.coord.lat);
                //Debug.Log("FromCurrentForecastweather function, Weather ID = \n" + CurrentWeatherClass.weather[0].id);
                //Debug.Log("FromCurrentForecastweather function, Weather main = \n" + CurrentWeatherClass.weather[0].main);
                //Debug.Log("FromCurrentForecastweather function, Weather description = \n" + CurrentWeatherClass.weather[0].description);
                //Debug.Log("FromCurrentForecastweather function, Weather Icon = \n" + CurrentWeatherClass.weather[0].icon);
                //Debug.Log("FromCurrentForecastweather function, base = \n" + CurrentWeatherClass.@base);
                //Debug.Log("FromCurrentForecastweather function, temp = \n" + CurrentWeatherClass.main.temp);
                //Debug.Log("FromCurrentForecastweather function, feels Like = \n" + CurrentWeatherClass.main.feels_like);
                //Debug.Log("FromCurrentForecastweather function, temp_min = \n" + CurrentWeatherClass.main.temp_min);
                //Debug.Log("FromCurrentForecastweather function, temp_max = \n" + CurrentWeatherClass.main.temp_max);
                //Debug.Log("FromCurrentForecastweather function, pressure = \n" + CurrentWeatherClass.main.pressure);
                //Debug.Log("FromCurrentForecastweather function, humidity = \n" + CurrentWeatherClass.main.humidity);
                //Debug.Log("FromCurrentForecastweather function, Visibilit� = \n" + CurrentWeatherClass.visibility);
                //Debug.Log("FromCurrentForecastweather function, Wind Speed = \n" + CurrentWeatherClass.wind.speed);
                //Debug.Log("FromCurrentForecastweather function, Wind deg = \n" + CurrentWeatherClass.wind.deg);
                //Debug.Log("FromCurrentForecastweather function, Couverture Clouds = \n" + CurrentWeatherClass.clouds.all);
                //Debug.Log("FromCurrentForecastweather function, Time of data calculation dt = \n" + CurrentWeatherClass.dt);
                //Debug.Log("FromCurrentForecastweather function, sys Type = \n" + CurrentWeatherClass.sys.type);
                //Debug.Log("FromCurrentForecastweather function, sys ID = \n" + CurrentWeatherClass.sys.id);
                //Debug.Log("FromCurrentForecastweather function, Country code = \n" + CurrentWeatherClass.sys.country);
                //Debug.Log("FromCurrentForecastweather function, sunrise = \n" + CurrentWeatherClass.sys.sunrise);
                //Debug.Log("FromCurrentForecastweather function, sunset = \n" + CurrentWeatherClass.sys.sunset);
                //Debug.Log("FromCurrentForecastweather function, Shift in seconds from UTC TimeZone = \n" + CurrentWeatherClass.timezone);
                //Debug.Log("FromCurrentForecastweather function, ID = \n" + CurrentWeatherClass.id);
                //Debug.Log("FromCurrentForecastweather function, location = \n" + CurrentWeatherClass.name);
                //Debug.Log("FromCurrentForecastweather function, cod = \n" + CurrentWeatherClass.cod);

                description.text = "Météo actuelle: \n" + CurrentWeatherClass.weather[0].description;
                temp.text = "Température actuelle :\n" + Mathf.Floor(CurrentWeatherClass.main.temp) + "C°";
                location.text = "Lieu :\n" + CurrentWeatherClass.name;
                country.text = "Pays :\n" + CurrentWeatherClass.sys.country;
                mainWeather.text = "A définir :\n" + CurrentWeatherClass.weather[0].main;
                feels_like.text = "Température ressentie :\n" + CurrentWeatherClass.main.feels_like + " C°";
                temp_min.text = "Température mini :\n" + CurrentWeatherClass.main.temp_min + " C°";
                temp_max.text = "Température max :\n" + CurrentWeatherClass.main.temp_max + " C°";
                pressure.text = "Pression :\n" + CurrentWeatherClass.main.pressure + " hPa";
                humidity.text = "Humidité :\n" + CurrentWeatherClass.main.humidity + " %";
                windspeed.text = "Vitesse vent :\n" + CurrentWeatherClass.wind.speed + " Km/h";
                windsOrientation.text = "Orientation vent :\n" + CurrentWeatherClass.wind.deg + " degré";
                visibility.text = "Visibilité :\n" + CurrentWeatherClass.visibility + " mètres";
            }
            else
            {
                if (choix == Choix_ForecastWeather5days)
                {
                    Debug.Log("infos Choix_ForecastWeather5days : " + choix);
                    ChoixForecast5Days.Rootobject ForecastWeather5daysClass = JsonUtility.FromJson<ChoixForecast5Days.Rootobject>(jsonStrings);
                    Debug.Log("FromCurrentForecastweather, Internal parameter = \n" + ForecastWeather5daysClass.cod);
                    Debug.Log("FromCurrentForecastweather, Internal message = \n" + ForecastWeather5daysClass);
                    Debug.Log("FromCurrentForecastweather, Cnt (timestamp returned by the API = \n" + ForecastWeather5daysClass.cnt);
                    Debug.Log("FromCurrentForecastweather, Time of data forecasted, unix, UTC = \n" + ForecastWeather5daysClass.list[0].dt);
                    Debug.Log("FromCurrentForecastweather, Temperature = \n" + ForecastWeather5daysClass.list[0].main.temp);
                    Debug.Log("FromCurrentForecastweather, human perception of weather = \n" + ForecastWeather5daysClass.list[0].main.feels_like);
                    Debug.Log("FromCurrentForecastweather, Minimum temperature at the moment of calculation = \n" + ForecastWeather5daysClass.list[0].main.temp_min);
                    Debug.Log("FromCurrentForecastweather, Maximum temperature at the moment of calculation = \n" + ForecastWeather5daysClass.list[0].main.temp_max);
                    Debug.Log("FromCurrentForecastweather, Atmospheric pressure on the sea level by default, hPa = \n" + ForecastWeather5daysClass.list[0].main.pressure);
                    Debug.Log("FromCurrentForecastweather, Atmospheric pressure on the sea level, hPa = \n" + ForecastWeather5daysClass.list[0].main.sea_level);
                    Debug.Log("FromCurrentForecastweather, Atmospheric pressure on the ground level, hPa = \n" + ForecastWeather5daysClass.list[0].main.grnd_level);
                    Debug.Log("FromCurrentForecastweather, Humidity, % = \n" + ForecastWeather5daysClass.list[0].main.humidity);
                    Debug.Log("FromCurrentForecastweather, Temp_kf (Internal parameter) = \n" + ForecastWeather5daysClass.list[0].main.temp_kf);
                    Debug.Log("FromCurrentForecastweather, Weather condition id = \n" + ForecastWeather5daysClass.list[0].weather[0].id);
                    Debug.Log("FromCurrentForecastweather, Group of weather parameters (Rain, Snow, Extreme etc.) = \n" + ForecastWeather5daysClass.list[0].weather[0].main);
                    Debug.Log("FromCurrentForecastweather, Weather condition within the group = \n" + ForecastWeather5daysClass.list[0].weather[0].description);
                    Debug.Log("FromCurrentForecastweather, Weather icon id = \n" + ForecastWeather5daysClass.list[0].weather[0].icon);
                    Debug.Log("FromCurrentForecastweather, Cloudiness, % = \n" + ForecastWeather5daysClass.list[0].clouds.all);
                    Debug.Log("FromCurrentForecastweather, Wind speed = \n" + ForecastWeather5daysClass.list[0].wind.speed);
                    Debug.Log("FromCurrentForecastweather, Wind direction, degrees (meteorological) = \n" + ForecastWeather5daysClass.list[0].wind.deg);
                    Debug.Log("FromCurrentForecastweather, Wind gust = \n" + ForecastWeather5daysClass.list[0].wind.gust);
                    Debug.Log("FromCurrentForecastweather, Average visibility. The maximum value of the visibility is 10km = \n" + ForecastWeather5daysClass.list[0].visibility);
                    Debug.Log("FromCurrentForecastweather, Probability of precipitation.\nThe values of the parameter vary\nbetween 0 and 1, \nwhere 0 is equal to 0%, 1 is equal to 100% = \n" + ForecastWeather5daysClass.list[0].pop);
                    Debug.Log("FromCurrentForecastweather, Rain volume for last 3 hours, mm = \n" + ForecastWeather5daysClass.list[0].rain._3h);
                    Debug.Log("FromCurrentForecastweather, Part of the day (n - night, d - day)  = \n" + ForecastWeather5daysClass.list[0].sys.pod);
                    Debug.Log("FromCurrentForecastweather, Time of data forecasted, ISO, UTC  = \n" + ForecastWeather5daysClass.list[0].dt_txt);
                    Debug.Log("FromCurrentForecastweather, City ID  = \n" + ForecastWeather5daysClass.city.id);
                    Debug.Log("FromCurrentForecastweather, City name.\nPlease note that built-in geocoder\nfunctionality has been deprecated = \n" + ForecastWeather5daysClass.city.name);
                    Debug.Log("FromCurrentForecastweather, City geo location, latitude = \n" + ForecastWeather5daysClass.city);
                    Debug.Log("FromCurrentForecastweather, Country code (GB, JP etc.) = \n" + ForecastWeather5daysClass.city.country);
                    Debug.Log("FromCurrentForecastweather, City population = \n" + ForecastWeather5daysClass.city.population);
                    Debug.Log("FromCurrentForecastweather, Shift in seconds from UTC = \n" + ForecastWeather5daysClass.city.timezone);
                    Debug.Log("FromCurrentForecastweather, Sunrise time, Unix, UTC = \n" + ForecastWeather5daysClass.city.sunrise);
                    Debug.Log("FromCurrentForecastweather, Sunset time, Unix, UTC = \n" + ForecastWeather5daysClass.city.sunset);
                }
            }
            {
                if (choix == Choix_LocationWeather)
                {
                    Debug.Log("infos Choix_LocationWeather : " + choix);
                    Choix_LocationWeather.Rootobject LocationWeatherClass = JsonUtility.FromJson<Choix_LocationWeather.Rootobject>("{\"Property1\":" + jsonStrings + "}");
                    //Root myDeserializedClass3 = JsonUtility.FromJson<Root>(jsonStrings);
                    Debug.Log("FromCurrentForecastweather, nom du lieu saisi = \n" + LocationWeatherClass.Property1[0].name);
                    Debug.Log("FromCurrentForecastweather, latitude du lieu saisi = \n" + LocationWeatherClass.Property1[0].lat);
                    Debug.Log("FromCurrentForecastweather, longitude du lieu saisi = \n" + LocationWeatherClass.Property1[0].lon);
                    Debug.Log("FromCurrentForecastweather, Pays du lieu saisi = \n" + LocationWeatherClass.Property1[0].country);
                    Debug.Log("FromCurrentForecastweather, l'état du lieu saisi = \n" + LocationWeatherClass.Property1[0].local_names);
                }

            }
        }
    }

    private string ReadAPIKey()
    {
        // Lecture du contenu du fichier contenant la clé API. OK
        cheminAppIdAPIkey = Application.streamingAssetsPath + "/OpenWeatherAPI";
        //Debug.Log("chemin OpenWeatherAPI = " + cheminAppIdAPIkey);
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