﻿using DevExpress.Xpf.Grid;
using OPT.Product.SimalorManager;
using OPT.Product.SimalorManager.Eclipse.FileInfos;
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
using Tester.View;
using Tester.ViewModel;

namespace Tester
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _viewModel = new MainViewModel();

        TestModel t;
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _viewModel;

            //t = new TestModel();

            //t.MyProperty = "dddddd";

            //Window1ViewModel vm = new Window1ViewModel(t);

            //Window1 w = new Window1(vm);

            //w.Show();

        }

        void tv_all_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            BaseKey bk = this.tv_all.SelectedItem as BaseKey;

            string ss = string.Empty;

            this.property.SelectedObject = bk;
        }

    }
}
