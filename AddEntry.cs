using Google.Apis.Auth.OAuth2;
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

namespace DarkDemo
{

    public partial class AddEntry : Form
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


        static bool once = false;
        

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
        public AddEntry()
        {
            InitializeComponent();

            InitializeSheet();
        }

        private void AddEntry_Load(object sender, EventArgs e)
        {

        }

        void CreateEntry(object rowA,object rowB,object rowC,object rowD)
        {
            var range = $"{sheet}!A:D";
            var valueRange = new ValueRange();

            var objectList = new List<object>() { rowA, rowB, rowC, rowD };
            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadSheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse = appendRequest.Execute();

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
           // textBox4.Text = "20 apr 2019";
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!once)
            {
                CreateEntry(textBox1.Text, textBox2.Text, textBox3.Text, "44");
                once = true;
            }
        }
    }
}
