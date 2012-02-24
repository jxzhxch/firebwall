using System.Collections.Generic;
using System;
using FM;
using System.Reflection;
using System.IO;

namespace PassThru
{
		public class ModuleList
        {
            List<int> ProcessingIndex = new List<int>();
            List<FirewallModule> modules = new List<FirewallModule>();
            object padlock = new object();
            Dictionary<string, string> loadedMods = new Dictionary<string, string>();
            NetworkAdapter na;

			public ModuleList(NetworkAdapter na) 
            {
                this.na = na;
			}

            public void LoadModule(string file)
            {
                FirewallModule mod = null;

                if (file.Contains("FirewallModule.dll") || !file.Contains(".dll"))
                    return;
                try
                {
                    if (loadedMods.ContainsValue(file))
                        return;
                    Assembly assembly = Assembly.Load(File.ReadAllBytes(file));
                    Type[] type = assembly.GetTypes();
                    foreach (Type t in type)
                    {
                        if (typeof(FirewallModule).IsAssignableFrom(t))
                        {
                            mod = (FirewallModule)Activator.CreateInstance(t);
                            mod.adapter = na;
                            mod.Enabled = false;
                            //mod.ModuleStart();
                            AddModule(mod);
                            loadedMods.Add(mod.MetaData.Name, file);
                        }
                    }
                }
                catch (ArgumentException ae)
                {
                    LogCenter.Instance.Push(mod.MetaData.Name, "Module attempted load twice.");
                    LogCenter.WriteErrorLog(ae);
                }
                catch (Exception e)
                {
                    LogCenter.WriteErrorLog(e);
                }
            }

            public void LoadExternalModules()
            {
                if (Directory.Exists("modules"))
                {
                    DirectoryInfo di = new DirectoryInfo("modules");
                    foreach (FileInfo fi in di.GetFiles())
                    {
                        LoadModule(fi.FullName);
                    }
                }
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if(Directory.Exists(folder + Path.DirectorySeparatorChar + "firebwall" + Path.DirectorySeparatorChar + "modules"))
                {
                    DirectoryInfo di = new DirectoryInfo(folder + Path.DirectorySeparatorChar + "firebwall" + Path.DirectorySeparatorChar + "modules");
                    foreach (FileInfo fi in di.GetFiles())
                    {
                        LoadModule(fi.FullName);
                    }
                }
            }

            public void InsertPIndex(int oIndex, int nIndex)
            {
                if (oIndex == nIndex) return;
                lock (padlock)
                {
                    int toMove = ProcessingIndex[oIndex];
                    ProcessingIndex.RemoveAt(oIndex);
                    ProcessingIndex.Insert(nIndex, toMove);
                }
            }

			public void AddModule(FirewallModule fm) 
            {
                lock (padlock)
                {
                    modules.Add(fm);
                    ProcessingIndex.Add(modules.Count - 1);
                }
			}

			public FirewallModule GetModule(int index) 
            {
                lock (padlock)
                {
                    return modules[ProcessingIndex[index]];
                }
			}

			public int Count 
            {
				get 
                {
					return modules.Count;
				}
			}
		}
}
