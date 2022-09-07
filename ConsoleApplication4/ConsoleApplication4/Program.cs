using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        public static List<Kone> KoneList = new List<Kone>();
        public static List<Tila> TilaList = new List<Tila>();
        public static List<Kone> VarastoList = new List<Kone>();

        public static int currentindexK = -1;
        public static int currentindexL = -1;

        static void Main(string[] args)
        {
            StartMessage();
            DoSomething();
            Console.ReadLine();
        }

        static void AddKone()
        {
            Console.Clear();
            Console.WriteLine("Syötä CPU, sitten RAM, sitten PSU ja lopuksi OS"); 
            KoneList.Add(new Kone(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), currentindexK));
            Console.Clear();
            StartMessage();
            DoSomething();
        }

        static void AddTila()
        {
            Console.Clear();
            Console.WriteLine("Syötä koko(max koneet)");
            try {
                int koko = Convert.ToInt32(Console.ReadLine()); if (koko <= 0) koko = 1;
                TilaList.Add(new Tila(koko, currentindexL));
                Console.Clear();
                StartMessage();
                DoSomething();
            }
            catch
            {
                Console.Write(Environment.NewLine + "Sinun pitää syöttää numeron");
                Console.ReadLine();
                Console.Clear();
                StartMessage();
                DoSomething();
            }
        }
        
        static void StartMessage()
        {
            Console.WriteLine("Jos haluat luoda uusi kone kirjoita 1" + Environment.NewLine + "Jos haluat luoda uusi tila kirjoita 2" +
                 Environment.NewLine + "Jos haluat nähdä koneiden lista kirjoita 3" + Environment.NewLine + "Jos haluat nähdä tilojen lista kirjoita 4"
                 + Environment.NewLine + "Jos haluat nähdä varaston sisältö kirjoita 5" + Environment.NewLine + "Jos haluat poistaa käytöstä sovellus kirjoita 6");
        }

        static void DoSomething()
        {
            string numb = Console.ReadLine();
            switch (numb)
            {
                case "1": currentindexK++; AddKone(); break;
                case "2": currentindexL++; AddTila(); break;
                case "3": GetAllKonesParametrs(); break;
                case "4": GetAllTila(); break;
                case "5": GetVarasto(); break;
                case "6": Environment.Exit(0); break;
                default: Console.Clear(); StartMessage(); DoSomething(); break;
            }
        }

        static void GetAllKonesParametrs()
        {
            Console.Clear();
            for (int i = 0; i < KoneList.Count; i++)
            {
                KoneList[i].GetInformation();
            }

            Console.Write(Environment.NewLine + "Jos haluat muokata konetta kirjoita 1, jos poistaa kirjoita 2, jos sirtää luokkaan kirjoita 3" + Environment.NewLine);
            string result = Console.ReadLine();
            if(result == "1") {
                Console.Write(Environment.NewLine + "Jos haluat muokata konetta, kirjoita koneen numero, jos et paina eri nappia" + Environment.NewLine);
                try
                {
                    int value = Convert.ToInt32(Console.ReadLine());
                    if(value <= KoneList.Count - 1) {
                        Console.Write(Environment.NewLine + "Syötä koneen uudet tiedot, kuten CPU RAM PSU OS" + Environment.NewLine);
                        int index = 0; int ii = 0;
                        for (int i = 0; i < TilaList.Count; i++)
                        {
                            if (TilaList[i].KoneidenIndexers.Contains(KoneList[value])) { index = TilaList[i].KoneidenIndexers.IndexOf(KoneList[value]); ii = i; }
                        }
                        KoneList[value] = new Kone(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), currentindexK);
                        TilaList[ii].KoneidenIndexers[index] = KoneList[value];
                        GetAllKonesParametrs();
                    }
                    else
                    {
                        Console.Write(Environment.NewLine + "Ei oo tämmöistä tietokonetta"); Console.ReadLine(); GetAllKonesParametrs();
                    }
                }
                catch
                {
                    Console.Clear();
                    StartMessage();
                    DoSomething();
                }
            }

            if(result == "2")
            {
                Console.Write(Environment.NewLine + "Jos haluat poistaa koneen, kirjoita sen numero, jos et paina eri nappia" + Environment.NewLine);
                try
                {
                    int value = Convert.ToInt32(Console.ReadLine());
                    if (value <= KoneList.Count - 1)
                    {
                        for (int i = 0; i < TilaList.Count; i++)
                        {
                            // if (TilaList[i].KoneidenIndexers.Contains(value)) TilaList[i].KoneidenIndexers.Remove(value);
                            if (TilaList[i].KoneidenIndexers.Contains(KoneList[value])) TilaList[i].KoneidenIndexers.Remove(KoneList[value]);
                        }

                        for (int i = value; i < KoneList.Count - 1; i++)
                        {
                            Kone btw = KoneList[i];
                            KoneList[i] = KoneList[i + 1];
                            KoneList[i + 1] = btw;
                        }
                        VarastoList.Add(KoneList.Last());
                        KoneList.Remove(KoneList.Last());
                        currentindexK--;

                        for (int i = 0; i < KoneList.Count; i++)
                        {
                            KoneList[i].CHangeIndex(i);
                        }
                        Console.Clear();
                        GetAllKonesParametrs();
                    }
                    else
                    {
                        Console.Write(Environment.NewLine + "Ei oo tämmöistä tietokonetta"); Console.ReadLine(); GetAllKonesParametrs();
                    }
                }
                catch
                {
                    Console.Clear();
                    StartMessage();
                    DoSomething();
                }
            }

            if(result == "3")
            {
                Console.Write(Environment.NewLine + "Jos haluat sirtää koneen, kirjoita koneen numero, jos et paina eri nappia" + Environment.NewLine);
                try
                {
                    int value = Convert.ToInt32(Console.ReadLine());
                    if (value <= KoneList.Count - 1)
                    {
                        if(TilaList.Count > 0) {
                            for (int i = 0; i < TilaList.Count; i++)
                            {
                                TilaList[i].GetInformation();
                            }
                            Console.Write(Environment.NewLine + "Valitse sopiva vapaa tila" + Environment.NewLine);
                            try
                            {
                                int tilannumero = Convert.ToInt32(Console.ReadLine());
                                if(tilannumero <= TilaList.Count - 1)
                                {
                                    if(TilaList[tilannumero].KoneidenIndexers.Count < TilaList[tilannumero].koko) {
                                        // if(!TilaList[tilannumero].KoneidenIndexers.Contains(value)) TilaList[tilannumero].KoneidenIndexers.Add(value);
                                        if (!TilaList[tilannumero].KoneidenIndexers.Contains(KoneList[value])) TilaList[tilannumero].KoneidenIndexers.Add(KoneList[value]);
                                        else
                                        {
                                            Console.Write(Environment.NewLine + "Kone jo on tilassa"); Console.ReadLine(); GetAllKonesParametrs();
                                        }
                                        Console.Clear();
                                        StartMessage();
                                        DoSomething();
                                    }else
                                    {
                                        Console.Write(Environment.NewLine + "Tila ei sovi"); Console.ReadLine(); GetAllKonesParametrs();
                                    }
                                }
                                else
                                {
                                    Console.Write(Environment.NewLine + "Ei oo tämmöistä tilaa"); Console.ReadLine(); GetAllKonesParametrs();
                                }
                            }
                            catch
                            {
                                Console.Write(Environment.NewLine + "Kirjoita numero seurava kerran"); Console.ReadLine(); GetAllKonesParametrs();
                            }
                        }
                        else
                        {
                            Console.Write(Environment.NewLine + "Luo ennen uusi tila, koska nykyinen tilan määrä on 0"); Console.ReadLine(); GetAllKonesParametrs();
                        }
                    }
                    else
                    {
                        Console.Write(Environment.NewLine + "Ei oo tämmöistä tietokonetta"); Console.ReadLine(); GetAllKonesParametrs();
                    }
                }
                catch
                {
                    Console.Clear();
                    StartMessage();
                    DoSomething();
                }
            }

            if(result != "1" && result != "2" && result != "3")
            {
                Console.Clear();
                StartMessage();
                DoSomething();
            }
        }

        static void GetAllTila()
        {
            Console.Clear();
            for (int i = 0; i < TilaList.Count; i++)
            {
                TilaList[i].GetInformation();
            }
            Console.Write(Environment.NewLine + "Jos haluat muokata konetta kirjoita 1, jos poistaa kirjoita 2" + Environment.NewLine);
            string result = Console.ReadLine();
            if(result == "1") {
                Console.Write(Environment.NewLine + "Jos haluat muokata tilaa, kirjoita tilan numero, jos et paina eri nappia" + Environment.NewLine);
                try
                {
                    int value = Convert.ToInt32(Console.ReadLine());
                    if (value <= TilaList.Count - 1)
                    {
                        Console.Write(Environment.NewLine + "Syötä tilan uudet tiedot, kuten koko" + Environment.NewLine);
                        try
                        {
                            int koko = Convert.ToInt32(Console.ReadLine()); if (koko <= 0) koko = 1;
                            TilaList[value] = new Tila(koko, value);
                            GetAllTila();
                        }
                        catch
                        {
                            Console.Write(Environment.NewLine + "Sinun pitää syöttää numeroja");
                            Console.ReadLine();
                            GetAllTila();
                        }
                    }
                    else
                    {
                        Console.Write(Environment.NewLine + "Ei oo tämmöistä tilaa"); Console.ReadLine(); GetAllTila();
                    }
                }
                catch
                {
                    Console.Clear();
                    StartMessage();
                    DoSomething();
                }
            }

            if (result == "2")
            {
                Console.Write(Environment.NewLine + "Jos haluat poistaa tilan, kirjoita sen numero, jos et paina eri nappia" + Environment.NewLine);
                try
                {
                    int value = Convert.ToInt32(Console.ReadLine());
                    if (value <= TilaList.Count - 1)
                    {
                        for (int i = value; i < TilaList.Count - 1; i++)
                        {
                            Tila btw = TilaList[i];
                            TilaList[i] = TilaList[i + 1];
                            TilaList[i + 1] = btw;
                        }

                        TilaList.Remove(TilaList.Last());
                        currentindexL--;

                        for (int i = 0; i < TilaList.Count; i++)
                        {
                            TilaList[i].tunnus = i;
                        }
                        Console.Clear();
                        GetAllTila();
                    }
                    else
                    {
                        Console.Write(Environment.NewLine + "Ei oo tämmöistä tilaa"); Console.ReadLine(); GetAllKonesParametrs();
                    }
                }
                catch
                {
                    Console.Clear();
                    StartMessage();
                    DoSomething();
                }
            }

            if (result != "1" && result != "2")
            {
                Console.Clear();
                StartMessage();
                DoSomething();
            }
        }

        static void GetVarasto()
        {
            Console.Clear();
            for (int i = 0; i < VarastoList.Count; i++) VarastoList[i].GetInformation();
            Console.ReadLine();
            Console.Clear();
            StartMessage();
            DoSomething();
        }
    }

    public class Kone
    {
        private string CPU, RAM, PSU, OS;
        private int IndividualIndex { get; set; }

        public Kone(string CPU, string RAM, string PSU, string OS, int IndividualIndex)
        {
            this.CPU = CPU;
            this.RAM = RAM;
            this.PSU = PSU;
            this.OS = OS;
            this.IndividualIndex = IndividualIndex;
        }

        public int GetIndex()
        {
            return this.IndividualIndex;
        }

        public void CHangeIndex(int index)
        {
            this.IndividualIndex = index;
        }

        public void GetInformation()
        {
            Console.Write(Environment.NewLine + "Koneen index on " + GetIndex() + " " + CPU + " " + RAM + " " + OS);
        }
    }

    public class Tila
    {
        public List<Kone> KoneidenIndexers = new List<Kone>(); //  public List<int> KoneidenIndexers = new List<int>();

        public int tunnus;
        public int koko;

        public void GetInformation()
        {
            Console.Write(Environment.NewLine + "Tilan koko on " + koko + " Tilan numero on " + tunnus + Environment.NewLine + "Luokassa ovat koneita: ");
            for (int i = 0; i < KoneidenIndexers.Count; i++)
            {
                KoneidenIndexers[i].GetInformation();
            }
            Console.Write(Environment.NewLine);
        }

        public Tila(int koko, int tunnus)
        {
            this.koko = koko;
            this.tunnus = tunnus;
        }
    }
}
