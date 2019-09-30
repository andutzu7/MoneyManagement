using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
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

namespace MoneyManagement
{
    public partial class Form1 : Form
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

        /// <summary>
        /// The initialize Sheet Is a function that has to be called when the form is created in order to connect to the 
        /// google sheet. Also, all the things declared above are google api stuff, dont rly fully comprehend it yet.
        /// </summary>
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
        public Form1()
        {
            InitializeComponent();

            InitializeSheet();
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
         
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
         

        }



        #region Some bullshit
        /*      private void checkBox5_CheckedChanged(object sender, EventArgs e)
              {
                  var range = $"{sheet}!A1:D3"; //modifiable
                  var request = service.Spreadsheets.Values.Get(SpreadSheetId, range); //modifiable
                  var response = request.Execute(); //always present
                  var values = response.Values; //always present too
                  if (values != null && values.Count > 0)
                  {
                      foreach (var row in values)
                      {
                          textBox1.Text = row[1].ToString();
                      }
                  }

              }*/

        /*      static void ReadEntries()
             {
                 var range = $"{sheet}!A1:D3";
                 var request = service.Spreadsheets.Values.Get(SpreadSheetId,range);

                 var response = request.Execute();
                 var values = response.Values;
                 if (values != null && values.Count > 0)
                 {
                     foreach (var row in values)
                     {
                         Console.WriteLine("{0} {1} | {2} | {3} ", row[3],row[2],row[1],row[0]);
                     }
                 }

             }
         */
        #endregion

        
       

        private void Button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void Chart2_Click(object sender, EventArgs e)
        {

        }

        private void Button15_Click(object sender, EventArgs e)
        {
            var range = $"{sheet}!A1:D5000"; //modifiable
            var request = service.Spreadsheets.Values.Get(SpreadSheetId, range); //modifiable
            var response = request.Execute(); //always present
            var values = response.Values; //always present too
            if (values != null && values.Count > 0)
            {
                string peleu = string.Empty;
                foreach (var row in values)
                {
                    for (int i = 0; i < row.Count; i++)
                    {
                        peleu += row[i];
                    }
                }
            }

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var range = $"{sheet}!A1:D500"; //modifiable. I used an enormous range to be sure that i get all the entries
            var request = service.Spreadsheets.Values.Get(SpreadSheetId, range); //modifiable
            var response = request.Execute(); //always present
            var values = response.Values; //always present too
            Dictionary<string, double> keyValuePairs = new Dictionary<string, double>(); 
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    /*
                    if (!(l.Contains(row[3].ToString())))
                    {
                        l.Add(row[3].ToString());
                        chart1.Series.Add(row[3].ToString());
                        chart1.Series[row[3].ToString()].Points.AddY(row[1]);
                    }
                    else
                    {

                       chart1.Series[row[3].ToString()].Points
                        //chart1.Series["cig"].Points.AddY(row[1]);

                    }*/
                    if (!(keyValuePairs.ContainsKey(row[3].ToString())))
                        {
                        keyValuePairs.Add(row[3].ToString(),Convert.ToDouble(row[1].ToString()));

                    }
                    else
                    {
                        keyValuePairs[row[3].ToString()] += Convert.ToDouble(row[1].ToString());
                    }
                                                         
                }
                foreach (var item in keyValuePairs)
                {
                    chart1.Series.Add(item.Key);
                    chart1.Series[item.Key].Points.AddY(item.Value);
                }
            }
        }
    }
}
