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
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Windows.Threading;
namespace WpfAutoOlio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer AutoKiihdytin = new DispatcherTimer();
        Car toyota = new Car();
        Car ferrari = new Car();
        

        public MainWindow()
        {
            InitializeComponent();
            cbTransMission.Items.Add("Manual");
            cbTransMission.Items.Add("Automatic");
            cbTransMission.Items.Add("Robotic");
            cbTransMission2.ItemsSource = cbTransMission.Items;

            AutoKiihdytin.Tick += AutoKiihdytin_Tick;
            AutoKiihdytin.Interval = TimeSpan.FromMilliseconds(500);
        }

        private void AutoKiihdytin_Tick(object sender, EventArgs e)
        {
            if (toyota.CurrentSpeed < toyota.GetMaxSpeed())
            {
                btnAccelerate.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else
            {
                AutoKiihdytin.Stop();
            }
        }
        private void BtnNaytaTiedot_Click(object sender, RoutedEventArgs e)
        {
            toyota.Color = txtVari.Text;
            toyota.Model = txtMalli.Text;
            if (IsAllDigits(txtMaxSpeed.Text) && (txtMaxSpeed.Text != ""))
            {
                //toyota.MaxSpeed = int.Parse(txtMaxSpeed.Text);
                try
                {
                    toyota.SetMaxSpeed(int.Parse(txtMaxSpeed.Text));
                    gauge.MaxValue = toyota.GetMaxSpeed();
                }
                catch (Exception)
                {
                    MessageBox.Show("Anna huippunopeus väliltä 1-400 km/h ");
                    txtMaxSpeed.Focus();
                }
            }
            else
            {
                MessageBox.Show("Anna maksiminopeus numeroina!");
                txtMaxSpeed.Text = "";
                txtMaxSpeed.Focus();
            }
            toyota.CurrentSpeed = int.Parse(txtCurrentSpeed.Text);
            if (IsNumeric(txtHorsePower.Text) && (txtHorsePower.Text != ""))
            {
                toyota.HorsePower = int.Parse(txtHorsePower.Text);
            }
            else
            {
                MessageBox.Show("Anna hevosvoimat numeroina!");
                txtHorsePower.Text = "";
                txtHorsePower.Focus();
            }
            toyota.TransMission = cbTransMission.Text;
            try
            {
                toyota.GearCount = int.Parse(txtGearCount.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Vaihdelukumäärä pitää olla välillä 4-9!");
                txtGearCount.Text = "";
                txtGearCount.Focus();
            }

            ShowCarInfo(toyota);
        }
        private void BtnNaytaTiedot2_Click(object sender, RoutedEventArgs e)
        {
            //SpeechSynthesizer synth = new SpeechSynthesizer();
            //synth.Speak("Hello Ferrari!");
            ferrari.Color = txtVari2.Text;
            ferrari.Model = txtMalli2.Text;
            try
            {
                ferrari.MaxSpeed = int.Parse(txtMaxSpeed2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Anna huippunopeus väliltä 1-400 km/h ");
                txtMaxSpeed2.Focus();
            }
            ferrari.CurrentSpeed = int.Parse(txtCurrentSpeed2.Text);
            ferrari.HorsePower = int.Parse(txtHorsePower2.Text);
            ferrari.TransMission = cbTransMission2.Text;
            ShowCarInfo(ferrari);
        }

        public void ShowCarInfo(Car auto)
        {
            string Message = "Model: " + auto.Model + "\r\n" +
                "Color: " + auto.Color + "\r\n" +
                //"Maxspeed: " + auto.MaxSpeed.ToString() + "\r\n" +
                "Maxspeed: " + auto.GetMaxSpeed().ToString() + "\r\n" +
                "Currentspeed: " + auto.CurrentSpeed.ToString() + "\r\n" +
                "Horsepower: " + auto.HorsePower.ToString() + "\r\n" +
                "Transmission: " + auto.TransMission.ToString() + "\r\n" +
                "Gearcount: " + auto.GearCount.ToString() + "\r\n" +
                "Engine running: " + auto.Running;
            MessageBox.Show(Message);
            //MessageBox.Show(auto.ToString());
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnStart)
            {
                toyota.StartEngine();
                if (toyota.Running == true)
                {
                    btnIndicator.Content = "R";
                    btnIndicator.Background = Brushes.PaleGreen;
                }
            }
            else if (sender == btnStart2)
            {
                ferrari.StartEngine();
                if (ferrari.Running == true)
                {
                    btnIndicator2.Content = "R";
                    btnIndicator2.Background = Brushes.PaleGreen;
                }
            }
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnStop)
            {
                toyota.StopEngine();
                if (toyota.Running == false)
                {
                    btnIndicator.Content = "";
                    btnIndicator.Background = Brushes.Red;
                }
            }
            else if (sender == btnStop2)
            {
                ferrari.StopEngine();
                if (ferrari.Running == false)
                {
                    btnIndicator2.Content = "";
                    btnIndicator2.Background = Brushes.Red;
                }
            }
        }

        private void BtnAccelerate_Click(object sender, RoutedEventArgs e)
        {
            if (!AutoKiihdytin.IsEnabled)
            {
                AutoKiihdytin.Start();
            }
            toyota.Accelerate();
            txtCurrentSpeed.Text = toyota.CurrentSpeed.ToString();
            gauge.CurrentValue = toyota.CurrentSpeed;
        }

        private void BtnBrake_Click(object sender, RoutedEventArgs e)
        {
            
            AutoKiihdytin.Stop();
            
            toyota.Brake();
            txtCurrentSpeed.Text = toyota.CurrentSpeed.ToString();
            gauge.CurrentValue = toyota.CurrentSpeed;
        }

        private void BtnAccelerate2_Click(object sender, RoutedEventArgs e)
        {
            ferrari.Accelerate();
            txtCurrentSpeed2.Text = ferrari.CurrentSpeed.ToString();
        }

        private void BtnBrake2_Click(object sender, RoutedEventArgs e)
        {
            ferrari.Brake();
            txtCurrentSpeed2.Text = ferrari.CurrentSpeed.ToString();
        }

        public static bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, "^[0-9]+$");
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            txtMalli.Text = "";
            txtMalli2.Text = "";
            txtVari.Text = "";
            txtVari2.Text = "";
            txtMaxSpeed.Text = "";
            txtMaxSpeed2.Text = "";
            txtCurrentSpeed.Text = "0";
            txtCurrentSpeed2.Text = "0";
            txtGearCount.Text = "";
            txtGearCount2.Text = "";
            txtHorsePower.Text = "";
            txtHorsePower2.Text = "";
            cbTransMission.Text = "";
            cbTransMission2.Text = "";

            toyota.Model = "";
            toyota.Color = "";
            toyota.CurrentSpeed = 0;
            toyota.GearCount = 0;
            toyota.SetMaxSpeed(0);
            toyota.HorsePower = 0;
            toyota.TransMission = "";
        }
    }
}
