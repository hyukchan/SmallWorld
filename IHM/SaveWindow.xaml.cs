﻿using System;
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
using System.Windows.Shapes;
using Small_World;

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        Game game;

        public SaveWindow(Game g)
        {
            game = g;
            InitializeComponent();
        }

        private void onClickSaveGame(object sender, RoutedEventArgs e)
        {
            if (saveFileName.Text.Length > 0)
            {
                game.saveAs(saveFileName.Text);
                this.Height = 280;
                saveWindowImage.Source = new BitmapImage(new Uri("./textures/saved.png", UriKind.Relative));
                saveWindowStackPanel.Visibility = System.Windows.Visibility.Hidden;
                saveWindowButton.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
