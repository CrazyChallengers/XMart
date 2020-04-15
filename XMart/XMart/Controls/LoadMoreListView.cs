using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XMart.Controls
{
    public class LoadMoreListView : ListView
    {
        private readonly StackLayout LoadingContent;
        private readonly StackLayout LoadMoreContent;
        private readonly StackLayout NoDataContent;

        public LoadMoreListView() : base(ListViewCachingStrategy.RecycleElement)
        {
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.ListView.SetSeparatorStyle(this, Xamarin.Forms.PlatformConfiguration.iOSSpecific.SeparatorStyle.FullWidth);
            LoadingContent = CreateFooter("正在加载中...", true);
            LoadMoreContent = CreateFooter("上拉加载更多", false);
            NoDataContent = CreateFooter("已加载全部数据", false);
            ItemAppearing += LoadMoreListView_ItemAppearing;
        }

        private void LoadMoreListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (ItemsSource is IList items && e.Item == items[items.Count - 1])
            {
                if (CanLoadMore && LoadStatus == LoadMoreStatus.StatusHasData)
                {
                    if (CanLoadMore && (LoadMoreCommand?.CanExecute(null) == true))
                        LoadMoreCommand.Execute(null);
                }
            }
        }

        private StackLayout CreateFooterContent(string content, bool indicator = false)
        {
            var item = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HeightRequest = 50,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            if (indicator)
            {
                try
                {
                    item.Children.Add(new ActivityIndicator
                    {
                        IsRunning = true,
                        WidthRequest = 20,
                        HeightRequest = 20,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    });
                }
                catch (Exception)
                {
                    //Debug.WriteLine(e.Message);
                    throw;
                }
            }

            item.Children.Add(new Label
            {
                Text = content,
                VerticalOptions = LayoutOptions.CenterAndExpand
            });
            return item;
        }

        private StackLayout CreateFooter(string content, bool hasIndicator)
        {
            var item = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HeightRequest = 50,
            };
            var contentStack = CreateFooterContent(content, hasIndicator);
            item.Children.Add(contentStack);
            return item;
        }

        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create(nameof(LoadMoreCommand), typeof(ICommand), typeof(LoadMoreListView), default(ICommand));

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public static readonly BindableProperty CanLoadMoreProperty = BindableProperty.Create(nameof(CanLoadMore), typeof(bool), typeof(LoadMoreListView), false);
        public bool CanLoadMore
        {
            get { return (bool)GetValue(CanLoadMoreProperty); }
            set { SetValue(CanLoadMoreProperty, value); }
        }

        public static readonly BindableProperty LoadStatusProperty = BindableProperty.Create(nameof(LoadStatus), typeof(LoadMoreStatus), typeof(LoadMoreListView), LoadMoreStatus.StatusDefault, propertyChanged: OnLoadStatusChanged);

        public LoadMoreStatus LoadStatus
        {
            get { return (LoadMoreStatus)GetValue(LoadStatusProperty); }
            set { SetValue(LoadStatusProperty, value); }
        }

        private static void OnLoadStatusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var lv = (LoadMoreListView)bindable;
            lv.NotifyLoadStatus((LoadMoreStatus)newValue);
        }

        public void NotifyLoadStatus(LoadMoreStatus loadStatus)
        {
            switch (loadStatus)
            {
                case LoadMoreStatus.StatusDefault:
                    this.Footer = null;
                    break;
                case LoadMoreStatus.StatusLoading:
                    this.Footer = LoadingContent;
                    break;
                case LoadMoreStatus.StatusHasData:
                    this.Footer = LoadMoreContent;
                    break;
                case LoadMoreStatus.StatusNoData:
                    this.Footer = NoDataContent;
                    break;
                default:
                    this.Footer = null;
                    break;
            }
        }

    }

    public enum LoadMoreStatus
    {
        StatusDefault = 0,
        StatusLoading = 1,
        StatusHasData = 2,
        StatusNoData = 3,
    }
}
