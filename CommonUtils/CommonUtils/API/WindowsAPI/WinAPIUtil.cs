using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
//using System.Management.Instrumentation;
//using System.Management;
using System.Net.NetworkInformation;
using System.Net;
using Microsoft.Win32;
//using IWshRuntimeLibrary;
using System.Drawing;
using System.Windows.Forms;

namespace CommonUtils.CommonUtils.API.WindowsAPI
{
    #region Win.dll
    /// <summary>
    ///大多数为 WinApi
    /// </summary>
    public  class Win_Window
    {
        /// <summary>
        /// 获取生肖    如：”猴“
        /// </summary>
        /// <returns></returns>
        public static string GetChineseLunisolar()
        {
            System.Globalization.ChineseLunisolarCalendar calendar = new System.Globalization.ChineseLunisolarCalendar();
            DateTime Date = DateTime.Now.Date;
            string Animals = "鼠牛虎兔龙蛇马羊猴鸡狗猪";
            int tem_n = calendar.GetSexagenaryYear(Date);                                     //获取今天在那个甲子年中
            string Resemble = Animals.Substring((tem_n - 1) % 12, 1);                            //获取生肖
            return Resemble;
        }
        /// <summary>
        /// 提升进程权限
        /// </summary>
        /// <param name="Privilege">所需要的权限名称，可以到MSDN查找关于Process Token &Privilege内容可以查到</param>
        /// <param name="Enable">如果为True 就是打开相应权限，如果为False 则是关闭相应权限</param>
        /// <param name="CurrentThread">如果为True 则仅提升当前线程权限，否则提升整个进程的权限</param>
        /// <param name="Enabled">输出原来相应权限的状态（打开 | 关闭）</param>
        /// <returns></returns>
        [DllImport("ntdll.dll")]
        public static extern int RtlAdjustPrivilege(string Privilege, bool Enable, bool CurrentThread, int Enabled);
        /// <summary>
        ///创建一个在其它进程地址空间中运行的线程(也称:创建远程线程).
        /// </summary>
        /// <param name="hProcess">目标进程的句柄</param>
        /// <param name="lpThreadAttributes">指向线程的安全描述 结构体 的指针，一般设置为NULL，表示使用默认的安全级别</param>
        /// <param name="dwStackSize">线程堆栈大小，一般设置为0，表示使用默认的大小，一般为1M</param>
        /// <param name="lpStartAddress">线程函数的地址</param>
        /// <param name="lpParameter">线程参数</param>
        /// <param name="dwCreationFlags">线程的创建方式    CREATE_SUSPENDED 线程以挂起方式创建</param>
        /// <param name="lpThreadId">输出参数，记录创建的远程线程的ID</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int CreateRemoteThread(
            IntPtr hProcess,
            int lpThreadAttributes,
            int dwStackSize,
            int lpStartAddress, int lpParameter, int dwCreationFlags, out int lpThreadId);

        /// <summary>
        ///在指定进程的虚拟空间保留或提交内存区域
        /// </summary>
        /// <param name="hProcess">申请内存所在的进程句柄 </param>
        /// <param name="lpAddress">保留页面的内存地址；一般用NULL自动分配 </param>
        /// <param name="dwSize"> 欲分配的内存大小，字节单位；注意实际分 配的内存大小是页内存大小的整数倍 </param>
        /// <param name="flAllocationType"></param>
        /// <param name="flProtect"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int VirtualAllocEx(
            IntPtr hProcess,
            int lpAddress, int dwSize, int flAllocationType, int flProtect);

        /// <summary>
        /// 动画显示窗口
        /// </summary>
        /// <param name="hwnd">目标窗口句柄</param>
        /// <param name="dwTime">动画的持续时间，数值越大动画效果的时间就越长</param>
        /// <param name="dwFlags">DwFlags参数是动画效果类型选项</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        /// <summary>
        /// 枚举窗口（与IsWindowVisible连用哟~~）
        /// </summary>
        /// <param name="lpEnumFunc">一个委托</param>
        /// <param name="lParam">一般设置为 ：0</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);
        /// <summary>
        /// 定义枚举窗口委托
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate bool WNDENUMPROC(IntPtr hwnd, int lParam);
        /// <summary>
        /// 获取文件图标1
        /// </summary>
        /// <param name="filePath">通过文件名</param>
        /// <returns></returns>
        //public static Icon GetIcon(string filePath)
        //{
        //    return Icon.ExtractAssociatedIcon(filePath);
        //}
        /// <summary>
        /// 获取文件图标2
        /// </summary>
        /// <param name="fileHandle">通过图标句柄</param>
        /// <returns></returns>
        //public static Icon GetIcon(IntPtr fileHandle)
        //{
        //    return Icon.FromHandle(fileHandle);
        //}
        /// <summary>
        /// 通过进程ID找关联进程的主模块
        /// </summary>
        /// <param name="ProcessId">进程ID</param>
        /// <returns></returns>
        public static string GetProcessModulePathById(int ProcessId)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(ProcessId);
            return p.MainModule.FileName;
        }
        /// <summary>
        /// 通过进程ID找窗口句柄
        /// </summary>
        /// <param name="ProcessId">进程ID</param>
        /// <returns></returns>
        public static IntPtr GetWindowHandleById(int ProcessId)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(ProcessId);
            return p.MainWindowHandle;
        }
        /// <summary>	
        /// 通过进程ID找文件版本号
        /// </summary>
        /// <param name="ProcessId">进程ID</param>
        /// <returns></returns>
        public static string GetProcessModuleVersionById(int ProcessId)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(ProcessId);
            return p.MainModule.FileVersionInfo.FileVersion;
        }
        /// <summary>
        /// 后台向DOS窗口输入代码，执行（返回执行结果）
        /// </summary>
        /// <param name="CmdString">代码，如：tasklist>1.txt  ........</param>
        /// <returns></returns>
        public static string InvokeCmd(string CmdString)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo ps = new System.Diagnostics.ProcessStartInfo();
            ps.FileName = "cmd.exe";
            ps.RedirectStandardError = true;
            ps.RedirectStandardInput = true;
            ps.RedirectStandardOutput = true;
            ps.UseShellExecute = false;
            ps.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            ps.CreateNoWindow = true;
            p.StartInfo = ps;
            p.Start();
            p.StandardInput.WriteLine(CmdString);
            p.StandardInput.WriteLine(" exit");
            string str = p.StandardOutput.ReadToEnd();
            p.Close();
            return str;
        }
        /// <summary>
        /// 判断程序是否为管理员身份运行
        /// </summary>
        /// <param name="WindowsBuiltInRole">System.Security.Principal.WindowsBuiltInRole</param>
        /// <returns></returns>
        //public static bool CheckAdministrator(System.Security.Principal.WindowsBuiltInRole WindowsBuiltInRole)
        //{
        //    bool isAdministrator = false;
        //    System.Security.Principal.WindowsIdentity id = System.Security.Principal.WindowsIdentity.GetCurrent();
        //    System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(id);
        //    if (principal.IsInRole(WindowsBuiltInRole))
        //    {
        //        isAdministrator = true;
        //    }
        //    else
        //    {
        //        isAdministrator = false;
        //    }
        //    return isAdministrator;
        //}
        [DllImport("user32.dll")]
        public static extern bool GetMessage(out MSG lpMsg, int hWnd, int wMsgFilterMin, int wMsgFilterMax);
        [DllImport("user32.dll")]
        public static extern int DefWindowProc(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int RegisterClassEx(ref WNDCLASS lpwcx);
        [DllImport("user32.dll")]
        public static extern bool TranslateMessage(out MSG lpMsg);
        [DllImport("gdi32.dll")]
        public static extern int GetStockObject(int fnObject);
        [DllImport("user32.dll")]
        public static extern int DispatchMessage(out MSG lpmsg);
        [DllImport("user32.dll")]
        public static extern int LoadCursor(int hInstance, string lpCursorName);
        [DllImport("user32.dll")]
        public static extern int LoadIcon(int hInstance, string lpIconName);
        public delegate int WNDPROC(IntPtr hwnd, int msg, int wParam, int lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASS
        {
            public int style;
            public WNDPROC lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public int hIcon;
            public int hCursor;
            public int hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [DllImport("shlwapi.dll")]
        public static extern bool StrToIntEx(string pszString, int dwFlags, out int piRet);
        /// <summary>
        /// 创建进程快照
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="th32ProcessID"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateToolhelp32Snapshot(int dwFlags, int th32ProcessID);
        /// <summary>
        /// 线程信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct THREADENTRY32
        {
            public int dwSize;
            public int cntUsage;
            public int th32ThreadID;
            public int th32OwnerProcessID;
            public int tpBasePri;
            public int tpDeltaPri;
            public int dwFlags;
        }
        /// <summary>
        /// 进程信息 结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESSENTRY32
        {
            public int dwSize;
            public int cntUsage;
            public int th32ProcessID;
            public int th32DefaultHeapID;
            public int th32ModuleID;
            public int cntThreads;
            public int th32ParentProcessID;
            public int pcPriClassBase;
            public int dwFlags;
            public char[] szExeFile;  //char[256]
        }
        /// <summary>
        /// 模块信息 结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MODULEENTRY32
        {
            public int dwSize;
            public int th32ModuleID;
            public int th32ProcessID;
            public int GlblcntUsage;
            public int ProccntUsage;
            public int modBaseAddr;
            public int modBaseSize;
            public int hModule;
            public char[] szModule;
            public char[] szExePath;
        }
        [DllImport("kernel32.dll")]
        public static extern bool Thread32First(IntPtr hSnapshot, out MODULEENTRY32 lpte);
        [DllImport("kernel32.dll")]
        public static extern bool Thread32Next(IntPtr hSnapshot, out MODULEENTRY32 lpte);
        [DllImport("kernel32.dll")]
        public static extern bool Module32First(IntPtr hSnapshot, out MODULEENTRY32 lpme);
        [DllImport("kernel32.dll")]
        public static extern bool Module32Next(IntPtr hSnapshot, out MODULEENTRY32 lpme);
        [DllImport("kernel32.dll")]
        public static extern bool Process32First(IntPtr hSnapshot, out   PROCESSENTRY32 lppe);
        [DllImport("kernel32.dll")]
        public static extern bool Process32Next(IntPtr hSnapshot, out   PROCESSENTRY32 lppe);
        [DllImport("Psapi.dll")]
        public static extern bool EnumProcesses(int[] pProcessIds, int cb, out int pBytesReturned);
        /// <summary>
        /// 执行MCI设备命令
        /// </summary>
        /// <param name="pszCommand">命令</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern bool mciExecute(string pszCommand);
        /// <summary>
        /// 切换用户
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int LockWorkStation();
        [DllImport("msimg32.dll")]
        public static extern bool TransparentBlt(
            IntPtr hdcDest,        // handle to destination DC
             int nXOriginDest,   // x-coord of destination upper-left corner
             int nYOriginDest,   // y-coord of destination upper-left corner
             int nWidthDest,     // width of destination rectangle
             int hHeightDest,    // height of destination rectangle
             IntPtr hdcSrc,         // handle to source DC
             int nXOriginSrc,    // x-coord of source upper-left corner
             int nYOriginSrc,    // y-coord of source upper-left corner
             int nWidthSrc,      // width of source rectangle
             int nHeightSrc,     // height of source rectangle
             int crTransparent);  // color to make transparent);


        // SetupPromptReboot功能要求用户如果他想重新启动系统	 
        /*
        如果TRUE，用户从不询问重启以及系统关机不启动。在这种情况下，FileQueue必须被指定。如果FALSE，用户被问重启，如前所述。
        使用ScanOnly，以确定是否关机需要独立于实际发起关机。
		
        SPFILEQ_FILE_IN_USE
    的至少一个文件在队列中使用提交过程和有延迟文件操作挂起。如果这个标志才会被置FileQueue指定。
        SPFILEQ_REBOOT_RECOMMENDED
    系统应重新启动。根据其他标志和用户响应关机查询，关机可能正在进行中。
        SPFILEQ_REBOOT_IN_PROGRESS
    系统关机正在进行中。
	
        */
        /// <summary>
        /// 安装重启提示_
        /// </summary>
        /// <param name="FileQueue">可为0</param>
        /// <param name="Owner">句柄</param>
        /// <param name="ScanOnly">仅扫描</param>
        /// <returns></returns>
        [DllImport("setupapi.dll")]
        public static extern int SetupPromptReboot(int FileQueue, IntPtr Owner, bool ScanOnly);

        /// <summary>
        /// 执行系统文件对象的操作（如:将文件删除置回收站）
        /// </summary>
        /// <param name="lpFileOp">SHFILEOPSTRUCT 结构体</param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern int SHFileOperation(ref SHFILEOPSTRUCT lpFileOp);
        /// <summary>
        /// SHFILEOPSTRUCT 结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public int wFunc;
            /// <summary>
            /// 源
            /// </summary>
            public string pFrom;
            /// <summary>
            /// 目标
            /// </summary>
            public string pTo;
            /// <summary>
            /// 标志：FOF_ALLOWUNDO
            /// </summary>
            public int fFlags;
            public bool fAnyOperationsAborted;
            public int hNameMappings;
            public string lpszProgressTitle;
        }
        /// <summary>
        /// 载入指定的动态链接库，并将它映射到当前进程使用的地址空间。一旦载入，即可访问库内保存的资源
        /// 一旦不需要，用FreeLibrary函数释放DLL
        /// 成功则返回库模块的句柄，零表示失败
        /// </summary>
        /// <param name="lpFileName">指定要载入的动态链接库的名称。
        /// 采用与CreateProcess函数的lpCommandLine参数指定的同样的搜索顺序</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);
        /// <summary>
        /// 释放指定的动态链接库，它们早先是用LoadLibrary
        /// 非零表示成功，零表示失败
        /// </summary>
        /// <param name="hLibModule">要释放的一个库句柄</param>
        /// <returns></returns >
        [DllImport("kernel32.dll")]
        public static extern int FreeLibrary(IntPtr hLibModule);
        /// <summary>
        /// 返回函数地址
        /// </summary>
        /// <param name="hModule"></param>
        /// <param name="lpProcName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        /// <summary>
        /// 对指定窗口捕获鼠标，然后其他窗口输入无作用
        /// </summary>
        /// <param name="hwnd">指定的句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hwnd);
        /// <summary>
        /// 为当前的应用程序释放鼠标捕获
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// 在指定的设备场景中取得一个像素的RGB值
        /// 指定点的RGB颜色。如指定的点位于设备场景的剪切区之外，则返回CLR_INVALID
        /// </summary>
        /// <param name="hdc">一个设备场景的句柄</param>
        /// <param name="x">逻辑坐标中要检查的点</param>
        /// <param name="y">逻辑坐标中要检查的点</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern int GetPixel(IntPtr hdc, int x, int y);

        /// <summary>
        /// 获取指定窗口的设备场景
        /// 指定窗口的设备场景句柄，出错则为0
        /// </summary>
        /// <param name="hwnd">将获取其设备场景的窗口的句柄(如PictureBox.Handle)。若为0，则要获取整个屏幕的DC</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(Int32 hwnd);

        /// <summary>
        /// （画图标）在指定的位置画一个图标
        /// </summary>
        /// <param name="hdc">设备场景句柄（GetDC()）</param>
        /// <param name="x">想描绘图标的位置（逻辑坐标）</param>
        /// <param name="y">想描绘图标的位置（逻辑坐标）</param>
        /// <param name="hIcon">欲描绘图标的句柄可用    ExtractIcon  函数获取</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);
        /// <summary>
        /// 清除图标
        /// </summary>
        /// <param name="hIcon">图标句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);
        /// <summary>
        /// 锁定键盘和鼠标
        /// </summary>
        /// <param name="fBlockIt">1：锁定   0：解除</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int BlockInput(int fBlockIt);

        /// <summary>
        /// 判断窗口是否可见
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);
        /// <summary>
        /// 判断一个矩形是否为空
        /// </summary>
        /// <param name="lpRect">要检查的矩形</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int IsRectEmpty(ref Rectangle lpRect);
        /// <summary>
        /// 判断一个矩形是否为空
        /// </summary>
        /// <param name="lpRect">要检查的矩形</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int IsRectEmpty(ref RECT lpRect);

        /// <summary>
        /// 获得整个窗口的范围矩形，窗口的边框、标题栏、滚动条及菜单等都在这个矩形内
        /// </summary>
        /// <param name="hwnd">想获得范围矩形的那个窗口的句柄</param>
        /// <param name="lpRect">Rectangle，屏幕坐标中随同窗口装载的矩形</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref Rectangle lpRect);
        /// <summary>
        /// 获得整个窗口的范围矩形，窗口的边框、标题栏、滚动条及菜单等都在这个矩形内
        /// </summary>
        /// <param name="hwnd">想获得范围矩形的那个窗口的句柄</param>
        /// <param name="lpRect">RECT，屏幕坐标中随同窗口装载的矩形</param>
        /// <returns></returns>

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        /// <summary>
        /// FLASHWINFO 结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public int cbSize;
            public IntPtr hwnd;
            public int dwFlags;
            public int uCount;
            public int dwTimeout;
        }


        //  [This function is not intended for general use. It may be altered or unavailable in subsequent versions of Windows.]
        //	Creates an array of handles to icons that are extracted from a specified file.
        //public  static extern int PrivateExtractIcons(
        //        string lpszFile,
        //        int nIconIndex,
        //        int cxIcon,
        //        int cyIcon,
        //         IntPtr* phicon,
        //        IntPtr* piconid,
        //        int nIcons,
        //        int flags
        //    );

        /// <summary>
        /// 闪烁窗口拓展__
        /// </summary>
        /// <param name="pfwi">FLASHWINFO</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool FlashWindowEx(ref FLASHWINFO pfwi);

        /// <summary>
        /// 延时
        /// </summary>
        /// <param name="dwMilliseconds">毫秒</param>
        [DllImport("kernel32.dll")]
        public static extern void Sleep(int dwMilliseconds);

        /// <summary>
        /// 更新回收站图标_
        /// </summary>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern int SHUpdateRecycleBinIcon();
        /// <summary>
        /// 判断一个可执行文件或DLL中是否有图标存在，并将其提取出来
        /// 如成功，返回指向图标的句柄；如文件中不存在图标，则返回零
        /// </summary>
        /// <param name="hInst">当前应用程序的实例句柄</param>
        /// <param name="lpszExeFileName">文件名（全名）</param>
        /// <param name="nIconIndex">欲获取的图标的索引。如果为-1，表示取得文件中的图标总数</param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern IntPtr ExtractIcon(IntPtr hInst,
    string lpszExeFileName,
    int nIconIndex);
        /// <summary>
        /// 取鼠标所在窗口句柄
        /// </summary>
        /// <param name="Point">坐标</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point Point);
        /// <summary>
        /// 取鼠标所在窗口句柄
        /// </summary>
        /// <param name="Point">坐标</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);
        /// <summary>
        /// 取鼠标所在窗口句柄
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(int x, int y);
        /// <summary>
        /// 更新窗口
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int UpdateWindow(IntPtr hwnd);

        /// <summary>
        /// 滚动窗口客户区的全部或一部分
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="XAmount">水平滚动的距离。正值向右滚动，负值向左滚动</param>
        /// <param name="YAmount">垂直滚动的距离。正值向下滚动，负值向上滚动</param>
        /// <param name="lpRect">RECT，用客户区坐标表示的一个矩形，它定义了客户区要滚动的一个部分。
        /// 如设为NULL，则滚动整个客户区。在NULL的情况下，子窗口和控件的位置也会随同任何无效区域移动。否则，子窗口和无效区域不会一起移动。因此，在滚动之前，如指定了lpRect，一个明智的做法是先调用UpdateWindow函数</param>
        /// <param name="lpClipRect">RECT，指定剪切区域。只有这个矩形的区域才可能滚动。该矩形优先于lpRect。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int ScrollWindow(
            IntPtr hwnd,
            int XAmount,
            int YAmount,
          ref  Rectangle lpRect,
            ref Rectangle lpClipRect);

        /// <summary>
        /// 锁定窗口
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int LockWindowUpdate(IntPtr hwnd);

        /// <summary>
        /// 判断窗口句柄是否有效
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int IsWindow(IntPtr hwnd);

        /// <summary>
        /// MSG  消息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public int message;
            public int wParam;
            public int lParam;
            public int time;
            public POINT pt;
        }

        /// <summary>
        /// 修改矩形
        /// </summary>
        /// <param name="lpRect">修改的矩形 RECT</param>
        /// <param name="x">修改的宽度</param>
        /// <param name="y">修改的高度</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int InflateRect(ref RECT lpRect, int x, int y);
        /// <summary>
        /// 修改矩形
        /// </summary>
        /// <param name="lpRect">修改的矩形 </param>
        /// <param name="x">修改的宽度</param>
        /// <param name="y">修改的高度</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int InflateRect(ref Rectangle lpRect, int x, int y);

        /// <summary>
        /// 取剪辑板窗口句柄_
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetOpenClipboardWindow();
        /// <summary>
        /// 打开剪辑板  调用函数：GetOpenClipboardWindow取得剪辑板   句柄
        /// </summary>
        /// <param name="hWndNewOwner">剪辑板句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int OpenClipboard(IntPtr hWndNewOwner);
        /// <summary>
        /// 清除剪辑板
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int EmptyClipboard();
        /// <summary>
        /// 关闭剪辑板
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int CloseClipboard();
        /// <summary>
        /// （取实例句柄_）获取一个应用程序或动态链接库的模块句柄
        /// </summary>
        /// <param name="lpModuleName">指定模块名，这通常是与模块的文件名相同的一个名字.dll   or  .exe</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        /// <summary>
        /// （取实例句柄_）获取一个应用程序或动态链接库的模块句柄
        /// </summary>
        /// <param name="lpModuleName">  可为：0</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(int lpModuleName);

        /// <summary>
        /// （取实例句柄_）获取一个应用程序或动态链接库的模块句柄_拓展
        /// </summary>
        /// <param name="dwFlags">标志 可为 ：0</param>
        /// <param name="lpModuleName">指定模块名，这通常是与模块的文件名相同的一个名字</param>
        /// <param name="phModule">用于返回装载句柄的变量</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool GetModuleHandleEx(int dwFlags, string lpModuleName, out IntPtr phModule);

        /// <summary>
        /// 获取指定模块的路径
        /// 如执行成功，返回复制到lpFileName的实际字符数量；零表示失败
        /// </summary>
        /// <param name="hModule">模块句柄：GetModuleHandleA（）</param>
        /// <param name="lpFilename">用于存储字符串的</param>
        /// <param name="nSize">长度</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetModuleFileName(IntPtr hModule,  StringBuilder lpFilename, int nSize);

        /// <summary>
        /// 获取当前拥有焦点的窗口的窗口句柄！
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        /// <summary>
        /// 获取鼠标当前坐标
        /// </summary>
        /// <param name="lpPoint">POINT 结构体</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT lpPoint);
        /// <summary>
        /// 获取鼠标当前坐标
        /// </summary>
        /// <param name="lpPoint">POINT 结构体</param>
        /// <returns></returns>

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        /// <summary>
        /// 取得指针剪裁区
        /// 取得一个矩形，用于描述目前为鼠标指针规定的剪切区域；该区域是由SetClipCursor函数定义的
        /// </summary>
        /// <param name="lpRect">RECT 结构体</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetClipCursor(ref RECT lpRect);
        /// <summary>
        /// 矩形结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        /// <summary>
        /// 是否控制窗口
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="bEnable">true :（允许）不禁止  false :禁止</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int EnableWindow(IntPtr hwnd, bool bEnable);

        /// <summary>
        /// 最小化窗口！！！
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int CloseWindow(IntPtr hwnd);

        /// <summary>
        /// 取子窗口句柄_
        /// 发现包含了指定点的第一个子窗口的句柄。
        /// 如未发现任何窗口，则返回hWnd（父窗口的句柄）。
        /// 如指定点位于父窗口外部，则返回零
        /// </summary>
        /// <param name="hWndParent">父窗口句柄</param>
        /// <param name="Point">坐标</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int ChildWindowFromPoint(IntPtr hWndParent, ref POINT Point);

        /// <summary>
        /// 取进程版本_
        /// </summary>
        /// <param name="ProcessId">进程标识符（进程ID）</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetProcessVersion(int ProcessId);
        /// <summary>
        ///取进程堆栈句柄_   --- 获取调用过程堆句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetProcessHeap();
        /// <summary>
        /// 获取当前进程一个唯一的标识符
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentProcessId();
        /// <summary>
        /// 获取当前进程的一个伪句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentProcess();

        /// <summary>
        /// 坐标结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
        /// <summary>
        /// 为窗口指定一个新位置和状态(非零表示成功，零表示失败)
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="hWndInsertAfter">*****可为0</param>
        /// <param name="X">新的X坐标</param>
        /// <param name="Y">新的Y坐标</param>
        /// <param name="cx">新窗口宽度</param>
        /// <param name="cy">新窗口高度</param>
        /// <param name="uFlags">标志  如：SWP_SHOWWINDOW</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        /// <summary>
        /// 破坏（即清除）指定的窗口以及它的所有子窗口（非零表示成功，零表示失败）
        /// </summary>
        /// <param name="hWnd">欲清除的窗口句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int DestroyWindow(IntPtr hWnd);

        static public byte GetRValue(uint color)
        {
            return (byte)color;
        }
        static public byte GetGValue(uint color)
        {
            return ((byte)(((short)(color)) >> 8));
        }
        static public byte GetBValue(uint color)
        {
            return ((byte)((color) >> 16));
        }
        static public byte GetAValue(uint color)
        {
            return ((byte)((color) >> 24));
        }
        /// <summary>
        /// 获取RGB值
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns></returns>
        public static uint RGB(Color color)
        {
            // 返回由RGB构成的32位uint
            uint R = color.R;
            uint G = color.G;
            uint B = color.B;
            G <<= 8;
            B <<= 16;
            return ((uint)(R | G | B));
        }
        /// <summary>
        /// 获取颜色
        /// </summary>
        /// <param name="handle">句柄，0  表示全局</param>
        /// <param name="p">鼠标所在坐标</param>
        /// <returns></returns>
        public static Color GetColor(int handle, Point p)
        {
            // 得到指定点的颜色RGB
            uint colorref = (uint)GetPixel(GetDC(handle), p.X, p.Y);  // picGraphics是我用picturePox类中的CreateGraphics方法创建的Graphics对象
            byte Red = GetRValue(colorref);
            byte Green = GetGValue(colorref);
            byte Blue = GetBValue(colorref);
            return Color.FromArgb(Red, Green, Blue);
        }
        /// <summary>
        /// 设置颜色
        /// </summary>
        /// <param name="handle">窗口句柄可为  0</param>
        /// <param name="p"></param>
        /// <param name="fillColor"></param>
        public static void SetColor(int handle, Point p, Color fillColor)
        {
            SetPixel(GetDC(handle), p.X, p.Y, (int)RGB(fillColor));
        }

        [DllImport("gdi32.dll")]
        public static extern int SetPixel(IntPtr hdc, int x1, int y1, int color);

        /// <summary>
        /// 访问远程系统的部分注册表
        /// 零（ERROR_SUCCESS）表示成功。其他任何值都代表一个错误代码
        /// </summary>
        /// <param name="lpMachineName">欲连接的系统。采用“\\计算机名”的形式</param>
        /// <param name="hKey">HKEY_LOCAL_MACHINE 或 HKEY_USERS</param>
        /// <param name="phkResult">用于装载指定项句柄的一个变量</param>
        /// <returns></returns>
        [DllImport("advapi32.dll")]
        //public static extern int RegConnectRegistry(string lpMachineName, Microsoft.Win32.RegistryKey hKey, int phkResult);

        /// <summary>
        /// 取得压缩文件全名---1表示成功，LZERROR_BADVALUE 表示失败
        /// 取得一个压缩文件的全名。文件必须是用 COMPRESS.EXE 程序压缩的，而且在压缩时适用/r选项
        /// </summary>
        /// <param name="lpszSource">压缩文件的名字</param>
        /// <param name="lpszBuffer">指定一个缓冲区，用于装载文件全名</param>
        /// <returns></returns>
        //[DllImport("lz32.dll")]
        //public static extern int GetExpandedName(string lpszSource, StringBuilder lpszBuffer);

        /// <summary>
        /// 将一条系统消息广播给系统中所有的顶级窗口（消息广播）
        /// </summary>
        /// <param name="dwFlags">下述常数的一个或多个BSF_FLUSHDISK：每次处理完一条消息后，都对磁盘进行刷新（将未存盘的数据存下来BSF_FORCEIFHUNG：如目标处于挂起状态，则在设定的超时后到期返回BSF_IGNORECURRENTTASK：发送任务不接收消息BSF_LPARAMBUFFER：lParam指向一个内存缓冲区BSF_NOHANG：跳过被挂起的所有进程BSF_POSTMESSAGE：投递消息。不与BSF_LPARAMBUFFER和BSF_QUERY兼容BSF_QUERY：将消息顺序发给进程，只有前一个返回TRUE时，才进入下一个进程</param>
        /// <param name="lpdwRecipients">下述常数的一个或多个BSF_ALLCOMPONENTS：消息进入能够接收消息的每一个系统组件BSF_APPLICATIONS：消息到达应用程序BSF_INSTALLABLEDRIVERS：消息到达可安装的驱动程序BSF_NETDRIVERS：消息到达网络驱动程序BSF_VXDS：消息到达系统设备驱动程序</param>
        /// <param name="uiMessage"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam">由消息决定。如指定了BSF_LPARAMBUFFER，这就是位于调用进程地址空间的一个内存缓冲区的地址，而且缓冲区的第一个16位字包含了缓冲区的长度</param>
        /// <param name="pBSMInfo"></param>
        /// <returns></returns>
        //[DllImport("user32.dll")]
        public static extern long BroadcastSystemMessageEx(
            int dwFlags,
    int lpdwRecipients,
    int uiMessage,
    int wParam,
    int lParam,
  ref  BSMINFO pBSMInfo);

        /// <summary>
        /// 将一条系统消息广播给系统中所有的顶级窗口
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="lpdwRecipients"></param>
        /// <param name="uiMessage"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern long BroadcastSystemMessage(
            int dwFlags,
    int lpdwRecipients,
    int uiMessage,
    int wParam,
    int lParam);

        /// <summary>
        /// BSMINFO 结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct BSMINFO
        {
            public int cbSize;
            public int hdesk;
            public int hwnd;
            public int luid;
        }
        /// <summary>
        /// 向窗口发送一条消息。
        /// 如目标窗口位于同调用方相同的线程内，则这个函数会表现为SendMessage函数。
        /// 而且除非消息得到处理，否则函数不会返回。
        /// 如目标窗口从属于一个不同的线程，则函数会立即返回
        /// </summary>
        /// <param name="hWnd">欲发送信息的窗口句柄</param>
        /// <param name="Msg">信息标识符</param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SendNotifyMessage(
    IntPtr hWnd,
    int Msg,
    int wParam,
    int lParam);

        /// <summary>
        /// 取屏幕桌面句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// 将指定的窗口带至窗口列表顶部。
        /// 倘若它部分或全部隐藏于其他窗口下面，则将隐藏的部分完全显示出来。
        /// 该函数也对弹出式窗口、顶级窗口以及MDI子窗口产生作用
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        /// <summary>
        /// 获取目前选择的鼠标指针的句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetCursor();

        /// <summary>
        /// 删除文件(非零表示成功，零表示失败)
        /// </summary>
        /// <param name="lpFileName">文件名(路径)</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool DeleteFile(string lpFileName);

        /// <summary>
        /// 这个函数能获取Windows目录的完整路径名。在这个目录里，保存了大多数windows应用程序文件及初始化文件
        /// </summary>
        /// <param name="lpBuffer">指定一个字串缓冲区，用于装载Windows目录名。除非是根目录，否则目录中不会有一个中止用的“\”字符</param>
        /// <param name="uSize">lpBuffer字串的最大长度</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetWindowsDirectory(StringBuilder lpBuffer, int uSize);

        /// <summary>
        /// (读取鼠标指针图标)：执行成功则返回指向指针的一个句柄，零表示失败
        /// If the function succeeds, the return value is the handle to the newly loaded cursor
        /// </summary>
        /// <param name="lpFileName">鼠标文件</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        /// <summary>
        /// 设置系统鼠标指针
        /// </summary>
        /// <param name="hcur"></param>
        /// <param name="i">：：：32512</param>
        [DllImport("user32.dll")]
        public static extern void SetSystemCursor(IntPtr hcur, int i);

        /// <summary>
        /// 画窗口区域：三角形，长方形，圆形......
        /// </summary>
        /// <param name="hWnd">要设置窗口区域的句柄</param>
        /// <param name="hRgn">Region的返回句柄，如：rgn.GetHrgn(g)</param>
        /// <param name="bRedraw">Bool，若为TRUE，则立即重画窗口</param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        public static extern int SetWindowRgn(
            IntPtr hWnd,     // handle to window
            IntPtr hRgn,     // handle to region
            bool bRedraw);   // window redraw option

        /// <summary>
        /// 是否交换鼠标左键与右键的作用
        /// </summary>
        /// <param name="fSwap">true:交换         false:取消交换：重置</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SwapMouseButton(bool fSwap);

        /// <summary>
        /// 显示信息框
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="lpText">内容文本</param>
        /// <param name="lpCaption">标题</param>
        /// <param name="uType">类型如：MB_OK</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int MessageBox(
     IntPtr hWnd,
    string lpText,
    string lpCaption,
    int uType);
        /// <summary>
        /// 显示系统关于窗口
        /// </summary>
        /// <param name="hWnd">窗口的句柄</param>
        /// <param name="szApp">要显示标题</param>
        /// <param name="szOtherStuff">要显示内容</param>
        /// <param name="hIcon">要显示图标的句柄</param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern int ShellAbout(IntPtr hWnd,
    string szApp,
    string szOtherStuff,
   IntPtr hIcon);

        /// <summary>
        /// 获取一个已中断进程的退出代码   非零表示成功，零表示失败
        /// </summary>
        /// <param name="hProcess">想获取退出代码的一个进程的句柄</param>
        /// <param name="uExitCode">用于装载进程退出代码的一个长整数变量
        /// 如进程尚未中止，则设为常数STILL_ACTIVE</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetExitCodeProcess(IntPtr hProcess, ref int uExitCode);

        /// <summary>
        /// 结束一个进程
        /// </summary>
        /// <param name="hProcess">进程句柄</param>
        /// <param name="uExitCode">进程的退出代码</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int TerminateProcess(IntPtr hProcess, int uExitCode);

        /// <summary>
        /// 锁定本地内存对象并返回指针
        /// </summary>
        /// <param name="hMem">句柄</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int LocalLock(IntPtr hMem);

        /// <summary>
        /// 打开一个已存在的进程对象，并返回进程句柄。
        /// </summary>
        /// <param name="dwDesiredAccess">0x1F0FFF /2035711 ：最高权限</param>
        /// <param name="bInheritHandle">通常为为false</param>
        /// <param name="dwProcessId">进程标识符PID</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
            int dwDesiredAccess,
            bool bInheritHandle,
            int dwProcessId);
        /// <summary>
        /// 写内存数据
        /// </summary>
        /// <param name="hProcess">进程的句柄</param>
        /// <param name="lpBaseAddress">（基址）写入进程的位置</param>
        /// <param name="lpBuffer">数据当前存放位置如new byte[] {  }</param>
        /// <param name="nSize">实际数据的长度(nSize以字节为单位,一个字节Byte等于8位基本数据类型的长度)   ：可为 0</param>
        /// <param name="lpNumberOfBytesWritten">可为0</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int nSize,
            int lpNumberOfBytesWritten);
        /// <summary>
        /// 关闭句柄
        /// </summary>
        /// <param name="hObject"></param>
        [DllImport("kernel32.dll")]
        public static extern void CloseHandle(IntPtr hObject);
        /// <summary>
        /// 读内存数据
        /// </summary>
        /// <param name="hProcess">远程进程句柄。 被读取者</param>
        /// <param name="lpBaseAddress">（基址）远程进程中内存地址, 从具体何处读取</param>
        /// <param name="lpBuffer">本地进程中内存地址. 函数将读取的内容写入此处</param>
        /// <param name="nSize">要传送的字节数。要写入多少</param>
        /// <param name="lpNumberOfBytesRead">实际传送的字节数. 函数返回时报告实际写入多少    ：可为 0</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
             byte[] lpBuffer,
            int nSize,
            int lpNumberOfBytesRead);
        /// <summary>
        /// 查询内存块信息
        /// </summary>
        /// <param name="hProcess">进程句柄</param>
        /// <param name="lpAddress">基址</param>
        /// <param name="lpBuffer">MEMORY_BASIC_INFORMATION 结构体</param>
        /// <param name="dwLength"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int VirtualQueryEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            out MEMORY_BASIC_INFORMATION lpBuffer,
            int dwLength);
        /// <summary>
        /// 内存块信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public int State;
            public int Protect;
            public int lType;
        }
        /// <summary>
        /// 获取窗口句柄
        /// </summary>
        /// <param name="hwndParent"></param>
        /// <param name="hwndChildAfter"></param>
        /// <param name="lpszClass">类名</param>
        /// <param name="lpszWindow">标题</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int FindWindowEx(IntPtr hwndParent,
    int hwndChildAfter,
    string lpszClass,
    string lpszWindow);
        /// <summary>
        /// 该函数向系统表明有个线程有终止请求。通常用来响应WM_DESTROY消息。
        /// PostQuitMessage寄送一个WM_QUIT消息给线程的消息队列并立即返回;此函数向系统表明有个线程请求在随后的某一时间终止。
        /// </summary>
        /// <param name="nExitCode"></param>
        [DllImport("user32.dll", EntryPoint = "PostQuitMessage")]
        public static extern void PostQuitMessage(int nExitCode);

        /// <summary>
        /// 如将消息传送给位于不同进程的一个窗口，通常第一个进程会暂时挂起，直到另一个进程中的窗口函数完成操作为止。在目标进程的窗口函数完成之前，另一个进程可用这个函数向第一个进程返回一个结果，使之能继续进行
        /// 如准备答复的消息是由另一个进程发来的，则返回TRUE。如果它是从同一个进程中发出来的，则返回FALSE（此时，该函数没有任何效果）
        /// </summary>
        /// <param name="lResult">指定发回调用进程的一个结果</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ReplyMessage(int lResult);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="HWnd">其窗口程序将接收消息的窗口的句柄</param>
        /// <param name="Msg">指定被发送的消息</param>
        /// <param name="WParam">指定附加的消息指定信息</param>
        /// <param name="LParam">指定附加的消息指定信息</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr HWnd, int Msg, int WParam, int LParam);
        /// <summary>
        /// 取按键状态(判断正在按下某键)
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int vKey);
        /// <summary>
        /// 取当前进程目录(不包含文件名)
        /// </summary>
        /// <param name="nBufferLength">长度</param>
        /// <param name="lpBuffer">欲存储的缓冲区</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentDirectory(int nBufferLength, StringBuilder lpBuffer);

        /// <summary>
        /// 取按键状态(判断刚按过了某键)
        /// </summary>
        /// <param name="nVirtKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetKeyState(int nVirtKey);
        /// <summary>
        /// 设置透明度
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="crKey"></param>
        /// <param name="bAlpha"></param>
        /// <param name="dwFlags">标志</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(
         IntPtr hwnd,
         int crKey,
          int bAlpha,
          int dwFlags);
        /// <summary>
        /// 获取窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd,
    int nIndex);
        /// <summary>
        /// 设置窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd,
    int nIndex,
    int dwNewLong);

        /// <summary>
        /// 设置窗口透明度
        /// </summary>
        /// <param name="Handle">窗口句柄</param>
        /// <param name="Value">值（0-255）/param>
        public static void SetOpacity(IntPtr handle, int Value)
        {
            SetWindowLong(handle, Win_WndConst.GWL_EXSTYLE, GetWindowLong(handle, Win_WndConst.GWL_EXSTYLE) | Win_WndConst.WS_EX_LAYERED);

            SetLayeredWindowAttributes(handle, 0, Value, Win_WndConst.LWA_ALPHA);

        }


        /// <summary>
        /// 创建快捷方式
        /// </summary>
        /// <param name="Path">路径 System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);//得到桌面文件夹</param>
        /// <param name="Name">名字</param>
        /// <param name="TargetPath">目标路径</param>
        /// <param name="WorkDirectory">程序所在文件夹</param>
        /// <param name="IconPath">图标路径</param>
        /// <param name="OtherValue">备注</param>
        /// <param name="HotKeyShow">快捷键</param>
        /// <param name="ShowStyle">运行方式</param>
        public static void CreateShortCut(string Path, string Name, string TargetPath, string WorkDirectory, string IconPath, string OtherValue,
                    string HotKeyShow, int ShowStyle)
        {

            // string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);//得到桌面文件夹
            //IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShellClass();
            //IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(Path + "\\" + Name);
            //shortcut.TargetPath = TargetPath;

            ////    MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
            //shortcut.Arguments = "";// 参数
            //shortcut.WorkingDirectory = WorkDirectory;//程序所在文件夹，在快捷方式图标点击右键可以看到此属性
            //shortcut.IconLocation = IconPath;//图标
            //shortcut.Hotkey = HotKeyShow;//热键
            //shortcut.WindowStyle = ShowStyle;
            //shortcut.Description = OtherValue;
            //shortcut.Save();

        }
        /// <summary>
        /// 写配置项
        /// </summary>
        /// <param name="lpAppName">节名称  " [ ] "</param>
        /// <param name="lpKeyName">键名,也就是里面具体的变量名</param>
        /// <param name="lpString">键值=数据</param>
        /// <param name="lpFileName">配置文件全路径</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool WritePrivateProfileString(
              string lpAppName,// INI文件中的一个字段名[节名]可以有很多个节名
              string lpKeyName,// lpAppName 下的一个键名，也就是里面具体的变量名
              string lpString, // 键值,也就是数据
              string lpFileName);// INI文件的路径
        /// <summary>
        /// 读配置项
        /// </summary>
        /// <param name="lpAppName">节名称  " [ ] "</param>
        /// <param name="lpKeyName">键名,也就是里面具体的变量名</param>
        /// <param name="lpDefault">如果lpReturnedString为空,则把个变量赋给lpReturnedString  </param>
        /// <param name="lpReturnedString">存放键值的指针变量,用于接收INI文件中键值(数据)的接收缓冲区  </param>
        /// <param name="nSize"> lpReturnedString的缓冲区大小</param>
        /// <param name="lpFileName"> INI文件的路径 </param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetPrivateProfileString(
            string lpAppName,// INI文件中的一个字段名[节名]可以有很多个节名
            string lpKeyName,// lpAppName 下的一个键名，也就是里面具体的变量名
            string lpDefault, // 如果lpReturnedString为空,则把个变量赋给lpReturnedString
            StringBuilder lpReturnedString,// 存放键值的指针变量,用于接收INI文件中键值(数据)的接收缓冲区
            int nSize,// lpReturnedString的缓冲区大小
            string lpFileName); // INI文件的路径

        /// <summary>
        /// 获取指定文件的短路径名
        /// </summary>
        /// <param name="path">指定欲获取短路径名的那个文件的名字。可以是个完整路径，或者由当前目录决定</param>
        /// <param name="shortPath">指定一个缓冲区，用于装载文件的短路径和文件名</param>
        /// <param name="shortPathLength">lpszShortPath缓冲区长度</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="lpstrCommand">要发送的命令字符串</param>
        /// <param name="lpstrReturnString">返回信息的缓冲区,为一指定了大小的字符串变量.</param>
        /// <param name="uReturnLength">缓冲区的大小,就是字符变量的长度</param>
        /// <param name="hWndCallback">回调方式，一般设为零</param>
        /// <returns></returns>
        [DllImport("Winmm.dll", CharSet = CharSet.Auto)]
        public static extern int mciSendString(string lpstrCommand,
        string lpstrReturnString, int uReturnLength, int hWndCallback);

        /// <summary>
        /// 检测网络是否连接
        /// </summary>
        /// <param name="lpdwFlags"></param>
        /// <param name="dwReserved">一般为0</param>
        /// <returns></returns>
        [DllImport("Wininet.dll")]
        public static extern bool InternetGetConnectedState(
           out  int lpdwFlags,
            int dwReserved);//0

        /// <summary>
        /// 窗口闪烁
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bInvert">一般为:true</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);
        /// <summary>
        /// 获取窗口类名
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpClassName"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetClassName(
             int hWnd,
    StringBuilder lpClassName,
    int nMaxCount);//获取类名

        /// <summary>
        /// 获取窗口标题
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="lpString">存储的字符串</param>
        /// <param name="nMaxCount">长度</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowText(
            int hWnd,
     StringBuilder lpString,
    int nMaxCount);//获取标题

        /// <summary>
        /// 播放系统声音
        /// </summary>
        /// <param name="uType"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(long uType);//播放系统声音

        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="hWnd">指定窗口句柄</param>
        /// <param name="X">CWnd的左边的新位置。</param>
        /// <param name="Y">CWnd的顶边的新位置。</param>
        /// <param name="nWidth">指定了CWnd的新宽度。</param>
        /// <param name="nHeight">指定了CWnd的新高度。</param>
        /// <param name="bRepaint">是否要重画CWnd</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool MoveWindow(
    int hWnd,//
    int X,//
    int Y,//
    int nWidth,//
    int nHeight,//
    bool bRepaint);

        /// <summary>
        /// 系统信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public uint dwOemId;
            /// <summary>
            ///分页大小
            /// </summary>
            public uint dwPageSize;
            /// <summary>
            /// 最小寻址空间
            /// </summary>
            public uint lpMinimumApplicationAddress;//
            /// <summary>
            /// 最大寻址空间
            /// </summary>
            public uint lpMaximumApplicationAddress;//
            /// <summary>
            /// 处理器掩码; 0..31 表示不同的处理器
            /// </summary>
            public uint dwActiveProcessorMask;//
            /// <summary>
            /// 处理器数目
            /// </summary>
            public uint dwNumberOfProcessors;//
            /// <summary>
            /// 处理器类型
            /// </summary>
            public uint dwProcessorType;//
            /// <summary>
            /// 虚拟内存空间的粒度}
            /// </summary>
            public uint dwAllocationGranularity;//
            /// <summary>
            /// 处理器等级
            /// </summary>
            public uint dwProcessorLevel;//
            /// <summary>
            /// 处理器版本
            /// </summary>
            public uint dwProcessorRevision;
        }
        /// <summary>
        /// 获取计算机信息
        /// </summary>
        /// <param name="lpSystemInfo"></param>
        [DllImport("kernel32.dll")]
        public static extern void GetSystemInfo(ref SYSTEM_INFO lpSystemInfo);//获取计算机信息

        /// <summary>
        /// 系统时间结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public uint wYear;
            public uint wMonth;
            public uint wDayOfWeek;
            public uint wDay;
            public uint wHour;
            public uint wMinute;
            public uint wSecond;
            public uint wMilliseconds;
        }
        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <param name="lpSystemTime"></param>
        [DllImport("kernel32.dll")]
        public static extern void GetSystemTime(ref SYSTEMTIME lpSystemTime);//获取系统时间
        /// <summary>
        /// 获取回收站信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Pack = 2)]
        public struct SHQUERYRBINFO
        {
            //这个结构必须是用户显示编写偏移量才能准确获取数值
            [FieldOffset(0)]
            public int cbsize;
            [FieldOffset(4)]
            public long i64Size;
            [FieldOffset(12)]
            public long i64NumItems;
        };

        /// <summary>
        /// 获取回收站信息
        /// </summary>
        /// <param name="pszRootPath"></param>
        /// <param name="pSHQueryRBInfo"></param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern int SHQueryRecycleBin(
             string pszRootPath,
             ref SHQUERYRBINFO pSHQueryRBInfo);
        /// <summary>
        /// 清空回收站
        /// </summary>
        /// <param name="hwnd">窗口句柄 可为 0</param>
        /// <param name="pszRootPath"> 可为 0 ,</param>
        /// <param name="dwFlags">(0 )  0x01无信息    0x02 无进展  0x04无声音</param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern int SHEmptyRecycleBin(int hwnd, string pszRootPath, int dwFlags);//清空回收站
        /// <summary>
        /// 获取窗口句柄
        /// </summary>
        /// <param name="lpClassName">类名</param>
        /// <param name="lpWindowName">标题</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int FindWindow(
        string lpClassName, //
        string lpWindowName);
        /// <summary>
        /// 取前台窗口句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetForegroundWindow(); //取前台窗口句柄
        /// <summary>
        /// 设置参数中更新用户配置文件
        /// </summary>
        /// <param name="uiAction">指定要设置的参数，参考uAction常数表
        /// 设置视窗的大小：6   SystemParametersInfo(6, 放大缩小值, P, 0)
        /// 开关屏保程序：17     SystemParametersInfo(17, False, P, 1)
        /// 改变桌面图标水平和垂直间距：13 ,24  uParam为间距值(像素)
        /// 设置屏保等待时间：15   SystemParametersInfo(15, 秒数, P, 1)
        ///设置桌面背景墙纸：20    SystemParametersInfo(20, 0, 图片路径, 1) 
        ///开关鼠标轨迹 ：93 SystemParametersInfo(93, 数值, P, 1)  uParam为False则关闭
        ///开关Ctrl+Alt+Del窗口 ：97  SystemParametersInfo(97, False, A, 0)  </param>
        /// <param name="uiParam">参考uAction常数表。可为  0</param>
        /// <param name="pvParam">按引用调用的Integer、Long和数据结构。</param>
        /// <param name="fWinIni">定了在设置系统参数的时候，是否应更新用户设置参数(  1:不更新   0:更新)</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SystemParametersInfo(int uiAction, int uiParam, string pvParam, int fWinIni);
        /// <summary>
        /// 设置鼠标样式
        /// </summary>
        /// <param name="cursorHandle">鼠标句柄->LoadCursorFromFile</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr cursorHandle);
        /// <summary>
        /// 销毁一个鼠标
        /// </summary>
        /// <param name="cursorHandle">鼠标句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern uint DestroyCursor(IntPtr cursorHandle);
        /// <summary>
        /// 设置鼠标X,Y坐标
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);  //设置鼠标X,Y坐标
        /// <summary>
        /// 模拟鼠标点击
        /// </summary>
        /// <param name="dwFlags">标志位集，指定点击按钮和鼠标动作的多种情况, 如:MOUSEEVENTF_MOVE</param>
        /// <param name="dx">X</param>
        /// <param name="dy">Y</param>
        /// <param name="cButtons">如果dwFlags为MOUSEEVENTF_WHEEL，则dwData指定鼠标轮移动的数量。正值表明鼠标轮向前转动，即远离用户的方向;负值表明鼠标轮向后转动，即朝向用户</param>
        /// <param name="dwExtraInfo">指定与鼠标事件相关的附加32位值</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int mouse_event(int dwFlags, int dx,
            int dy, int cButtons, int dwExtraInfo);//模拟鼠标点击
        /// <summary>
        /// 显示窗口......
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="nCmdShow">显示方式</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(
            int hWnd,//窗口句柄
            int nCmdShow //方式
            );


        /// <summary>
        /// 打开exe应用程序
        /// </summary>
        /// <param name="lpCmdLine"></param>
        /// <param name="uCmdShow"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int WinExec(
        string lpCmdLine,
        int uCmdShow);  //打开exe应用程序
        /// <summary>
        /// 打开网址,文件,应用程序...
        /// </summary>
        /// <param name="hWnd">一般为null</param>
        /// <param name="Operation">”open、print、edit、explore、find“</param>
        /// <param name="FileName">文件或程序</param>
        /// <param name="Parameters">null</param>
        /// <param name="Directory">null</param>
        /// <param name="ShowCmd">显示方式</param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public static extern int ShellExecute(
            int hWnd, //
            string Operation,//
            string FileName,//
            string Parameters,//
            string Directory,//
            int ShowCmd);

        /// <summary>
        /// 模拟键盘
        /// </summary>
        /// <param name="bvk">键值</param>
        /// <param name="bScan">0</param>
        /// <param name="dwFlags"></param>
        /// <param name="dwExtralnfo">0</param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(
            int bvk,//
            int bScan,//
            int dwFlags,
            int dwExtralnfo);
        /// <summary>
        /// 关闭系统
        /// </summary>
        /// <param name="uFlags">关闭参数</param>
        /// <param name="dwReserved"> 系统保留，一般取0</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        //关闭系统
        public static extern bool ExitWindowsEx(
       int uFlags, //
       int dwReserved);

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);//获取系统信息

        /// <summary>
        /// 取进程PID,线程PID 注意:返回值为 线程PID
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="lpdwProcessId">用于装载进程ID的变量</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(
            int hWnd,
           ref int lpdwProcessId);

        /// <summary>
        /// CPU序列号
        /// </summary>
        /// <returns></returns>
        //public static string GetID_CPU()// CPU序列号
        //{
        //    string cpuInfo = "";//cpu序列号
        //    ManagementClass cimobject = new ManagementClass("Win32_Processor");
        //    ManagementObjectCollection moc = cimobject.GetInstances();
        //    foreach (ManagementObject mo in moc)
        //    {
        //        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
        //    }
        //    return cpuInfo;
        //}
        /// <summary>
        /// 获取网卡
        /// </summary>
        /// <returns></returns>
        //public static string GetID_NetCard()// 获取网卡
        //{
        //    string NCid = "";
        //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    ManagementObjectCollection moc = mc.GetInstances();
        //    foreach (ManagementObject mo in moc)
        //    {
        //        if ((bool)mo["IPEnabled"] == true)
        //            NCid = mo["MacAddress"].ToString();
        //        mo.Dispose();
        //    }
        //    return NCid;
        //}
        /// <summary>
        /// Int 转 Ip
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string IntToIp(int a)
        {
            string sb = "";
            int b = (a >> 0) & 0xff;
            sb = "." + b;
            b = (a >> 8) & 0xff;
            sb = "." + b + sb;
            b = (a >> 16) & 0xff;
            sb = "." + b + sb;
            b = (a >> 24) & 0xff;
            sb = b + sb;
            return sb;
        }
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetID_IPAddress()// 获取IP地址
        {
            IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] address = hostInfo.AddressList;
            if (address.Length == 0)
                return "";
            else
                return address[0].ToString();
        }
        /// <summary>
        /// 自定义获取窗口句柄
        /// </summary>
        /// <param name="Classname">类名（可为null）</param>
        /// <param name="Windowname">标题（可为null）</param>
        /// <returns></returns>
        public static int GetWindows(string Classname, string Windowname)//自定义获取窗口句柄
        {
            if (Classname == null && Windowname == null)
            {
                int ret = GetForegroundWindow();
                return ret;
            }
            else
            {
                int ret2 = FindWindow(Classname, Windowname);
                return ret2;
            }
        }
        /// <summary>
        /// 获取网络是否连接
        /// </summary>
        /// <returns></returns>
        public static bool GetInterIsConnect()
        {
            int i = 0;
            InternetGetConnectedState(out i, 0);
            return i == 0 ? false : true;
        }
        /// <summary>
        /// 是否隐藏桌面图标 "Progman"
        /// </summary>
        /// <param name="Isvalue"></param>
        public static void IsHideDesktopIco(bool Isvalue)
        {
            int hwnd = FindWindow("Progman", null);
            if (Isvalue == true)
            {
                ShowWindow(hwnd, Win_WndConst.SW_HIDE);
            }
            else
            {
                if (Isvalue == false)
                {
                    ShowWindow(hwnd, Win_WndConst.SW_SHOW);
                }
            }
        }
        /// <summary>
        /// 是否隐藏任务栏  "Shell_TrayWnd"
        /// </summary>
        /// <param name="Isvalue"></param>
        public static void IsHideTask(bool Isvalue)
        {
            int hwnd = FindWindow("Shell_TrayWnd", null);
            if (Isvalue == true)
            {
                ShowWindow(hwnd, Win_WndConst.SW_HIDE);
            }
            else
            {
                if (Isvalue == false)
                {
                    ShowWindow(hwnd, Win_WndConst.SW_SHOW);
                }
            }
        }
        /// <summary>
        /// 写配置项
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static bool WriteIni(string Section, string Key, string Value, string FileName)
        {
            bool t = WritePrivateProfileString(Section, Key, Value, FileName);
            return t;
        }
        public static int ReadIni(string Section, string Key, string FileName)
        {
            StringBuilder str = new StringBuilder(1000);
            int Count = GetPrivateProfileString(Section, Key, "", str, 1000, FileName);
            return Count;
        }
        /// <summary>
        /// 读配置项
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Size"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string ReadIni(string Section, string Key, int Size, string FileName)
        {
            StringBuilder str = new StringBuilder(Size);
            GetPrivateProfileString(Section, Key, "", str, Size, FileName);
            return str.ToString();
        }
        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="ProcessName">进程名</param>
        /// <returns></returns>
        public static bool KillProcess(string ProcessName)
        {
            System.Diagnostics.Process[] pro = System.Diagnostics.Process.GetProcesses();
            for (int i = 0; i < pro.Length; i++)
            {
                if (pro[i].ProcessName == ProcessName)
                {
                    try
                    {
                        pro[i].Kill();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #region
        //public static void SetFormSkin(string filepath)
        //{
        //    string dll_path = System.IO.Directory.GetCurrentDirectory() +"\\" + "SkinH_CS.dll";
        //    string dll_path2 = System.IO.Directory.GetCurrentDirectory() + "\\" + "SkinH_Net.dll";
        //    if (System.IO.File.Exists(dll_path) == true && System.IO.File.Exists(dll_path2) == true)
        //    {
        //        SkinSharp.SkinH_Net skin = new SkinSharp.SkinH_Net();
        //        skin.AttachEx(filepath, null);
        //    }
        //    else
        //    {
        //        Win_Window.MessageBox(new IntPtr(Win_Window.GetForegroundWindow()),
        //            "未能加载:\'SkinH_CS.dll\'或\'SkinH_Net.dll\',请检查文件是否同时存在于同一目录！",
        //            "错误", Win_WndConst.MB_ICONHAND);

        //        Environment.Exit(0);
        //        return;
        //    }
        //}
        #endregion
    }
    /// <summary>
    /// WinApi常量
    /// </summary>
    public class Win_WndConst//常量
    {
        /// <summary>
        /// 自左向右显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略
        /// </summary>
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        /// <summary>
        /// 自右向左显示窗口。当使用AW_CENTER 标志时该标志被忽略
        /// </summary>
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        /// <summary>
        /// 自顶向下显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略
        /// </summary>
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        /// <summary>
        /// 自下向上显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略
        /// </summary>
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        /// <summary>
        /// 若使用AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展
        /// </summary>
        public const Int32 AW_CENTER = 0x00000010;
        /// <summary>
        /// 隐藏窗口，缺省则显示窗口
        /// </summary>
        public const Int32 AW_HIDE = 0x00010000;
        /// <summary>
        /// 激活窗口。在使用AW_HIDE标志后不要使用这个标志
        /// </summary>
        public const Int32 AW_ACTIVATE = 0x00020000;
        /// <summary>
        /// 使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
        /// </summary>
        public const Int32 AW_SLIDE = 0x00040000;
        /// <summary>
        /// 使用淡入效果。只有当hWnd为顶层窗口的时候才可以使用此标志
        /// </summary>
        public const Int32 AW_BLEND = 0x00080000;

        public const int FO_COPY = 2;
        public const int FO_DELETE = 3;
        public const int FO_MOVE = 1;
        public const int FO_RENAME = 4;
        public const int FOF_ALLOWUNDO = 64;

        public const int FLASHW_ALL = 0x0003;
        public const int FLASHW_CAPTION = 0x0001;
        public const int FLASHW_STOP = 0x0000;
        public const int FLASHW_TIMER = 0x0004;
        public const int FLASHW_TRAY = 0x0002;
        public const int FLASHW_TIMERNOFG = 0x000C;
        /// <summary>
        /// 保持当前位置（x和y设定将被忽略）
        /// </summary>
        public const int SWP_NOMOVE = 0x0002;
        /// <summary>
        /// 保持当前大小（cx和cy会被忽略）
        /// </summary>
        public const int SWP_NOSIZE = 0x0001;
        /// <summary>
        /// 保持窗口在列表的当前位置（hWndInsertAfter将被忽略）
        /// </summary>
        public const int SWP_NOZORDER = 0x0004;
        /// <summary>
        /// 窗口不自动重画
        /// </summary>
        public const int SWP_NOREDRAW = 0x0008;
        /// <summary>
        /// 不激活窗口
        /// </summary>
        public const int SWP_NOACTIVATE = 0x0010;
        /// <summary>
        /// 强迫一条WM_NCCALCSIZE消息进入窗口，即使窗口的大小没有改变
        /// </summary>
        public const int SWP_FRAMECHANGED = 0x0020;
        /// <summary>
        /// 显示窗口
        /// </summary>
        public const int SWP_SHOWWINDOW = 0x0040;
        /// <summary>
        /// 隐藏窗口
        /// </summary>
        public const int SWP_HIDEWINDOW = 0x0080;
        public const int SWP_NOCOPYBITS = 0x0100;
        public const int SWP_NOOWNERZORDER = 0x0200;
        public const int SWP_NOSENDCHANGING = 0x0400;
        /// <summary>
        /// 围绕窗口画一个框
        /// </summary>
        public const int SWP_DRAWFRAME = SWP_FRAMECHANGED;
        public const int SWP_NOREPOSITION = SWP_NOOWNERZORDER;

        /// <summary>
        /// //对话框建立失败
        /// </summary>
        public const int IDBREAK = 0;
        /// <summary>
        /// 　//按确定按钮
        /// </summary>
        public const int IDOK = 1;
        /// <summary>
        /// 　//按取消按钮
        /// </summary>
        public const int IDCANCEL = 2;
        /// <summary>
        ///  //按异常终止按钮
        /// </summary>
        public const int IDABOUT = 3;
        /// <summary>
        ///  //按重试按钮
        /// </summary>
        public const int IDRETRY = 4;
        /// <summary>
        /// //按忽略按钮
        /// </summary>
        public const int IDIGNORE = 5;
        /// <summary>
        ///  //按是按钮
        /// </summary>
        public const int IDYES = 6;
        /// <summary>
        /// //按否按钮
        /// </summary>
        public const int IDNO = 7;


        public const int GWL_EXSTYLE = -20;
        public const int GWL_HINSTANCE = -6;
        public const int GWL_HWNDPARENT = -8;
        public const int GWL_ID = -12;
        public const int GWL_STYLE = -16;
        public const int GWL_USERDATA = -21;
        public const int GWL_WNDPROC = -4;
        public const int LWA_ALPHA = 0x2;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x20;


        public const int MB_ICONASTERISK = 0x0040;
        public const int MB_ICONEXCLAMATION = 0x0030;
        public const int MB_ICONHAND = 0x0010;
        public const int MB_ICONQUESTION = 0x0020;
        public const int MB_OK = 0x00;
        public const int MB_BUTTON2 = 0x0100;
        public const int MB_BUTTON4 = 0x0300;
        public const int MB_BUTTON3 = 0x0200;
        public const int MB_OKCANCEL = 0x001;　　　　　　//一个确定按钮，一个取消按钮
        public const int MB_ABORTRETRYIGNORE = 0x002;　　//一个异常终止按钮，一个重试按钮，一个忽略按钮
        public const int MB_YESNOCANCEL = 0x0003;　　　　 //一个是按钮，一个否按钮，一个取消按钮
        public const int MB_YESNO = 0x0004;　　　　　　　 //一个是按钮，一个否按钮
        public const int MB_RETRYCANCEL = 0x005;　　　　 //一个重试按钮，一个取消按钮
        /// <summary>
        /// 无提示
        /// </summary>
        public const int SHERB_NOCONFIRMATION = 0x000001;
        /// <summary>
        /// 无进展显示
        /// </summary>
        public const int SHERB_NOPROGRESSUI = 0x000002;
        /// <summary>
        /// 无声音
        /// </summary>
        public const int SHERB_NOSOUND = 0x000004;

        /// <summary>
        /// //按下
        /// </summary>
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        /// <summary>
        /// //放开
        /// </summary>
        public const int KEYEVENTF_KEYUP = 0x0002;

        /// <summary>
        /// 移动鼠标
        /// </summary>
        public const int MOUSEEVENTF_MOVE = 0x0001;      //
        /// <summary>
        /// 模拟鼠标左键按下
        /// </summary>
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002; //
        /// <summary>
        /// 模拟鼠标左键抬起
        /// </summary>
        public const int MOUSEEVENTF_LEFTUP = 0x0004; //
        /// <summary>
        /// 模拟鼠标右键按下
        /// </summary>
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;//
        /// <summary>
        /// 模拟鼠标右键抬起
        /// </summary>
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;//
        /// <summary>
        /// 模拟鼠标中键按下
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;//
        /// <summary>
        /// 模拟鼠标中键抬起
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;//
        public const int XDown = 0x0080;
        public const int XUp = 0x0100;
        public const int MOUSEEVENTF_WHEEL = 0x0800;
        public const int VirtualDesk = 0x4000;
        /// <summary>
        ///  标示是否采用绝对坐标
        /// </summary>
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;//


        public const int SW_HIDE = 0;//隐藏
        public const int SW_SHOWNORMAL = 1;//显示
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;//最小化显示
        public const int SW_SHOWMAXIMIZED = 3;//最大化显示
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;//显示
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;
        /// <summary>
        /// 注销用户
        /// </summary>
        public const int EWX_LOGOFF = 0;
        /// <summary>
        /// 安全关机
        /// </summary>
        public const int EWX_SHUTDOWN = 0x1;//
        /// <summary>
        /// 重新启动
        /// </summary>
        public const int EWX_REBOOT = 0x2;
        /// <summary>
        /// 强制终止进程
        /// </summary>
        public const int EWX_FORCE = 0x4;
        /// <summary>
        /// 关闭系统并关闭电源。该系统必须支持断电
        /// </summary>
        public const int EWX_POWEROFF = 0x8;

        /// <summary>
        /// //X
        /// </summary>
        public const int SM_CXSCREEN = 0;
        /// <summary>
        /// //Y
        /// </summary>
        public const int SM_CYSCREEN = 1;
        public const int SM_CXVSCROLL = 2;
        public const int SM_CYHSCROLL = 3;
        public const int SM_CYCAPTION = 4;
        public const int SM_CXBORDER = 5;
        public const int SM_CYBORDER = 6;
        public const int SM_CXDLGFRAME = 7;
        public const int SM_CYDLGFRAME = 8;
        public const int SM_CYVTHUMB = 9;
        public const int SM_CXHTHUMB = 10;
        public const int SM_CXICON = 11;
        public const int SM_CYICON = 12;
        public const int SM_CXCURSOR = 13;
        public const int SM_CYCURSOR = 14;
        public const int SM_CYMENU = 15;
        public const int SM_CXFULLSCREEN = 16;
        public const int SM_CYFULLSCREEN = 17;
        public const int SM_CYKANJIWinDOW = 18;
        public const int SM_MOUSEPRESENT = 19;
        public const int SM_CYVSCROLL = 20;
        public const int SM_CXHSCROLL = 21;
        public const int SM_DEBUG = 22;
        public const int SM_SWAPBUTTON = 23;
        public const int SM_RESERVED1 = 24;
        public const int SM_RESERVED2 = 25;
        public const int SM_RESERVED3 = 26;
        public const int SM_RESERVED4 = 27;
        public const int SM_CXMIN = 28;
        public const int SM_CYMIN = 29;
        public const int SM_CXSIZE = 30;
        public const int SM_CYSIZE = 31;
        public const int SM_CXFRAME = 32;
        public const int SM_CYFRAME = 33;
        public const int SM_CXMINTRACK = 34;
        public const int SM_CYMINTRACK = 35;
        public const int SM_CXDOUBLECLK = 36;
        public const int SM_CYDOUBLECLK = 37;
        public const int SM_CXICONSPACING = 38;
        public const int SM_CYICONSPACING = 39;
        public const int SM_MENUDROPALIGNMENT = 40;
        public const int SM_PENWinDOWS = 41;
        public const int SM_DBCSENABLED = 42;
        public const int SM_CMOUSEBUTTONS = 43;

    }
    /// <summary>
    /// 窗口热键类
    /// </summary>
    public class Win_HotKey
    {
        /// <summary>
        /// 窗口消息-创建
        /// </summary>
        public const int WM_CREATE = 0x1;
        /// <summary>
        /// 窗口消息-热键
        /// </summary>
        public const int WM_HOTKEY = 0x0312;
        /// <summary>
        /// 窗口消息-销毁
        /// </summary>
        public const int WM_DESTROY = 0x2;
        public const int MOD_ALT = 0x1; //ALT
        public const int MOD_CONTROL = 0x2; //CTRL
        public const int MOD_SHIFT = 0x4; //SHIFT
        public const int MOD_Win = 0x8;//Win
        public const int vk_SPACE = 0x20; //SPACE
        /// <summary>
        /// 卸载热键
        /// </summary>
        /// <param name="hwnd">句柄</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int UnregisterHotKey(IntPtr hwnd, int id);
        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">句柄</param>
        /// <param name="id">ID</param>
        /// <param name="fsModifiers"></param>
        /// <param name="vk"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);

    }
    /// <summary>
    /// 键值类
    /// </summary>
    public class Win_HKConst//常量
    {
        public const int vk_Esc = 27;
        public const int vk_Return = 13;
        public const int vk_Tab = 9;
        public const int vk_CapsLock = 20;
        public const int vk_Shift = 16;
        public const int vk_Ctrl = 17;
        public const int vk_Alt = 18;
        public const int vk_Space = 32;
        public const int vk_Back = 8;
        public const int vk_L_Win = 91;
        public const int vk_R_Win = 92;
        public const int vk_Apps = 93;
        public const int vk_Insert = 45;
        public const int vk_Home = 36;
        public const int vk_PgUp = 33;
        public const int vk_PgDown = 34;
        public const int vk_End = 35;
        public const int vk_Delete = 46;
        public const int vk_Left = 37;
        public const int vk_Up = 38;
        public const int vk_Right = 39;
        public const int vk_Down = 40;
        public const int vk_F1 = 112;
        public const int vk_F2 = 113;
        public const int vk_F3 = 114;
        public const int vk_F4 = 115;
        public const int vk_F5 = 116;
        public const int vk_F6 = 117;
        public const int vk_F7 = 118;
        public const int vk_F8 = 119;
        public const int vk_F9 = 120;
        public const int vk_F10 = 121;
        public const int vk_F11 = 122;
        public const int vk_F12 = 123;
        public const int vk_NumLock = 144;
        public const int vk_Num0 = 96;
        public const int vk_Num1 = 97;
        public const int vk_Num2 = 98;
        public const int vk_Num3 = 99;
        public const int vk_Num4 = 100;
        public const int vk_Num5 = 101;
        public const int vk_Num6 = 102;
        public const int vk_Num7 = 103;
        public const int vk_Num8 = 104;
        public const int vk_Num9 = 105;
        public const int vk_点 = 110;
        public const int vk_乘 = 106;
        public const int vk_加 = 107;
        public const int vk_减 = 109;
        public const int vk_除以 = 111;
        public const int vk_Pause = 19;
        public const int vk_ScrollLock = 145;
    }
    /// <summary>
    /// 注册表类
    /// </summary>
    public class Win_Register
    {
        /// <summary>
        /// 写注册表
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey">子项</param>
        /// <param name="name">要存储的值得名称</param>
        /// <param name="value">要存储的数据</param>
        //public static void SetRegistryValue(RegistryKey root, string subkey, string name, string value)
        //{
        //    RegistryKey aimdir = root.CreateSubKey(subkey);
        //    aimdir.SetValue(name, value);
        //}
        /// <summary>
        /// 取指定名称的注册表的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //public static string GetRegistryValue(RegistryKey root, string subkey, string name)
        //{
        //    string registData = "";
        //    RegistryKey myKey = root.OpenSubKey(subkey, true);
        //    if (myKey != null)
        //    {
        //        registData = myKey.GetValue(name).ToString();
        //    }

        //    return registData;
        //}
        /// <summary>
        /// 检索注册表子项是否存在
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey">路径</param>
        /// <param name="name">子项</param>
        /// <returns></returns>
        //public static bool RegistrySubkeyIsExist(RegistryKey root, string subkey, string name)
        //{
        //    bool _exit = true;
        //    string[] subkeyNames;
        //    RegistryKey myKey = root.OpenSubKey(subkey, true);
        //    subkeyNames = myKey.GetSubKeyNames();
        //    foreach (string keyName in subkeyNames)
        //    {
        //        if (keyName == name)
        //        {
        //            _exit = true;
        //            return _exit;
        //        }
        //    }
        //    return _exit;
        //}
        /// <summary>
        /// 检索注册表键值的名称是否存在
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey">路径</param>
        /// <param name="name">键值的名称</param>
        /// <returns></returns>
        //public bool RegistryValueIsExist(RegistryKey root, string subkey, string name)
        //{
        //    bool Is = false;
        //    RegistryKey rk = root.OpenSubKey(subkey, true);
        //    string[] names = rk.GetValueNames();
        //    foreach (string n in names)
        //    {
        //        if (n == name)
        //        {
        //            Is = true;
        //        }
        //    }
        //    return Is;
        //}
        /// <summary>
        /// 删除注册表键值
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        //public static void DeleteValue(RegistryKey root, string subkey, string name)
        //{
        //    RegistryKey reg = root.CreateSubKey(subkey);
        //    reg.DeleteValue(name);
        //}

        /// <summary>
        /// 删除注册表子项
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        //public static void DeleteSubkey(RegistryKey root, string subkey, string name)
        //{
        //    string[] subkeyNames;
        //    RegistryKey myKey = root.OpenSubKey(subkey, true);
        //    subkeyNames = myKey.GetSubKeyNames();
        //    foreach (string aimKey in subkeyNames)
        //    {
        //        if (aimKey == name)
        //            myKey.DeleteSubKeyTree(name);
        //    }
        //}
        /// <summary>
        /// 遍历指定注册表路径子项
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey"></param>
        /// <param name="name">要遍历的子项名！</param>
        /// <returns></returns>
        //public static string[] ReadRegistrySubkeys(RegistryKey root, string subkey, string name)
        //{
        //    RegistryKey rk = root.OpenSubKey(subkey, true);
        //    string[] names = rk.GetSubKeyNames();
        //    return names;
        //}
        /// <summary>
        /// 遍历指定注册表路径键值的名称
        /// </summary>
        /// <param name="root"></param>
        /// <param name="subkey"></param>
        /// <param name="name">要遍历的键值的名称！</param>
        /// <returns></returns>
        //public static string[] ReadRegistryValues(RegistryKey root, string subkey, string name)
        //{
        //    RegistryKey rk = root.OpenSubKey(subkey, true);
        //    string[] names = rk.GetValueNames();
        //    return names;
        //}
        /// <summary>
        /// HKEY_CLASSES_ROOT
        /// </summary>
        //public static RegistryKey HKEY_CLASSES_ROOT = Registry.ClassesRoot;
        /// <summary>
        /// HKEY_CURRENT_USER
        /// </summary>
        //public static RegistryKey HKEY_CURRENT_USER = Registry.CurrentUser;
        /// <summary>
        /// HKEY_LOCAL_MACHINE
        /// </summary>
        //public static RegistryKey HKEY_LOCAL_MACHINE = Registry.LocalMachine;
        /// <summary>
        ///HKEY_USERS
        /// </summary>
        //public static RegistryKey HKEY_USERS = Registry.Users;
        /// <summary>
        /// HKEY_CURRENT_CONFIG
        /// </summary>
        //public static RegistryKey HKEY_CURRENT_CONFIG = Registry.CurrentConfig;
    }
    /// <summary>
    /// 数值转换类
    /// </summary>
    public class Win_Convert
    {
        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="value">欲转换的值</param>
        /// <returns></returns>
        public static string Get_MD5(string value)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] buff = Encoding.Default.GetBytes(value);
            byte[] md5buff = md5.ComputeHash(buff);
            string strnew = "";
            //一个一个的来
            for (int i = 0; i < md5buff.Length; i++)
            {
                strnew += md5buff[i].ToString("x2");
            }
            // 不行string r = Encoding.GetEncoding("GB2312").GetString(md5buff);
            return strnew;
        }
        /// <summary>
        /// 使用RSA算法进行解密
        /// </summary>
        /// <param name="text">要加密的文本</param>
        /// <returns></returns>
        public static string RSA_Jiemi(string text)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = GetRSAProviderFromContainer("rsa");
            byte[] encryptedData = Convert.FromBase64String(text);//64->8
            byte[] decryptedData = rsa.Decrypt(encryptedData, true);
            return Encoding.Unicode.GetString(decryptedData);
        }
        /// <summary>
        /// 使用RSA算法进行加密
        /// </summary>
        /// <param name="text">要加密的文本</param>
        /// <returns></returns>
        public static string RSA_Jiami(string text)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = GetRSAProviderFromContainer("rsa");
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            byte[] encryptedData = rsa.Encrypt(bytes, true);
            return Convert.ToBase64String(encryptedData);//8->64
        }

        public static System.Security.Cryptography.RSACryptoServiceProvider GetRSAProviderFromContainer(string containername)
        {
            System.Security.Cryptography.CspParameters cp = new System.Security.Cryptography.CspParameters();
            //将ProviderType初始化值为24，该值指定PROV_RSA_AES提供程序
            cp.ProviderType = 24;

            cp.KeyContainerName = containername;

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(cp);
            return rsa;
        }

        /// <summary>
        /// 十到二
        /// </summary>
        /// <param name="ToValue"></param>
        /// <returns></returns>
        public static string DecToBinary(string ToValue)
        {
            string str = Convert.ToString(Convert.ToInt32(ToValue), 2);
            return str;
        }
        /// <summary>
        /// 八到二
        /// </summary>
        /// <param name="ToValue"></param>
        /// <returns></returns>
        public static string OctToBinary(string ToValue)
        {
            string str = Convert.ToString(Convert.ToInt32(ToValue), 8);
            return str;
        }
        /// <summary>
        /// 十六到二
        /// </summary>
        /// <param name="ToValue"></param>
        /// <returns></returns>
        public static string HexToBinary(string ToValue)
        {
            string str = Convert.ToString(Convert.ToInt32(ToValue), 16);
            return str;
        }
        /// <summary>
        /// 十六到十
        /// </summary>
        /// <param name="ToValue"></param>
        /// <returns></returns>
        public static int HexToDec(string ToValue)
        {
            int str = Convert.ToInt32(ToValue, 16);
            return str;
        }
        /// <summary>
        /// 十到十六
        /// </summary>
        /// <param name="ToValue"></param>
        /// <returns></returns>
        public static string DecToHex(string ToValue)
        {
            string str = Convert.ToString(Convert.ToInt32(ToValue), 16);
            return str;
        }
        /// <summary>
        /// 二到十
        /// </summary>
        /// <param name="ToValue"></param>
        /// <returns></returns>
        public static int BinaryToDex(string ToValue)
        {
            int str = Convert.ToInt32(ToValue, 2);
            return str;
        }
    }
    /// <summary>
    /// 键盘记录
    /// </summary>
    public class Win_GetKBoard
    {
        /// <summary>
        /// 键盘记录
        /// </summary>
        /// <returns></returns>
        public static string GetKBoard()
        {
            string Keys_Return_Text;
            bool Capslook;//是否按下Capslook键
            bool Shift;//是否按下Shift键
            ////////////////////获取CapsLock
            if (Win_Window.GetKeyState(Win_HKConst.vk_CapsLock) != 0)
            {
                Capslook = true;
            }
            else
            {
                Capslook = false;
            }
            /////////////////////获取Shift状态
            if (Win_Window.GetKeyState(Win_HKConst.vk_Shift) != 0)
            {
                Shift = true;
            }
            else
            {
                Shift = false;
            }
            /////////////////////////主键盘
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Tab) != 0)
            {
                Keys_Return_Text = "[Tab] ";
                return Keys_Return_Text;
            }
            //if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Shift) != 0)
            //{
            //    Keys_Return_Text = "[Shift] ";
            //    return Keys_Return_Text;
            //}
            if (Win_Window.GetAsyncKeyState('A') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "A ";
                }
                else
                {
                    Keys_Return_Text = "a ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('B') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "B ";
                }
                else
                {
                    Keys_Return_Text = "b ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('C') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "C ";
                }
                else
                {
                    Keys_Return_Text = "c ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('D') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "D ";
                }
                else
                {
                    Keys_Return_Text = "d ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('E') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "E ";
                }
                else
                {
                    Keys_Return_Text = "e ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('F') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "F ";
                }
                else
                {
                    Keys_Return_Text = "f ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('G') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "G ";
                }
                else
                {
                    Keys_Return_Text = "g ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('H') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "H ";
                }
                else
                {
                    Keys_Return_Text = "h ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('I') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "I ";
                }
                else
                {
                    Keys_Return_Text = "i ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('J') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "J ";
                }
                else
                {
                    Keys_Return_Text = "j ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('K') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "K ";
                }
                else
                {
                    Keys_Return_Text = "k ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('L') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "L ";
                }
                else
                {
                    Keys_Return_Text = "l ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('M') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "M ";
                }
                else
                {
                    Keys_Return_Text = "m ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('O') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "O ";
                }
                else
                {
                    Keys_Return_Text = "o ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('P') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "P ";
                }
                else
                {
                    Keys_Return_Text = "p ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('Q') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "Q ";
                }
                else
                {
                    Keys_Return_Text = "q ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('R') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "R ";
                }
                else
                {
                    Keys_Return_Text = "r ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('S') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "S ";
                }
                else
                {
                    Keys_Return_Text = "s ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState('T') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "T ";

                }
                else
                {
                    Keys_Return_Text = "t ";

                }
                return Keys_Return_Text;

            }

            if (Win_Window.GetAsyncKeyState('U') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "U ";

                }
                else
                {
                    Keys_Return_Text = "u ";

                }
                return Keys_Return_Text;

            }

            if (Win_Window.GetAsyncKeyState('V') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "V ";

                }
                else
                {
                    Keys_Return_Text = "v ";

                }
                return Keys_Return_Text;

            }

            if (Win_Window.GetAsyncKeyState('W') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "W ";

                }
                else
                {
                    Keys_Return_Text = "w ";

                }
                return Keys_Return_Text;

            }

            if (Win_Window.GetAsyncKeyState('X') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "X ";

                }
                else
                {
                    Keys_Return_Text = "x ";

                }
                return Keys_Return_Text;

            }

            if (Win_Window.GetAsyncKeyState('Y') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "Y ";

                }
                else
                {
                    Keys_Return_Text = "y ";

                }
                return Keys_Return_Text;

            }

            if (Win_Window.GetAsyncKeyState('Z') != 0)
            {
                if (Capslook == true)
                {
                    Keys_Return_Text = "Z ";

                }
                else
                {
                    Keys_Return_Text = "z ";

                }
                return Keys_Return_Text;

            }


            if (Win_Window.GetAsyncKeyState(49) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "! ";

                }
                else
                {
                    Keys_Return_Text = "1 ";

                }
                return Keys_Return_Text;

            }
            if (Win_Window.GetAsyncKeyState(50) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "@ ";

                }
                else
                {
                    Keys_Return_Text = "2 ";

                }
                return Keys_Return_Text;

            }
            if (Win_Window.GetAsyncKeyState(51) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "#";

                }
                else
                {
                    Keys_Return_Text = "3 ";

                }
                return Keys_Return_Text;

            }
            if (Win_Window.GetAsyncKeyState(52) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "$ ";

                }
                else
                {
                    Keys_Return_Text = "4 ";

                }
                return Keys_Return_Text;

            }
            if (Win_Window.GetAsyncKeyState(53) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "% ";

                }
                else
                {
                    Keys_Return_Text = "5 ";

                }
                return Keys_Return_Text;

            }
            if (Win_Window.GetAsyncKeyState(54) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "^ ";

                }
                else
                {
                    Keys_Return_Text = "6 ";

                }
                return Keys_Return_Text;

            }
            if (Win_Window.GetAsyncKeyState(55) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "& ";

                }
                else
                {
                    Keys_Return_Text = "7 ";

                }
                return Keys_Return_Text;

            }
            if (Win_Window.GetAsyncKeyState(56) != 0)
            {

                if (Shift == true)
                {
                    Keys_Return_Text = "* ";

                }
                else
                {
                    Keys_Return_Text = "8 ";

                }
                return Keys_Return_Text;

            }
            if (Win_Window.GetAsyncKeyState(57) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "( ";

                }
                else
                {
                    Keys_Return_Text = "9 ";

                }
                return Keys_Return_Text;

            }
            if (Win_Window.GetAsyncKeyState(48) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = ") ";

                }
                else
                {
                    Keys_Return_Text = "0 ";

                }
                return Keys_Return_Text;

            }

            //标点符号
            if (Win_Window.GetAsyncKeyState(187) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "+ ";
                }
                else
                {
                    Keys_Return_Text = "= ";
                }
                return Keys_Return_Text;
            }

            if (Win_Window.GetAsyncKeyState(188) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "< ";
                }
                else
                {
                    Keys_Return_Text = ", ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(186) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "; ";
                }
                else
                {
                    Keys_Return_Text = ": ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(189) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "_ ";
                }
                else
                {
                    Keys_Return_Text = "- ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(190) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "> ";
                }
                else
                {
                    Keys_Return_Text = ". ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(192) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "~ ";
                }
                else
                {
                    Keys_Return_Text = "` ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(219) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "{ ";
                }
                else
                {
                    Keys_Return_Text = "[ ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(191) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "? ";
                }
                else
                {
                    Keys_Return_Text = "/ ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(220) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "| ";
                }
                else
                {
                    Keys_Return_Text = "\\ ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(221) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "} ";
                }
                else
                {
                    Keys_Return_Text = "] ";
                }
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(222) != 0)
            {
                if (Shift == true)
                {
                    Keys_Return_Text = "\" ";
                }
                else
                {
                    Keys_Return_Text = "\' ";
                }
                return Keys_Return_Text;
            }
            // 数字键
            if (Win_Window.GetAsyncKeyState(106) != 0)
            {
                Keys_Return_Text = "× ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(107) != 0)
            {
                Keys_Return_Text = "+ ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(13) != 0)
            {
                Keys_Return_Text = "[回车键] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(109) != 0)
            {
                Keys_Return_Text = "－ ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(110) != 0)
            {
                Keys_Return_Text = ". ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(111) != 0)
            {
                Keys_Return_Text = "÷ ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num0) != 0)
            {
                Keys_Return_Text = "Num0 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num1) != 0)
            {
                Keys_Return_Text = "Num1 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num2) != 0)
            {
                Keys_Return_Text = "Num2 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num3) != 0)
            {
                Keys_Return_Text = "Num3 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num4) != 0)
            {
                Keys_Return_Text = "Num4 ";
                return Keys_Return_Text;
            }


            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num5) != 0)
            {
                Keys_Return_Text = "Num5 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num6) != 0)
            {
                Keys_Return_Text = "Num6 ";
                return Keys_Return_Text;
            }

            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num7) != 0)
            {
                Keys_Return_Text = "Num7 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num8) != 0)
            {
                Keys_Return_Text = "Num8 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Num9) != 0)
            {
                Keys_Return_Text = "Num9 ";
                return Keys_Return_Text;
            }

            // F1-F12键
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F1) != 0)
            {
                Keys_Return_Text = "F1 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F2) != 0)
            {
                Keys_Return_Text = "F2 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F3) != 0)
            {
                Keys_Return_Text = "F3 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F4) != 0)
            {
                Keys_Return_Text = "F4 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F5) != 0)
            {
                Keys_Return_Text = "F5 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F6) != 0)
            {
                Keys_Return_Text = "F6 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F7) != 0)
            {
                Keys_Return_Text = "F7 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F8) != 0)
            {
                Keys_Return_Text = "F8 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F9) != 0)
            {
                Keys_Return_Text = "F9 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F10) != 0)
            {
                Keys_Return_Text = "F10 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F11) != 0)
            {
                Keys_Return_Text = "F11 ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_F12) != 0)
            {
                Keys_Return_Text = "F12 ";
                return Keys_Return_Text;
            }


            //功能键
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Home) != 0)
            {
                Keys_Return_Text = "[Home] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_End) != 0)
            {
                Keys_Return_Text = "[End] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Insert) != 0)
            {
                Keys_Return_Text = "[Insert] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Delete) != 0)
            {
                Keys_Return_Text = "[Delete] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Esc) != 0)
            {
                Keys_Return_Text = "[Esc] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Ctrl) != 0)
            {
                Keys_Return_Text = "[Ctrl] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Alt) != 0)
            {
                Keys_Return_Text = "[Alt] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_CapsLock) != 0)
            {
                Keys_Return_Text = "[CapsLock] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_NumLock) != 0)
            {
                Keys_Return_Text = "[NumLock] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_PgUp) != 0)
            {
                Keys_Return_Text = "[PgUp] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_PgDown) != 0)
            {
                Keys_Return_Text = "[PgDown] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Up) != 0)
            {
                Keys_Return_Text = "[↑] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Down) != 0)
            {
                Keys_Return_Text = "[↓] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Left) != 0)
            {
                Keys_Return_Text = "[←] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Right) != 0)
            {
                Keys_Return_Text = "[→] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Back) != 0)
            {
                Keys_Return_Text = "[Backspace] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_Space) != 0)
            {
                Keys_Return_Text = "[空格键] ";
                return Keys_Return_Text;
            }
            if (Win_Window.GetAsyncKeyState(Win_HKConst.vk_L_Win) != 0)
            {
                Keys_Return_Text = "L[Win_] ";
                return Keys_Return_Text;
            }
            return null;
        }
    }
    /// <summary>
    /// 消息常量
    /// </summary>
    public class Win_WM
    {
        /// <summary>
        /// The WM_APP constant is used by applications to help define private messages, usually of the form WM_APP+X, where X is an integer value.
        /// </summary>
        public const int WM_APP = 0x8000;
        /// <summary>
        /// The WM_USER constant is used by applications to help define private messages for use by private window classes, usually of the form WM_USER+X, where X is an integer value.
        /// </summary>
        public const int WM_USER = 0x0400;
        /// <summary>
        /// U盘插入后，OS的底层会自动检测到，然后向应用程序发送“硬件设备状态改变“的消息
        /// </summary>
        public const int WM_DEVICECHANGE = 0x0219;
        /// <summary>
        /// 窗口消息-热键
        /// </summary>
        public const int WM_HOTKEY = 0x0312;
        /// <summary>
        /// 创建一个窗口:  0x01
        /// </summary>
        public const int WM_CREATE = 0x01;
        /// <summary>
        /// 当一个窗口被破坏时发送   0x02
        /// </summary>
        public const int WM_DESTROY = 0x02;
        /// <summary>
        /// 移动一个窗口 0x03
        /// </summary>
        public const int WM_MOVE = 0x03;
        /// <summary>
        /// 改变一个窗口的大小   0x05
        /// </summary>
        public const int WM_SIZE = 0x05;
        /// <summary>
        /// 一个窗口被激活或失去激活状态 0x06
        /// </summary>
        public const int WM_ACTIVATE = 0x06;
        /// <summary>
        /// 一个窗口获得焦点   0x07
        /// </summary>
        public const int WM_SETFOCUS = 0x07;
        /// <summary>
        /// 一个窗口失去焦点 0x08
        /// </summary>
        public const int WM_KILLFOCUS = 0x08;
        //一个窗口改变成Enable状态   0x0A
        public const int WM_ENABLE = 0x0A;
        //设置窗口是否能重画   0x0B
        public const int WM_SETREDRAW = 0x0B;
        /// <summary>
        /// 应用程序发送此消息来设置一个窗口的文本  0x0C
        /// </summary>
        public const int WM_SETTEXT = 0x0C;
        //应用程序发送此消息来复制对应窗口的文本到缓冲区   0x0D
        public const int WM_GETTEXT = 0x0D;
        /// <summary>
        /// 得到与一个窗口有关的文本的长度（不包含空字符）
        /// </summary>
        public const int WM_GETTEXTLENGTH = 0x0E;
        /// <summary>
        /// 要求一个窗口重画自己   0x0F
        /// </summary>
        public const int WM_PAINT = 0x0F;
        /// <summary>
        /// 当一个窗口或应用程序要关闭时发送一个信号  0x10
        /// </summary>
        public const int WM_CLOSE = 0x10;
        /// <summary>
        /// 当用户选择结束对话框或程序自己调用ExitWindows函数  0x11
        /// </summary>
        public const int WM_QUERYENDSESSION = 0x11;
        /// <summary>
        /// 用来结束程序运行   0x12
        /// </summary>
        public const int WM_QUIT = 0x12;
        /// <summary>
        /// 当用户窗口恢复以前的大小位置时，把此消息发送给某个图标   0x13
        /// </summary>
        public const int WM_QUERYOPEN = 0x13;
        //当窗口背景必须被擦除时（例在窗口改变大小时）
        public const int WM_ERASEBKGND = 0x14;
        //当系统颜色改变时，发送此消息给所有顶级窗口   0x15
        public const int WM_SYSCOLORCHANGE = 0x15;
        //当系统进程发出WM_QUERYENDSESSION消息后，此消息发送给应用程序，通知它对话是否结束
        public const int WM_ENDSESSION = 0x16;
        /// <summary>
        /// 当隐藏或显示窗口是发送此消息给这个窗口   0x18
        /// </summary>
        public const int WM_SHOWWinDOW = 0x18;
        /// <summary>
        /// 发此消息给应用程序哪个窗口是激活的，哪个是非激活的   0x1C
        /// </summary>
        public const int WM_ACTIVATEAPP = 0x1C;
        //当系统的字体资源库变化时发送此消息给所有顶级窗口 0x1D
        public const int WM_FONTCHANGE = 0x1D;
        //当系统的时间变化时发送此消息给所有顶级窗口   0x1E
        public const int WM_TIMECHANGE = 0x1E;
        //发送此消息来取消某种正在进行的摸态（操作）   0x1F
        public const int WM_CANCELMODE = 0x1F;
        /// <summary>
        /// 如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口   0x20
        /// </summary>
        public const int WM_SETCURSOR = 0x20;
        /// <summary>
        /// 当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给//当前窗口   0x21
        /// </summary>
        public const int WM_MOUSEACTIVATE = 0x21;
        //发送此消息给MDI子窗口//当用户点击此窗口的标题栏，或//当窗口被激活，移动，改变大小   0x22
        public const int WM_CHILDACTIVATE = 0x22;
        //此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序分离出用户输入消息
        public const int WM_QUEUESYNC = 0x23;
        /// <summary>
        /// 此消息发送给窗口当它将要改变大小或位置   0x24
        /// </summary>
        public const int WM_GETMINMAXINFO = 0x24;
        /// <summary>
        /// 发送给最小化窗口当它图标将要被重画   0x26
        /// </summary>
        public const int WM_PAINTICON = 0x26;
        //此消息发送给某个最小化窗口，仅//当它在画图标前它的背景必须被重画   0x27
        public const int WM_ICONERASEBKGND = 0x27;
        //发送此消息给一个对话框程序去更改焦点位置  0x28
        public const int WM_NEXTDLGCTL = 0x28;
        //每当打印管理列队增加或减少一条作业时发出此消息    0x2A
        public const int WM_SPOOLERSTATUS = 0x2A;
        /// <summary>
        /// 当button，combobox，listbox，menu的可视外观改变时发送 0x2B
        /// </summary>
        public const int WM_DRAWITEM = 0x2B;
        /// <summary>
        /// 当button, combo box, list box, list view control, or menu item 被创建时   0x2C
        /// </summary>
        public const int WM_MEASUREITEM = 0x2C;
        //此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息
        public const int WM_VKEYTOITEM = 0x2E;
        //此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息
        public const int WM_CHARTOITEM = 0x2F;
        /// <summary>
        /// 当绘制文本时程序发送此消息得到控件要用的颜色 0x30
        /// </summary>
        public const int WM_SETFONT = 0x30;
        //应用程序发送此消息得到当前控件绘制文本的字体   0x31
        public const int WM_GETFONT = 0x31;
        /// <summary>
        /// 应用程序发送此消息让一个窗口与一个热键相关连    0x32
        /// </summary>
        public const int WM_SETHOTKEY = 0x32;
        /// <summary>
        /// 应用程序发送此消息来判断热键与某个窗口是否有关联 0x33
        /// </summary>
        public const int WM_GETHOTKEY = 0x33;
        /// <summary>
        /// 此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标，应用程序能返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标
        /// </summary>
        public const int WM_QUERYDRAGICON = 0x37;
        //发送此消息来判定combobox或listbox新增加的项的相对位置   0x39
        public const int WM_COMPAREITEM = 0x39;
        /// <summary>
        /// 显示内存已经很少了   0x41
        /// </summary>
        public const int WM_COMPACTING = 0x41;
        /// <summary>
        /// 发送此消息给那个窗口的大小和位置将要被改变时，来调用setWindowpos函数或其它窗口管理函数
        /// </summary>
        public const int WM_WinDOWPOSCHANGING = 0x46;
        //发送此消息给那个窗口的大小和位置已经被改变时，来调用setWindowpos函数或其它窗口管理函数
        public const int WM_WinDOWPOSCHANGED = 0x47;
        //当系统将要进入暂停状态时发送此消息   0x48
        public const int WM_POWER = 0x48;
        /// <summary>
        /// 程序发送此消息给一个编辑框或combobox来复制当前选择的文本到剪贴板  0x301   /769
        /// </summary>
        public const int WM_COPY = 0x301;
        /// <summary>
        /// 当一个应用程序传递数据给另一个应用程序时发送此消息   0x4A
        /// </summary>
        public const int WM_COPYDATA = 0x4A;
        /// <summary>
        /// 当某个用户取消程序日志激活状态，提交此消息给程序   0x4B
        /// </summary>
        public const int WM_CANCELJOURNA = 0x4B;
        /// <summary>
        /// 当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口  0x4E
        /// </summary>
        public const int WM_NOTIFY = 0x4E;
        /// <summary>
        /// 当用户选择某种输入语言，或输入语言的热键改变 0x50
        /// </summary>
        public const int WM_INPUTLANGCHANGEREQUEST = 0x50;
        //当平台现场已经被改变后发送此消息给受影响的最顶级窗口   0x51
        public const int WM_INPUTLANGCHANGE = 0x51;
        /// <summary>
        /// 当程序已经初始化Windows帮助例程时发送此消息给应用程序   0x52
        /// </summary>
        public const int WM_TCARD = 0x52;
        /// <summary>
        /// 此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单，否则就发送给有焦点的窗口，如果//当前都没有焦点，就把此消息发送给//当前激活的窗口
        /// </summary>
        public const int WM_HELP = 0x53;
        /// <summary>
        /// 当用户已经登入或退出后发送此消息给所有的窗口，//当用户登入或退出时系统更新用户的具体设置信息，在用户更新设置时系统马上发送此消息
        /// </summary>
        public const int WM_USERCHANGED = 0x54;
        //公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNICODE结构   0x55
        public const int WM_NOTIFYFORMAT = 0x55;
        /// <summary>
        /// 当用户某个窗口中点击了一下右键就发送此消息给这个窗口  7B  /  123
        /// </summary>
        public const int WM_CONTEXTMENU = 0x7B;
        //当调用SETWinDOWLONG函数将要改变一个或多个 窗口的风格时发送此消息给那个窗口   0x7C
        public const int WM_STYLECHANGING = 0x7C;
        /// <summary>
        /// 当调用SETWinDOWLONG函数一个或多个 窗口的风格后发送此消息给那个窗口   0x7D
        /// </summary>
        public const int WM_STYLECHANGED = 0x7D;
        /// <summary>
        /// 当显示器的分辨率改变后发送此消息给所有的窗口
        /// </summary>
        public const int WM_DISPLAYCHANGE = 0x7E;
        /// <summary>
        /// 此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄   0x7F
        /// </summary>
        public const int WM_GETICON = 0x7F;
        /// <summary>
        /// 程序发送此消息让一个新的大图标或小图标与某个窗口关联 0x80
        /// </summary>
        public const int WM_SETICON = 0x80;
        /// <summary>
        /// 当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送   0x81
        /// </summary>
        public const int WM_NCCREATE = 0x81;
        /// <summary>
        /// 此消息通知某个窗口，非客户区正在销毁 0x82
        /// </summary>
        public const int WM_NCDESTROY = 0x82;
        /// <summary>
        /// 当某个窗口的客户区域必须被核算时发送此消息   0x83
        /// </summary>
        public const int WM_NCCALCSIZE = 0x83;
        /// <summary>
        /// 移动鼠标，按住或释放鼠标时发生 0x84
        /// </summary>
        public const int WM_NCHITTEST = 0x84;
        /// <summary>
        /// 程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时 0x85
        /// </summary>
        public const int WM_NCPAINT = 0x85;
        /// <summary>
        /// 此消息发送给某个窗口仅当它的非客户区需要被改变来显示是激活还是非激活状态 0x86
        /// </summary>
        public const int WM_NCACTIVATE = 0x86;
        //发送此消息给某个与对话框程序关联的控件，widdows控制方位键和TAB键使输入进入此控件通过应
        public const int WM_GETDLGCODE = 0x87;
        /// <summary>
        /// 当光标在一个窗口的非客户区内移动时发送此消息给这个窗口 非客户区为：窗体的标题栏及窗 的边框体
        /// </summary>
        public const int WM_NCMOUSEMOVE = 0xA0;
        /// <summary>
        /// 当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息 0xA1
        /// </summary>
        public const int WM_NCLBUTTONDOWN = 0xA1;
        /// <summary>
        /// 当用户释放鼠标左键同时光标某个窗口在非客户区十发送此消息 0xA2
        /// </summary>
        public const int WM_NCLBUTTONUP = 0xA2;
        /// <summary>
        /// /当用户双击鼠标左键同时光标某个窗口在非客户区十发送此消息  0xA3
        /// </summary>
        public const int WM_NCLBUTTONDBLCLK = 0xA3;
        /// <summary>
        /// 当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息 0xA4
        /// </summary>
        public const int WM_NCRBUTTONDOWN = 0xA4;
        /// <summary>
        /// 当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息  0xA5
        /// </summary>
        public const int WM_NCRBUTTONUP = 0xA5;
        /// <summary>
        /// 当用户双击鼠标右键同时光标某个窗口在非客户区十发送此消息   0xA6
        /// </summary>
        public const int WM_NCRBUTTONDBLCLK = 0xA6;
        /// <summary>
        /// 当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息   0xA7
        /// </summary>
        public const int WM_NCMBUTTONDOWN = 0xA7;
        /// <summary>
        /// 当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息   0xA8
        /// </summary>
        public const int WM_NCMBUTTONUP = 0xA8;
        /// <summary>
        /// 当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息   0xA9
        /// </summary>
        public const int WM_NCMBUTTONDBLCLK = 0xA9;
        /// <summary>
        /// 按下一个键  0x0100
        /// </summary>
        public const int WM_KEYDOWN = 0x0100;
        /// <summary>
        /// 释放一个键   0x0101
        /// </summary>
        public const int WM_KEYUP = 0x0101;
        /// <summary>
        /// 按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息  0x102
        /// </summary>
        public const int WM_CHAR = 0x102;
        /// <summary>
        /// 当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口   0x103
        /// </summary>
        public const int WM_DEADCHAR = 0x103;
        /// <summary>
        /// 当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口   0x104
        /// </summary>
        public const int WM_SYSKEYDOWN = 0x104;
        /// <summary>
        /// 当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口   0x105
        /// </summary>
        public const int WM_SYSKEYUP = 0x105;
        /// <summary>
        /// 当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口   0x106
        /// </summary>
        public const int WM_SYSCHAR = 0x106;
        /// <summary>
        /// 当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口   0x107
        /// </summary>
        public const int WM_SYSDEADCHAR = 0x107;
        /// <summary>
        /// 在一个对话框程序被显示前发送此消息给它，通常用此消息初始化控件和执行其它任务   0x110
        /// </summary>
        public const int WM_INITDIALOG = 0x110;
        /// <summary>
        /// 当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译   0x111
        /// </summary>
        public const int WM_COMMAND = 0x111;
        /// <summary>
        /// 当用户选择窗口菜单的一条命令或//当用户选择最大化或最小化时那个窗口会收到此消息   0x112
        /// </summary>
        public const int WM_SYSCOMMAND = 0x112;
        /// <summary>
        /// 发生了定时器事件   0x113
        /// </summary>
        public const int WM_TIMER = 0x113;
        /// <summary>
        /// 当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件  0x114
        /// </summary>
        public const int WM_HSCROLL = 0x114;
        //当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件   0x115
        public const int WM_VSCROLL = 0x115;
        /// <summary>
        /// 当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单   0x116
        /// </summary>
        public const int WM_INITMENU = 0x116;
        //当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部   0x117
        public const int WM_INITMENUPOPUP = 0x117;
        /// <summary>
        /// 当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口）   0x11F
        /// </summary>
        public const int WM_MENUSELECT = 0x11F;
        /// <summary>
        /// 当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者   0x120
        /// </summary>
        public const int WM_MENUCHAR = 0x120;
        //当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载状态就是在处理完一条或几条先前的消息后没有消息它的列队中等待
        public const int WM_ENTERIDLE = 0x121;
        /// <summary>
        /// 在Windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色
        /// </summary>
        public const int WM_CTLCOLORMSGBOX = 0x132;
        //当一个编辑型控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
        public const int WM_CTLCOLOREDIT = 0x133;

        //当一个列表框控件将要被绘制前发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色
        public const int WM_CTLCOLORLISTBOX = 0x134;
        //当一个按钮控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色
        public const int WM_CTLCOLORBTN = 0x135;
        //当一个对话框控件将要被绘制前发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色
        public const int WM_CTLCOLORDLG = 0x136;
        //当一个滚动条控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色
        public const int WM_CTLCOLORSCROLLBAR = 0x137;
        //当一个静态控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以 通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色
        public const int WM_CTLCOLORSTATIC = 0x138;
        /// <summary>
        /// 当鼠标轮子转动时发送此消息个当前有焦点的控件   0x120
        /// </summary>
        public const int WM_MOUSEWHEEL = 0x20A;
        /// <summary>
        /// 双击鼠标中键   0x209
        /// </summary>
        public const int WM_MBUTTONDBLCLK = 0x209;
        /// <summary>
        /// 释放鼠标中键   0x208
        /// </summary>
        public const int WM_MBUTTONUP = 0x208;
        /// <summary>
        /// 移动鼠标时发生，同WM_MOUSEFIRST 0x200
        /// </summary>
        public const int WM_MOUSEMOVE = 0x200;
        /// <summary>
        /// 按下鼠标左键 0x201
        /// </summary>
        public const int WM_LBUTTONDOWN = 0x201;
        /// <summary>
        /// 释放鼠标左键 0x202
        /// </summary>
        public const int WM_LBUTTONUP = 0x202;
        /// <summary>
        /// 双击鼠标左键   0x203
        /// </summary>
        public const int WM_LBUTTONDBLCLK = 0x203;
        /// <summary>
        /// 按下鼠标右键  0x204
        /// </summary>
        public const int WM_RBUTTONDOWN = 0x204;
        /// <summary>
        /// 释放鼠标右键   0x205
        /// </summary>
        public const int WM_RBUTTONUP = 0x205;
        /// <summary>
        /// 双击鼠标右键   0x206
        /// </summary>
        public const int WM_RBUTTONDBLCLK = 0x206;
        /// <summary>
        /// 按下鼠标中键   0x207
        /// </summary>
        public const int WM_MBUTTONDOWN = 0x207;

    }
    /// <summary>
    /// 播放音乐类
    /// </summary>
    public class Win_PlaySound
    {
        /// <summary>
        /// 打开音乐文件
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="alias">别名</param>
        public static void OpenMusic(string Filename, string My_alias)
        {
            Win_Window.mciSendString("open " + Filename + " alias " + My_alias, null, 0, 0);
        }
        /// <summary>
        ///播放音乐(开始播放)
        /// </summary>
        public static void PlayMusic(string alias)
        {
            Win_Window.mciSendString("play " + alias, null, 0, 0);
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <param name="alias"></param>
        public static void PauseMusic(string alias)
        {
            Win_Window.mciSendString("pause " + alias, null, 0, 0);
        }
        /// <summary>
        /// 继续播放
        /// </summary>
        /// <param name="alias"></param>
        public static void ResumeMusic(string alias)
        {
            Win_Window.mciSendString("resume " + alias, null, 0, 0);
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="alias"></param>
        public static void StopMusic(string alias)
        {
            Win_Window.mciSendString("stop " + alias, null, 0, 0);
        }
        /// <summary>
        /// 关闭设备(关闭)
        /// </summary>
        /// <param name="alias"></param>
        public static void CloseMusic(string alias)
        {
            Win_Window.mciSendString("stop " + alias, null, 0, 0);
        }
        /// <summary>
        /// 设置音量大小
        /// </summary>
        /// <param name="alias">别名</param>
        /// <param name="Valume">音量值</param>
        /// <returns></returns>
        public static bool SetMusicValume(string alias, int Valume)
        {
            bool result = false;
            string MciCommand = string.Format("setaudio " + alias + " volume to {0}", Valume);
            int RefInt = Win_Window.mciSendString(MciCommand, null, 0, 0);
            if (RefInt == 0)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 设置播放音乐速度
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="Speed">速度</param>
        /// <returns></returns>
        public static bool SetMusicSpeed(string alias, int Speed)
        {
            bool result = false;
            string MciCommand = string.Format("set " + alias + " speed to {0}", Speed);
            int RefInt = Win_Window.mciSendString(MciCommand, null, 0, 0);
            if (RefInt == 0)
                result = true;
            return result;
        }
        /// <summary>
        /// 设置静音 True为静音，FALSE为取消静音
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="AudioOff">True为静音，FALSE为取消静音 </param>
        /// <returns></returns>
        public static bool SetAudioOnOff(string alias, bool AudioOff)
        {
            bool resut = false;
            string OnOff;
            if (AudioOff)
                OnOff = "off";
            else
                OnOff = "on";
            int RefInt = Win_Window.mciSendString("setaudio " + alias + " " + OnOff, null, 0, 0);
            if (RefInt == 0)
                resut = true;
            return resut;
        }

        /// <summary>
        /// 获取当前播放进度（毫秒）
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static int GetMusicPos(string alias)
        {
            string durLength;
            durLength = "";
            durLength = durLength.PadLeft(128, Convert.ToChar(" "));
            Win_Window.mciSendString("status " + alias + " position", durLength, durLength.Length, 0);
            durLength = durLength.Trim();
            if (string.IsNullOrEmpty(durLength))
                return 0;
            else
                return (int)(Convert.ToDouble(durLength));
        }
        /// <summary>
        /// 获取当前播放进度 格式  (00:00:00)
        /// </summary>
        /// <returns></returns>
        public static string GetMusicPosString(string alias)
        {
            string durLength;
            durLength = "";
            durLength = durLength.PadLeft(128, Convert.ToChar(" "));
            Win_Window.mciSendString("status " + alias + " position", durLength, durLength.Length, 0);
            durLength = durLength.Trim();
            if (string.IsNullOrEmpty(durLength))
                return "00:00:00";
            else
            {
                int s = Convert.ToInt32(durLength) / 1000;
                int h = s / 3600;
                int m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);
                return string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
            }
        }
        /// <summary>
        ///  获取媒体的长度
        /// </summary>
        /// <returns></returns>
        public static int GetMusicLength(string alias)
        {
            string durLength;
            durLength = "";
            durLength = durLength.PadLeft(128, Convert.ToChar(" "));
            Win_Window.mciSendString("status " + alias + " length", durLength, durLength.Length, 0);
            durLength = durLength.Trim();
            if (string.IsNullOrEmpty(durLength))
                return 0;
            else
                return Convert.ToInt32(durLength);
        }
        /// <summary>
        /// 获取媒体的长度 00:00:00
        /// </summary>
        /// <returns></returns>
        public static string GetMusicLengthString(string alias)
        {
            string durLength;
            durLength = "";
            durLength = durLength.PadLeft(128, Convert.ToChar(" "));
            Win_Window.mciSendString("status " + alias + " length", durLength, durLength.Length, 0);
            durLength = durLength.Trim();
            if (string.IsNullOrEmpty(durLength))
                return "00:00:00";
            else
            {
                int s = Convert.ToInt32(durLength) / 1000;
                int h = s / 3600;
                int m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);
                return string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
            }
        }


    }
    /// <summary>
    /// Http POST GET
    /// </summary>
    public class Win_Http
    {
        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GB2312toUTF8(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("GB2312");
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("UTF-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符   
            return utf8.GetString(gb);
        }

        /// <summary>
        /// UTF8转换成GB2312
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UTF8toGB2312(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("UTF-8");
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("GB2312");
            byte[] utf;
            utf = utf8.GetBytes(text);
            utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
            //返回转换后的字符   
            return gb2312.GetString(utf);
        }

        /// <summary>
        /// Http_post数据
        /// </summary>
        /// <param name="Uri">请求地址</param>
        /// <param name="Method">方法(POST ,'GET'......)</param>
        /// <param name="ContentType">获取或设置 Content-typeHTTP 标头的值</param>
        /// <param name="PostData">提交数据</param>
        /// <returns></returns>
        public static string Http_post(string Uri, string Method, string ContentType, string PostData)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Uri);
            string postdata = PostData;//提交数据
            request.Method = Method;//方法POST
            request.ContentType = ContentType;
            request.ContentLength = PostData.Length;//长度
            System.IO.StreamWriter sw = new System.IO.StreamWriter(request.GetRequestStream());
            sw.Write(postdata);
            sw.Flush();
            sw.Close();
            sw.Dispose();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            System.IO.Stream stream = response.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream, Encoding.Default);
            string result_text = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            stream.Close();
            response.Close();
            return result_text;
        }
        /// <summary>
        /// Http读文本
        /// </summary>
        /// <param name="Uri">请求地址</param>
        /// <returns></returns>
        public static string Http_read(string Uri)
        {
            HttpWebRequest re = (HttpWebRequest)HttpWebRequest.Create(Uri);
            System.IO.Stream str = re.GetResponse().GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(str, Encoding.Default);
            return sr.ReadToEnd();
        }
        /// <summary>
        ///  Http读文本
        /// </summary>
        /// <param name="Uri">Url</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static string Http_read(string Uri, Encoding encoding)
        {
            HttpWebRequest re = (HttpWebRequest)HttpWebRequest.Create(Uri);
            System.IO.Stream str = re.GetResponse().GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(str, encoding);
            return sr.ReadToEnd();
        }
        /// <summary>
        /// 获取在线音乐歌词
        /// </summary>
        /// <param name="songName">歌名</param>
        /// <param name="songerName">歌手名字</param>
        /// <returns></returns>
        public static string GetWebSongWords(string songName, string singerName)
        {
            string urlGeCi = "http://box.zhangmen.baidu.com/bdlrc/";
            string web = "http://box.zhangmen.baidu.com/x?op=12&count=1&title={0}$${1}$$$$";
            string result = Http_read(web = string.Format(web, songName, singerName), Encoding.GetEncoding("gb2312"));
            string content = @"<count>(?<count>\d+)</count>";
            string matchLrcid = @"<lrcid>(?<id>\d+)</lrcid>";
            int lrcid = 0;
            int i = 0;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(content);
            System.Text.RegularExpressions.Match songinfo = regex.Match(result);
            i = Convert.ToInt32(songinfo.Groups["count"].Value);
            if (i == 0)
            {
                return "Sorry,没有找到歌词！";
            }
            regex = new System.Text.RegularExpressions.Regex(matchLrcid);
            System.Text.RegularExpressions.MatchCollection m = regex.Matches(result);
            foreach (System.Text.RegularExpressions.Match item in m)
            {
                lrcid = Convert.ToInt32(item.Groups["id"].ToString());
                break;
            }
            int fileid = lrcid / 100;
            urlGeCi += fileid + "/" + lrcid + ".lrc";
            return Http_read(urlGeCi, Encoding.GetEncoding("gb2312"));
        }
        /// <summary>
        /// 寻找文本： true(存在) false(不存在)
        /// </summary>
        /// <param name="OldText">要寻找的源文本</param>
        /// <param name="Value">欲查找的文本</param>
        /// <returns></returns>
        public static bool FindText(string OldText, string Value)
        {

            if (OldText.Contains(Value) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Win_Hook
    {

        /// <summary>
        /// 键盘 2
        /// </summary>
        public const int WH_KEYBOARD = 2;
        /// <summary>
        /// 鼠标  7
        /// </summary>
        public const int WH_MOUSE = 7;
        /// <summary>
        /// 键盘  13
        /// </summary>
        public const int WH_KEYBOARD_LL = 13;
        /// <summary>
        /// 鼠标  14
        /// </summary>
        public const int WH_MOUSE_LL = 14;

        /// <summary>
        /// 钩子委托声明
        /// </summary>
        /// <param name="nCode">nCode</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns></returns>
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        /// <summary>
        /// 安装钩子（返回钩子句柄）
        /// </summary>
        /// <param name="idHook">如：WH_KEYBOARD_LL</param>
        /// <param name="lpfn">委托</param>
        /// <param name="pInstance">如：GetModuleHandle(0)</param>
        /// <param name="threadId">可为：0 </param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(
            int idHook,
            HookProc lpfn,
            IntPtr pInstance,
            int threadId);
        /// <summary>
        /// 卸载钩子
        /// </summary>
        /// <param name="pHookHandle">钩子句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr pHookHandle);
        /// <summary>
        /// 传递钩子
        /// </summary>
        /// <param name="pHookHandle">钩子句柄</param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(
            IntPtr pHookHandle,
            int nCode,
            Int32 wParam,
            IntPtr lParam);
    }

    public class Win_Skin : Form
    {
        /// <summary>
        ///绘制TextBox控件
        /// </summary>
        /// <param name="g"></param>
        /// <param name="textbox"></param>
        /// <param name="c_border">边框</param>
        /// <param name="border_size">边框大小</param>
        /// <param name="font_name">字体名</param>
        /// <param name="fon_size">字体大小</param>
        //public static void Skin_TextBox(Graphics g, TextBox textbox, Color c_border, int border_size, string font_name, int fon_size)
        //{
        //    textbox.BorderStyle = BorderStyle.None;
        //    textbox.Font = new Font(new FontFamily(font_name), fon_size);
        //    g.DrawRectangle(new Pen(c_border, border_size), textbox.Location.X, textbox.Location.Y, textbox.Width, textbox.Height);
        //}
        /// <summary>
        /// 绘制Button控件
        /// </summary>
        /// <param name="g"></param>
        /// <param name="button"></param>
        /// <param name="forecolor">前景色</param>
        /// <param name="backcolor">背景色</param>
        /// <param name="c_border">边框颜色</param>
        /// <param name="c_overback">移过</param>
        /// <param name="c_downback">按下</param>
        /// <param name="font_name">字体名</param>
        /// <param name="fon_size">字体大小</param>
        //public static void Skin_Button(Graphics g, Button button, Color forecolor, Color backcolor, Color c_border, Color c_overback, Color c_downback, string font_name, int fon_size)
        //{
        //    button.FlatStyle = FlatStyle.Flat;
        //    button.FlatAppearance.BorderSize = 0;
        //    button.FlatAppearance.MouseOverBackColor = c_overback;
        //    button.FlatAppearance.MouseDownBackColor = c_downback;
        //    button.BackColor = backcolor;
        //    button.ForeColor = forecolor;
        //    button.Font = new Font(new FontFamily(font_name), fon_size);
        //    g.DrawRectangle(new Pen(c_border, 2), button.Location.X, button.Location.Y, button.Width, button.Height);
        //}
    }


    #endregion
}