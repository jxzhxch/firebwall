using System.Collections.Generic;
using System;
using FM;
using System.Reflection;
using System.IO;
using FM;
using System.Runtime.Serialization.Formatters.Binary;

namespace PassThru
{
		public class ModuleList
        {
            List<int> ProcessingIndex = new List<int>();
            List<FirewallModule> modules = new List<FirewallModule>();
            object padlock = new object();
            Dictionary<string, string> loadedMods = new Dictionary<string, string>();
            List<KeyValuePair<bool, string>> moduleOrder = new List<KeyValuePair<bool, string>>();
            NetworkAdapter na;

            public void SaveModuleOrder()
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folder = folder + Path.DirectorySeparatorChar + "firebwall";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                folder = folder + Path.DirectorySeparatorChar + "Adapters";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                folder = folder + Path.DirectorySeparatorChar + na.Name;
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string file = folder + Path.DirectorySeparatorChar + "modules.cfg";
                FileStream stream = File.Open(file, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, moduleOrder);
                stream.Close();
            }

            public void LoadModuleOrder()
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folder = folder + Path.DirectorySeparatorChar + "firebwall";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                folder = folder + Path.DirectorySeparatorChar + "Adapters";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                folder = folder + Path.DirectorySeparatorChar + na.Name;
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string file = folder + Path.DirectorySeparatorChar + "modules.cfg";
                if (File.Exists(file))
                {
                    FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    moduleOrder = (List<KeyValuePair<bool, string>>)bFormatter.Deserialize(stream);
                    stream.Close();
                }
            }

			public ModuleList(NetworkAdapter na) 
            {
                this.na = na;
                LoadModuleOrder();
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

            public List<KeyValuePair<bool, string>> GetModuleOrder()
            {
                return moduleOrder;
            }

            public void UpdateModuleOrder(List<KeyValuePair<bool, string>> mO)
            {
                moduleOrder = mO;
                UpdateModuleOrder();
            }

            public void UpdateModuleOrder()
            {
                lock (padlock)
                {
                    ProcessingIndex.Clear();
                    for (int i = 0; i < moduleOrder.Count; i++)
                    {
                        int mindex = GetModuleIndex(moduleOrder[i].Value);
                        if (mindex != -1)
                        {
                            ProcessingIndex.Add(mindex);
                            if (modules[mindex].Enabled != moduleOrder[i].Key)
                            {
                                if (modules[mindex].Enabled)
                                    modules[mindex].ModuleStop();
                                else
                                    modules[mindex].ModuleStart();
                            }
                        }
                    }
                    moduleOrder.Clear();
                    for (int i = 0; i < Count; i++)
                    {
                        FirewallModule fm = GetModule(i);
                        moduleOrder.Add(new KeyValuePair<bool, string>(fm.Enabled, fm.MetaData.Name));
                    }
                    SaveModuleOrder();
                }
            }

            void InsertPIndex(int oIndex, int nIndex)
            {
                if (oIndex == nIndex) return;
                lock (padlock)
                {
                    int toMove = ProcessingIndex[oIndex];
                    ProcessingIndex.RemoveAt(oIndex);
                    if (nIndex < ProcessingIndex.Count)
                    {
                        ProcessingIndex.Insert(nIndex, toMove);
                    }
                    else
                    {
                        ProcessingIndex.Add(toMove);
                    }
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

            int GetModuleIndex(string name)
            {
                for (int x = 0; x < modules.Count; x++)
                {
                    if (modules[x].MetaData.Name == name)
                    {
                        return x;
                    }
                }
                return -1;
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
