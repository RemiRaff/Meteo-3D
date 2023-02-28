using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Button = UnityEngine.UI.Button;
using File = System.IO.File;

public class WeatherController : MonoBehaviour
{
    //[SerializeField] Pointable _pointable; // Permet de récupérer datas ou méthodes from script "Pointable"
    ChoixMeteo _choixMeteo;

    [Header("Coordonnées géographiques par défaut")]
    [SerializeField] public float latitude = 43.700936f; // Paris
    [SerializeField] public float longitude = 7.268391f; // Paris

    [SerializeField] private string ville, cityName, stateCode, countryCode, limit;

    [Header("UI Current Weather")]
    public TextMeshProUGUI coord_lon;
    public TextMeshProUGUI coord_lat;
    public TextMeshProUGUI weather_id;
    public TextMeshProUGUI weather_main;
    public TextMeshProUGUI weather_description;
    public TextMeshProUGUI weather_icon;
    public TextMeshProUGUI @base;
    public TextMeshProUGUI main_temp;
    public TextMeshProUGUI main_feels_like;
    public TextMeshProUGUI main_pressure;
    public TextMeshProUGUI main_humidity;
    public TextMeshProUGUI main_temp_min;
    public TextMeshProUGUI main_temp_max;
    public TextMeshProUGUI main_sea_level;
    public TextMeshProUGUI main_grnd_level;
    public TextMeshProUGUI visibility;
    public TextMeshProUGUI wind_speed;
    public TextMeshProUGUI wind_deg;
    public TextMeshProUGUI wind_gust;
    public TextMeshProUGUI clouds_all;
    public TextMeshProUGUI rain_1h;
    public TextMeshProUGUI rain_3h;
    public TextMeshProUGUI snow_1h;
    public TextMeshProUGUI snow_3h;
    public TextMeshProUGUI dt;
    public TextMeshProUGUI sys_type;
    public TextMeshProUGUI sys_id;
    public TextMeshProUGUI sys_message;
    public TextMeshProUGUI sys_country;
    public TextMeshProUGUI sys_sunrise;
    public TextMeshProUGUI sys_sunset;
    public TextMeshProUGUI timezone;
    public TextMeshProUGUI id;
    public TextMeshProUGUI Cityname;
    public TextMeshProUGUI cod;

    [Header("UI Forecast 5 days Weather")]
    public TextMeshProUGUI cod_Internal_parameter;
    public TextMeshProUGUI message_Internal_parameter;
    public TextMeshProUGUI cnt_number_timestamps;
    public TextMeshProUGUI listdt_Time_of_data_forecasted;
    public TextMeshProUGUI list_main_temp;
    public TextMeshProUGUI list_main_feels_like;
    public TextMeshProUGUI list_main_temp_min;
    public TextMeshProUGUI list_main_temp_max;
    public TextMeshProUGUI list_main_pressure;
    public TextMeshProUGUI list_main_sea_level;
    public TextMeshProUGUI list_main_grnd_level;
    public TextMeshProUGUI list_main_humidity;
    public TextMeshProUGUI list_main_temp_kf;
    public TextMeshProUGUI list_weather_id;
    public TextMeshProUGUI list_weather_main;
    public TextMeshProUGUI list_weather_description;
    public TextMeshProUGUI list_weather_icon;
    public TextMeshProUGUI list_clouds_all;
    public TextMeshProUGUI list_wind_speed;
    public TextMeshProUGUI list_wind_deg;
    public TextMeshProUGUI list_wind_gust;
    public TextMeshProUGUI list_visibility;
    public TextMeshProUGUI list_pop;
    public TextMeshProUGUI list_rain_3h;
    public TextMeshProUGUI list_snow_3h;
    public TextMeshProUGUI list_sys_pod;
    public TextMeshProUGUI list_dt_txt;
    public TextMeshProUGUI city_id;
    public TextMeshProUGUI city_name;
    public TextMeshProUGUI city_coord_lat;
    public TextMeshProUGUI city_coord_lon;
    public TextMeshProUGUI city_country;
    public TextMeshProUGUI city_population;
    public TextMeshProUGUI city_timezone;
    public TextMeshProUGUI city_sunrise;
    public TextMeshProUGUI city_sunset;

    [Header("UI Location Weather")]
    public TextMeshProUGUI foundLocationName;
    public TextMeshProUGUI local_names_Language_Code;
    public TextMeshProUGUI local_names_ascii;
    public TextMeshProUGUI local_names_feature_name;
    public TextMeshProUGUI lat;
    public TextMeshProUGUI lon;
    public TextMeshProUGUI country;
    public TextMeshProUGUI state;

    public string appID_API_key = "", weatherURL = "";

    private TextMeshProUGUI statusText;
    private const string URL_GetCurrentWeatherData = "http://api.openweathermap.org/data/2.5/weather";
    private const string URL_ForecastWeather5days = "http://api.openweathermap.org/data/2.5/forecast?lat=";
    private const string URL_fetch_Coordinates_by_location_name = "http://api.openweathermap.org/geo/1.0/direct?q=";
    //private string fetch_By_zip_post_code = "http://api.openweathermap.org/geo/1.0/zip?zip={zip code},{country code}&appid={API key}";
    //string getWeatherIcon = "http://openweathermap.org/img/w/" + myDeserializedClass.weather[0].icon + ".png";

    // from "https://openweathermap.org/api/geocoding-api#reverse"
    //private string reverse_geocoding = "http://api.openweathermap.org/geo/1.0/reverse?lat={lat}&lon={lon}&limit={limit}&appid={API key}";

    private string cheminAppIdAPIkey, cheminJSON, jsonStrings;
    private int timestamps = 3;
    private Button boutonValidez;
    private UnityEngine.UI.Toggle toggleChoixMétéoactuelle, toggleChoixmétéo5jours;

    private void Awake()
    {
        // Récupération du choix de type de Météo choisie "Locale ou prévisionelle sur 5 jours" en provenance du script ChoixMeteo.cs
        _choixMeteo = GameObject.Find("Choix Météo").GetComponent<ChoixMeteo>();
    }
    private void Start()
    {
        boutonValidez = GameObject.Find("Validation saisie (Parse JSON Infos) Button").GetComponent<Button>();
        boutonValidez.onClick.AddListener(BoutonValidez);
    }

    private void BoutonValidez()
    {
        //Debug.Log("Button 'Validez' On " + boutonValidez.name);
        Debug.Log("From BoutonValidez, _choixMeteo = " + _choixMeteo.choosenMTO);
        ConstructURL2Send();
    }

    private void OnDisable()
    {
        boutonValidez.onClick.RemoveListener(BoutonValidez);
        Debug.Log("RemovedListener(BoutonValidez) désactivé");
    }

    private void ConstructURL2Send()
    {
        switch (_choixMeteo.choosenMTO)
        {
            case "Météo actuelle":
                {
                    weatherURL = URL_GetCurrentWeatherData;
                    weatherURL += $"?lat={latitude}";
                    weatherURL += $"&lon={longitude}";
                    weatherURL += $"&lang=fr";
                    weatherURL += $"&units=metric";
                    weatherURL += $"&appid={ReadAPIKey()}";
                    Debug.Log("URL à envoyer '" + _choixMeteo.choosenMTO + " :" + weatherURL);

                    StartCoroutine(GetWeather_Informations(weatherURL));
                }
                break;
            case "Météo sur 5 jours":
                {
                    string weatherURL = URL_ForecastWeather5days;
                    weatherURL += $"{latitude}";
                    weatherURL += $"&lon={longitude}";
                    weatherURL += $"&lang=fr";
                    weatherURL += $"&units=metric";
                    weatherURL += $"&cnt={timestamps}";
                    weatherURL += $"&appid={ReadAPIKey()}";
                    Debug.Log("URL à envoyer Choix_ForecastWeather5days : " + weatherURL);

                    StartCoroutine(GetWeather_Informations(weatherURL));
                }
                break;
            //case "Météo Lieu Saisi":
            //    {
            //        string weatherURL = URL_ForecastWeather5days;
            //        weatherURL += $"{latitude}";
            //        weatherURL += $"&lon={longitude}";
            //        weatherURL += $"&lang=fr";
            //        weatherURL += $"&units=metric";
            //        weatherURL += $"&cnt={timestamps}";
            //        weatherURL += $"&appid={ReadAPIKey()}";
            //        Debug.Log("URL à envoyer Choix_ForecastWeather5days : " + weatherURL);

            //        StartCoroutine(GetWeather_Informations(weatherURL));
            //    }
            //    break;
        }
    }

    //fetchWeatherRequest.Dispose();

    private IEnumerator GetWeather_Informations(string url2use)
    {
        Debug.Log("Inside GetWeather_Informations, URL à envoyer : url2use = " + url2use);

        // attempt to retrieve the Weather data
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

                    // Ecriture du contenu reçu au format JSON dans un fichier, OK
                    cheminJSON = Application.streamingAssetsPath + "/current_Forecast_weather_received.json";
                    jsonStrings = jsonResults;
                    File.WriteAllText(cheminJSON, jsonStrings);

                    // Lecture du contenu reçu se trouvant dans le fichier précédemment créé, OK
                    jsonStrings = File.ReadAllText(cheminJSON);
                    Debug.Log("Lecture du fichier MyJSON_File.json : \n" + jsonStrings);
                    DisplayMTO();
                }
            }
            else
            {
                Debug.LogError($"Failed to get geographic data: {request.downloadHandler.text}");
            }
        }
    }

    private void DisplayMTO()
    {
        switch (_choixMeteo.choosenMTO)
        {
            case "Météo actuelle":
                {
                    //Debug.Log("infos Choix_CurrentWeather : " + choix);
                    Choix_CurrentWeather.Rootobject CurrentWeatherClass = JsonUtility.FromJson<Choix_CurrentWeather.Rootobject>(jsonStrings);
                    //Debug.Log("Choix_CurrentWeather function, longitude = \n" + CurrentWeatherClass.coord.lon);
                    //Debug.Log("Choix_CurrentWeather function, latitude = \n" + CurrentWeatherClass.coord.lat);
                    //Debug.Log("Choix_CurrentWeather function, Weather ID = \n" + CurrentWeatherClass.weather[0].id);
                    //Debug.Log("Choix_CurrentWeather function, Weather main = \n" + CurrentWeatherClass.weather[0].main);
                    //Debug.Log("Choix_CurrentWeather function, Weather description = \n" + CurrentWeatherClass.weather[0].description);
                    //Debug.Log("Choix_CurrentWeather function, Weather Icon = \n" + CurrentWeatherClass.weather[0].icon);
                    //Debug.Log("Choix_CurrentWeather function, base = \n" + CurrentWeatherClass.@base);
                    //Debug.Log("Choix_CurrentWeather function, temp = \n" + CurrentWeatherClass.main.temp);
                    //Debug.Log("Choix_CurrentWeather function, feels Like = \n" + CurrentWeatherClass.main.feels_like);
                    //Debug.Log("Choix_CurrentWeather function, temp_min = \n" + CurrentWeatherClass.main.temp_min);
                    //Debug.Log("Choix_CurrentWeather function, temp_max = \n" + CurrentWeatherClass.main.temp_max);
                    //Debug.Log("Choix_CurrentWeather function, pressure = \n" + CurrentWeatherClass.main.pressure);
                    //Debug.Log("Choix_CurrentWeather function, humidity = \n" + CurrentWeatherClass.main.humidity);
                    //Debug.Log("Choix_CurrentWeather function, Visibilit� = \n" + CurrentWeatherClass.visibility);
                    //Debug.Log("Choix_CurrentWeather function, Wind Speed = \n" + CurrentWeatherClass.wind.speed);
                    //Debug.Log("Choix_CurrentWeather function, Wind deg = \n" + CurrentWeatherClass.wind.deg);
                    //Debug.Log("Choix_CurrentWeather function, Couverture Clouds = \n" + CurrentWeatherClass.clouds.all);
                    //Debug.Log("Choix_CurrentWeather function, Time of data calculation dt = \n" + CurrentWeatherClass.dt);
                    //Debug.Log("Choix_CurrentWeather function, sys Type = \n" + CurrentWeatherClass.sys.type);
                    //Debug.Log("Choix_CurrentWeather function, sys ID = \n" + CurrentWeatherClass.sys.id);
                    //Debug.Log("Choix_CurrentWeather function, Country code = \n" + CurrentWeatherClass.sys.country);
                    //Debug.Log("Choix_CurrentWeather function, sunrise = \n" + CurrentWeatherClass.sys.sunrise);
                    //Debug.Log("Choix_CurrentWeather function, sunset = \n" + CurrentWeatherClass.sys.sunset);
                    //Debug.Log("Choix_CurrentWeather function, Shift in seconds from UTC TimeZone = \n" + CurrentWeatherClass.timezone);
                    //Debug.Log("Choix_CurrentWeather function, ID = \n" + CurrentWeatherClass.id);
                    //Debug.Log("Choix_CurrentWeather function, location = \n" + CurrentWeatherClass.name);
                    //Debug.Log("Choix_CurrentWeather function, cod = \n" + CurrentWeatherClass.cod);

                    weather_description.text = "Météo actuelle: \n" + CurrentWeatherClass.weather[0].description;
                    main_temp.text = "Température actuelle :\n" + Mathf.Floor(CurrentWeatherClass.main.temp) + "C°";
                    visibility.text = "Visibilité :\n" + (CurrentWeatherClass.visibility) + "mètres";
                    main_feels_like.text = "Température ressentie :\n" + CurrentWeatherClass.main.feels_like + " C°";
                    main_temp_min.text = "Température mini :\n" + CurrentWeatherClass.main.temp_min + " C°";
                    main_temp_max.text = "Température max :\n" + CurrentWeatherClass.main.temp_max + " C°";
                    main_pressure.text = "Pression :\n" + CurrentWeatherClass.main.pressure + " hPa";
                    main_humidity.text = "Humidité :\n" + CurrentWeatherClass.main.humidity + " %";
                    wind_speed.text = "Vitesse vent :\n" + CurrentWeatherClass.wind.speed + " Km/h";
                    wind_gust.text = "Vitesse rafale vent :\n" + CurrentWeatherClass.wind.gust + " Km/h";
                    wind_deg.text = "Orientation vent :\n" + CurrentWeatherClass.wind.deg + " degré";
                    main_sea_level.text = "Pression au niveau de la mer :\n" + CurrentWeatherClass.main.sea_level + " hPa";
                    clouds_all.text = "Couverture nuageuse :\n" + CurrentWeatherClass.clouds.all + " %";
                    //rain_1h.text = "Visibilité :\n" + CurrentWeatherClass.rain.1h + " mètres";
                    //sys_sunrise.text = "Levé du soleil :\n" + CurrentWeatherClass.sys.sunrise + " UTC";
                    //sys_sunset.text = "Couché du soleil :\n" + CurrentWeatherClass.sys.sunset + " UTC";
                    //timezone.text = "Visibilité :\n" + CurrentWeatherClass.timezone + " UTC";
                }
                break;
            case "Météo sur 5 jours":
                {
                    //Debug.Log("infos Choix_ForecastWeather5days : " + choix);
                    ChoixForecast5Days.Rootobject ForecastWeather5daysClass = JsonUtility.FromJson<ChoixForecast5Days.Rootobject>(jsonStrings);
                    Debug.Log("Choix_ForecastWeather5days, Internal parameter = \n" + ForecastWeather5daysClass.cod);
                    Debug.Log("Choix_ForecastWeather5days, Internal message = \n" + ForecastWeather5daysClass);
                    Debug.Log("Choix_ForecastWeather5days, Cnt (A number of timestamps, which will be returned in the API response = \n" + ForecastWeather5daysClass.cnt);
                    Debug.Log("Choix_ForecastWeather5days, Time of data forecasted, unix, UTC = \n" + ForecastWeather5daysClass.list[0].dt);
                    Debug.Log("Choix_ForecastWeather5days, Temperature = \n" + ForecastWeather5daysClass.list[0].main.temp);
                    Debug.Log("Choix_ForecastWeather5days, human perception of weather = \n" + ForecastWeather5daysClass.list[0].main.feels_like);
                    Debug.Log("Choix_ForecastWeather5days, Minimum temperature at the moment of calculation = \n" + ForecastWeather5daysClass.list[0].main.temp_min);
                    Debug.Log("Choix_ForecastWeather5days, Maximum temperature at the moment of calculation = \n" + ForecastWeather5daysClass.list[0].main.temp_max);
                    Debug.Log("Choix_ForecastWeather5days, Atmospheric pressure on the sea level by default, hPa = \n" + ForecastWeather5daysClass.list[0].main.pressure);
                    Debug.Log("Choix_ForecastWeather5days, Atmospheric pressure on the sea level, hPa = \n" + ForecastWeather5daysClass.list[0].main.sea_level);
                    Debug.Log("Choix_ForecastWeather5days, Atmospheric pressure on the ground level, hPa = \n" + ForecastWeather5daysClass.list[0].main.grnd_level);
                    Debug.Log("Choix_ForecastWeather5days, Humidity, % = \n" + ForecastWeather5daysClass.list[0].main.humidity);
                    Debug.Log("Choix_ForecastWeather5days, Temp_kf (Internal parameter) = \n" + ForecastWeather5daysClass.list[0].main.temp_kf);
                    Debug.Log("Choix_ForecastWeather5days, Weather condition id = \n" + ForecastWeather5daysClass.list[0].weather[0].id);
                    Debug.Log("Choix_ForecastWeather5days, Group of weather parameters (Rain, Snow, Extreme etc.) = \n" + ForecastWeather5daysClass.list[0].weather[0].main);
                    Debug.Log("Choix_ForecastWeather5days, Weather condition within the group = \n" + ForecastWeather5daysClass.list[0].weather[0].description);
                    Debug.Log("Choix_ForecastWeather5days, Weather icon id = \n" + ForecastWeather5daysClass.list[0].weather[0].icon);
                    Debug.Log("Choix_ForecastWeather5days, Cloudiness, % = \n" + ForecastWeather5daysClass.list[0].clouds.all);
                    Debug.Log("Choix_ForecastWeather5days, Wind speed = \n" + ForecastWeather5daysClass.list[0].wind.speed);
                    Debug.Log("Choix_ForecastWeather5days, Wind direction, degrees (meteorological) = \n" + ForecastWeather5daysClass.list[0].wind.deg);
                    Debug.Log("Choix_ForecastWeather5days, Wind gust = \n" + ForecastWeather5daysClass.list[0].wind.gust);
                    Debug.Log("Choix_ForecastWeather5days, Average visibility. The maximum value of the visibility is 10km = \n" + ForecastWeather5daysClass.list[0].visibility);
                    Debug.Log("Choix_ForecastWeather5days, Probability of precipitation.\nThe values of the parameter vary\nbetween 0 and 1, \nwhere 0 is equal to 0%, 1 is equal to 100% = \n" + ForecastWeather5daysClass.list[0].pop);
                    Debug.Log("Choix_ForecastWeather5days, Rain volume for last 3 hours, mm = \n" + ForecastWeather5daysClass.list[0].rain._3h);
                    Debug.Log("Choix_ForecastWeather5days, Part of the day (n - night, d - day)  = \n" + ForecastWeather5daysClass.list[0].sys.pod);
                    Debug.Log("Choix_ForecastWeather5days, Time of data forecasted, ISO, UTC  = \n" + ForecastWeather5daysClass.list[0].dt_txt);
                    Debug.Log("Choix_ForecastWeather5days, City ID  = \n" + ForecastWeather5daysClass.city.id);
                    Debug.Log("Choix_ForecastWeather5days, City name.\nPlease note that built-in geocoder\nfunctionality has been deprecated = \n" + ForecastWeather5daysClass.city.name);
                    Debug.Log("Choix_ForecastWeather5days, City geo location, latitude = \n" + ForecastWeather5daysClass.city);
                    Debug.Log("Choix_ForecastWeather5days, Country code (GB, JP etc.) = \n" + ForecastWeather5daysClass.city.country);
                    Debug.Log("Choix_ForecastWeather5days, City population = \n" + ForecastWeather5daysClass.city.population);
                    Debug.Log("Choix_ForecastWeather5days, Shift in seconds from UTC = \n" + ForecastWeather5daysClass.city.timezone);
                    Debug.Log("Choix_ForecastWeather5days, Sunrise time, Unix, UTC = \n" + ForecastWeather5daysClass.city.sunrise);
                    Debug.Log("Choix_ForecastWeather5days, Sunset time, Unix, UTC = \n" + ForecastWeather5daysClass.city.sunset);

                }
                break;
            case "Météo Lieu Saisi":
                {
                    //        Debug.Log("infos Choix_LocationWeather : " + choix);
                    //        Choix_LocationWeather.Rootobject LocationWeatherClass = JsonUtility.FromJson<Choix_LocationWeather.Rootobject>("{\"Property1\":" + jsonStrings + "}");
                    //        Debug.Log("Choix_LocationWeather, nom du lieu saisi = \n" + LocationWeatherClass.Property1[0].name);
                    //        Debug.Log("Choix_LocationWeather, latitude du lieu saisi = \n" + LocationWeatherClass.Property1[0].lat);
                    //        Debug.Log("Choix_LocationWeather, longitude du lieu saisi = \n" + LocationWeatherClass.Property1[0].lon);
                    //        Debug.Log("Choix_LocationWeather, Pays du lieu saisi = \n" + LocationWeatherClass.Property1[0].country);
                    //        Debug.Log("Choix_LocationWeather, l'état du lieu saisi = \n" + LocationWeatherClass.Property1[0].local_names);
                }
                break;
        }
    }

    private string ReadAPIKey()
    {
        // Lecture du contenu du fichier contenant la clé API.
        cheminAppIdAPIkey = Application.streamingAssetsPath + "/OpenWeatherAPI";
        string appID_API_key = File.ReadAllText(cheminAppIdAPIkey);
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