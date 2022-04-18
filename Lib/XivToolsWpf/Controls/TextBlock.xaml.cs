﻿// © XIV-Tools.
// Licensed under the MIT license.

namespace XivToolsWpf.Controls
{
	using System.ComponentModel;
	using System.Windows;
	using XivToolsWpf.DependencyInjection;
	using XivToolsWpf.DependencyProperties;

	/// <summary>
	/// Interaction logic for Label.xaml.
	/// </summary>
	public partial class TextBlock : System.Windows.Controls.TextBlock
	{
		public static readonly IBind<string> KeyDp = Binder.Register<string, TextBlock>(nameof(Key), OnKeyChanged, BindMode.OneWay);
		public static readonly IBind<string?> ValueDp = Binder.Register<string?, TextBlock>(nameof(Value), OnValueChanged, BindMode.OneWay);
		public static readonly IBind<bool> AllLanguagesDp = Binder.Register<bool, TextBlock>(nameof(AllLanguages), BindMode.OneWay);
		private static ILocaleProvider? cachedlocaleProvider;

		public TextBlock()
		{
			this.Loaded += this.TextBlock_Loaded;
			this.Unloaded += this.TextBlock_Unloaded;

			this.LoadString();
		}

		public string? Key { get; set; }

		public string? Value
		{
			get => ValueDp.Get(this);
			set => ValueDp.Set(this, value);
		}

		public bool AllLanguages
		{
			get => AllLanguagesDp.Get(this);
			set => AllLanguagesDp.Set(this, value);
		}

		private static ILocaleProvider? LocaleProvider
		{
			get
			{
				if (cachedlocaleProvider == null)
				{
					try
					{
						cachedlocaleProvider = DependencyFactory.GetDependency<ILocaleProvider>();
					}
					catch
					{
					}
				}

				return cachedlocaleProvider;
			}
		}

		public static void OnKeyChanged(TextBlock sender, string val)
		{
			sender.Key = val;
			sender.LoadString();
		}

		public static void OnValueChanged(TextBlock sender, string? val)
		{
			sender.LoadString();
		}

		private void TextBlock_Loaded(object sender, RoutedEventArgs e)
		{
			if (LocaleProvider != null)
			{
				LocaleProvider.LocaleChanged += this.OnLocaleChanged;
			}

			this.LoadString();
		}

		private void TextBlock_Unloaded(object sender, RoutedEventArgs e)
		{
			if (LocaleProvider != null)
			{
				LocaleProvider.LocaleChanged -= this.OnLocaleChanged;
			}
		}

		private void OnLocaleChanged()
		{
			this.LoadString();
		}

		private void LoadString()
		{
			if (string.IsNullOrEmpty(this.Key))
				return;

			string? val = null;

			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				if (LocaleProvider != null)
				{
					if (LocaleProvider.Loaded)
					{
						if (this.Value == null)
						{
							if (this.AllLanguages)
							{
								val = LocaleProvider.GetStringAllLanguages(this.Key);
							}
							else
							{
								val = LocaleProvider.GetString(this.Key);
							}
						}
						else
						{
							val = LocaleProvider.GetStringFormatted(this.Key, this.Value);
						}
					}
				}
			}

			if (string.IsNullOrEmpty(val))
			{
				if (!string.IsNullOrEmpty(this.Text))
					return;

				val = "[" + this.Key + "]";
			}

			this.Text = val;
		}
	}
}
