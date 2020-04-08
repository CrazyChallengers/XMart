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

		private string streetName;   //Comment
		public string StreetName
		{
			get { return streetName; }
			set { SetProperty(ref streetName, value); }
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

		public Command AddOrUpdateAddressCommand { get; set; }
		public Command DeleteAddressCommand { get; set; }
		public Command DeleteCommand { get; set; }
		public Command BackCommand { get; set; }

		RestSharpService _restSharpService = new RestSharpService();

		public EditAddressViewModel()
		{
			Visible = false;

			AddOrUpdateAddressCommand = new Command(() =>
			{
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
		}

		public EditAddressViewModel(AddressInfo addressInfo)
		{
			UserName = addressInfo.userName;
			Tel = addressInfo.tel;
			StreetName = addressInfo.streetName;
			IsDefault = addressInfo.isDefault;
			AddressId = addressInfo.addressId;
			Visible = false;

			AddOrUpdateAddressCommand = new Command(() =>
			{
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
	}
}
