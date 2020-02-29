using System;
using System.Collections.Generic;
using System.Text;
using XMart.Util;
using XMart.Views;
using Xamarin.Forms;

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

		private bool visible;   //Comment
		public bool Visible
		{
			get { return visible; }
			set { SetProperty(ref visible, value); }
		}

		public Command<string> NavigateCommand { get; set; }

		public MePageViewModel()
		{
			UserName = GlobalVariables.LoggedUser.username;
			UserId = GlobalVariables.LoggedUser.id.ToString();
			UserType = GlobalVariables.LoggedUser.userType == "0" ? "客户" : "设计师";
			UserAvatar = GlobalVariables.LoggedUser.file;
			Visible = GlobalVariables.LoggedUser.userType == "0" ? false : true;

			NavigateCommand = new Command<string>((pageName) =>
			{
				Type type = Type.GetType(pageName);
				Page page = (Page)Activator.CreateInstance(type);
				Application.Current.MainPage.Navigation.PushModalAsync(page);
			}, (pageName) => { return true; });
		}

	}
}
