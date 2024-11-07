using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Knonkaty
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeTablePage : ContentPage
    {
        private MainPage main;
        public ChangeTablePage(MainPage main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void addTable(object sender, EventArgs e)
        {
            string name = entryName.Text;
            string nameRegex = "^[a-zA-Z0-9_]+$";

            if (!Regex.IsMatch(name, nameRegex))
            {
                errorLabel.Text = "* Nazwa bazy jest niepoprawna";
            }
            else
            {
                Model.addTable(name);
                Navigation.PopAsync();
                main.refresh();
            }
        }
    }
}