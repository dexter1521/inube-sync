using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;

namespace SincronizadorConfigUIv8
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            EnsureRunAsAdmin();
            ApplicationConfiguration.Initialize();
            Application.Run(new Panel());
        }

        static bool IsAdministrator()
        {
            using var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static void EnsureRunAsAdmin()
        {
            if (IsAdministrator()) return;
            var exe = Process.GetCurrentProcess().MainModule!.FileName!;
            var psi = new ProcessStartInfo(exe)
            {
                UseShellExecute = true,
                Verb = "runas"
            };
            try
            {
                Process.Start(psi);
                Environment.Exit(0);
            }
            catch
            {
                MessageBox.Show("Se requieren privilegios de Administrador para controlar el servicio.");
                Environment.Exit(1);
            }
        }
    }
}