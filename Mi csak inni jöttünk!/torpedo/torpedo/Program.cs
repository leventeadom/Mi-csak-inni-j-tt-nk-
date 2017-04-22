using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace torpedo
{
    //Mi csak inni jöttünk!
    class Program
    {
        static Random rnd = new Random();

        static public void tablatakaritas(int[,] mezo, int mezohossz) //mezo kiuritese
        {
            for (int i = 0; i < mezohossz; i++)
            {
                for (int j = 0; j < mezohossz; j++)
                {
                    mezo[i, j] = 0;

                }

            }
        }

        //---------------------------------------------------------------------------------------------
        static public bool lehetallo(int[,] mezo, int xkoord, int ykoord, int hajohossz, int mezohossz)
        {
            if (ykoord + hajohossz - 1 > mezohossz-1) return false;
            if (mezo[xkoord, ykoord] != 0) return false;

            int i = ykoord;
            while (i < ykoord + hajohossz - 1)
            {
                if (mezo[xkoord, i] != 0) return false;
                i++;
            }

            return true;
        }

        //---------------------------------------------------------------------------------------------
        static public bool lehetfekvo(int[,] mezo, int xkoord, int ykoord, int hajohossz, int mezohossz)
        {
            if (xkoord + hajohossz - 1 > mezohossz-1) return false;
            if (mezo[xkoord, ykoord] != 0) return false;

            int i = xkoord;
            while (i < xkoord + hajohossz - 1)
            {
                if (mezo[xkoord, i] != 0) return false;
                i++;
            }

            return true;
        }

        //..................................................................................
        static public void AIhajokelhejezese(int[,] mezo, int mezohossz)
        {
            tablatakaritas(mezo, mezohossz);
            int maxhajoszam = mezohossz - 1;
            bool[] fentvan = new bool[mezohossz - 1];
            mezo[rnd.Next(0, mezohossz), rnd.Next(0, mezohossz)] = 1;
            fentvan[0] = true;

            for (int i = 1; i < fentvan.Length; i++)
            {
                bool siker = false;
                while (!siker)
                {
                    int allo = rnd.Next(0, 2);
                    if (allo == 0)
                    {
                        int xkoord = rnd.Next(0, mezohossz);
                        int ykoord = rnd.Next(0, mezohossz);
                        
                        if (lehetallo(mezo, xkoord, ykoord, i + 1, mezohossz))
                        {
                            for (int j = 0; j < i + 2; j++)
                            {
                                StreamWriter sw = new StreamWriter(@"C:\Users\delld\Desktop\Mi csak inni jöttünk!\gephajoi.txt");
                                mezo[xkoord, ykoord + j] = i + 1;
                                sw.WriteLine("{0},{1} kordinátán van hajó.", xkoord, ykoord);
                                sw.Close();
                            }
                            siker = true;
                        }
                    }
                    else
                    {
                        int xkoord = rnd.Next(0, mezohossz);
                        int ykoord = rnd.Next(0, mezohossz);
                        if (lehetfekvo(mezo, xkoord, ykoord, i + 1,mezohossz))
                        {
                            for (int j = 0; j < i + 2; j++)
                            {
                                mezo[xkoord + j, ykoord] = i + 1;
                            }
                            siker = true;
                        }
                    }
                }

            }

        }

        static public void mezokiir(int[,] mezo, int mezohossz)
        {
            for (int i = 0; i < mezohossz; i++)
            {
                for (int j = 0; j < mezohossz; j++)
                {
                    Console.Write(mezo[i, j]);
                }
                Console.WriteLine();
            }
        }

        //---------------------------------------------------------------------------

       static public void ElerhetoHajok(int[] hajok)
        {
            for(int i =1;i<hajok.Length;i++)
            {
                if (hajok[i] == 0)
                {
                    Console.WriteLine("{0} egységű hajó elérhető.",i);
                }
            }
        }


        static public bool szabadE(int[,] mezo, int mezomeret, int koordx, int koordy)
        {
            if (koordx >= mezomeret || koordy >= mezomeret) return false;
            if(mezo[koordx,koordy]==0)
            {
                return true;
            }
            return false;
        }

        static public bool HelyesKovetkezoKoord(int[,] mezo, int mezomeret, int koordx, int koordy, int hajoszam)
        {
            if (koordx >= mezomeret || koordy >= mezomeret) return false;
            if ((koordx <= mezomeret || koordy <= mezomeret) && (mezo[koordx - 1, koordy] == hajoszam || mezo[koordx, koordy - 1] == hajoszam || mezo[koordx + 1, koordy] == hajoszam || mezo[koordx, koordy + 1] == hajoszam)) return true;
            return false;
        }

        static void Main(string[] args)
        {
            Console.Write("Írja be a tábla méretét (1 szám):");
            int mezomeret = int.Parse(Console.ReadLine());

            int[,] jatekos1 = new int[mezomeret, mezomeret];
            int[,] jatekos2 = new int[mezomeret, mezomeret];
            int[,] gep1 = new int[mezomeret, mezomeret];
            int[,] gep2 = new int[mezomeret, mezomeret];

            AIhajokelhejezese(gep1, mezomeret);
            tablatakaritas(gep2, mezomeret);

            tablatakaritas(jatekos1, mezomeret);
            mezokiir(jatekos1, mezomeret);
            tablatakaritas(jatekos2, mezomeret);


            int[] hajok = new int[mezomeret];
            for (int i = 0; i < hajok.Length; i++)
            {
                hajok[i] = 0;
            }

            Console.Write("új hajó felvétele(2): ");
            int menu = int.Parse(Console.ReadLine());

            if (menu == 2)
            {
                System.Console.Clear();
                bool ok = false;
                StreamWriter sw = new StreamWriter(@"C:\Users\delld\Desktop\Mi csak inni jöttünk!\gephajoi.txt");

                while (!ok)
                {
                    ElerhetoHajok(hajok);
                    Console.Write("hány egységű hajót szeretne lerakni: ");
                    int melyikhajo = int.Parse(Console.ReadLine());
                    if (hajok[melyikhajo] ==0)
                    {
                        Console.Write("Írja be a hajója kezdő xkoordinátáját: ");
                        int startxkoord = int.Parse(Console.ReadLine());
                        Console.Write("Írja be a hajója kezdő ykoordinátáját: ");
                        int startykoord = int.Parse(Console.ReadLine());

                        if (szabadE(jatekos1, mezomeret, startxkoord, startykoord))
                        {
                            hajok[melyikhajo]++;
                            sw.WriteLine("{0},{1} koordinátán van egy {2} egységű hajó", startxkoord, startykoord, melyikhajo);
                            for (int i = 1; i < melyikhajo; i++)
                            {
                                Console.Write("írja be a következő x koordinátát:");
                                int xkovkoord = int.Parse(Console.ReadLine());
                                Console.Write("írja be a következő y koordinátát:");
                                int ykovkoord = int.Parse(Console.ReadLine());
                                if(HelyesKovetkezoKoord(jatekos1,mezomeret,xkovkoord,ykovkoord,melyikhajo))
                                {
                                    jatekos1[xkovkoord, ykovkoord] = melyikhajo;
                                    hajok[melyikhajo]++;
                                    sw.WriteLine("{0},{1} koordinátán van egy {2} egységű hajó", xkovkoord, ykovkoord, melyikhajo);
                                    sw.Close();

                                }
                                Console.Write("ha befejezte a hajók lerakását nyomja meg a 0-s gombot!");
                                int n = int.Parse(Console.ReadLine());
                                if(n==0)
                                    ok = true;
                            }
                        }
                        mezokiir(jatekos2, mezomeret);
                    }
                }
            }
            
                             

            Console.ReadLine();
        }
    }
}
