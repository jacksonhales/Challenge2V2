using Challenge2_v2.ClassLibrary;
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

namespace Challenge2_v2.WPF
{
    /// <summary>
    /// Interaction logic for Procedures.xaml
    /// </summary>
    public partial class Procedures : Page
    {

        APIClient apiClient;
        List<Procedure> allProcedures;
        List<Treatment> allTreatments;
        List<Pet> allPets;
        List<PetTreatmentsView> petTreatmentsView;
        Procedure selectedProcedure;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Procedures()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                apiClient = new APIClient();

                allProcedures = await apiClient.GetProcedures();
                dgrProcedures.ItemsSource = allProcedures;

                allTreatments = await apiClient.GetTreatments();
                allPets = await apiClient.GetPets();
            }
            catch (Exception ex)
            {
                logger.Fatal("Unknown error.");
                MessageBox.Show(ex.Message);
            }
        }

        private void dgrProcedures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (dgrProcedures.SelectedItem != null)
                {
                    selectedProcedure = (Procedure)dgrProcedures.SelectedItem;
                }

                petTreatmentsView = (from p in allPets
                                     join t in allTreatments on p.PetID equals t.PetID
                                     where t.ProcedureID == selectedProcedure.ProcedureID
                                     select new PetTreatmentsView
                                     {
                                         Name = p.Name,
                                         Type = p.Type,
                                         Date = t.Date,
                                         Notes = t.Notes,
                                         Price = t.Price
                                     }).ToList();

                dgrTreatments.ItemsSource = petTreatmentsView;
            }
            catch (Exception ex)
            {
                logger.Fatal("Unknown error.");
                MessageBox.Show(ex.Message);
            }
        }
    }
}
