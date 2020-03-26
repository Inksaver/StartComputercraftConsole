using System;
using System.IO;
using System.Diagnostics;

namespace MinecraftLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            // find the working directory, eg C:\Users\Public\PortableApps\MinecraftPortable
            // or X:\PortableApps\MinecraftPortable where X is the USB drive letter
        	string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        	/*
 			Add the location of Mojang's MinecraftLauncher in the 'Minecraft' sub-folder
        	PortableApps usually uses  'app' as the subfolder name 
        	eg X:\PortableApps\MinecraftPortable\app\mcp_data\.minecraft
        	This code is used where the folder stucture is:
        	X:\PortableApps\MinecraftPortable\Minecraft\mcp_data\.minecraft
        	*/
            string startFile = Path.Combine(appPath, "Minecraft", "MinecraftLauncher.exe");
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = startFile; // X:\PortableApps\MinecraftPortable\Minecraft\MinecraftLauncher.exe - Mojang's file
            //args = ' --workDir X:\PortableApps\MinecraftPortable\Minecraft\mcp_data\.minecraft'
            start.Arguments = " --workDir " + Path.Combine(appPath, "Minecraft", "mcp_data",".minecraft");
            Process.Start(start);
        }
    }
}