using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using SmartNews.Models;
using SmartNews.Utils;
using SmartNews.Views;
using Xamarin.Forms;

namespace SmartNews.ViewModels
{
    public class RssItemViewModel : BaseViewModel
    {
        public string Url { get; set; }
        public string Parameter { get; set; }
        public string searchText { get; set; }
        public double heightImages { get; set; }
        public bool CheckMenuItem { get; set; }
        public ObservableCollection<RSSFeedItem> Items { get; set; } = new ObservableCollection<RSSFeedItem>();
        public ObservableListCollection<TabBarItemModel> ItemTabBar => GetTabBarItemModel();
        public ObservableCollection<TabBarItemModel> ItemCategory { get; set; }
        private Command _editCommand;

        private Command _updateCommand;
        private bool _allowOrdering;
        public bool AllowOrdering
        {
            get
            {
                return _allowOrdering;
            }
            set
            {
                SetProperty(ref _allowOrdering, value);
            }
        }
        bool isRefreshing;

        public RssItemViewModel()
        {
            RefreshCommand = new Command(
                execute: () =>
                {
                    LoadRssFeed();
                },
                canExecute: () =>
                {
                    return !IsRefreshing;
                });
            GetDataTabItem();
            _editCommand = new Command(() => { AllowOrdering = !AllowOrdering; });
            //_updateCommand = new Command(async () => await UpdatePlayers());
            ItemTabBar.OrderChanged += (sender, e) =>
            {
                int jersey = 1;
                foreach (var item in ItemTabBar)
                {
                    item.TabPosition = jersey++;
                }
            };
        }
        // Get data from DataTabItem.json file
        public void GetDataTabItem()
        {
            var data = LoadResourceText.GetJsonData("DataTabItem.json");
            var result = JsonConvert.DeserializeObject<List<TabBarItemModel>>(data);
            ItemCategory = new ObservableCollection<TabBarItemModel>(result);
        }

        public ObservableListCollection<TabBarItemModel> GetTabBarItemModel()
        {
            var list = new List<TabBarItemModel>();
            list.Add(new TabBarItemModel()
            {
                TitleBar = "24h.com",
                Url = "https://cdn.24h.com.vn/upload/rss/trangchu24h.rss",
                UrlImages = "icon-setting.png",
                ItemColor = Color.LightCoral,
                TabPosition = 1
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "Tinhte",
                Url = "https://tinhte.vn/rss",
                UrlImages = "settingHome.png",
                ItemColor = Color.Orange,
                TabPosition = 2
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "Thanhnien",
                Url = "https://thanhnien.vn/rss/home.rss",
                UrlImages = "icon-setting.png",
                ItemColor = Color.Turquoise,
                TabPosition = 3
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "Trithuc",
                Url = "https://trithucvn.net/feed",
                UrlImages = "settingHome.png",
                ItemColor = Color.LightSkyBlue,
                TabPosition = 4
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "Cafebiz-cn",
                Url = "https://cafebiz.vn/cong-nghe.rss",
                UrlImages = "icon-setting.png",
                ItemColor = Color.MediumOrchid,
                TabPosition = 5
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "Cafebiz-kd",
                Url = "https://cafebiz.vn/cau-chuyen-kinh-doanh.rss",
                UrlImages = "settingHome.png",
                ItemColor = Color.LightCoral,
                TabPosition = 6
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "Vnreview",
                Url = "https://vnreview.vn/feed/-/rss/home",
                UrlImages = "icon-setting.png",
                ItemColor = Color.LightSkyBlue,
                TabPosition = 7
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "Soha",
                Url = "https://soha.vn/kinh-doanh.rss",
                UrlImages = "settingHome.png",
                ItemColor = Color.Orange,
                TabPosition = 8
            });
            //add--
            list.Add(new TabBarItemModel()
            {
                TitleBar = "Tinhte",
                Url = "https://tinhte.vn/rss",
                UrlImages = "icon-setting.png",
                ItemColor = Color.Orange,
                TabPosition = 9
            });
            list.Add(new TabBarItemModel()
            {
                TitleBar = "Thanhnien",
                Url = "https://thanhnien.vn/rss/home.rss",
                UrlImages = "settingHome.png",
                ItemColor = Color.Turquoise,
                TabPosition = 10
            });

            return new ObservableListCollection<TabBarItemModel>(list);
        }

        #region property RefreshCommand
        public ICommand RefreshCommand { private set; get; }

        public bool IsRefreshing
        {
            set { SetProperty(ref isRefreshing, value); }
            get { return isRefreshing; }
        }
        public ICommand EditCommand
        {
            get
            {
                return _editCommand;
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand;
            }
        }
        #endregion
        #region Load LoadRssFeed url rss
        public void LoadRssFeed()
        {
            if (!string.IsNullOrEmpty(Url))
            {
                WebRequest request = WebRequest.Create(Url);
                request.BeginGetResponse((args) =>
                {
                    try
                    {
                        // Download XML.
                        Stream stream = request.EndGetResponse(args).GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        string xml = reader.ReadToEnd();
                        // Parse XML to extract data from RSS feed.
                        XDocument doc = XDocument.Parse(xml);
                        XElement rss = doc.Element(XName.Get("rss"));
                        XElement channel = rss.Element(XName.Get("channel"));
                        // Set Title property.
                        Title = channel.Element(XName.Get("title")).Value;
                        // Set Items property.
                        List<RSSFeedItem> list =
                        channel.Elements(XName.Get("item")).Select((XElement element) =>
                        {
                            var desciption = element.Element(XName.Get("description"));
                            //var image = desciption.Element(XName.Get("img")).Attribute("src").Value.ToString();
                            var result = new RSSFeedItem();
                            result.Title = element.Element(XName.Get("title")).Value;
                            result.Description = desciption.Value;
                            result.Link = element.Element(XName.Get("link")).Value;
                            result.PubDate = element.Element(XName.Get("pubDate")).Value;
                            #region get images form description
                            Regex regx = new Regex("http(s?)://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?.(?:jpg|bmp|gif|png)", RegexOptions.IgnoreCase);
                            MatchCollection mactches = regx.Matches(desciption.ToString());
                            if (mactches.Count > 0)
                            {
                                foreach (var urlImage in mactches)
                                {
                                    result.Thumbnail = urlImage.ToString();
                                }
                            }
                            else
                            {
                                result.Thumbnail = "https://joebalestrino.com/wp-content/uploads/2019/02/Marketplace-Lending-News.jpg";
                            }
                            #endregion
                            return result;

                        }).ToList();
                        var lstItem = list.OrderByDescending(s => s.PubDateTime).ToList();
                        try
                        {
                            if (!string.IsNullOrEmpty(searchText))
                            {
                                Items = lstItem.Where(s => s.Title.ToLower().Contains(searchText.ToLower())).ToList().ToObservableCollection();
                            }
                            else
                            {
                                Items = lstItem.ToObservableCollection();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        // Set IsRefreshing to false to stop the 'wait' icon.
                        IsRefreshing = false;
                    }
                    catch (Exception)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current.MainPage.DisplayAlert("Server Error", "Not Connected", "OK");
                        });
                    }
                }, null);
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Please check url no empty", "OK");
                });
            }


        }
    }
    #endregion
}
