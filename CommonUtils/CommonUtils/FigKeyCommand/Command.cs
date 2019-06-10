using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.FigKeyCommand
{
    public enum DEVICE_MSG_TYPE
    {
        /// <summary>
        /// 心跳
        /// </summary>
        INST_MSG_TYPE_HEARTBEAT = 1,
        /// <summary>
        /// 连接速率 params: int*1=4
        /// </summary>
        INST_MSG_TYPE_LINKSPEED = 2,
        /// <summary>
        /// 获取4G地址 params: 0
        /// </summary>
        INST_MSG_TYPE_GET4GADDR = 3,
        /// <summary>
        /// 设置4G地址 params: byte*16+int*1=20
        /// </summary>
        INST_MSG_TYPE_SET4GADDR = 4,
        /// <summary>
        /// 获取采样频率 params: 0
        /// </summary>
        INST_MSG_TYPE_GETSAMPLE = 5,
        /// <summary>
        /// 设置采样频率 params: float*1=4
        /// </summary>
        INST_MSG_TYPE_SETSAMPLE = 6,
        /// <summary>
        /// 获取某参数设定的值 params: int*2=4
        /// </summary>
        INST_MSG_TYPE_GETPARAMVAL = 7,
        /// <summary>
        /// 获取某参数的可选项 params: int*2=4
        /// </summary>
        INST_MSG_TYPE_GETPARAMOPT = 8,
        /// <summary>
        /// 设置某参数的值 params: int*2+byte*X=8+X
        /// </summary>
        INST_MSG_TYPE_SETPARAMVAL = 9,
        /// <summary>
        /// 获取存储规则 params: 0
        /// </summary>
        INST_MSG_TYPE_GETSTORRULE = 10,
        /// <summary>
        /// 设置存储规则 params: byte+int+int+float+int+int+int+int+string=29+string.length=61
        /// string:通道ID,触发是否生效,触发值|通道ID, 触发是否生效,触发值|通道ID, 触发是否生效,触发值)
        /// 通道ID.length=1,触发是否生效.length=1,触发值.length=float.tostring=8:string.length=30+2=32
        /// </summary>
        INST_MSG_TYPE_SETSTORRULE = 11,
        /// <summary>
        /// 启动采集 params:0
        /// </summary>
        INST_MSG_TYPE_CLCTSTART = 12,
        /// <summary>
        /// 停止采集 params:0
        /// </summary>
        INST_MSG_TYPE_CLCTSTOP = 13,
        /// <summary>
        /// 下载文件
        /// </summary>
        INST_MSG_TYPE_DATA = 14,
        /// <summary>
        /// 特征值
        /// </summary>
        INST_MSG_TYPE_EIGENVAL = 15,
        /// <summary>
        /// 获取回收时间区间 params:0
        /// </summary>
        INST_MSG_TYPE_GETRECTIME = 16,
        /// <summary>
        /// 启动回收 params:int+int=8 2038问题
        /// </summary>
        INST_MSG_TYPE_RECSTART = 17,
        /// <summary>
        /// 回收数据
        /// </summary>
        INST_MSG_TYPE_RECDATA = 18,
        /// <summary>
        /// 回收结束
        /// </summary>
        INST_MSG_TYPE_RECEND = 19,
        /// <summary>
        /// 查询是否在回收 params:0
        /// </summary>
        INST_MSG_TYPE_CHKISREC = 20,
        /// <summary>
        /// 特征值动态库 sizeof(lib)
        /// </summary>
        INST_MSG_TYPE_EIGENDYNLIB = 21,
        INST_MSG_TYPE_BUTT
    }

    public static class ClientCommand
    {
        /// <summary>
        /// 客户端连接时发送
        /// </summary>
        public const string ClientConfirmStr = "ClientConnect";
        /// <summary>
        /// 客户端连接
        /// </summary>
        public const string CON_SERVER = "*0";
        /// <summary>
        /// 查询当前设备状态
        /// </summary>
        public const string DEVICE_STATUS = "*1";
        /// <summary>
        /// 查询所有设备状态
        /// </summary>
        public const string DEVICE_STATUS_ALL = "*2";
    }

    public static class ServerCommand
    {
        public const string HEAET_CON = "$1";
    }
}
