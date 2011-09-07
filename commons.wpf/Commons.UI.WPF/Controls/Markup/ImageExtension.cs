using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace Commons.UI.WPF.Controls.Markup
{
	/// <summary>
	/// Extension for create image using markup extension, done for conciseness
	/// because with it instead of, e.g.:
	/// <code>				
	/// <MenuItem Header="About" Name="aboutMenuItem" Click="aboutMenuItem_Click">
	///			<MenuItem.Icon>
	///				<Image Source="Images/about.png"></Image>
	///			</MenuItem.Icon>
	/// </MenuItem>
	/// </code>
	/// 
	/// you can write:
	/// <code>
	///		<MenuItem Header="About" Name="aboutMenuItem" Icon="{Markup:Image Images/about.png}" Click="aboutMenuItem_Click" />
	/// </code>
	/// 
	/// Also, this extension support some conventions:
	///		.png extension for image files
	///		Images/ for image folder
	/// so, if you application support this convention, instead of {m:Image Images/about.png} you can simply write {m:Image about}
	/// </summary>
	[MarkupExtensionReturnType(typeof (ImageSource))]
	public class ImageExtension : MarkupExtension
	{
		private readonly string imagePath;

		public ImageExtension(string imagePath)
		{
			this.imagePath = imagePath;
		}

		public double? Width { get; set; }
		public double? Height { get; set; }
		public Thickness? Margin { get; set; }

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			//first we try find image by given path (it must by full relative like "Images/add.png"
			string path = imagePath;
			object image = TryGetImage(serviceProvider, path);
			if (image != null) return image;

			//if not found - we try some conventions - add file extension
			if (!Path.HasExtension(path))
			{
				path = path + ".png";
				image = TryGetImage(serviceProvider, path);
				if (image != null) return image;
			}

			//add folder
			if(!path.StartsWith("Images/"))
			{
				path = "Images/" + path;
				image = TryGetImage(serviceProvider, path);
				if (image != null) return image;
			}

			//try step one level up
			path = "../"+path;
			image = TryGetImage(serviceProvider, path);
			if (image != null) return image;

			throw new IOException(string.Format("Cannot locate resource '{0}'", imagePath));
		}

		private object TryGetImage(IServiceProvider serviceProvider, string path)
		{
			try
			{
//				BitmapDecoder.CreateFromUriOrStream(baseUri, uri, null, createOptions, cacheOption, uriCachePolicy, true);
//				Assembly asm = Assembly.GetCallingAssembly();
//				Assembly asm = Assembly.GetExecutingAssembly();
//				Stream iconStream = asm.GetManifestResourceStream(imagePath);
//				var provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget))
//									   as IProvideValueTarget;
//				var targetObject = provideValueTarget.TargetObject as DependencyObject;
//				var targetProperty = provideValueTarget.TargetProperty as DependencyProperty;

				IUriContext uriContext = (IUriContext) serviceProvider.GetService
				                                       	(typeof (IUriContext));

//				ResourceManager manager = new ResourceManager();
//				manager.G


				Uri uri = new Uri(path, UriKind.Relative);
				uri = new Uri(uriContext.BaseUri, uri);
//				ProvideValueServiceProvider provider = serviceProvider;
//				Uri uri = new Uri(string.Format("pack://application:,,,/Resources/{0}", this.imagePath));
//				uri = new Uri(serviceProvider);	
//				GetRe
//				PngBitmapDecoder decoder = new PngBitmapDecoder(uri,BitmapCreateOptions.None,BitmapCacheOption.Default);
//				PngBitmapDecoder decoder = new PngBitmapDecoder(iconStream,BitmapCreateOptions.None,BitmapCacheOption.Default);
//				BitmapFrame frame = decoder.Frames[0];
				BitmapImage frame = new BitmapImage(uri);


//				StreamResourceInfo stream = Application.GetResourceStream(uri);
//				BitmapFrame frame = BitmapFrame.Create(uri);
				Image image = new Image();
				image.Source = frame;

				if (Width != null) image.Width = (double) Width;
				if (Height != null) image.Height = (double) Height;
				if (Margin != null) image.Margin = (Thickness) Margin;


				return image;
			}
			catch (IOException e)
			{
				//intentionally
			}
			return null;
		}
	}
}
