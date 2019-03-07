using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace StartComputercraft
{
    class Program
    {
        
        static bool errorPresent = false;
        static List<string> lstNatives;
        static string launcherEXEPath = @"\MinecraftLauncher.exe";
        static string javaPath = @"\runtime\jre-x64\bin\javaw.exe";
        static string nativesPath = @"\mcp_data\.minecraft\natives";
        static string workDir = @"\mcp_data\.minecraft";
        static string libDir = @"\mcp_data\.minecraft\libraries";

        static void Main(string[] args)
        {
            string MCPath = string.Empty;
            if (Debugger.IsAttached) //running from ide
            {
                MCPath = @"C:\Users\Public\PortableApps\MinecraftPortable";
            }
            else
            {
                MCPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Console.WriteLine("Launching from: " + MCPath);
            }
            launcherEXEPath = Path.Combine(MCPath, launcherEXEPath);
            javaPath = MCPath + javaPath;
            nativesPath = MCPath + nativesPath;
            workDir = MCPath + workDir;
            libDir = MCPath + libDir;
            lstNatives = new List<string> { "jinput-dx8.dll",
                                            "jinput-dx8_64.dll",
                                            "jinput-raw.dll",
                                            "jinput-raw_64.dll",
                                            "jinput-wintab.dll",
                                            "lwjgl.dll",
                                            "lwjgl64.dll",
                                            "OpenAL32.dll",
                                            "OpenAL64.dll" };
            User.ReadUser(Path.Combine(workDir, "launcher_profiles.json"));
            DownloadMinecraft();
            if (errorPresent)
            {
                Console.Write("Press any key to exit");
                Console.ReadKey(true);
            }
            else
            {
                Thread.Sleep(2000);
                LaunchMinecraft();
                Console.Write("Launch successful. Closing in 3 seconds...");
                Thread.Sleep(3000);
            }
        }
        private static void LaunchMinecraft()
        {
            //Start creating command using StringBuilder
            var cmd = new StringBuilder();
            cmd.Append("-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump ");
            cmd.Append("-Djava.library.path=" + workDir + @"\natives ");
            cmd.Append("-Dminecraft.launcher.brand=minecraft-launcher ");
            cmd.Append("-Dminecraft.launcher.version=2.1.2481 ");
            cmd.Append("-Dminecraft.client.jar=" + workDir + @"\versions\1.7.10\1.7.10.jar ");
            cmd.Append("-cp ");
            cmd.Append(libDir + @"\net\minecraftforge\forge\1.7.10-10.13.4.1614-1.7.10\forge-1.7.10-10.13.4.1614-1.7.10.jar;"); //1
            cmd.Append(libDir + @"\net\minecraft\launchwrapper\1.12\launchwrapper-1.12.jar;"); //2
            cmd.Append(libDir + @"\org\ow2\asm\asm-all\5.0.3\asm-all-5.0.3.jar;"); //3
            cmd.Append(libDir + @"\com\typesafe\akka\akka-actor_2.11\2.3.3\akka-actor_2.11-2.3.3.jar;"); //4
            cmd.Append(libDir + @"\com\typesafe\config\1.2.1\config-1.2.1.jar;"); //5
            cmd.Append(libDir + @"\org\scala-lang\scala-actors-migration_2.11\1.1.0\scala-actors-migration_2.11-1.1.0.jar;"); //6
            cmd.Append(libDir + @"\org\scala-lang\scala-compiler\2.11.1\scala-compiler-2.11.1.jar;"); //7
            cmd.Append(libDir + @"\org\scala-lang\plugins\scala-continuations-library_2.11\1.0.2\scala-continuations-library_2.11-1.0.2.jar;"); //8
            cmd.Append(libDir + @"\org\scala-lang\plugins\scala-continuations-plugin_2.11.1\1.0.2\scala-continuations-plugin_2.11.1-1.0.2.jar;"); //9
            cmd.Append(libDir + @"\org\scala-lang\scala-library\2.11.1\scala-library-2.11.1.jar;"); //10
            cmd.Append(libDir + @"\org\scala-lang\scala-parser-combinators_2.11\1.0.1\scala-parser-combinators_2.11-1.0.1.jar;"); //11
            cmd.Append(libDir + @"\org\scala-lang\scala-reflect\2.11.1\scala-reflect-2.11.1.jar;"); //12
            cmd.Append(libDir + @"\org\scala-lang\scala-swing_2.11\1.0.1\scala-swing_2.11-1.0.1.jar;"); //13
            cmd.Append(libDir + @"\org\scala-lang\scala-xml_2.11\1.0.2\scala-xml_2.11-1.0.2.jar;");// 14
            cmd.Append(libDir + @"\lzma\lzma\0.0.1\lzma-0.0.1.jar;"); //15
            cmd.Append(libDir + @"\net\sf\jopt-simple\jopt-simple\4.5\jopt-simple-4.5.jar;"); //16
            cmd.Append(libDir + @"\com\google\guava\guava\17.0\guava-17.0.jar;"); //17
            cmd.Append(libDir + @"\org\apache\commons\commons-lang3\3.3.2\commons-lang3-3.3.2.jar;");  //18
            cmd.Append(libDir + @"\com\mojang\netty\1.6\netty-1.6.jar;"); //19
            cmd.Append(libDir + @"\com\mojang\realms\1.3.5\realms-1.3.5.jar;"); //20
            cmd.Append(libDir + @"\org\apache\commons\commons-compress\1.8.1\commons-compress-1.8.1.jar;"); //21
            cmd.Append(libDir + @"\org\apache\httpcomponents\httpclient\4.3.3\httpclient-4.3.3.jar;"); //22
            cmd.Append(libDir + @"\commons-logging\commons-logging\1.1.3\commons-logging-1.1.3.jar;"); //23
            cmd.Append(libDir + @"\org\apache\httpcomponents\httpcore\4.3.2\httpcore-4.3.2.jar;"); //24
            cmd.Append(libDir + @"\java3d\vecmath\1.3.1\vecmath-1.3.1.jar;"); //25
            cmd.Append(libDir + @"\net\sf\trove4j\trove4j\3.0.3\trove4j-3.0.3.jar;"); //26
            cmd.Append(libDir + @"\com\ibm\icu\icu4j-core-mojang\51.2\icu4j-core-mojang-51.2.jar;"); //27
            cmd.Append(libDir + @"\net\sf\jopt-simple\jopt-simple\4.5\jopt-simple-4.5.jar;"); //28
            cmd.Append(libDir + @"\com\paulscode\codecjorbis\20101023\codecjorbis-20101023.jar;"); //29
            cmd.Append(libDir + @"\com\paulscode\codecwav\20101023\codecwav-20101023.jar;"); //30
            cmd.Append(libDir + @"\com\paulscode\libraryjavasound\20101123\libraryjavasound-20101123.jar;"); //31
            cmd.Append(libDir + @"\com\paulscode\librarylwjglopenal\20100824\librarylwjglopenal-20100824.jar;"); //32
            cmd.Append(libDir + @"\com\paulscode\soundsystem\20120107\soundsystem-20120107.jar;"); //33
            cmd.Append(libDir + @"\io\netty\netty-all\4.0.10.Final\netty-all-4.0.10.Final.jar;"); //34
            cmd.Append(libDir + @"\com\google\guava\guava\15.0\guava-15.0.jar;"); //35
            cmd.Append(libDir + @"\org\apache\commons\commons-lang3\3.1\commons-lang3-3.1.jar;"); //36
            cmd.Append(libDir + @"\commons-io\commons-io\2.4\commons-io-2.4.jar;"); //37
            cmd.Append(libDir + @"\commons-codec\commons-codec\1.9\commons-codec-1.9.jar;"); //38
            cmd.Append(libDir + @"\net\java\jinput\jinput\2.0.5\jinput-2.0.5.jar;"); //39
            cmd.Append(libDir + @"\net\java\jutils\jutils\1.0.0\jutils-1.0.0.jar;"); //40
            cmd.Append(libDir + @"\com\google\code\gson\gson\2.2.4\gson-2.2.4.jar;"); //41
            cmd.Append(libDir + @"\com\mojang\authlib\1.5.21\authlib-1.5.21.jar;"); //42
            cmd.Append(libDir + @"\org\apache\logging\log4j\log4j-api\2.0-beta9\log4j-api-2.0-beta9.jar;"); //43
            cmd.Append(libDir + @"\org\apache\logging\log4j\log4j-core\2.0-beta9\log4j-core-2.0-beta9.jar;"); //44
            cmd.Append(libDir + @"\org\lwjgl\lwjgl\lwjgl\2.9.1\lwjgl-2.9.1.jar;"); //45
            cmd.Append(libDir + @"\org\lwjgl\lwjgl\lwjgl_util\2.9.1\lwjgl_util-2.9.1.jar;"); //46
            cmd.Append(libDir + @"\tv\twitch\twitch\5.16\twitch-5.16.jar;"); //47
            cmd.Append(workDir + @"\versions\1.7.10\1.7.10.jar ");
            cmd.Append("-Xmx2G ");
            cmd.Append("-XX:+UnlockExperimentalVMOptions ");
            cmd.Append("-XX:+UseG1GC ");
            cmd.Append("-XX:G1NewSizePercent=20 ");
            cmd.Append("-XX:G1ReservePercent=20 ");
            cmd.Append("-XX:MaxGCPauseMillis=50 ");
            cmd.Append("-XX:G1HeapRegionSize=32M ");
            cmd.Append("-Dlog4j.configurationFile=" + workDir + @"\assets\log_configs\client-1.7.xml ");
            cmd.Append("net.minecraft.launchwrapper.Launch ");
            cmd.Append("--username  "+ User.UserName + " ");
            cmd.Append("--version 1.7.10-Forge10.13.4.1614-1.7.10 ");
            cmd.Append("--gameDir " + workDir + " ");
            cmd.Append("--assetsDir " + workDir + @"\assets ");
            cmd.Append("--assetIndex 1.7.10 ");
            cmd.Append("--uuid " + User.UserID + " ");
            cmd.Append("--accessToken  "+ User.AccessToken + " ");
            cmd.Append("--userProperties {} ");
            cmd.Append("--userType mojang ");
            cmd.Append("--tweakClass cpw.mods.fml.common.launcher.FMLTweaker");

            Console.WriteLine("Launching Minecraft");
            try
            {
                var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = javaPath,
                        Arguments = cmd.ToString(),
                        UseShellExecute = false
                    }
                };
                p.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                errorPresent = true;
            }
        }
        private static void DownloadMinecraft() // Downloads Missing files for 1.7.10
        {
            // Check if natives exist
            bool nativesExist = true;
            foreach(string fileName in lstNatives)
            {
                if(!File.Exists(Path.Combine(nativesPath, fileName)))
                {
                    Console.WriteLine("Missing Native file: " + fileName);
                    nativesExist = false;
                }
            }
        }
    }
}
