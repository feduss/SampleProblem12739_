using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleProblem12739
{
    public partial class DocumentList : ContentPage
    {
        public DocumentList()
        {
            InitializeComponent();

            ListView DocList = this.FindByName<ListView>("DocList_");
            List<DocumentSaved> DocumentsSaved = new List<DocumentSaved>();
            DocumentsSaved.Add(new DocumentSaved("Document", "N.Scan", "Last Change"));
            //Add fake document (because this is a sample)
            DateTime Now = DateTime.Now;
            String NowString = Now.ToString("dd-MM-yyyy-HH:mm:ss");
            DocumentsSaved.Add(new DocumentSaved("Test1", "1", NowString));
            DocumentsSaved.Add(new DocumentSaved("Test2", "1", NowString));
            DocList.ItemsSource = DocumentsSaved;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Same handle for both document, because this is a sample
            await Navigation.PushAsync(new DocumentEditor());
        }
    }
}
