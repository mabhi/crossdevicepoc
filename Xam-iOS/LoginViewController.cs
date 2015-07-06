using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Xam_iOS
{

	partial class LoginViewController : UIViewController
	{
		public LoginViewController (IntPtr handle) : base (handle)
		{
			
		}

		partial void loginPressed (UIButton sender){
			Console.WriteLine("Button pressed {0}",sender);
		}

	}
}
