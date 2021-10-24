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
        static readonly string PLAYERDATA_FOLDER_PATH = @"C:\Users\Dainsleif_fractal\source\repos\Test_Program1\";
        static readonly string PLAYERDATA_FILE = "player.txt";
        static readonly string CSV_FOLDER_PATH = @"C:\Users\Dainsleif_fractal\source\repos\Test_Program1\";
        static readonly string WEAPON_CSV = "weapon.csv";
        List<string> lWeapon;
        DataTable dtWeapon;
        DataSet dtWeaponSet;
        List<long> PlayerWeaponList;

        public MainForm()
        {
            dtWeapon = new DataTable();
            dtWeaponSet = new DataSet();

            //Read_CSV_to_DATATABLE_Test1();
            //Read_CSV_to_DATATABLE_Test2();
            //Read_CSV_to_DATATABLE_Test3();
            //Read_CSV_to_DATATABLE_Test4();
            Read_CSV_to_DATATABLE_Test4_Plus();
            //Read_CSV_to_LIST_Test1();
            //Read_CSV_to_LIST_Test2();
            Load_Data_Test1();
            Save_Data_Test1();            
        }


        //[失敗]あまり上手くいかない
        void Read_CSV_to_DATATABLE_Test1()
        {
            StreamReader file;
            file = new StreamReader(CSV_FOLDER_PATH + WEAPON_CSV);
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
            using (TextFieldParser csv = new TextFieldParser(CSV_FOLDER_PATH + WEAPON_CSV))
            {
                csv.SetDelimiters(new string[] { "," });
                csv.CommentTokens = new string[] { "#" };
                csv.TrimWhiteSpace = true;
                //Line = 行 = 横
                //カラム数の割り出し
                int c = csv.ReadLine().Length;
                string l = csv.ReadLine();
                string[] str = l.Split(',');
                Console.WriteLine($"{c}");
                for (int i = 0; i < c; i++)
                {
                    dtWeapon.Columns.Add(new DataColumn(Convert.ToString(str[i])));
                }
                Console.WriteLine($"{str[14]}");
                //dtWeapon.Rows.Add(csv.ReadLine());
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
            var csv = new TextFieldParser(CSV_FOLDER_PATH + WEAPON_CSV)
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = new string[] { "," }
            };
            //ヘッダー読み込みと記述があるが最初の1行しか読まない挙動コード
            var heads = csv.ReadFields();
            //Console.WriteLine($"{heads[18]}");

        }

        //なんとかcsvをデータテーブルに格納できた
        void Read_CSV_to_DATATABLE_Test4()
        {
            TextFieldParser csv = new TextFieldParser(CSV_FOLDER_PATH + WEAPON_CSV);
            csv.CommentTokens = new string[] { "#" };
            csv.Delimiters = new string[] { "," };
            //↓ReadLineはコメント行を関係なく開始1行目を読み込む
            //string l = csv.ReadLine();

            //DataTableにヘッダーを作成
            //↓ReadFieldsはコメント行を飛ばした1行目を読み込む
            string[] header = csv.ReadFields();
            Console.WriteLine($"{header[1]}");
            for (int i = 0; i < header.Length; i++)
            {
                //header.Lengthの分だけカラムを作成
                dtWeapon.Columns.Add(header[i]);
            }


            //このあとwhileを使ってEndOfDataで最後の行まで読み込む処理を書く
            while (!csv.EndOfData)
            {
                //csvの内容を一行ずつDataTableに格納
                string[] l = csv.ReadFields();
                dtWeapon.Rows.Add(l);
            }


            foreach (DataRow dataRow in dtWeapon.Rows)
            {
                Console.WriteLine(string.Join(",", Array.ConvertAll(dataRow.ItemArray, x => x.ToString())));
            }
        }

        void Read_CSV_to_DATATABLE_Test4_Plus()
        {
            //フォルダ内のcsvファイル(マスタ)を読みだす
            IEnumerable<string> CSV_FILES = Directory.EnumerateFiles(CSV_FOLDER_PATH).Where(str => str.EndsWith(".csv"));
            List<string> CSV_LIST = new List<string>();
            //フォルダ内のcsvファイルをリストに格納
            CSV_LIST = CSV_FILES.ToList();
            Console.WriteLine($"{CSV_LIST[0]}");
            for (int i = 0; i < CSV_LIST.Count; i++)
            {
                TextFieldParser csv = new TextFieldParser(CSV_LIST[i]);
                Console.WriteLine($"{CSV_LIST[i]}");
                csv.CommentTokens = new string[] { "#" };
                csv.Delimiters = new string[] { "," };

                string[] header = csv.ReadFields();
                Console.WriteLine($"{header[1]}");
                //文字を切り出す
                //参考URL(https://www.fenet.jp/dotnet/column/language/4521/)
                string CSV_FILE = CSV_LIST[i].Substring(CSV_FOLDER_PATH.Length);

                for (int j = 0; j < header.Length; j++)
                {
                    switch (CSV_FILE)
                    {
                        case "weapon.csv":
                            dtWeapon.Columns.Add(header[j]);
                            break;

                        default:
                            Console.WriteLine($"ヘッダー追加処理のcaseに下記ファイルを追加していない");
                            Console.WriteLine($"{CSV_LIST[i]}");
                            break;
                    }

                }

                while (!csv.EndOfData)
                {
                    string[] rf = csv.ReadFields();
                    switch (CSV_FILE)
                    {
                        case "weapon.csv":
                            dtWeapon.Rows.Add(rf);
                            break;

                        default:
                            Console.WriteLine($"データテーブル追加処理のcaseに下記ファイルを追加していない");
                            Console.WriteLine($"{CSV_LIST[i]}");
                            break;
                    }
                }

                DataRow[] drs = dtWeapon.Select("id='51'");
                foreach (DataRow d in drs)
                {
                    Console.WriteLine(d["name"]);
                }
                /*
                foreach (DataRow dataRow in dtWeapon.Rows)
                {
                    Console.WriteLine(string.Join(",", Array.ConvertAll(dataRow.ItemArray, x => x.ToString())));
                }
                */
            }
        }


        //[失敗]あまり上手くいかない
        void Read_CSV_to_LIST_Test1()
        {
            StreamReader file;
            file = new StreamReader(CSV_FOLDER_PATH + WEAPON_CSV);
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
            using (TextFieldParser csvReader = new TextFieldParser(CSV_FOLDER_PATH + WEAPON_CSV))
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

        //疑似ロード
        void Load_Data_Test1()
        {
            if (!System.IO.File.Exists(PLAYERDATA_FOLDER_PATH + PLAYERDATA_FILE))
            {
                Console.WriteLine($"true");
                FileInfo fi = new FileInfo(PLAYERDATA_FOLDER_PATH + PLAYERDATA_FILE);
                FileStream fs = fi.Create();
                fs.Close();
                using (StreamWriter sw = new StreamWriter(PLAYERDATA_FOLDER_PATH + PLAYERDATA_FILE))
                {
                    sw.Write("PLAYER_WEAPON_LIST_START" + PlayerWeaponList + "PLAYER_WEAPON_LIST_END");
                }
            }
            else
            {
                //指定文字から指定文字まで文字の切り抜き
                //参考URL(https://hamalabo.net/string-extraction)
                //参考URL(https://www.fenet.jp/dotnet/column/language/6249/)
                string str = File.ReadAllText(PLAYERDATA_FOLDER_PATH + PLAYERDATA_FILE);

                //最初から指定文字まで削除
                string str_rm = str.Remove(0, str.IndexOf("PLAYER_WEAPON_LIST_START"));
                //指定文字を変換
                str_rm = str_rm.Replace("PLAYER_WEAPON_LIST_START", "");
                //指定文字から最後まで削除
                str_rm = str_rm.Remove(str_rm.IndexOf("PLAYER_WEAPON_LIST_END"));
                Console.WriteLine($"{str_rm}");
            }
        }

        //疑似セーブ
        void Save_Data_Test1()
        {
            if (!System.IO.File.Exists(PLAYERDATA_FOLDER_PATH + PLAYERDATA_FILE))
            {
                Console.WriteLine($"true");
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(PLAYERDATA_FOLDER_PATH + PLAYERDATA_FILE))
                {
                    //所持武器のデータを格納
                    sw.Write("PLAYER_WEAPON_LIST_START" + PlayerWeaponList + "PLAYER_WEAPON_LIST_END");
                }
            }
        }




    }
}
