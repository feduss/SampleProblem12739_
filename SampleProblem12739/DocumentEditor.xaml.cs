using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleProblem12739
{
    public partial class DocumentEditor : ContentPage
    {
        private StackLayout Dialog_;
        private StackLayout DialogBackGround_;
        private Label TitleLabel;
        private Label QuestionLabel;
        private ProgressBar ProgressBar;
        private String Path;
        private List<Scan> Scans;

        public DocumentEditor()
        {
            InitializeComponent();
            ListView ScanList = this.FindByName<ListView>("ScanList_");
            Dialog_ = this.FindByName<StackLayout>("Dialog");
            DialogBackGround_ = this.FindByName<StackLayout>("DialogBackGround");
            TitleLabel = this.FindByName<Label>("Title_");
            TitleLabel.Text = "Uploading...";
            QuestionLabel = this.FindByName<Label>("Question_");
            ProgressBar = this.FindByName<ProgressBar>("ProgressBar_");
            Path = Device.RuntimePlatform == Device.Android
                ? "TestImage.jpeg"
                : "Images/TestImage.jpeg";
            ImageSource Source = Device.RuntimePlatform == Device.Android
                ? ImageSource.FromFile(Path)
                : ImageSource.FromFile(Path);
            Scans = new List<Scan>();
            Scans.Add(new Scan(Source, "test", "n/a", "n/a"));
            ScanList.ItemsSource = Scans;
        }

        private void AddScan_Clicked(object sender, EventArgs e)
        {

        }

        private async void SendDoc_Clicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Confirm", "Do you want to send the image?", "Yes", "No");

            if (response)
            {
                DialogBackGround.IsVisible = true;
                Dialog.IsVisible = true;
                MultipartFormDataContent multiContent = new MultipartFormDataContent();

                /***ImageSource to stream is not present in my project***/
                //Ref: https://stackoverflow.com/a/37482681/11227702
                var assembly = this.GetType().GetTypeInfo().Assembly;
                byte[] buffer = null;

                using (System.IO.Stream s = assembly.GetManifestResourceStream("SampleProblem12739.TestImage.jpeg"))
                {
                    if (s != null)
                    {
                        long length = s.Length;
                        buffer = new byte[length];
                        s.Read(buffer, 0, (int)length);
                    }
                }
                /***********/
                int i = 0;
                var fileStreamContent1 = new ProgressableStreamContent(new ByteArrayContent(buffer)
                            , (sent, total) => {
                                int Percentage = (int)(((float)sent / total));
                                int num = i;
                                Task.Run(() => {
                                    QuestionLabel.Text = "Uploading scan num" + i + 1 + "...";
                                    ProgressBar.Progress = Percentage;
                                });
                            });

                //var fileStreamContent1 = new ByteArrayContent(File.ReadAllBytes(Path));

                multiContent.Add(fileStreamContent1, "image", "n/a");

                try
                {
                    HttpClient client = new HttpClient();
                    client.Timeout = TimeSpan.FromMinutes(2);
                    HttpResponseMessage response2 = await client.PostAsync("https://postimages.org/", multiContent);
                    client.Dispose();
                    //String responseString = await response2.Content.ReadAsStringAsync();
                    //await DisplayAlert("Result", "Upload response: " + responseString, "Ok");
                    await DisplayAlert("Result", "Post Async completed! Press ok to return to MainPage and to see the bug.", "Ok");
                    await Navigation.PopToRootAsync();


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString(), "Error: ");
                    Dialog_.IsVisible = false;
                    DialogBackGround_.IsVisible = false;
                    await DisplayAlert("Errore", "An error occurs: " + ex.Message, "Ok");
                }
            }
        }

        private void DeleteDoc_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}
