<Query Kind="Program">
  <NuGetReference>Frank.Wpf.Hosting</NuGetReference>
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Frank.Wpf.Hosting</Namespace>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
  <Namespace>Microsoft.CodeAnalysis.Text</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>static UserQuery</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
</Query>

[STAThread]
void Main()
{
	var builder = Host.CreateWpfHostBuilder();
	
	builder.Services.AddWindow<LoginDialog<TokenCredentials>>();
	
	var host = builder.Build<LinqPadWindow>();

	host.Run();
}

public class LinqPadWindow : MainWindow
{
	public LinqPadWindow(LoginDialog<TokenCredentials> loginDialog)
	{
		loginDialog.ShowDialog();
	}
}

public class LoginDialog<TCredentials> : Window
{
	public TCredentials Credentials { get; private set; }

	public LoginDialog()
	{
		var stackPanel = new StackPanel();
		var properties = typeof(TCredentials).GetProperties();
		foreach (var property in properties)
		{
			var attribute = property.GetCustomAttribute<PasswordInputAttribute>();
			if (attribute != null)
			{
				var passwordBox = new PasswordBox();
				stackPanel.Children.Add(passwordBox);

				passwordBox.PasswordChanged += (sender, args) =>
				{
					var password = (sender as PasswordBox).Password;
					property.SetValue(Credentials, password);
				};
			}

		}

		Content = stackPanel;
	}
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class PasswordInputAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class IntegerInputAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class NumberInputAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class BooleanInputAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class GuidInputAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class TextInputAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class FileInputAttribute : Attribute;

public class TokenCredentials
{
	[PasswordInput]
	public string Token { get; set; }
}