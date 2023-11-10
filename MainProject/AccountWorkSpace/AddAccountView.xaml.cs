﻿using MainProject.ViewModel;
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

namespace MainProject.AccountWorkSpace
{
    /// <summary>
    /// Interaction logic for AddAccountView.xaml
    /// </summary>
    public partial class AddAccountView : UserControl
    {
        public AddAccountView()
        {
            InitializeComponent();
            txt_newpass.PasswordChanged += NewPasswordChanged;
            txt_oldpass.PasswordChanged += OldPasswordChanged;
        }

        private void NewPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext!=null)
            {
                
            }
        }
        private void OldPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {

            }
        }
    }
}
