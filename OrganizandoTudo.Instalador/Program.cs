using System;
using System.Diagnostics;

namespace OrganizandoTudo.Instalador
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool isSucess = true;

                if (!IsDockerInstalled())
                {
                    Console.WriteLine("Docker não encontrado. Instalando Docker...");
                    isSucess = InstallDocker();
                }

                if (isSucess)
                {
                    Console.WriteLine("Docker encontrado. Baixando o projeto...");
                    isSucess = DownloadDockerImage("thaleslj/organizandotudo.admin:latest");
                }

                if (isSucess)
                {
                    Console.WriteLine("Projeto baixado. Instalando o projeto...");
                    isSucess = InstallDockerImage("thaleslj/organizandotudo.admin:latest");
                }

                if (isSucess) Console.WriteLine("Instalação concluída. Acesse o projeto em http://localhost:5555");
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ocorreu uma falha nos processos anteriores");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }

        private static bool IsDockerInstalled()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = "--version",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool InstallDocker()
        {
            try
            {
                var installerUrl = "https://desktop.docker.com/win/stable/Docker%20Desktop%20Installer.exe";
                var installerPath = @"C:\Temp\DockerInstaller.exe";

                using (var client = new System.Net.WebClient())
                {
                    client.DownloadFile(installerUrl, installerPath);
                }

                Process.Start(installerPath).WaitForExit();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool DownloadDockerImage(string imageName)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "docker",
                        Arguments = $"pull {imageName}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                process.WaitForExit();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool InstallDockerImage(string imageName)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "docker",
                        Arguments = $"run -d -p 5555:5555 --name Organizando.Tudo.Admin {imageName}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                process.WaitForExit();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
