using System;
using System.Collections.Generic;
using System.Text;
using XMart.ResponseData;
using XMart.Models;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using XMart.Util;
using XMart.Services;
using Xamarin.Forms;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace XMart.ViewModels
{
    public class EditAddressViewModel : BaseViewModel
    {
		private string userName;   //Comment
		public string UserName
		{
			get { return userName; }
			set { SetProperty(ref userName, value); }
		}

		private string tel;   //Comment
		public string Tel
		{
			get { return tel; }
			set { SetProperty(ref tel, value); }
		}

		private ObservableCollection<string> provinceList;   //Comment
		public ObservableCollection<string> ProvinceList
		{
			get { return provinceList; }
			set { SetProperty(ref provinceList, value); }
		}

		private ObservableCollection<string> cityList;   //Comment
		public ObservableCollection<string> CityList
		{
			get { return cityList; }
			set { SetProperty(ref cityList, value); }
		}

		private ObservableCollection<string> countyList;   //Comment
		public ObservableCollection<string> CountyList
		{
			get { return countyList; }
			set { SetProperty(ref countyList, value); }
		}

		private ObservableCollection<string> townList;   //Comment
		public ObservableCollection<string> TownList
		{
			get { return townList; }
			set { SetProperty(ref townList, value); }
		}

		private string province;   //省
		public string Province
		{
			get { return province; }
			set { SetProperty(ref province, value); }
		}

		private string city;   //市
		public string City
		{
			get { return city; }
			set { SetProperty(ref city, value); }
		}

		private string county;   //区县
		public string County
		{
			get { return county; }
			set { SetProperty(ref county, value); }
		}

		private string town;   //乡镇
		public string Town
		{
			get { return town; }
			set { SetProperty(ref town, value); }
		}

		private string street;   //街道门牌号
		public string Street
		{
			get { return street; }
			set { SetProperty(ref street, value); }
		}

		private bool isDefault;   //Comment
		public bool IsDefault
		{
			get { return isDefault; }
			set { SetProperty(ref isDefault, value); }
		}

		private bool visible;   //Comment
		public bool Visible
		{
			get { return visible; }
			set { SetProperty(ref visible, value); }
		}

		private long AddressId { get; set; }
		private string StreetName { get; set; }
		private JObject AllPlaces { get; set; }

		public Command AddOrUpdateAddressCommand { get; set; }
		public Command DeleteAddressCommand { get; set; }
		public Command DeleteCommand { get; set; }
		public Command BackCommand { get; set; }
		public Command SelectProvinceCommand { get; set; }
		public Command SelectCityCommand { get; set; }
		public Command SelectCountyCommand { get; set; }
		public Command SelectTownCommand { get; set; }

		RestSharpService _restSharpService = new RestSharpService();

		public EditAddressViewModel()
		{
			Visible = false;
			ProvinceList = new ObservableCollection<string>();
			CityList = new ObservableCollection<string>();
			CountyList = new ObservableCollection<string>();
			TownList = new ObservableCollection<string>();
			StreetName = string.Empty;

			InitPCAS();

			AddOrUpdateAddressCommand = new Command(async () =>
			{
				StreetName = Province + City + County + Town + Street;
				string message = "收件人：" + UserName + "\n电话号码：" + Tel + "\n收货地址：" + StreetName;
				bool action = await Application.Current.MainPage.DisplayAlert("请再次确认收货地址", message, "确认", "取消");
				if (!action)
				{
					return;
				}

				if (AddressId == 0)
				{
					AddAddress();
				}
				else
				{
					UpdateAddress();
				}
			}, () => { return true; });

			DeleteAddressCommand = new Command(() =>
			{
				DeleteAddress();
			}, () => { return true; });

			DeleteCommand = new Command(() =>
			{
				Visible = !Visible;
			}, () => { return true; });

			BackCommand = new Command(() =>
			{
				Application.Current.MainPage.Navigation.PopModalAsync();
			}, () => { return true; });

			SelectProvinceCommand = new Command(() =>
			{
				CityList.Clear();
				CountyList.Clear();
				TownList.Clear();
				City = string.Empty;
				County = string.Empty;
				Town = string.Empty;
				foreach (var item in (JObject)AllPlaces[Province])
				{
					CityList.Add(item.Key);
				}
			}, () => { return true; });

			SelectCityCommand = new Command(() =>
			{
				CountyList.Clear();
				TownList.Clear();
				County = string.Empty;
				Town = string.Empty;
				foreach (var item in (JObject)AllPlaces[Province][City])
				{
					CountyList.Add(item.Key);
				}
			}, () => { return true; });

			SelectCountyCommand = new Command(() =>
			{
				TownList.Clear();
				Town = string.Empty;
				foreach (var item in AllPlaces[Province][City][County])
				{
					TownList.Add(item.ToString());
				}
			}, () => { return true; });

			SelectTownCommand = new Command(() =>
			{

			}, () => { return true; });
		}

		public EditAddressViewModel(AddressInfo addressInfo)
		{
			UserName = addressInfo.userName;
			Tel = addressInfo.tel;
			Street = addressInfo.streetName;
			IsDefault = addressInfo.isDefault;
			AddressId = addressInfo.addressId;
			Visible = false;
			ProvinceList = new ObservableCollection<string>();
			CityList = new ObservableCollection<string>();
			CountyList = new ObservableCollection<string>();
			TownList = new ObservableCollection<string>();

			InitPCAS();

			AddOrUpdateAddressCommand = new Command(async () =>
			{
				StreetName = Province + City + County + Town + Street;
				string message = "收件人：" + UserName + "\n电话号码：" + Tel + "\n收货地址：" + StreetName;
				bool action = await Application.Current.MainPage.DisplayAlert("请再次确认收货地址", message, "确认", "取消");
				if (!action)
				{
					return;
				}

				if (AddressId == 0)
				{
					AddAddress();
				}
				else
				{
					UpdateAddress();
				}
			}, () => { return true; });

			DeleteAddressCommand = new Command(() =>
			{
				DeleteAddress();
			}, () => { return true; });

			DeleteCommand = new Command(() =>
			{
				Visible = !Visible;
			}, () => { return true; });

			BackCommand = new Command(() =>
			{
				Application.Current.MainPage.Navigation.PopModalAsync();
			}, () => { return true; });

			SelectProvinceCommand = new Command(() =>
			{
				CityList.Clear();
				CountyList.Clear();
				TownList.Clear();
				City = string.Empty;
				County = string.Empty;
				Town = string.Empty;
				foreach (var item in (JObject)AllPlaces[Province])
				{
					CityList.Add(item.Key);
				}
			}, () => { return true; });

			SelectCityCommand = new Command(() =>
			{
				CountyList.Clear();
				TownList.Clear();
				County = string.Empty;
				Town = string.Empty;
				foreach (var item in (JObject)AllPlaces[Province][City])
				{
					CountyList.Add(item.Key);
				}
			}, () => { return true; });

			SelectCountyCommand = new Command(() =>
			{
				TownList.Clear();
				Town = string.Empty;
				foreach (var item in AllPlaces[Province][City][County])
				{
					TownList.Add(item.ToString());
				}
			}, () => { return true; });

			SelectTownCommand = new Command(() =>
			{

			}, () => { return true; });
		}

		/// <summary>
		/// 删除地址
		/// </summary>
		private async void DeleteAddress()
		{
			try
			{
				SimpleRD simpleRD = await _restSharpService.DeleteAddressById(AddressId);

				if (simpleRD.success)
				{
					CrossToastPopUp.Current.ShowToastSuccess("删除成功！", ToastLength.Long);
					await Application.Current.MainPage.Navigation.PopModalAsync();
				}
				else
				{
					CrossToastPopUp.Current.ShowToastError(simpleRD.message, ToastLength.Long);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// 修改地址
		/// </summary>
		private async void UpdateAddress()
		{
			try
			{
				if (!Tools.IsPhoneNumber(Tel))
				{
					CrossToastPopUp.Current.ShowToastWarning("手机号格式不标准，请检查。", ToastLength.Long);
					return;
				}

				if (!Tools.IsNetConnective())
				{
					CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
					return;
				}

				AddressInfo addressInfo = new AddressInfo
				{
					addressId = AddressId,
					userId = GlobalVariables.LoggedUser.id,
					userName = UserName,
					tel = Tel,
					streetName = StreetName,
					isDefault = IsDefault
				};

				SimpleRD simpleRD = await _restSharpService.UpdateAddress(addressInfo);

				if (simpleRD.success)
				{
					CrossToastPopUp.Current.ShowToastSuccess("更新成功！", ToastLength.Long);
					await Application.Current.MainPage.Navigation.PopModalAsync();
				}
				else
				{
					CrossToastPopUp.Current.ShowToastError(simpleRD.message, ToastLength.Long);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// 新增地址
		/// </summary>
		private async void AddAddress()
		{
			try
			{
				if (!Tools.IsPhoneNumber(Tel))
				{
					CrossToastPopUp.Current.ShowToastWarning("手机号格式不标准，请检查。", ToastLength.Long);
					return;
				}

				if (!Tools.IsNetConnective())
				{
					CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
					return;
				}

				AddressInfo addressInfo = new AddressInfo
				{
					addressId = 0,
					userId = GlobalVariables.LoggedUser.id,
					userName = UserName,
					tel = Tel,
					streetName = StreetName,
					isDefault = IsDefault
				};

				SimpleRD simpleRD = await _restSharpService.AddAddress(addressInfo);

				if (simpleRD.success)
				{
					CrossToastPopUp.Current.ShowToastSuccess("添加成功！", ToastLength.Long);
					await Application.Current.MainPage.Navigation.PopModalAsync();
				}
				else
				{
					CrossToastPopUp.Current.ShowToastError("添加失败！网络可能出问题了！", ToastLength.Long);
				}
			}
			catch (Exception)
			{
				throw;
			}


		}

		/// <summary>
		/// 初始化行政区划
		/// </summary>
		private void InitPCAS()
		{
			try
			{
				var assembly = IntrospectionExtensions.GetTypeInfo(typeof(EditAddressViewModel)).Assembly;
				using (var reader = new StreamReader(assembly.GetManifestResourceStream("XMart.Util.pcas.json")))
				{
					var jsonData = reader.ReadToEnd();
					JObject questionsList = (JObject)JsonConvert.DeserializeObject(jsonData);
					AllPlaces = questionsList;
					foreach (var item in AllPlaces)
					{
						ProvinceList.Add(item.Key);
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
