using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Private.logik_aussagen
{
    //Spaß projekt zur Übung
    //Dieses Programm wandelt Logische Aussagen in boolische Werte um
    //Zwischen Logischen Ausdrücken und Operatóren
    public class YesNoToBoolConverter : IValueConverter
    {
        public bool debug = true; // wenn true => Wird in Console geschrieben
        public bool isClipInvald = false;
        public bool sY = false;
        public bool mT3O = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            try
            {
                value = value as string ?? string.Empty;
                sY = false;
                isClipInvald = false;
                mT3O = false;

                if (!isOk((string)value))
                {
                    sY = true;
                    return false;
                }

                for (int i = 1; i > 0; i++)
                {
                    if (isClipInvald)
                    {
                        sY = true;
                        return false;
                    }
                    if (isClip(value))
                    {
                        if (!isClipInvald)
                        {
                            value = dissolveClamp(value);
                        }
                        else if (isClipInvald)
                        {
                            sY = true;
                            return false;
                        }
                    }
                    else if (!isClip(value))
                    {
                        var sArray = makeArrayWithoutBlankFields((string)value);
                        string cach = "";
                        string result = "";
                        string cach2 = "";
                        mT3O = false;
                        //wenn mehr als 3 operatoren sind, löse nach einadern auf
                        if (isMoreThan3_Ops((string)value))
                        {

                            do
                            {
                                if (sArray.Length == 3)
                                { break; }
                                else if (sArray.Length > 3)
                                {
                                    cach = sArray[0] + " " + sArray[1] + " " + sArray[2];
                                    if (compare(cach))
                                    {
                                        result = "true";
                                    }
                                    else { result = "false"; }
                                    sArray[0] = result;
                                    sArray[1] = "";
                                    sArray[2] = "";
                                    cach2 = "";
                                    foreach (string s in sArray)
                                    {
                                        cach2 = cach2 + s + " ";
                                    }
                                    mT3O = true;
                                    sArray = makeArrayWithoutBlankFields(cach2);

                                }

                            }
                            while (true);

                        }
                        if (mT3O)
                        {
                            string cach3 = "";
                            foreach (string s in sArray)
                            {
                                cach3 = cach3 + s + " ";
                            }
                            value = cach3;
                        }

                        return compare(value);
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                Console.WriteLine("TRY CATCH Ausgelöchst! => Converter");
                sY = true;
                return false;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (sY)
                {
                    return "false Syntax => falsche Klammersetzung";
                }
                if (value is null)
                    return "Invalit Input";

                if (value is bool)
                {
                    if ((bool)value == true)
                    {
                        return "yes";
                    }
                    else
                        return "no";
                }
                return "false Syntax => falsche Klammersetzung";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                Console.WriteLine("TRY CATCH Ausgelöchst! => ConverterBack");
                return "false Syntax";
            }
        }

        public  bool compare(object value)
        {
            //Console.WriteLine("Compare Value ===>> " + value);
            string s = value.ToString();
            s = s.ToLower();
            var split = s.Split(' ');
            int len = split.Length;
            string[] stringA = new string[len];
            int zähler = 0;
            int pos = 0;
            for (int i = 0; i < len; i++)
            {
                if (split[i] != "")
                {
                    pos = i;
                    zähler++;
                    stringA[i] = split[i];
                }
            }

            if (zähler == 1)
            {
                if (split[pos] == "true")
                {
                    return true;
                }
                if (split[pos] == "false")
                {
                    return false;
                }
                //negierung
                if (split[pos] == "!true")
                {
                    return false;
                }
                if (split[pos] == "!false")
                {
                    return false;
                }


            }


            if (s == null)
            {
                isClipInvald = true;
                return false;
            }
            var sA2 = s.Split(' ');

            //leere felder entfernen
            int zähler2 = 0;
            string[] sA = new string[sA2.Length];
            for (int i = 0; i < sA2.Length; i++)
            {
                if (sA2[i] != " " && sA2[i] != "")
                {
                    sA[zähler2] = sA2[i];
                    zähler2++;
                }
            }

            //array Größe


            if (value.ToString().Contains('(') && value.ToString().Contains(')'))
            {
                zähler2 -= 2;
            }
            int zähler3 = 0;
            string[] aOhneLuK = new string[zähler2]; //Array ohne Leere Felder und Klammerns
            for (int i = 0; i < sA.Length; i++)
            {
                if (sA[i] != "" && sA[i] != " " && sA[i] != null)
                {
                    if (sA[i] != "(" && sA[i] != ")")
                    {
                        aOhneLuK[zähler3] = sA[i];
                        zähler3++;
                    }

                }
            }



            if (aOhneLuK.Length != 3)
            {
                isClipInvald = true;
                return false;

            }
            string o1 = aOhneLuK[0];
            string v = aOhneLuK[1];
            string o2 = aOhneLuK[2];



            bool b1;
            bool b2;
            //Wert 1 
            if (o1.ToLower() == "true")
            {
                b1 = true;
            }
            else if (o1.ToLower() == "false")
            {
                b1 = false;
            }
            //negierung
            else if (o1.ToLower() == "!true")
            {
                b1 = false;
            }
            else if (o1.ToLower() == "!false")
            {
                b1 = true;
            }
            else
            {
                isClipInvald = true;
                return false;

            }
            //Wert 2
            if (o2.ToLower() == "true")
            {
                b2 = true;
            }
            else if (o2.ToLower() == "false")
            {
                b2 = false;
            }
            //negierung
            else if (o2.ToLower() == "!true")
            {
                b2 = false;
            }
            else if (o2.ToLower() == "!false")
            {
                b2 = true;
            }
            else
            {
                isClipInvald = true;
                return false;

            }
            //operator / vergleicher
            switch (v)
            {
                case "&":
                    return (b1 && b2);
                case "|":
                    return (b1 || b2);
                case "&&":
                    return (b1 && b2);
                case "||":
                    return (b1 || b2);
                default:
                    isClipInvald = true;
                    return false;
            }
        }

        public  bool isClip(object value)
        {
            if (value is string)
            {
                if (value.ToString().Contains('(') || value.ToString().Contains(')'))
                {
                    if (value.ToString().Contains('(') ^ value.ToString().Contains(')'))
                    {
                        isClipInvald = true;
                        return true;
                    }
                    return true;
                }
            }
            return false;
        }

        public  object dissolveClamp(object value)
        {
            string ss = value.ToString();
            if (ss == null)
            {
                isClipInvald = true;
                return false;
            }

            var splitt = ss.Split(' ');
            Console.WriteLine("TEST");
            cW(ss + "   :TEST: \n" + splitt);


            int lastKA = 0; //letzt "("
            int lastKZ = 0; //letzt ")"
            string zG = ""; //zusammengesetzer Teil

            for (int i = 0; i < splitt.Length; i++)
            {
                if (splitt[i] == "(")
                {
                    lastKA = i;
                }

            }
            int i3 = lastKA;
            for (int i = i3; i < splitt.Length; i++)
            {
                if (splitt[i] == ")")
                { lastKZ = i; break; }
            }

            //leere felder entfernen
            int zähler2 = 0;
            string[] sA = new string[splitt.Length];
            var sA2 = splitt;
            for (int i = 0; i < sA2.Length; i++)
            {
                if (sA2[i] != " " && sA2[i] != "")
                {
                    sA[zähler2] = sA2[i];
                    zähler2++;
                }
            }


            for (int i = lastKA + 1; i < lastKZ; i++)
            {
                zG += sA[i] + " ";
            }

            //zG = splitt[lastKA + 1] + " " + splitt[lastKA + 2] + " " + splitt[lastKA + 3];
            bool goal2 = compare(zG);
            string goal;

            if (goal2)
            {
                goal = "true";
            }
            else { goal = "false"; }

            for (int i = lastKA; i <= lastKZ; i++)
            {
                splitt[i] = "";
            }
            splitt[lastKA] = goal;
            string result = "";
            foreach (string s in splitt)
            {
                result = result + s + " ";
            }
            return result;
        }

        public  void cW(object v)
        {
            if (debug)
            {
                Console.WriteLine(v);
            }
        }

        public  bool isMoreThan3_Ops(string value)
        {
            var split = value.Split(' ');

            //leere felder entfernen
            int zähler2 = 0;
            string[] sA = new string[split.Length];
            var sA2 = split;
            for (int i = 0; i < sA2.Length; i++)
            {
                if (sA2[i] != " " && sA2[i] != "")
                {
                    sA[zähler2] = sA2[i];
                    zähler2++;
                }
            }

            //array Größe

            int zähler3 = 0;
            string[] aOhneLuK = new string[zähler2]; //Array ohne Leere Felder und Klammerns
            for (int i = 0; i < sA.Length; i++)
            {
                if (sA[i] != "" && sA[i] != " " && sA[i] != null)
                {
                    if (sA[i] != "(" && sA[i] != ")")
                    {
                        aOhneLuK[zähler3] = sA[i];
                        zähler3++;
                    }

                }
            }

            if (aOhneLuK.Length > 3)
            {
                return true;
            }

            return false;
        }

        public  string[] makeArrayWithoutBlankFields(string value)
        {
            var split = value.Split(' ');

            //leere felder entfernen
            int zähler2 = 0;
            string[] sA = new string[split.Length];
            var sA2 = split;
            for (int i = 0; i < sA2.Length; i++)
            {
                if (sA2[i] != " " && sA2[i] != "")
                {
                    sA[zähler2] = sA2[i];
                    zähler2++;
                }
            }

            //array Größe

            int zähler3 = 0;
            string[] aOhneLuK = new string[zähler2]; //Array ohne Leere Felder und Klammerns
            for (int i = 0; i < sA.Length; i++)
            {
                if (sA[i] != "" && sA[i] != " " && sA[i] != null)
                {
                    if (sA[i] != "(" && sA[i] != ")")
                    {
                        aOhneLuK[zähler3] = sA[i];
                        zähler3++;
                    }

                }
            }
            return aOhneLuK;
        }

        public bool isOk(string value)
        {
            string[] sA = value.Split(' ');
            if (sA[0] == "|" || sA[0] == "&")
            {
                return false;
            }

            int l = sA.Length;

            if (sA[l - 1] == "|" || sA[l - 1] == "&")
            {
                return false;
            }

            if (sA[0] == "||" || sA[0] == "&&")
            {
                return false;
            }


            if (sA[l - 1] == "||" || sA[l - 1] == "&&")
            {
                return false;
            }

            int zählerOPs = 0;
            int zählerL = 0;
            foreach (string s in sA)
            {
                if (s == "|" || s == "&")
                { zählerOPs++; }
                else if (s == "||" || s == "&&")
                { zählerOPs++; }
                else if (s.ToLower() == "true" || s.ToLower() == "false")
                { zählerL++; }
                else if (s.ToLower() == "!true" || s.ToLower() == "!false")
                { zählerL++; }
                else if (s == " " || s == "")
                { }
                else if (s == "(" || s == ")") { }
                else { return false; }
            }
            if (zählerOPs == 0 || zählerL == 1)
            {
                return true;
            }
            else if (zählerL - 1 == zählerOPs)
            {
                return true;
            }
            return false;
        }
    }
}