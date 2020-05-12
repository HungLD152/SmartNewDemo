using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SmartNews.Views
{
    public partial class CategoryPage : ContentPage
    {
        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList<TabBarItemModel>), typeof(ControlTabBar), null, BindingMode.TwoWay);
        public static BindableProperty ItemsSelectedProperty = BindableProperty.Create(nameof(ItemSelected), typeof(object), typeof(ControlTabBar), null, BindingMode.TwoWay);
        public IList<TabBarItemModel> ItemsSource
        {
            get { return (IList<TabBarItemModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public object ItemSelected
        {
            get { return GetValue(ItemsSelectedProperty); }
            set { SetValue(ItemsSelectedProperty, value); }
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                Container.Children.Clear();
                //Container2.Children.Clear();
                if (ItemsSource?.Count > 0)
                    foreach (var data in ItemsSource)
                    {
                        var item = new CategoryView { BindingContext = data };
                        item.OnCategoryItemClicked += Item_OnCategoryItemClicked;
                        Container.Children.Add(item);
                        //Container2.Children.Add(item);
                    }
            }
            if (propertyName == ItemsSelectedProperty.PropertyName)
            {

            }
        }
        private void Item_OnCategoryItemClicked(object sender, string e)
        {

        }
        public CategoryPage()
        {
            InitializeComponent();
        }
    }
}
