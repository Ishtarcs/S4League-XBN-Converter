using System;
using System.IO;
using System.Linq;

namespace XBNLibraryExtended.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("           Attica's XBN/XML Converter            ");
            Console.WriteLine("=================================================");
            Console.WriteLine("Starting program...");

            var xbnFolder = Path.Combine(Environment.CurrentDirectory, "xbn");
            var xmlFolder = Path.Combine(Environment.CurrentDirectory, "xml");
            var outFolder = Path.Combine(Environment.CurrentDirectory, "out");
            Console.WriteLine("Checking for required directories...");

            if (!Directory.Exists(xbnFolder))
            {
                Directory.CreateDirectory(xbnFolder);
                Console.WriteLine($"[INFO] Created missing directory: {xbnFolder}");
            }
            else
            {
                Console.WriteLine($"[INFO] Directory exists: {xbnFolder}");
            }

            if (!Directory.Exists(xmlFolder))
            {
                Directory.CreateDirectory(xmlFolder);
                Console.WriteLine($"[INFO] Created missing directory: {xmlFolder}");
            }
            else
            {
                Console.WriteLine($"[INFO] Directory exists: {xmlFolder}");
            }

            if (!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
                Console.WriteLine($"[INFO] Created missing directory: {outFolder}");
            }
            else
            {
                Console.WriteLine($"[INFO] Directory exists: {outFolder}");
            }

           
            Console.Write("[INPUT] Select conversion type: [0] xbn->xml, [1] xml->xbn: ");
            int conversionType;
            while (!int.TryParse(Console.ReadLine(), out conversionType) || conversionType < 0 || conversionType > 1)
            {
                Console.WriteLine("[ERROR] Invalid selection. Please enter 0 or 1.");
                Console.Write("[INPUT] Select conversion type: [0] xbn->xml, [1] xml->xbn: ");
            }
            Console.WriteLine($"[INFO] Conversion type selected: {(conversionType == 0 ? "XBN to XML" : "XML to XBN")}");

           
            if (conversionType == 0)
            {
                Console.WriteLine("[INFO] Beginning XBN to XML conversion...");
                string[] files = Directory.GetFiles(xbnFolder).Where(x => Path.GetExtension(x).Equals(".xbn")).ToArray();
                if (files.Length <= 0)
                {
                    Console.WriteLine("[WARNING] No .xbn files found in the xbn folder. Conversion aborted.");
                }
                else
                {
                    Console.WriteLine("[INFO] .xbn files available for conversion:");
                    for (int i = 0; i < files.Length; i++)
                        Console.WriteLine($"[{i}] --> {Path.GetFileName(files[i])}");
                    Console.WriteLine();

                    int selected = -1;
                    bool sel = false;

                    while (!sel)
                    {
                        Console.Write("[INPUT] Select a file by its number: ");
                        if (int.TryParse(Console.ReadLine(), out selected) && selected >= 0 && selected < files.Length)
                        {
                            sel = true;
                            Console.WriteLine($"[INFO] File selected for conversion: {Path.GetFileName(files[selected])}");
                        }
                        else
                        {
                            Console.WriteLine("[ERROR] Invalid selection, please try again.");
                        }
                    }

                    string outputPath = $"{Path.Combine(outFolder, Path.GetFileNameWithoutExtension(files[selected]))}.xml";
                    Console.WriteLine($"[INFO] Starting conversion of {Path.GetFileName(files[selected])} to {outputPath}");

                    XMLConverter converter = new XMLConverter(files[selected], outputPath);
                    converter.StartConversion();
                    Console.WriteLine("[SUCCESS] XBN to XML conversion completed successfully.");
                }
            }
            else if (conversionType == 1)
            {
                Console.WriteLine("[INFO] Beginning XML to XBN conversion...");
                string[] files = Directory.GetFiles(xmlFolder).Where(x => Path.GetExtension(x).Equals(".xml")).ToArray();
                if (files.Length <= 0)
                {
                    Console.WriteLine("[WARNING] No .xml files found in the xml folder. Conversion aborted.");
                }
                else
                {
                    Console.WriteLine("[INFO] .xml files available for conversion:");
                    for (int i = 0; i < files.Length; i++)
                        Console.WriteLine($"[{i}] --> {Path.GetFileName(files[i])}");
                    Console.WriteLine();

                    int selected = -1;
                    bool sel = false;

                    while (!sel)
                    {
                        Console.Write("[INPUT] Select a file by its number: ");
                        if (int.TryParse(Console.ReadLine(), out selected) && selected >= 0 && selected < files.Length)
                        {
                            sel = true;
                            Console.WriteLine($"[INFO] File selected for conversion: {Path.GetFileName(files[selected])}");
                        }
                        else
                        {
                            Console.WriteLine("[ERROR] Invalid selection, please try again.");
                        }
                    }

                    string outputPath = $"{Path.Combine(outFolder, Path.GetFileNameWithoutExtension(files[selected]))}.xbn";
                    Console.WriteLine($"[INFO] Starting conversion of {Path.GetFileName(files[selected])} to {outputPath}");

                    XBNConverter converter = new XBNConverter(files[selected], outputPath);
                    converter.StartConversion();
                    Console.WriteLine("[SUCCESS] XML to XBN conversion completed successfully.");
                }
            }

            Console.WriteLine("=================================================");
            Console.WriteLine("       Attica's XBN/XML Converter - Finished      ");
            Console.WriteLine("=================================================");
            Console.WriteLine("Program completed. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
