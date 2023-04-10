using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfAppLab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewData viewData = new ViewData();

        public List<String> RawDataList { get; set; }
        public List<String> SplineDataList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewData;
            RawDataList = new List<string>();
            SplineDataList = new List<string>();
            cbFunction.ItemsSource = Enum.GetValues(typeof(FRawEnum));
        }

        public void ShowToListBox()
        {
           
            try
            {

                lbRawData.Items.Clear();
                lbSplineData.Items.Clear();

                //for (int i = 0; i < viewData.BindNumberGridNodes; i++)
                //{
                //    lbRawData.Items.Add($"Nodes={viewData.RawData!.GridNodes[i]}; Value={viewData.RawData.GridValues[i]}");
                //}
                for (int i = 0; i < viewData.RawData.NumberGridNodes; i++)
                {
                    lbRawData.Items.Add($"Nodes={viewData.RawData.GridNodes[i]}; Value={viewData.RawData.GridValues[i]}");
                }

                foreach (var item in viewData.SplineData.ListSplineData)
                {
                    lbSplineData.Items.Add(item.ToString());
                }
                
                tbIntegral.Text = $"{viewData.SplineData.IntegralValue:F3}";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }



        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new()
            {
                FileName = "RawData",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                viewData.CalculateSplines();
                viewData.Save(filename);
            }
        }

        private void OnViewDataFromFileClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new()
            {
                FileName = "RawData",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                //viewData.Load(filename);
                viewData.RawData = new RawData(filename);
                ////viewData.CalculateSplines();

                viewData.SplineData = new SplineData(viewData.RawData, viewData.BindLeftDeriv, viewData.BindRightDeriv, viewData.BindNumberSplineGridNodes);
                viewData.SplineData.MakeMKLSpline();

                //viewData.BindLeftAndRightEndPoint[0] = viewData.RawData.LeftEndPoint;
                //viewData.BindLeftAndRightEndPoint[1] = viewData.RawData.RightEndPoint;
                //viewData.BindNumberGridNodes = viewData.RawData.NumberGridNodes;
                //viewData.BindIsUniform = viewData.RawData.IsUniform;

                ShowToListBox();

                lbRawData.Items.Refresh();
                lbSplineData.Items.Refresh();
            }
        }
        private void OnViewDataFromControlsClicked(object sender, RoutedEventArgs e)
        {
            viewData.CalculateSplines();
            ShowToListBox();
            
            lbRawData.Items.Refresh();
            lbSplineData.Items.Refresh();


        }

        //private void LbSplineData_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    object Temp = lbSplineData.SelectedItem;
        //    tbSelectedNode.Text = Temp.ToString();
        //}

        private void LbSplineData_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int Temp = lbSplineData.SelectedIndex;
            tbSelectedNode.Text = viewData.SplineData!.ListSplineData[Temp].ToString("F3");
        }
    }

    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return Binding.DoNothing;
        }
    }

    public class StringToValuesConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double[] range)
            {
                return $"{range[0]};{range[1]}";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
            {
                string[] parts = input.Split(';');
                if (parts.Length == 2 && double.TryParse(parts[0], out double start) && double.TryParse(parts[1], out double end))
                {
                    return new double[] { start, end };
                }
            }
            return new double[] { 0, 0 };
        }
    }
}
