using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FM;

namespace PassThru
{
    public class UpdateChecker
    {
        int versionA = 0;
        int versionB = 3;
        int versionC = 10;
        int versionD = 0;
        Thread updateThread;

        [Serializable]
        public class UpdateConfig
        {
            public uint MinuteInterval = 15;
            public bool Enabled = true;
            public bool StartUpCheck = true;
        }

        public UpdateConfig Config
        {
            get;
            set;
        }

        public void LoadConfig()
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folder = folder + Path.DirectorySeparatorChar + "firebwall";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string file = folder + Path.DirectorySeparatorChar + "updating.cfg";
                if (File.Exists(file))
                {
                    FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    Config = (UpdateConfig)bFormatter.Deserialize(stream);
                    stream.Close();
                }
                else
                {
                    Config = new UpdateConfig();
                }
            }
            catch
            {
                Config = new UpdateConfig();
            }
        }

        public void SaveConfig()
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folder = folder + Path.DirectorySeparatorChar + "firebwall";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string file = folder + Path.DirectorySeparatorChar + "updating.cfg";
                FileStream stream = File.Open(file, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, Config);
                stream.Close();
            }
            catch { }
        }

        public void Updater()
        {
            LoadConfig();
            updateThread = new Thread(new ThreadStart(UpdateLoop));
            updateThread.Start();            
        }

        public void Close()
        {
            updateThread.Abort();
        }

        public void MyDownloadFile(Uri url, string outputFilePath)
        {
            const int BUFFER_SIZE = 16 * 1024;
            using (var outputFileStream = File.Create(outputFilePath, BUFFER_SIZE))
            {
                WebRequest req = WebRequest.Create(url);
                using (WebResponse response = req.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        byte[] buffer = new byte[BUFFER_SIZE];
                        int bytesRead;
                        do
                        {
                            bytesRead = responseStream.Read(buffer, 0, BUFFER_SIZE);
                            outputFileStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }
            }
        }

        // REVIEW
        public void UpdateLoop()
        {
            WebClient wc = new WebClient();
            bool firstTime = true;
            while (true)
            {
                Thread.Sleep(30000);
                if ((firstTime && Config.StartUpCheck) || Config.Enabled)
                {
                    string ret = CheckForNewVersion();
                    if (ret != null)
                    {
                        string questionA = "Would you like to download the newest version of fireBwall?";
                        string headerA = "New Update Available!";
                        if (MessageBox.Show(questionA, headerA, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(ret);                            
                        }
                        return;
                    }
                }
                firstTime = false;
                Thread.Sleep(new TimeSpan(0, (int)Config.MinuteInterval, 0));
            }
        }

        // REVIEW
        public string CheckForNewVersion()
        {
            try
            {
                WebClient wc = new WebClient();
                string xml = wc.DownloadString("http://www.firebwall.com/currentVersion.php");
                string url = "http://www.firebwall.com/index.php#fireBwall " + xml;
                string version = xml;
                string a = version.Substring(0, version.IndexOf("."));
                if (versionA == int.Parse(a))
                {
                    version = version.Substring(version.IndexOf(".") + 1);
                    string b = version.Substring(0, version.IndexOf("."));
                    if (versionB == int.Parse(b))
                    {
                        version = version.Substring(version.IndexOf(".") + 1);
                        string c = version.Substring(0, version.IndexOf("."));
                        if (versionC == int.Parse(c))
                        {
                            version = version.Substring(version.IndexOf(".") + 1);
                            if (versionD == int.Parse(version))
                            {
                                return null;
                            }
                            else if (versionD < int.Parse(version))
                            {
                                return url;
                            }
                        }
                        else if (versionC < int.Parse(c))
                        {
                            return url;
                        }
                    }
                    else if (versionB < int.Parse(b))
                    {
                        return url;
                    }
                }
                else if (versionA < int.Parse(a))
                {
                    return url;
                }
                return null;
            }
            catch (Exception e) 
            {
                LogCenter.WriteErrorLog(e);
            }
            return null;
        }
    }
}
