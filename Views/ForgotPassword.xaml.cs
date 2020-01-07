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

namespace it_company.Views
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void TblLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void TblExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}