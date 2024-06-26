﻿using ComputerClub.ViewModel;
using ComputerClub.ViewModel.modal;
using System;
using System.Windows;


namespace ComputerClub.View.windows
{
    public partial class AddComputerModalWindow : Window, IModalWindow
    {

        public AddComputerModalWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new AddComputerWindowVM();
            ViewModel.Done += Finish;
        }

        public void Finish(object sender, EventArgs e)
        {
            DialogResult = true;
        }

        private AddComputerWindowVM ViewModel { get; set; }
    }
}
