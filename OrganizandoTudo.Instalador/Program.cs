using System;
using System.Diagnostics;

namespace OrganizandoTudo.Instalador
{
    class Program
    {
        readonly static string url = "http://localhost:5555";

        static void Main(string[] args)
        {
            try
            {
                bool isSucess = true;

                if (!IsDockerInstalled())
                {
                    Console.WriteLine($"Docker não encontrado. Instalando Docker...");
                    isSucess = InstallDocker();
                }

                if (isSucess)
                {
                    Console.WriteLine($"Docker encontrado. Baixando o projeto...");
                    isSucess = DownloadDockerImage("thaleslj/organizandotudo.admin:latest");
                }

                if (isSucess)
                {
                    Console.WriteLine($"Projeto baixado. Instalando o projeto...");
                    isSucess = InstallDockerImage("thaleslj/organizandotudo.admin:latest");
                }

                if (isSucess)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true // Para abrir no navegador padrão
                    });
                    Console.WriteLine($"Instalação concluída. Acesse o projeto em {url}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ocorreu um erro: " + ex.Message);
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
            catch { }

            return false;
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
            catch { }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Falha ao instalar as dependências");
            return false;
        }

        private static bool RemoveDockerContainerAndImage(string containerName, string imageName)
        {
            try
            {
                // Parar o container
                var stopProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "docker",
                        Arguments = $"stop {containerName}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                stopProcess.Start();
                stopProcess.WaitForExit();

                // Remover o container
                var removeContainerProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "docker",
                        Arguments = $"rm {containerName}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                removeContainerProcess.Start();
                removeContainerProcess.WaitForExit();

                // Remover a imagem
                var removeImageProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "docker",
                        Arguments = $"rmi {imageName}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                removeImageProcess.Start();
                removeImageProcess.WaitForExit();

                return true;
            }
            catch { }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Falha ao remover a versão antiga do aplicativo");
            return false;
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
            catch { }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Falha ao baixar a nova versão do aplicativo");
            return false;
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
                        Arguments = $"run -d  --restart unless-stopped -p 5555:5000 --name organizandotudo.admin {imageName}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                process.WaitForExit();

                return true;
            }
            catch { }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Falha ao instalar a nova versão do aplicativo");
            return false;
        }
    }
}
