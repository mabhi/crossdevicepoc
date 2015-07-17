using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using PortableCode.Models;
using PortableCode.Services;
using PortableCode.Exceptions;

namespace Xam_iOS
{

	partial class LoginViewController : UIViewController
	{
        
        NSObject didShowNotification,didHideNotification;
        UITextField activeTxtField;

		public LoginViewController (IntPtr handle) : base (handle)
		{
			
		}
        
		 public async void loginPressed (object sender, EventArgs e){
            actVwIndicator.StartAnimating();
            await AuthenticateAsync();
            if (null != UserWebservice.Instance.CurrentUser)
            {
                this.DismissViewController(true, () =>
                {
                });

            }
            actVwIndicator.StopAnimating();

		}
        

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            actVwIndicator.StopAnimating();
			NSLayoutConstraint leftConstraint = NSLayoutConstraint.Create (View, NSLayoutAttribute.Left, NSLayoutRelation.Equal, contentView, NSLayoutAttribute.Left, 1, 0);
			NSLayoutConstraint rightConstraint = NSLayoutConstraint.Create (View, NSLayoutAttribute.Right, NSLayoutRelation.Equal, contentView, NSLayoutAttribute.Right, 1, 0);

			View.AddConstraint (leftConstraint);
			View.AddConstraint (rightConstraint);

           didShowNotification = UIKeyboard.Notifications.ObserveDidShow(keyboardDidShowHandler);
           didHideNotification = UIKeyboard.Notifications.ObserveDidHide(keyboardDidHideHandler);
            
            unameTxtField.EditingDidBegin += textFieldBeginEditing;
            pswdTxtField.EditingDidBegin += textFieldBeginEditing;
			termsTxtField.EditingDidBegin += textFieldBeginEditing;

            unameTxtField.EditingDidEnd += textFieldEndEditing;
            pswdTxtField.EditingDidEnd += textFieldEndEditing;
			termsTxtField.EditingDidEnd += textFieldEndEditing;

            unameTxtField.ShouldReturn += textFieldShouldReturnHandler;
            pswdTxtField.ShouldReturn += textFieldShouldReturnHandler;
			termsTxtField.ShouldReturn += textFieldShouldReturnHandler;

			loginBtn.TouchUpInside += loginPressed;
		}

        void keyboardDidShowHandler(object sender, UIKit.UIKeyboardEventArgs args)
        {
            CGRect kbRect = args.FrameBegin;
            CGRect convRect = this.View.ConvertRectFromView(kbRect,null);
            UIEdgeInsets contentInsets = new UIEdgeInsets(0, 0, convRect.Height, 0);
            this.scrollView.ContentInset = contentInsets;
            this.scrollView.ScrollIndicatorInsets = contentInsets;

            CGRect aRect = this.View.Frame;
            aRect.Height -= convRect.Height;
//            aRect.Y = 44;

            if(!aRect.Contains(this.activeTxtField.Frame))
                this.scrollView.ScrollRectToVisible(this.activeTxtField.Frame,true);
        }

        void keyboardDidHideHandler(object sender, UIKit.UIKeyboardEventArgs args)
        {
            UIEdgeInsets resetInsets = new UIEdgeInsets();
            this.scrollView.ContentInset = resetInsets;
            this.scrollView.ScrollIndicatorInsets = resetInsets;
        }

        void textFieldBeginEditing(object sender, EventArgs args)
        {
            this.activeTxtField = (UITextField)sender;
        }

        void textFieldEndEditing(object sender, EventArgs args)
        {
            this.activeTxtField = null;
        }

        bool textFieldShouldReturnHandler(UITextField sender)
        {
            
            if (sender.IsEqual(unameTxtField))
                pswdTxtField.BecomeFirstResponder();
                
            else
                sender.ResignFirstResponder();
            return true;
        }

        private async Task AuthenticateAsync()
        {
            
                var client = UserWebservice.Instance.MobileWebClient;
                if (client == null)
                    return;

                try
                {
                    await UserWebservice.Instance.AuthenticateWithCredentialsAsync(unameTxtField.Text, pswdTxtField.Text);
                            
                }
			catch (CustomHttpException ex)
                {
                    Console.WriteLine("Http Request failed {0}", ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Invalid Request failed {0}", ex.Message);

                    var message = "You must log in. Login Required\n" + ex.Message;
                    var alert = new UIAlertView("Login", message, null, "OK", null);
                    alert.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generic failed {0}", ex.Message);
                    var alert = new UIAlertView("Login", ex.Message, null, "OK", null);
                    alert.Show();
                }
            
        }

        protected override void Dispose(bool disposing)
        {

            didHideNotification.Dispose();
            didShowNotification.Dispose();
            activeTxtField.Dispose();
            base.Dispose(disposing);

        }
        
    }
}
