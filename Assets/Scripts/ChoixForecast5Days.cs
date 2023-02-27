public class ChoixForecast5Days
{
    [System.Serializable]
    public class Rootobject
    {
        public string cod;
        public int message;
        public int cnt;
        public List[] list;
        public City city;
    }

    [System.Serializable]
    public class City
    {
        public int id;
        public string name;
        public Coord coord;
        public string country;
        public int population;
        public int timezone;
        public int sunrise;
        public int sunset;
    }

    [System.Serializable]
    public class Coord
    {
    }

    [System.Serializable]
    public class List
    {
        public int dt;
        public Main main;
        public Weather[] weather;
        public Clouds clouds;
        public Wind wind;
        public int visibility;
        public float pop;
        public Sys sys;
        public string dt_txt;
        public Rain rain;
    }

    [System.Serializable]
    public class Main
    {
        public float temp;
        public float feels_like;
        public float temp_min;
        public float temp_max;
        public int pressure;
        public int sea_level;
        public int grnd_level;
        public int humidity;
        public float temp_kf;
    }

    [System.Serializable]
    public class Clouds
    {
        public int all;
    }

    [System.Serializable]
    public class Wind
    {
        public float speed;
        public int deg;
        public float gust;
    }

    [System.Serializable]
    public class Sys
    {
        public string pod;
    }

    [System.Serializable]
    public class Rain
    {
        public float _3h;
    }

    [System.Serializable]
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }
}