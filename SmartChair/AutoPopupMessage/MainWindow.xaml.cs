using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoPopupMessage.BusinessObjects;
using AutoPopupMessage.controller;

namespace AutoPopupMessage
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyMessageController notifyMessageController;
        private Random rand = new Random(5);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            notifyMessageController = new NotifyMessageController
               (
                   Screen.Width,
                   Screen.Height,
                   200,
                   150
               );
            notifyMessageController.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            notifyMessageController.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            notifyMessageController.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var value = rand.Next(1, 10);

            NotifyMessage msg = null;

            if (value % 2 == 0)
            {
                msg = new NotifyMessage("Images/GreenSkin.png", "Green Skin Title", "Du hast nicht heute nicht viel bewogen. Willst du Spiel starten?",
                                        () =>
                                    MessageBox.Show("Green Skin has been chosen.", "Green Skin", MessageBoxButton.OK));
            }
            else
            {
                msg = new NotifyMessage("Images/BlueSkin.png", "Blue Skin Title", "Blue Skin has been chosen.",
                    () =>
                        MessageBox.Show("Blue Skin has been chosen.", "Blue Skin", MessageBoxButton.OK));
            }

            notifyMessageController.EnqueueMessage(msg);
        }
    }
}
