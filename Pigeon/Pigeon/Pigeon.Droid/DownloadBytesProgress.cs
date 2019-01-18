using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Pigeon.Droid
{
    public class DownloadBytesProgress
    {
        public DownloadBytesProgress(string fileName, int bytesReceived, int totalBytes)
        {
            Filename = fileName;
            BytesReceived = bytesReceived;
            TotalBytes = totalBytes;
        }

        public int TotalBytes { get; private set; }

        public int BytesReceived { get; private set; }

        public float PercentComplete { get { return (float)BytesReceived / TotalBytes; } }

        public string Filename { get; private set; }

        public bool IsFinished { get { return BytesReceived == TotalBytes; } }
    }
}