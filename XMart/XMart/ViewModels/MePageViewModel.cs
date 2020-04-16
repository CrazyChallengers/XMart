using System;
using System.Collections.Generic;
using System.Text;
using XMart.Util;
using XMart.Views;
using Xamarin.Forms;
using System.IO;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using XMart.Services;
using XMart.ResponseData;

namespace XMart.ViewModels
{
    public class MePageViewModel : BaseViewModel
    {
		private string userName;   //Comment
		public string UserName
		{
			get { return userName; }
			set { SetProperty(ref userName, value); }
		}

		private string userType;   //Comment
		public string UserType
		{
			get { return userType; }
			set { SetProperty(ref userType, value); }
		}

		private string userId;   //Comment
		public string UserId
		{
			get { return userId; }
			set { SetProperty(ref userId, value); }
		}

		private string userAvatar;   //Comment
		public string UserAvatar
		{
			get { return userAvatar; }
			set { SetProperty(ref userAvatar, value); }
		}

		private bool designerVisible;   //Comment
		public bool DesignerVisible
		{
			get { return designerVisible; }
			set { SetProperty(ref designerVisible, value); }
		}

		private bool customerVisible;   //Comment
		public bool CustomerVisible
		{
			get { return customerVisible; }
			set { SetProperty(ref customerVisible, value); }
		}

		private int orderNumber;   //Comment
		public int OrderNumber
		{
			get { return orderNumber; }
			set { SetProperty(ref orderNumber, value); }
		}

		private int collectionNumber;   //Comment
		public int CollectionNumber
		{
			get { return collectionNumber; }
			set { SetProperty(ref collectionNumber, value); }
		}

		public Command<string> NavigateCommand { get; set; }
		public Command LoginOutCommand { get; set; }
		public Command ReloadCommand { get; set; }
		public Command EditUserInfoCommand { get; set; }

		public MePageViewModel()
		{
			UserName = string.Empty;
			UserType = string.Empty;
			UserId = string.Empty;
			UserAvatar = string.Empty;
			DesignerVisible = false;
			CustomerVisible = false;

			InitMePage();

			NavigateCommand = new Command<string>((pageName) =>
			{
				Type type = Type.GetType(pageName);
				Page page = (Page)Activator.CreateInstance(type);
				Application.Current.MainPage.Navigation.PushModalAsync(page);
			}, (pageName) => { return true; });

			LoginOutCommand = new Command(async () =>
			{
				bool action = await Application.Current.MainPage.DisplayAlert("退出登录", "确定要退出登录吗？", "确定", "取消");
				if (action)
				{
					LoginOut();
				}
			}, () => { return true; });

			ReloadCommand = new Command(() =>
			{
				InitMePage();
			}, () => { return true; });

		}

		private void InitMePage()
		{
			try
			{
				UserName = GlobalVariables.LoggedUser.username;
				UserId = GlobalVariables.LoggedUser.id.ToString();
				UserType = GlobalVariables.LoggedUser.userType == "0" ? "客户" : "设计师";
				UserAvatar = GlobalVariables.LoggedUser.file == null ? "star_yellow.png" : GlobalVariables.LoggedUser.file;
				CustomerVisible = GlobalVariables.LoggedUser.userType == "0" ? true : false;
				DesignerVisible = !CustomerVisible;

				//if (!Tools.IsNetConnective())
				//{
				//	CrossToastPopUp.Current.ShowToastError("无网络连接，请检查网络。", ToastLength.Long);
				//	return;
				//}
				//
				//RestSharpService _restSharpService = new RestSharpService();
				//OrderListRD orderListRD = await _restSharpService.GetOrderListById(order)
			}
			catch (Exception)
			{
				throw;
			}
		}

		private	void LoginOut()
		{
			GlobalVariables.IsLogged = false;

			string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "log.dat");
			File.Delete(fileName);

			MainPage mainPage = new MainPage();
			Application.Current.MainPage.Navigation.PushModalAsync(mainPage);
		}
	}
}
