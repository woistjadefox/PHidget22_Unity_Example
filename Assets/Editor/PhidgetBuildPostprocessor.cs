using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class PhidgetBuildPostprocessor{
        [PostProcessBuildAttribute(1)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject){
                if (target == BuildTarget.StandaloneOSX){
                        InsertPhidgetDLLs(pathToBuiltProject);
                }
        }

        private static void InsertPhidgetDLLs(string buildPath){
                string dllmap = "	<dllmap dll=\"phidget22\" target=\"/Library/Frameworks/Phidget22.framework/Versions/Current/Phidget22\" />";  
                string[] configPaths = new string[]{"/Contents/Mono/etc/mono/config", "/Contents/MonoBleedingEdge/etc/mono/config"};
                foreach (string configPath in configPaths){
                        if (File.Exists(buildPath+configPath)){
                                string[] linesArray = File.ReadAllLines(buildPath + configPath);   //Fill a list with the lines from the txt file.
                                List<string> lines = new List<string>();
                                foreach (string line in linesArray){
                                        lines.Add(line);
                                }
                                lines.Insert(lines.Count - 1, dllmap);  //Insert the line you want to add last under the tag 'item1'.
                                File.WriteAllLines(buildPath + configPath, lines.ToArray());
                                Debug.Log("dll map successfully insertet into config file at: " + configPath);     
                        } else {
                                Debug.LogError("Config file not found in build at: " + configPath + " could not insert dllmap");
                        }
                }
        }
}
