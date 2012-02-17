using System;
using System.Collections.Generic;
using System.Text;

namespace FM
{
    public class DataTransport
    {
        public enum TransportType
        {
            Module,
            ModuleUI,
            Adapter
        }

        public enum StaticTransportType
        {
            Updater,
            UpdateConfiguration,
            OptionsGUI,
            Options,
            AdapterList,
            TrayIcon,
            Program,
            MainWindow,
            LogCenter
        }

        public Guid TransID = Guid.Empty;

        public StaticTransportType statType;
        public TransportType tType;

        public bool StaticTransport = false;

        public DataTransport(DataTransport.StaticTransportType statType)
        {
            StaticTransport = true;
            this.statType = statType;
        }

        public DataTransport(TransportType tType)
        {
            StaticTransport = false;
            this.tType = tType;
            TransID = Guid.NewGuid();
        }

        public delegate void Message(Guid from, object data);
        public delegate void StaticMessage(StaticTransportType from, object data);

        public event Message NewMessage;
        public event StaticMessage NewStaticMessage;

        public void RecvMessage(Guid from, object data)
        {
            if (NewMessage != null)
            {
                NewMessage(from, data);
            }
        }

        public void RecvMessage(StaticTransportType from, object data)
        {
            if (NewStaticMessage != null)
            {
                NewStaticMessage(from, data);
            }
        }

        public bool SendMessage(Guid to, object data)
        {
            return DataHub.Instance.SendMessage(this, to, data);
        }

        public bool SendMessage(StaticTransportType statType, object data)
        {
            return DataHub.Instance.SendMessage(this, statType, data);
        }
    }
}
