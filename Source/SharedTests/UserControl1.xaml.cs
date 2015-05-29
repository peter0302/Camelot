/***********************************************************************************************
 * © Copyright 2014-2015 Peter Moore. All rights reserved.
 *
 *  This file is part of Camelot.
 *  
 *  Camelot is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *  
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 ***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Reflection;

#if __WPF__
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
#elif __IOS__
using Camelot.Core;
using Camelot.iOS;
#elif WINDOWS_APP
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;
#endif




namespace Camelot.Test.Shared
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    
#if __IOS__
    [XamlResourceLocation("Camelot.iOS.Test.UserControl1.xaml")]
#endif
    public partial class UserControl1 : UserControl, INotifyPropertyChanged
    {
        public UserControl1() 
        {
            DateTime start = DateTime.Now;
            InitializeComponent();
            TimeSpan total = DateTime.Now.Subtract(start);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         //   _StatusText.Text = "Clicked!";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        //    _StatusText.Text = "Fucked!";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string[] coll = new string[] { "Hello", "Goodbye", "Farewell" };
            this.DataContext = this;

            //_MyListBox.ItemsSource = coll;          
        }


        string _ScrollPosition = "256";
        public string ScrollPosition
        {
            get
            {
                return _ScrollPosition;
            }
            set
            {
                _ScrollPosition = value;
                OnPropertyChanged("ScrollPosition");
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

#if __IOS__
        
        TextBlock _StatusText;
        ItemsControl _MyItems;

        protected override void InitializeComponent()
        {
            

            base.InitializeComponent();
            _StatusText = (TextBlock)_XamlReader.GetElement("_StatusText");
            _MyItems = (ItemsControl)_XamlReader.GetElement("_MyItems");



     //       _TheCanvas = (Canvas)_XamlReader.GetElement("_TheCanvas");
       //     _TheRectangle = (Rectangle)_XamlReader.GetElement("_TheRectangle");
        }


#endif
        /*
        private void BindingPerformanceTest()
        {
            Foo foo1 = new Foo();
            Bar bar1 = new Bar { MyBar = "Cheer's" };
            Hobo hobo1 = new Hobo { MyHobo = "Moe" };

            Binding theBinding = new Binding("MyBar");
            theBinding.Source = bar1;
            BindingOperations.SetBinding(foo1, Foo.MyFooProperty, theBinding);

            Binding theOtherBinding = new Binding("MyHobo");
            theOtherBinding.Mode = BindingMode.TwoWay;
            theOtherBinding.Source = hobo1;
            BindingOperations.SetBinding(foo1, Foo.MyOtherFooProperty, theOtherBinding);

            bar1.MyBar = "Sheffield's";
            hobo1.MyHobo = "Frank";
            string result = foo1.MyFoo;
            string result2 = foo1.MyOtherFoo;

            bar1.MyBar = "Cubby Bear";
            hobo1.MyHobo = "Larry";
            string result3 = foo1.MyFoo;
            string result4 = foo1.MyOtherFoo;

            foo1.MyOtherFoo = "Curly";
            string result5 = hobo1.MyHobo;

            var start = DateTime.Now;

            for (int i = 0; i < 100000; i++)
            {
                foo1.MyOtherFoo = "Alice";
                hobo1.MyHobo = "Bob";
            }

            var time = DateTime.Now.Subtract(start);
        }*/


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }



    public class Foo : DependencyObject
    {
        
        #region string MyFoo dependency property
        public static DependencyProperty MyFooProperty = DependencyProperty.Register("MyFoo", typeof(string), typeof(Foo), new PropertyMetadata((string)"",
                                                               (obj, args) => { ((Foo)obj).OnMyFooChanged(args); }));
       	public string MyFoo
       	{
            get
            {
                return (string)GetValue(MyFooProperty);
            }
            set
            {
                SetValue(MyFooProperty, value);
            }
        }
        private void OnMyFooChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        
        #region string MyOtherFoo dependency property
        public static DependencyProperty MyOtherFooProperty = DependencyProperty.Register("MyOtherFoo", typeof(string), typeof(Foo), new PropertyMetadata((string)"",
                                                               (obj, args) => { ((Foo)obj).OnMyOtherFooChanged(args); }));
       	public string MyOtherFoo
       	{
            get
            {
                return (string)GetValue(MyOtherFooProperty);
            }
            set
            {
                SetValue(MyOtherFooProperty, value);
            }
        }
        private void OnMyOtherFooChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion


    }

    public class Bar : DependencyObject
    {        

        #region string MyBar dependency property
        public static DependencyProperty MyBarProperty = DependencyProperty.Register("MyBar", typeof(string), typeof(Bar), new PropertyMetadata((string)"",
                                                               (obj, args) => { ((Bar)obj).OnMyBarChanged(args); }));
       	public string MyBar
       	{
            get
            {
                return (string)GetValue(MyBarProperty);
            }
            set
            {
                SetValue(MyBarProperty, value);
            }
        }
        private void OnMyBarChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
    }

    public class Hobo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        string _MyHobo;
        public string MyHobo
        {
            get { return _MyHobo; }
            set
            {
                _MyHobo = value;
                OnPropertyChanged();
            }
        }

    }


}
