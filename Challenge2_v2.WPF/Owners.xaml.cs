using Challenge2_v2.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Owners.xaml
    /// </summary>
    public partial class Owners : Page
    {
        APIClient apiClient;
        List<Owner> allOwners;
        List<Pet> allPets;
        List<Pet> ownersPets;
        Owner selectedOwner;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Owners()
        {
            InitializeComponent();
        }

        private void dgrOwners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 

                if (dgrOwners.SelectedItem != null)
                {
                    selectedOwner = (Owner)dgrOwners.SelectedItem;
                    txtSurname.Text = selectedOwner.Surname;
                    txtGivenName.Text = selectedOwner.GivenName;
                    txtPhone.Text = selectedOwner.Phone;
                }
            }
            catch (Exception ex)
            {
                logger.Fatal("Unknown error");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnCreate.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Given Name validation
                if (String.IsNullOrEmpty(txtGivenName.Text))
                {
                    throw new ValidationFailureException("Given Name field empty, please enter a first name and retry.");
                }
                if (txtGivenName.Text.Any(t => char.IsDigit(t)))
                {
                    throw new ValidationFailureException("Given Name contains numbers, please remove any numbers from the first name field and retry.");
                }
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
                if (!regexItem.IsMatch(txtGivenName.Text))
                {
                    throw new ValidationFailureException("Given Name contains special characters, please remove any special characters from the Given Name field and retry");
                }
                // Surname validation
                if (String.IsNullOrEmpty(txtSurname.Text))
                {
                    throw new ValidationFailureException("Surname field empty, please enter a last name and retry.");
                }
                if (txtSurname.Text.Any(t => char.IsDigit(t)))
                {
                    throw new ValidationFailureException("Surname contains numbers, please remove any numbers from the last name field and retry.");
                }
                regexItem = new Regex("^[a-zA-Z0-9 ]*$");
                if (!regexItem.IsMatch(txtSurname.Text))
                {
                    throw new ValidationFailureException("Surname contains special characters, please remove any special characters from the surname field and retry");
                }
                // Phone validation
                if (String.IsNullOrEmpty(txtPhone.Text))
                {
                    throw new ValidationFailureException("Phone field empty, please enter a gender and retry.");
                }
                regexItem = new Regex("^[0-9 ]*$");
                if (!regexItem.IsMatch(txtPhone.Text))
                {
                    throw new ValidationFailureException("Phone field contains special characters, please use numbers only.");
                }


                apiClient = new APIClient();

                Owner newOwner = new Owner()
                {
                    OwnerID = await apiClient.GetNextOwnerID(),
                    GivenName = txtGivenName.Text,
                    Surname = txtSurname.Text,
                    Phone = txtPhone.Text
                };
                await apiClient.CreateOwner(newOwner);
            }
            catch (ValidationFailureException ex)
            {
                logger.Debug("ValidationFailureException");
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("Unknown error");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dgrOwners.UnselectAll();

                txtGivenName.Text = null;
                txtSurname.Text = null;
                txtPhone.Text = null;

                await LoadOwnerGrid();
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Given Name validation
                if (String.IsNullOrEmpty(txtGivenName.Text))
                {
                    throw new ValidationFailureException("Given Name field empty, please enter a given name and retry.");
                }
                if (txtGivenName.Text.Any(t => char.IsDigit(t)))
                {
                    throw new ValidationFailureException("Given Name contains numbers, please remove any numbers from the given name field and retry.");
                }
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
                if (!regexItem.IsMatch(txtGivenName.Text))
                {
                    throw new ValidationFailureException("Given Name contains special characters, please remove any special characters from the Given Name field and retry");
                }
                // Surname validation
                if (String.IsNullOrEmpty(txtSurname.Text))
                {
                    throw new ValidationFailureException("Surname field empty, please enter a surname and retry.");
                }
                if (txtSurname.Text.Any(t => char.IsDigit(t)))
                {
                    throw new ValidationFailureException("Surname contains numbers, please remove any numbers from the surname field and retry.");
                }
                regexItem = new Regex("^[a-zA-Z0-9 ]*$");
                if (!regexItem.IsMatch(txtSurname.Text))
                {
                    throw new ValidationFailureException("Surname contains special characters, please remove any special characters from the surname field and retry");
                }
                // Phone validation
                if (String.IsNullOrEmpty(txtPhone.Text))
                {
                    throw new ValidationFailureException("Phone field empty, please enter a phone number and retry.");
                }
                regexItem = new Regex("^[0-9 ]*$");
                if (!regexItem.IsMatch(txtPhone.Text))
                {
                    throw new ValidationFailureException("Phone field contains special characters, please use numbers only.");
                }

                apiClient = new APIClient();

                Owner updatedOwner = new Owner()
                {
                    OwnerID = selectedOwner.OwnerID,
                    GivenName = txtGivenName.Text,
                    Surname = txtSurname.Text,
                    Phone = txtPhone.Text
                };
                await apiClient.UpdateOwner(updatedOwner);
                await LoadOwnerGrid();
            }
            catch (ValidationFailureException ex)
            {
                logger.Debug("ValidationFailureException");
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("Unknown error");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dgrOwners.UnselectAll();

                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;

                txtGivenName.Text = null;
                txtSurname.Text = null;
                txtPhone.Text = null;
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                apiClient = new APIClient();

                ownersPets = await apiClient.GetPetsByOwnerID(selectedOwner.OwnerID);

                await apiClient.DeleteOwnerPetsTreatments(ownersPets);
                await apiClient.DeleteOwnerPets(selectedOwner.OwnerID);
                await apiClient.DeleteOwner(selectedOwner);
            }
            catch (Exception ex)
            {
                logger.Fatal("Unknown error");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dgrOwners.UnselectAll();

                txtGivenName.Text = null;
                txtSurname.Text = null;
                txtPhone.Text = null;

                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;

                await LoadOwnerGrid();
            }
        }

        private async Task LoadOwnerGrid()
        {
            try
            {
                apiClient = new APIClient();
                allOwners = await apiClient.GetOwners();
                allPets = await apiClient.GetPets();
                dgrOwners.ItemsSource = allOwners;
            }
            catch (Exception ex)
            {
                logger.Fatal("Unknown error");
                MessageBox.Show(ex.Message);
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await LoadOwnerGrid();
            }
            catch (Exception ex)
            {
                logger.Fatal("Unknown error");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }
    }
}
