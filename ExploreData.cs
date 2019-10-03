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

namespace DarkDemo
{   public partial class ExploreData : Form
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
        //function used for coloring the income category stuff
        private void RowsColor()
        {
            ///valori hardcodate pt ca sectiunea aia nu se schimba niciodata.primele 3 is  albastre, urm 4 is verzi si ultima e rosie mereu
            for(int i=0;i<dataGridView2.Rows.Count;i++)
            {
                if(i<3)
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
                if(i>=3 && i<7)
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.ForestGreen;
                }
                if (i == 7)
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }
        private void InitializePanel()
        {
            //First range of values
            var range = $"{sheet}!A1:D5000"; //modifiable. I used an enormous range to be sure that i get all the entries
            var request = service.Spreadsheets.Values.Get(SpreadSheetId, range); //modifiable
            var response = request.Execute(); //always present
            var values = response.Values; //always present too
            
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {

                    int n = dataGridView1.Rows.Add();
                    for (int i = 0; i < 4; i++)
                    {
                        dataGridView1.Rows[n].Cells[i].Value = row[i];
                    }
                   
                }
               
            }
            //second range of values
            var secondRange = $"{sheet}!F3:G10";
            var secondRequest = service.Spreadsheets.Values.Get(SpreadSheetId, secondRange); //modifiable
            var secondResponse = secondRequest.Execute(); //always present
            var values2 = secondResponse.Values; //always present too

            if (values2 != null && values2.Count > 0)
            {
                foreach (var row in values2)
                {
                    int n = dataGridView2.Rows.Add();
                    for (int i = 0; i < 2; i++)
                    {
                        dataGridView2.Rows[n].Cells[i].Value = row[i];
                    }
                }

                RowsColor(); //coloram bby
            }
        }

        public ExploreData()
        {
            InitializeComponent();

            InitializeSheet();

            InitializePanel();


            dataGridView2.RowHeadersWidth = 4;//hardcodez o valoare ca sa fie primul camp mic ca e enerevant
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
