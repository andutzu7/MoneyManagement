using DarkDemo;
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
{/// <summary>
/// /!!!!!!!!!!!!!!!!!! 
/// DE ADAUGAT DATA CHECK CA SA NU MAI PATESC CA IN CAZU CU LED ZEPELIN!!!!!!!!!!!!!!
/// </summary>
    public partial class Menu : Form
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
        /// panel area. i summon them as the app starts to b sure that i dont have 1000 windows open()
        /// </summary>


        AddEntry panel = new AddEntry();

        qmessage q = new qmessage();

        ExploreData d = new ExploreData();

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

        public Menu()
        {
            InitializeComponent();

            InitializeSheet();
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
         
        }
        public void HideForms()
        {
            List<Form> openForms = new List<Form>();

            foreach (Form f in Application.OpenForms)
                openForms.Add(f);

            foreach (Form f in openForms)
            {
                if (f.Name != "Menu")
                    f.Hide();
            }
        }
       
        private void Button1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            HideForms();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
            HideForms();
            panel.Show();
            
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
                    if (!(keyValuePairs.ContainsKey(row[3].ToString())))
                        {
                        keyValuePairs.Add(row[3].ToString(),Convert.ToDouble(row[1]));

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

        private void Button1_Click_1(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
     

        }

        private void Button3_Click(object sender, EventArgs e)
        {

            SidePanel.Height = button3.Height;
            SidePanel.Top = button3.Top;
            HideForms(); 
            ///this bullshit is made in order to keep the explore data stuff updated;
            d.Close();
            d = new ExploreData();
            d.Show();
            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button4.Height;
            SidePanel.Top = button4.Top;    
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button7.Height;
            SidePanel.Top = button7.Top;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button5.Height;
            SidePanel.Top = button5.Top;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button6.Height;
            SidePanel.Top = button6.Top;
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button13.Height;
            SidePanel.Top = button13.Top;
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            q.Show();
        }
    }
}
