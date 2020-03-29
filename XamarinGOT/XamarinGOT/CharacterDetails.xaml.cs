using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinGOT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterDetails : ContentPage
    {
        public CharacterDetails()
        {
            InitializeComponent();
        }

        public CharacterDetails(string url) {
        }
    }
}