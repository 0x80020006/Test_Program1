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
            //Read_CSV_to_DATATABLE_Test2();
            //Read_CSV_to_DATATABLE_Test3();
            //Read_CSV_to_LIST_Test1();
            //Read_CSV_to_LIST_Test2();

        }


        //[失敗]あまり上手くいかない
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

        //[失敗]あまり上手くいかない
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

        //[失敗]あまり上手くいかない
        //参考URL(https://vviilloovv.hatenablog.com/entry/2019/01/28/173420)
        void Read_CSV_to_DATATABLE_Test3()
        {
            //var path = FOLDER_PATH;
            //var csvs = Directory.GetFiles(path);
            // 読み込んだファイル数分ループ
            //foreach (var csv in csvs)
            //{
                // CSV読み込み
            var csv = new TextFieldParser(FOLDER_PATH + WEAPON_CSV)
            {
              TextFieldType = FieldType.Delimited,
              Delimiters = new string[] { "," }
            };
            //ヘッダー読み込みと記述があるが最初の1行しか読まない挙動コード
            var heads = csv.ReadFields();
            //Console.WriteLine($"{heads[18]}");

        }

        //参考URL(https://resanaplaza.com/2020/09/01/%E3%80%90c%E3%80%91csv%E3%81%AE%E8%AA%AD%E3%81%BF%E8%BE%BC%E3%81%BF%E3%81%A8%E6%9B%B8%E3%81%8D%E8%BE%BC%E3%81%BF%E3%82%92%E9%83%A8%E5%93%81%E5%8C%96%E3%81%97%E3%81%BE%E3%81%97%E3%81%9F%EF%BC%81/#CSVDataTable)
        void Read_CSV_to_DATATABLE_Test4()
        {

        }


        //[失敗]あまり上手くいかない
        void Read_CSV_to_LIST_Test1()
        {
            StreamReader file;
            file = new StreamReader(FOLDER_PATH + WEAPON_CSV);
            lWeapon = new List<string>();
            string l = file.ReadLine();
            string[] str = l.Split(',');
            lWeapon.AddRange(str);
        }

        //[失敗]あまり上手くいかない
        void Read_CSV_to_LIST_Test2()
        {
            lWeapon = new List<string>();
            //csvを簡単に読み込める？
            using (TextFieldParser csvReader = new TextFieldParser(FOLDER_PATH + WEAPON_CSV))
            {
                //csvでコメントアウトできるっぽい
                csvReader.CommentTokens = new string[] { "#" };
                //csvでセルを区切る(.Split(',')と同じ効果だがReadLineには無効っぽい？)
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
