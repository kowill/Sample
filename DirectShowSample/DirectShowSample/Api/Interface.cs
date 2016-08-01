using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using static DirectShowSample.Api.Definition;

namespace DirectShowSample.Api
{

    [ComImport]
    [Guid("29840822-5b84-11d0-bd3b-00a0c911ce86")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICreateDevEnum
    {
        int CreateClassEnumerator(ref Guid clsidDeviceClass, [Out] out IEnumMoniker ppEnumMoniker, int dwFlags);
    }

    [ComImport]
    [Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyBag
    {
        int Read([In] string propName, out object ptrVar, int errorLog);

        int Write([In] string propName, [In] ref object ptrVar);
    }

    [ComImport]
    [Guid("56A86895-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBaseFilter
    {
        int EnumPins([In, Out] ref IEnumPins ppEnum);
        int FindPin([In, MarshalAs(UnmanagedType.LPWStr)]string Id, [Out] IPin ppPin);
        int JoinFilterGraph([In]IFilterGraph pGraph, [In, MarshalAs(UnmanagedType.LPWStr)]string pName);
        int QueryFilterInfo([Out] FILTER_INFO pInfo);
        int QueryVendorInfo([Out, MarshalAs(UnmanagedType.LPWStr)] string pVendorInfo);
    }

    [ComImport]
    [Guid("56A86892-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumPins
    {
        int Next([In]int cPins, [Out]IPin ppPpins, [Out]int pcFetched);
        int Skip([In]int cPins);
        int Reset();
        int Clone([Out]IEnumPins ppEnum);
    }


    [ComImport]
    [Guid("56A86891-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPin
    {
        int Connect([In]IPin pReceivePin, [In] AM_MEDIA_TYPE pmt);
        int ReceiveConnection([In]IPin pConnector, [In]AM_MEDIA_TYPE pmt);
        int Disconnect();
        int ConnectedTo([Out] IPin ppPint);
        int ConnectionMediaType([Out]AM_MEDIA_TYPE pmt);
        int QueryPinInfo([Out] PIN_INFO pInfo);
        int QueuyId([Out, MarshalAs(UnmanagedType.LPWStr)] string id);
        int QueryAccept([In] AM_MEDIA_TYPE pmt);
        int EnumMediaTypes([Out]IEnumMediaTypes ppEnum);
        int QueryInternalConnections([Out] IPin apPin, [In, Out] ref int nPin);
        int EndOfStream();
        int BeginFlush();
        int EndFlush();
        int NewSegment(long tStart, long tStop, double dRate);
        int QueryDirection([Out]PIN_DIRECTION pPinDir);
    }

    [ComImport]
    [Guid("89C31040-846B-11CE-97D3-00AA0055595A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumMediaTypes
    {
        int Clone([Out]IEnumMediaTypes ppEnum);
        int Next([In] int cMediaTypes, [Out]AM_MEDIA_TYPE ppMediaTypes, [Out]int pcFetched);
        int Reset();
        int Skip([In]int cMediaTypes);
    }

    [ComImport]
    [Guid("56A868A9-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterGraph
    {
        int AddFilter([In]IBaseFilter pFilter, [In, MarshalAs(UnmanagedType.LPWStr)]string pName);
        int RemoveFilter([In]IBaseFilter pFilter);
        int EnumFilters([Out]IEnumFilters ppEnum);
        int FindFilterByName([In, MarshalAs(UnmanagedType.LPWStr)] string pName, [Out]IBaseFilter ppFilter);
        int ConnectDirect([In]IPin ppinOut, [In]IPin ppinIn, [In]AM_MEDIA_TYPE pmt);
        int Reconnect([In]IPin ppin);
        int Disconnect([In]IPin ppin);
        int SetDefaultSyncSource();
    }


    [ComImport]
    [Guid("56A868A9-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGraphBuilder : IFilterGraph
    {
        new int AddFilter([In]IBaseFilter pFilter, [In, MarshalAs(UnmanagedType.LPWStr)]string pName);
        new int RemoveFilter([In]IBaseFilter pFilter);
        new int EnumFilters([Out]IEnumFilters ppEnum);
        new int FindFilterByName([In, MarshalAs(UnmanagedType.LPWStr)] string pName, [Out]IBaseFilter ppFilter);
        new int ConnectDirect([In]IPin ppinOut, [In]IPin ppinIn, [In]AM_MEDIA_TYPE pmt);
        new int Reconnect([In]IPin ppin);
        new int Disconnect([In]IPin ppin);
        new int SetDefaultSyncSource();

        int Connect([In] IPin ppinOut, [In]IPin ppinIn);
        int Render([In]IPin ppinOut);
        int RenderFile([In, MarshalAs(UnmanagedType.LPWStr)]string lpwstrFile, [In, MarshalAs(UnmanagedType.LPWStr)]string lpwstrPlayList);
        int AddSourceFilter([In, MarshalAs(UnmanagedType.LPWStr)]string lpwstrFileName, [In, MarshalAs(UnmanagedType.LPWStr)]string lpwstrFilterName, [Out]IBaseFilter ppFilter);
        int SetLogFile(IntPtr hFile);
        int Abort();
        int ShouldOperationContinue();
    }


    [ComImport]
    [Guid("36B73882-C2C8-11CF-8B46-00805F6CEF60")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterGraph2 : IGraphBuilder
    {
        new int AddFilter([In]IBaseFilter pFilter, [In, MarshalAs(UnmanagedType.LPWStr)]string pName);
        new int RemoveFilter([In]IBaseFilter pFilter);
        new int EnumFilters([Out]IEnumFilters ppEnum);
        new int FindFilterByName([In, MarshalAs(UnmanagedType.LPWStr)] string pName, [Out]IBaseFilter ppFilter);
        new int ConnectDirect([In]IPin ppinOut, [In]IPin ppinIn, [In]AM_MEDIA_TYPE pmt);
        new int Reconnect([In]IPin ppin);
        new int Disconnect([In]IPin ppin);
        new int SetDefaultSyncSource();
        int AddSourceFilterForMoniker([In] IMoniker pMoniker, [In] IBindCtx pCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName, [Out] out IBaseFilter ppFilter);
        int ReconnectEx([In] IPin ppin, [In] AM_MEDIA_TYPE pmt);
        int RenderEx([In] IPin pPinOut, [In] AMRenderExFlags dwFlags, [In, Out] IntPtr pvContext);
    }

    [ComImport]
    [Guid("56A86893-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumFilters
    {
        int Clone([Out]IEnumFilters ppEnum);
        int Next([In] int cFilters, [Out]IBaseFilter ppMediaTypes, [Out]int pcFetched);
        int Reset();
        int Skip([In]int cFilter);
    }

    [ComImport]
    [Guid("93E5A4E0-2D50-11d2-ABFA-00A0C9C6E38D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICaptureGraphBuilder2
    {
        int AllocCapFile([In, MarshalAs(UnmanagedType.LPWStr)]string lpwstr, [In]long dwlSize);
        int ControlStream([In] Guid pCategory, [In]Guid pType, [In]IBaseFilter pFilter, [In]long pstart, [In]long pstop, [In]short wStartCookie, [In]short wStopCookie);
        int CopyCaptureFile([In, MarshalAs(UnmanagedType.LPWStr)]string lpwstrOld, [In, MarshalAs(UnmanagedType.LPWStr)]string lpwstrNew, [In]int fAllowEscAbort, [In]IAMCopyCaptureFileProgress pCallBack);
        int FindInterface([In]Guid pCategory, [In]Guid pType, [In]IBaseFilter pf, [In]Guid riid, [Out]object ppint);
        int FindPin([In]object pSource, [In]PIN_DIRECTION pindir, [In]Guid pCategory, [In]Guid pType, [In]bool fUnconnected, [In] int num, [Out]IPin ppPin);
        int GetFiltergraph([Out]IGraphBuilder ppfg);
        int RenderStream([In]Guid pCategory, [In]Guid pType, [In]object pSource, [In]IBaseFilter pintermediate, [In]IBaseFilter pSink);
        [MethodImpl(MethodImplOptions.PreserveSig)]
        int SetFiltergraph([In]IGraphBuilder pfg);
        int SetOutputFileName([In]Guid pType, [In, MarshalAs(UnmanagedType.LPWStr)]string lpwstrFile, [Out]IBaseFilter ppf, [Out]IFileSinkFilter pSink);
    }


    [ComImport]
    [Guid("A2104830-7C70-11CF-8BCE-00AA00A3F1A6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileSinkFilter
    {
        int SetFileName([In, MarshalAs(UnmanagedType.LPWStr)]string pszFileName, [In]AM_MEDIA_TYPE pmt);
        int GetCurFile([Out, MarshalAs(UnmanagedType.LPWStr)]string ppszFileName, [Out]AM_MEDIA_TYPE pmt);
    }

    [ComImport]
    [Guid("670D1D20-A068-11D0-B3F0-00AA003761C5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMCopyCaptureFileProgress
    {
        int Progress([In]int iProgress);
    }
    [ComImport]
    [Guid("6B652FFF-11FE-4fce-92AD-0266B5D7C78F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabber
    {
        int SetOneShot([In, MarshalAs(UnmanagedType.Bool)] bool OneShot);

        int SetMediaType([In, MarshalAs(UnmanagedType.LPStruct)] AM_MEDIA_TYPE pmt);

        int GetConnectedMediaType([Out, MarshalAs(UnmanagedType.LPStruct)] AM_MEDIA_TYPE pmt);

        int SetBufferSamples([In, MarshalAs(UnmanagedType.Bool)] bool BufferThem);

        int GetCurrentBuffer(ref int pBufferSize, IntPtr pBuffer);

        int GetCurrentSample(out IMediaSample ppSample);

        int SetCallback(ISampleGrabberCB pCallback, int WhichMethodToCallback);
    }
    [ComImport]
    [Guid("56a8689a-0ad4-11ce-b03a-0020af0ba770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSample
    {
        int GetPointer([Out] out IntPtr ppBuffer);
        int GetSize();

        int GetTime([Out] out long pTimeStart, [Out] out long pTimeEnd);

        int SetTime([In] long pTimeStart, [In, MarshalAs(UnmanagedType.LPStruct)] long pTimeEnd);

        int IsSyncPoint();

        int SetSyncPoint([In] bool bIsSyncPoint);

        int IsPreroll();

        int SetPreroll([In] bool bIsPreroll);

        int GetActualDataLength();

        int SetActualDataLength([In] int len);

        int GetMediaType([Out] out AM_MEDIA_TYPE ppMediaType);

        int SetMediaType([In] AM_MEDIA_TYPE pMediaType);

        int IsDiscontinuity();

        int SetDiscontinuity([In] bool bDiscontinuity);

        int GetMediaTime([Out] out long pTimeStart, [Out] out long pTimeEnd);
        int SetMediaTime([In] long pTimeStart, [In] long pTimeEnd);
    }
    [ComImport]
    [Guid("0579154A-2B53-4994-B0D0-E773148EFF85")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabberCB
    {
        int SampleCB(double SampleTime, IMediaSample pSample);

        int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen);
    }
}
