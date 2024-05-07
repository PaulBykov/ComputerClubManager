using ComputerClub.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace ComputerClub.View.shared
{
    public partial class Combobox : UserControl
    {
        public Combobox()
        {
            InitializeComponent();
            DataContext = this;

        }

        public static readonly DependencyProperty WidthDependencyProperty =
            DependencyProperty.Register("Width", typeof(int), typeof(Combobox));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ICollection<object>), typeof(Combobox), new PropertyMetadata(null));

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(Combobox));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(Combobox));

        public static readonly DependencyProperty OnSelectedItemChangedProperty =
            DependencyProperty.Register("OnSelectedItemChanged", typeof(Action<Club>), typeof(Combobox));


        public new int Width
        {
            get { return (int)GetValue(WidthDependencyProperty); }
            set { SetValue(WidthDependencyProperty, value); }
        }

        public ICollection<object> ItemsSource
        {
            get { return (ICollection<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public Action<object> OnSelectedItemChanged
        {
            get { return (Action<object>) GetValue(OnSelectedItemChangedProperty); }
            set { SetValue(OnSelectedItemChangedProperty, value); }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OnSelectedItemChanged((object)((System.Windows.Controls.ComboBox)sender).SelectedItem);
            }
            catch(Exception exc) 
            {
                Console.WriteLine(exc.ToString());
            }
        }
    }
}
