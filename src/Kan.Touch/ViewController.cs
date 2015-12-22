using System;

using UIKit;
using OpenSSL.Crypto;
using System.Text;
using OpenSSL.Crypto.EC;

namespace Kan.Touch
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var data = "";
			var checksum = "";
			var publicKey = @"";

			var result = Verify (publicKey, data, checksum);
			// Perform any additional setup after loading the view, typically from a nib.
			System.Diagnostics.Debug.WriteLine(result);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public bool Verify(string publicKeyAsPem, string data, string sign)
		{
			CryptoKey cryptoKey = CryptoKey.FromPublicKey(publicKeyAsPem, null);
			Key ecKey = cryptoKey.GetEC();

			var digest = Encoding.ASCII.GetBytes(data);

			var ha = System.Security.Cryptography.SHA256Managed.Create();

			var hash = ha.ComputeHash(digest);

			return ecKey.Verify(0, hash, Convert.FromBase64String(sign));
		}
	}
}

