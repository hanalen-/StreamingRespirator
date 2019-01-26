using System;
using System.Text;
using System.Threading;
using StreamingRespirator.Utilities;

namespace StreamingRespirator.Core.Streaming
{
    internal class StreamingConnection : IDisposable
    {
        private readonly Timer m_keepAlive;

        public WaitableStream Stream      { get; }
        public string         Description { get; }
        public long           OwnerId     { get; }

        public long LastStatus        { get; set; }
        public long LastActivity      { get; set; }
        public long LastDirectMessage { get; set; }

        public StreamingConnection(WaitableStream item, long ownerId, string description)
        {
            this.Stream      = item;
            this.OwnerId     = ownerId;
            this.Description = description;

            this.m_keepAlive = new Timer(this.SendKeepAlive, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        ~StreamingConnection()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool m_disposed;
        private void Dispose(bool disposing)
        {
            if (this.m_disposed) return;
            this.m_disposed = true;

            if (disposing)
            {
                this.m_keepAlive.Dispose();
                this.Stream     .Dispose();
            }
        }
        
        private static readonly byte[] KeepAlivePacket = Encoding.UTF8.GetBytes("\r\n");
        private void SendKeepAlive(object sender)
        {
            this.SendToStream(KeepAlivePacket);
        }

        public void SendToStream(string data)
        {
            this.SendToStream(Encoding.UTF8.GetBytes(data + "\r\n"));

            this.m_keepAlive.Change(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        private void SendToStream(byte[] data)
        {
            try
            {
                this.Stream.Write(data, 0, data.Length);
                this.Stream.Flush();
            }
            catch
            {
                this.Stream.Close();
            }
        }
    }
}
