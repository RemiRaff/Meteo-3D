public class Choix_CurrentWeather
{
    /*     From : Visual Studio Special Paste
           to use => Rootobject myDeserializedClass = JsonUtility.FromJson<Rootobject>(jsonStrings);
           example : description.text = "Météo actuelle: \n" + myDeserializedClass.weather[0].description;
    */

    [System.Serializable]
    public class Rootobject
    {
        public Coord coord;
        public Weather[] weather;
        public string @base;
        public Main main;
        public int visibility;
        public Wind wind;
        public Clouds clouds;
        public int dt;
        public Sys sys;
        //public int timezone;
        //public int id;
        //public string name;
        //public int cod;
    }

    [System.Serializable]
    public class Coord
    {
        public float lon;
        public float lat;
    }

    [System.Serializable]
    public class Main
    {
        public float temp;
        public float feels_like;
        public float temp_min;
        public float temp_max;
        public int pressure;
        public int humidity;
        public int sea_level;
        public int grnd_level;
    }

    [System.Serializable]
    public class Wind
    {
        public float speed;
        public int deg;
        public int gust;
    }

    [System.Serializable]
    public class Clouds
    {
        public int all;
    }

    [System.Serializable]
    public class Sys
    {
        public int sunrise;
        public int sunset;
        public int timezone;
        public int id;
        public int name;
        public int cod;
        //public int type;
        //public string country;
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