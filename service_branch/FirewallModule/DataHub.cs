using System;
using System.Collections.Generic;
using System.Text;

namespace FM
{
    public sealed class DataHub
    {
        static DataHub instance = null;
        static readonly object padlock = new object();

        DataHub()
        {

        }

        public static DataHub Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataHub();
                    }
                    return instance;
                }
            }
        }

        Dictionary<Guid, DataTransport> localTransports = new Dictionary<Guid, DataTransport>();
        Dictionary<DataTransport.StaticTransportType, DataTransport> localStaticTransports = new Dictionary<DataTransport.StaticTransportType,DataTransport>();

        public void RegisterTransport(Guid g, DataTransport dt)
        {
            localTransports[g] = dt;
        }

        public void RegisterStaticTransport(DataTransport.StaticTransportType g, DataTransport dt)
        {
            localStaticTransports[g] = dt;
        }

        public bool SendMessage(DataTransport from, Guid g, object o)
        {
            DataTransport dt;
            if (localTransports.TryGetValue(g, out dt))
            {
                if (from.StaticTransport)
                {
                    dt.RecvMessage(from.statType, o);
                }
                else
                {
                    dt.RecvMessage(from.TransID, o);
                }
                return true;
            }
            //put remote check here
            return false;
        }

        public bool SendMessage(DataTransport from, DataTransport.StaticTransportType g, object o)
        {
            DataTransport dt;
            if (localStaticTransports.TryGetValue(g, out dt))
            {
                if (from.StaticTransport)
                {
                    dt.RecvMessage(from.statType, o);
                }
                else
                {
                    dt.RecvMessage(from.TransID, o);
                }
                return true;
            }
            //put remote check here
            return false;
        }
    }
}
