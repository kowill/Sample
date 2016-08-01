using System;
using System.Runtime.InteropServices;

namespace DirectShowSample.Api
{
    public static class Definition
    {
        public const string PropertyBag = "55272A00-42CB-11CE-8135-00AA004BB851";
        public const string MEDIASUBTYPE_Avi = "{E436EB88-524F-11CE-9F53-0020AF0BA770}";
        public const string PIN_CATEGORY_CAPTURE = "{FB6C4281-0353-11D1-905F-0000C0CC16BA}";
        public const string SAMMPLE_GRABBER = "{C1F400A0-3F08-11D3-9F0B-006008039E37}";

        public const string CLSID_CaptureGraphBuilder2 = "{BF87B6E1-8C27-11d0-B3F0-00AA003761C5}";
        public const string CLSID_FilterGraph = "{E436EBB3-524F-11CE-9F53-0020AF0BA770}";
        public const string CLSID_GraphBuilder = "{E436EBB3-524F-11CE-9F53-0020AF0BA770}";
        public const string CLSID_SystemDeviceEnum = "{62BE5D10-60EB-11d0-BD3B-00A0C911CE86}";
        public const string CLSID_VideoCompressorCategory = "{33D9A760-90C8-11d0-BD43-00A0C911CE86}";
        public const string CLSID_VideoInputDeviceCategory = "{860BB310-5D01-11d0-BD3B-00A0C911CE86}";


        public const string IID_IBaseFilter = "{56A86895-0AD4-11CE-B03A-0020AF0BA770}";

        public const int CLSCTX_INPROC = 0x03;

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct AM_MEDIA_TYPE
        {
            public Guid MejorType;
            public Guid SubType;
            [MarshalAs(UnmanagedType.Bool)]
            public bool FixedSizeSamples;
            [MarshalAs(UnmanagedType.Bool)]
            public bool TemporalCompression;
            public int SampleSize;
            public Guid FomatType;
            public IntPtr Unk;
            public int CbFormat;
            public IntPtr PbFormat;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct PIN_INFO
        {
            public IBaseFilter Filter;
            public PIN_DIRECTION Dir;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string AchName;
        }

        public enum PIN_DIRECTION { PINDIR_INPUT, PINDIR_OUTPUT }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct FILTER_INFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string achName;
            IFilterGraph pGraph;
        }

        [Flags]
        public enum AMRenderExFlags
        {
            None = 0,
            RenderToExistingRenderers = 1
        }
    }
}
