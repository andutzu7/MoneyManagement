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
    }
}
