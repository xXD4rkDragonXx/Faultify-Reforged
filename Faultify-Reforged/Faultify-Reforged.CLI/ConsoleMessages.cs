﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faultify_Reforged.CLI
{
    internal class ConsoleMessages
    {
        public static string GetLogo()
        {
            var _logo = @"
      ███████╗ █████╗ ██╗   ██╗██╗  ████████╗██╗███████╗██╗   ██╗   
      ██╔════╝██╔══██╗██║   ██║██║  ╚══██╔══╝██║██╔════╝╚██╗ ██╔╝   
      █████╗  ███████║██║   ██║██║     ██║   ██║█████╗   ╚████╔╝    
      ██╔══╝  ██╔══██║██║   ██║██║     ██║   ██║██╔══╝    ╚██╔╝     
      ██║     ██║  ██║╚██████╔╝███████╗██║   ██║██║        ██║      
      ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝   ╚═╝╚═╝        ╚═╝      
  ██████╗ ███████╗███████╗ ██████╗ ██████╗  ██████╗ ███████╗██████╗ 
  ██╔══██╗██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝ ██╔════╝██╔══██╗
  ██████╔╝█████╗  █████╗  ██║   ██║██████╔╝██║  ███╗█████╗  ██║  ██║
  ██╔══██╗██╔══╝  ██╔══╝  ██║   ██║██╔══██╗██║   ██║██╔══╝  ██║  ██║
  ██║  ██║███████╗██║     ╚██████╔╝██║  ██║╚██████╔╝███████╗██████╔╝
  ╚═╝  ╚═╝╚══════╝╚═╝      ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═════╝
";
            return _logo;
        }

        public static void printMessage(string message, ConsoleColor consoleColor = ConsoleColor.Cyan, bool keepColor = false)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            if(!keepColor)
            {
                Console.ForegroundColor = currentColor;
            }
        }

        public static void printOptions(Options options)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Input Project: {options.InputProject}");
            Console.WriteLine($"Output Location: {(options.OutputLocation != null ? options.OutputLocation : "Default")}");
            Console.ForegroundColor = currentColor;
        }
    }
}
