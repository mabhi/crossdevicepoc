// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Xam_iOS
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		UIKit.UIActivityIndicatorView actVwIndicator { get; set; }

		[Outlet]
		UIKit.UITextField pswdTxtField { get; set; }

		[Outlet]
		UIKit.UITextField unameTxtField { get; set; }

		[Action ("loginPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void loginPressed (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
