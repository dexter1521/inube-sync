using System;
using System.ServiceProcess;
using System.Configuration.Install;
using System.ComponentModel;

namespace InubeInstallerActions
{
	[RunInstaller(true)]
	public class CustomActions : Installer
	{
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			base.Install(stateSaver);
		}

		public override void Uninstall(System.Collections.IDictionary savedState)
		{
			base.Uninstall(savedState);
		}

		public override void Commit(System.Collections.IDictionary savedState)
		{
			base.Commit(savedState);

			try
			{
				using (ServiceController sc = new ServiceController("InubeSync"))
				{
					if (sc.Status != ServiceControllerStatus.Running)
					{
						sc.Start();
					}
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.EventLog.WriteEntry("CustomAction", $"Error al iniciar el servicio: {ex.Message}");
			}
		}
	}
}