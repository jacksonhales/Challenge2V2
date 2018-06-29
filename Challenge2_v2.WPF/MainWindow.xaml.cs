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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            frmMain.Content = new Procedures();
            logger.Trace("Application Launched");
        }

        private void btnProcedures_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new Procedures();
        }

        private void btnOwners_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new Owners();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            logger.Trace("Application Closed");
        }
    }
}
