using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ActividadFlickr1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            claseFlickr();
        }


        public async void claseFlickr()
        {

            HttpClient cliente = new HttpClient();

            string url = "https://api.flickr.com/services/rest/?" +
                "method=flickr.photos.search&" +
                "api_key=4445a1e2866e4b2d68351bde81fce75c&" +
                "user_id=144298112%40N04&" +
                "format=json" +
                "&nojsoncallback=1&";

            url = string.Format(url);

            string resultadoFlickr = await cliente.GetStringAsync(url);
            //Resultado.Text = resultadoFlickr;

            FlickrData apiData = JsonConvert.DeserializeObject<FlickrData>(resultadoFlickr);

            if (apiData.stat == "ok")
            {
                foreach (Photo data in apiData.photos.photo)
                {
                   string photoUrl = "http://farm{0}.staticflickr.com/{1}/{2}_{3}_n.jpg";

                    string baseFlickrUrl = string.Format(photoUrl,
                        data.farm,
                        data.server,
                        data.id,
                        data.secret);

                    
                    MiFlickr.Source = new BitmapImage(new Uri(baseFlickrUrl));

                    
                    break;
                }// cierre foreach
            }//cierre if

        }

    }//partial class

    public class Photo
    {
        public string id { get; set; }
        public string owner { get; set; }
        public string secret { get; set; }
        public string server { get; set; }
        public int farm { get; set; }
        public string title { get; set; }
        public int ispublic { get; set; }
        public int isfriend { get; set; }
        public int isfamily { get; set; }
    }

    public class Photos
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int perpage { get; set; }
        public string total { get; set; }
        public List<Photo> photo { get; set; }
    }

    public class FlickrData
    {
        public Photos photos { get; set; }
        public string stat { get; set; }
    }

}//namespace


