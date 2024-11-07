using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace Knonkaty
{
    public partial class MainPage : FlyoutPage
    {
        static int page = 0;
        static int maxPage;

        public MainPage()
        {
            InitializeComponent();
            Model.connect();
            refresh();
        }
        public void refresh()
        {
            if (Model.getPersons(page, searchBar.Text).Count <= 0) { page--; labelPage.Text = (page + 1).ToString(); }

            var count = Model.countPersons(searchBar.Text);
            if (count == 0) { count = 1; page = 0; labelPage.Text = (page + 1).ToString(); }
            maxPage = (count % Model.limit == 0) ? count / Model.limit : count / Model.limit + 1;

            double progress = (Convert.ToDouble(page) + 1) / Convert.ToDouble(maxPage);
            progressBar.Progress = progress;

            listView.ItemsSource = Model.getPersons(page, searchBar.Text);
            listView.SelectedItem = null;
        }
        private void searchValue(object sender, TextChangedEventArgs e)
        {
            refresh();
        }
        private void addPerson(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPage(this));
        }
        private void removePerson(object sender, EventArgs e)
        {
            Person person;
            if(sender is MenuItem)
            {
                MenuItem item = sender as MenuItem;
                person = (Person)item.BindingContext;
            }
            else
            {
                person = (Person)listView.SelectedItem;
                if (person == null) { return; }
            }
            Model.removePerson(person.id);
            refresh();
        }
        private void mofdifyPerson(object sender, EventArgs e)
        {
            Person person;
            if (sender is MenuItem)
            {
                MenuItem item = sender as MenuItem;
                person = (Person)item.BindingContext;
            }
            else
            {
                person = (Person)listView.SelectedItem;
                if (person == null) { return; }
            }
            Navigation.PushAsync(new ModifyPage(this, person));
        }
        private void closeTable(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void changeTable(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangeTablePage(this));
        }
        private void previousPage(object sender, EventArgs e)
        {
            if (page <= 0) { return; }
            page--;
            labelPage.Text = (page + 1).ToString();
            refresh();
        }
        private void nextPage(object sender, EventArgs e)
        {
            if (page >= maxPage - 1) { return; }
            page++;
            labelPage.Text = (page + 1).ToString();
            refresh();
        }
    }
}
