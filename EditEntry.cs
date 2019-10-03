﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// de adaugat o chestie ca atunci cand cauti 2 treburi diferite sa se goleasca tabelu precedent.
/// /// DE ADAUGAT DATA CHECK CA SA NU FIE CUMVA EDITATE DATE CORECTE IN DATE FRAUDULOASE !!!!!!!!!!!!!!!!
/// DE FACUT POST LUNG CA SA INTELEG SI EU CE DRACU AM FACUT IN VIITOR CAND RECITESC CODU

/// </summary>
namespace DarkDemo
{
    public partial class EditEntry : Form
    {

        //change the root path with ur own one
        readonly string rootPath = @"G:\interesting shit\MoneyManagement";

        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

        static readonly string ApplicationName = "MoneyM";

        //also change with your own
        static readonly string SpreadSheetId = "1LAF4vjm9BpkRFgcUghiMlcZ_aYimJOHR2Mf039HVwnU";

        static readonly string sheet = "template";

        static SheetsService service;

        static GoogleCredential credential;

        List<int> index = new List<int>(); //se putea fara o solutie de ? aici tin index pt toate entryurile ca sa stiu unde modific
        private void InitializeSheet()
        {
            using (var stream = new FileStream(Path.Combine(rootPath, @"MoneyManagement-ff042d0d31ae.json"), FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


        }
        public EditEntry()
        {
            InitializeComponent();

            InitializeSheet();
        }

        private void EditEntry_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var range = $"{sheet}!A1:D5000"; //modifiable. I used an enormous range to be sure that i get all the entries
            var request = service.Spreadsheets.Values.Get(SpreadSheetId, range); //modifiable
            var response = request.Execute(); //always present
            var values = response.Values; //always present too

            var response2 = request.Execute(); //always present
            var values2 = response2.Values;// AM DESCOPERIT POINTERI IN C# INTR UN MOMENT FOARTE INOPORTUN:)))
            List<IList<object>> rows = new List<IList<object>>();//where i store the damn rows
            int count = -1;
            if (values != null && values.Count > 0)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    count++;//incremetam rowcountul
                    var individualRow = values[i];
                    //o secventa foarte urata si neoptimizata de if-uri

                    if (textBox1.Text == string.Empty)
                    {
                        individualRow[0] = "";

                    }
                    if (textBox2.Text == string.Empty)
                    {
                        individualRow[1] = "";

                    }
                    if (textBox3.Text == string.Empty)
                    {
                        individualRow[2] = "";

                    }
                    if (textBox4.Text == string.Empty)
                    {
                        individualRow[3] = "";

                    }
                    if (individualRow[0].ToString() == textBox1.Text && individualRow[1].ToString() == textBox2.Text &&
                        individualRow[2].ToString() == textBox3.Text && individualRow[3].ToString() == textBox4.Text)
                    {
                        //its a match
                        rows.Add(values2[i]);
                        index.Add(count);
                    }

                }
                foreach (var item in rows)
                {
                    int n = dataGridView1.Rows.Add();
                    for (int i = 0; i < 4; i++)
                    {
                        dataGridView1.Rows[n].Cells[i].Value = item[i].ToString();
                    }

                }
            }

            }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            var range = $"{sheet}!A1:D5000"; //modifiable. I used an enormous range to be sure that i get all the entries
            var request = service.Spreadsheets.Values.Get(SpreadSheetId, range); //modifiable
            var response = request.Execute(); //always present
            var values = response.Values; //always present too imi trebuie ca sa ma pot deplasas prin spread

           
            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";//"ROWS";//COLUMNS GOOOGLE API STUFF, BY DEFAULT ITS ROWS BUT TO BE SURE:))
            int itemIndex = 0;
            for (int i = 0; i < values.Count; i++)
            {
                if(index.Contains(i)) //daca pe pozitia i a fost gasit un match
                {
                    var l = new List<object>() { dataGridView1.Rows[itemIndex].Cells[0].Value.ToString(), dataGridView1.Rows[itemIndex].Cells[1].Value.ToString() ,
                    dataGridView1.Rows[itemIndex].Cells[2].Value.ToString(),dataGridView1.Rows[itemIndex].Cells[3].Value.ToString()};
                    valueRange.Values = new List<IList<object>> { l };
                    SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, SpreadSheetId, $"{sheet}!A{i+1}:D{i+1}");
                    update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    update.Execute();
                    
                    itemIndex++;
                }
                if (itemIndex == dataGridView1.Rows.Count)
                    break;

            }

        }
    }
    }