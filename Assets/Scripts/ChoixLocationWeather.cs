using System.Collections.Generic;

public class ChoixLocationWeather
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);

    [System.Serializable]
    public class Root
    {
        public string name;
        public LocalNames local_names;
        public double lat;
        public double lon;
        public string country;
        public string state;
    }

    [System.Serializable]
    public class LocalNames
    {
        public string ug;
        public string hy;
        public string be;
        public string wo;
        public string fi;
        public string tl;
        public string sh;
        public string @is;
        public string co;
        public string br;
        public string bn;
        public string cv;
        public string nl;
        public string ku;
        public string ga;
        public string th;
        public string la;
        public string lv;
        public string my;
        public string ko;
        public string ta;
        public string sc;
        public string hr;
        public string cs;
        public string el;
        public string it;
        public string gl;
        public string mk;
        public string ht;
        public string ur;
        public string gu;
        public string sk;
        public string bo;
        public string fa;
        public string yi;
        public string mr;
        public string yo;
        public string de;
        public string ps;
        public string li;
        public string am;
        public string za;
        public string sr;
        public string lb;
        public string kv;
        public string sv;
        public string ha;
        public string tk;
        public string eo;
        public string hi;
        public string oc;
        public string hu;
        public string an;
        public string sl;
        public string he;
        public string lt;
        public string vi;
        public string ne;
        public string eu;
        public string ka;
        public string zh;
        public string es;
        public string tg;
        public string ln;
        public string mi;
        public string pl;
        public string no;
        public string wa;
        public string gn;
        public string cu;
        public string ru;
        public string fr;
        public string bs;
        public string et;
        public string ba;
        public string ar;
        public string af;
        public string sq;
        public string fy;
        public string os;
        public string so;
        public string kn;
        public string uk;
        public string or;
        public string bg;
        public string ml;
        public string km;
        public string uz;
        public string ca;
        public string zu;
        public string tt;
        public string mn;
        public string ky;
        public string gv;
        public string ja;
        public string kk;
        public string pa;
        public string te;
        public string pt;
        public string gd;
        public string en;
    }

    /*     From : Visual Studio Special Paste
           to use => Rootobject myDeserializedClass = JsonUtility.FromJson<Rootobject>(jsonStrings);
           example : description.text = "Météo actuelle: \n" + myDeserializedClass.weather[0].description;
    */

    //[System.Serializable]
    //public class Rootobject
    //{
    //    public Class1[] Property1;
    //}

    //[System.Serializable]
    //public class Class1
    //{
    //    public string name;
    //    public Local_Names local_names;
    //    public float lat;
    //    public float lon;
    //    public string country;
    //    public string state;
    //}

    //[System.Serializable]
    //public class Local_Names
    //{
    //    public string ja;
    //    public string ky;
    //    public string kn;
    //    public string sv;
    //    public string ar;
    //    public string hu;
    //    public string eo;
    //    public string za;
    //    public string tl;
    //    public string cs;
    //    public string kv;
    //    public string tt;
    //    public string ml;
    //    public string li;
    //    public string te;
    //    public string gv;
    //    public string mk;
    //    public string sh;
    //    public string lv;
    //    public string bg;
    //    public string gu;
    //    public string cv;
    //    public string wa;
    //    public string pa;
    //    public string bo;
    //    public string ur;
    //    public string fy;
    //    public string la;
    //    public string uk;
    //    public string wo;
    //    public string ba;
    //    public string gn;
    //    public string zu;
    //    public string lb;
    //    public string bn;
    //    public string an;
    //    public string uz;
    //    public string ru;
    //    public string mn;
    //    public string bs;
    //    public string ln;
    //    public string hr;
    //    public string vi;
    //    public string sl;
    //    public string ka;
    //    public string hi;
    //    public string cu;
    //    public string or;
    //    public string ht;
    //    public string el;
    //    public string so;
    //    public string ha;
    //    public string es;
    //    public string fi;
    //    public string gl;
    //    public string tg;
    //    public string tk;
    //    public string ta;
    //    public string os;
    //    public string sr;
    //    public string kk;
    //    public string de;
    //    public string hy;
    //    public string ku;
    //    public string nl;
    //    public string fa;
    //    public string eu;
    //    public string af;
    //    public string sk;
    //    public string ca;
    //    public string oc;
    //    public string fr;
    //    public string am;
    //    public string my;
    //    public string pl;
    //    public string he;
    //    public string it;
    //    public string br;
    //    public string sc;
    //    public string ga;
    //    public string no;
    //    public string lt;
    //    public string sq;
    //    public string be;
    //    public string th;
    //    public string ne;
    //    public string ko;
    //    public string co;
    //    public string mi;
    //    public string et;
    //    public string mr;
    //    public string yo;
    //    public string ug;
    //    public string zh;
    //    public string _is;
    //    public string yi;
    //    public string ps;
    //    public string km;
    //    public string gd;
    //    public string en;
    //    public string pt;
    //}
}
