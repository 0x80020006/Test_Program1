using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace Test_Program1
{
    class MainForm : Form
    {
        static readonly string FOLDER_PATH = @"C:\Users\Dainsleif_fractal\source\repos\Test_Program1\";
        static readonly string WEAPON_CSV = "weapon.csv";
        List<string> lWeapon;
        DataTable dtWeapon;
        DataSet dtWeaponSet;

        public MainForm()
        {

            dtWeapon = new DataTable();
            dtWeaponSet = new DataSet();

            //Read_CSV_to_DATATABLE_Test1();
            Read_CSV_to_DATATABLE_Test2();
            //Read_CSV_to_LIST_Test1();
            //Read_CSV_to_LIST_Test2();

        }



        void Read_CSV_to_DATATABLE_Test1()
        {
            StreamReader file;
            file = new StreamReader(FOLDER_PATH + WEAPON_CSV);
            string l = file.ReadLine();
            string[] str = l.Split(',');
            int c = str.Length;
            Console.WriteLine($"{c}");
            for (int i = 0; i < c; i++)
            {
                dtWeapon.Columns.Add(new DataColumn(str[i]));
            }
            
            while (file.EndOfStream == false)
            {

                dtWeaponSet.Tables.Add(dtWeapon);

            }
        }

        void Read_CSV_to_DATATABLE_Test2()
        {
            using (TextFieldParser csv = new TextFieldParser(FOLDER_PATH + WEAPON_CSV))
            {
                csv.SetDelimiters(new string[] { "," });
                csv.CommentTokens = new string[] { "#" };
                csv.TrimWhiteSpace = true;
                //Line = 行 = 横
                //カラム数の割り出し
                int c = csv.ReadLine().Length;
                string l = csv.ReadLine();
                string [] str = l.Split(',');
                Console.WriteLine($"{c}");
                for (int i = 0; i < c; i++)
                {
                    dtWeapon.Columns.Add(new DataColumn(Convert.ToString(str[i])));
                }
                dtWeapon.Rows.Add(csv.ReadLine());
            }
        }

        void Read_CSV_to_LIST_Test1()
        {
            StreamReader file;
            file = new StreamReader(FOLDER_PATH + WEAPON_CSV);
            lWeapon = new List<string>();
            string l = file.ReadLine();
            string[] str = l.Split(',');
            lWeapon.AddRange(str);
        }

        void Read_CSV_to_LIST_Test2()
        {
            lWeapon = new List<string>();
            //csvを簡単に読み込める？
            using (TextFieldParser csvReader = new TextFieldParser(FOLDER_PATH + WEAPON_CSV))
            {
                //csvでコメントアウトできるっぽい
                csvReader.CommentTokens = new string[] { "#" };
                //csvでセルを区切る(.Split(',')と同じ効果)
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                csvReader.ReadLine();

                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    lWeapon.AddRange(fields);
                }
            }
            Console.WriteLine($"{lWeapon[2]}");
        }


    }
}
