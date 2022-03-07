using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Test Klasse: Wird nicht verwendet
namespace Private.logik_aussagen
{
    public class Class1       
    {
        public bool isClipInvald = false;

        //TODO
        //Main Methode
        public void Main()
        {
            Console.WriteLine("Test");
        }


        public bool compare(object value)
        {
            string s = value.ToString();
            if (s == null)
            {
                isClipInvald = true;
                return false;
            }
            var sA = s.Split(' ');
            if (sA.Length != 3)
            {
                isClipInvald = true;
                return false;

            }
            string o1 = sA[0];
            string v = sA[1];
            string o2 = sA[2];



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
                default:
                    isClipInvald = true;
                    return false;
            }
        }

        public object isClip(object value)
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

        public object dissolveClamp(object value)
        {

            string ss = value.ToString();
            if (ss == null)
            {
                isClipInvald = true;
                return false;
            }

            var splitt = ss.Split(' ');


            int lastKA = 0; //letzt "("
            int lastKZ; //letzt ")"
            string zG = ""; //zusammengesetzer Teil

            for (int i = 0; i < splitt.Length; i++)
            {
                if (splitt[i] == "(")
                {
                    lastKA = i;
                }

            }
            int i3 = lastKA;
            for (int i2 = i3; i2 < splitt.Length; i2++)
            {
                if (splitt[i2] == ")")
                { lastKZ = i2; break; }
            }

            zG = splitt[lastKA + 1] + " " + splitt[lastKA + 2] + " " + splitt[lastKA + 3];
            bool goal2 = compare(splitt);
            string goal;

            if (goal2)
            {
                goal = "true";
            }
            else { goal = "false"; }


            splitt[lastKA] = goal;
            for (int i = 1; i < 4; i++)
            {
                splitt[lastKA + i] = "";
            }
            string result = "";
            foreach (string s in splitt)
            {
                result = result + " " + s;
            }
            return result;
        }
    }
}
