using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demoexwpf
{
    /// <summary>
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();

            var allTypes = ToursEntities.GetContext().Type.ToList();
            allTypes.Insert(0, new Type
            {
                Name = "Все типы"
            }
                );
            ComboType.ItemsSource = allTypes;

            CheckIsActual.IsChecked = true;
            ComboType.SelectedIndex = 0;

            UpdateTours();
            //ChangeColor(LVTors.FindName("ISActual") as TextBlock);
        }

        private void UpdateTours()
        {            
            var currentTours = ToursEntities.GetContext().Tour.ToList();

            if (ComboType.SelectedIndex > 0)
                currentTours = currentTours.Where(p => p.Type.Contains(ComboType.SelectedItem as Type)).ToList();

            currentTours = currentTours.Where(p => p.Name.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();

            if (CheckIsActual.IsChecked.Value)
                currentTours = currentTours.Where(p => p.IsActual).ToList();

            LVTors.ItemsSource = currentTours.OrderBy(p => p.TicketCount).ToList();            
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckIsActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckIsActual_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeColor(TextBlock text)
        {
            if (text.Text == "Актуален")
                text.Foreground = Brushes.Green;
            else
                text.Foreground = Brushes.Red;
        }
    }
}
