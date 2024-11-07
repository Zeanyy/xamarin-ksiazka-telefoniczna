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
    public partial class ModifyPage : ContentPage
    {
        private int id;
        private MainPage main;
        public ModifyPage(MainPage main, Person person)
        {
            InitializeComponent();
            this.main = main;

            this.id = person.id;
            entryName.Text = person.name;
            entrySurname.Text = person.surname;
            radioMale.IsChecked = true;
            if(person.gender == "k") { radioFemale.IsChecked = true; }
            entryEmail.Text = person.email;
            pickerState.SelectedItem = person.state;
            entryNumber.Text = person.number.ToString();
        }

        private void editPerson(object sender, EventArgs e)
        {
            string alert = "";
            errorLabel.Text = alert;
            bool check = true;

            string name = entryName.Text;
            string surname = entrySurname.Text;
            string gender = "m";
            if (radioFemale.IsChecked == true)
            {
                gender = "k";
            }
            string email = entryEmail.Text;
            string state = (pickerState.SelectedItem != null) ? pickerState.SelectedItem.ToString() : null;
            string number = entryNumber.Text;

            string nameRegex = "^[A-Z]{1}[a-ząćęłńóśźżĄĆĘŁŃÓŚŹŻ]+$";
            if (name == null || !Regex.IsMatch(name, nameRegex))
            {
                check = false;
                alert += "* Imie jest niepoprawne\n";
            }
            string surnameRegex = "^[A-Z]{1}[a-ząćęłńóśźżĄĆĘŁŃÓŚŹŻ]+$";
            if (surname == null || !Regex.IsMatch(surname, surnameRegex))
            {
                check = false;
                alert += "* Nazwisko jest niepoprawne\n";
            }
            string emailRegex = "^[a-zA-Z0-9_.+-]+@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}$";
            if (email == null || !Regex.IsMatch(email, emailRegex))
            {
                check = false;
                alert += "* Email jest niepoprawny\n";
            }
            if (state == null)
            {
                check = false;
                alert += "* Wojewodztwo nie jest wybrane\n";
            }
            string numberRegex = "^[0-9]{9}$";
            if (number == null || !Regex.IsMatch(number, numberRegex))
            {
                check = false;
                alert += "* Numer jest niepoprawny\n";
            }

            if (!check)
            {
                errorLabel.Text = alert;
            }
            else
            {
                Model.modifyPerson(id, name, surname, gender, email, state, Convert.ToInt32(number));
                Navigation.PopAsync();
                main.refresh();
            }
        }
    }
}