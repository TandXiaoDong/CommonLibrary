using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ZedGraph;

namespace EquipmentClient.ControlGraph
{
    public class ComGraph
    {
        /// <summary>
        /// 小图
        /// </summary>
        public static float titleFontSizeA = 12.0f;
        public static float axisFontSizeA = 11f;
        /// <summary>
        /// 中图
        /// </summary>
        public static float titleFontSizeB = titleFontSizeA-3f;
        public static float axisFontSizeB = axisFontSizeA-3f;
        /// <summary>
        /// 大图
        /// </summary>
        public static float titleFontSizeC = titleFontSizeA-5f;
        public static float axisFontSizeC = axisFontSizeA-5f;

        public const string IsBold = "粗体";
        public const string IsAntiAlias = "反斜体";
        public const string IsItalic = "斜体";

        public class GraphConfig
        {
            /// <summary>
            /// 用于判断当前是否需要显示图形，默认显示
            /// </summary>
            public static bool IsVisable = true;
            public static bool IsGraphX = true;
            public static bool IsGraphY = true;
            public static bool IsGraphZ = true;
            public static string ZED_TITLE1 = "X信号";
            public static string ZED_TITLE2 = "Y信号";
            public static string ZED_TITLE3 = "Z信号";
            /// <summary>
            /// 底色
            /// </summary>
            public static Color zedControlColor = Color.Black;
            /// <summary>
            /// 边框
            /// </summary>
            public static Color graphBorderColor = Color.White;
            /// <summary>
            /// 字体颜色
            /// </summary>
            public static Color graphFontColor = Color.White;
            /// <summary>
            /// 3字形:
            /// </summary>
            public static bool IsBold = true;
            public static bool IsAntiAlias = true;
            public static bool IsItalic = true;
            public static float titleFontSize = 12f;
            public static float axisFontSize = titleFontSize-1.5f;
            public static AxisType LineAxisType = AxisType.Linear;
            public static Color LineColor = Color.LightGreen;
            public static Color gridLineColor = Color.White;
            public static bool IsGridLine = true;
            public static float lineSize = 2f;
            public static bool IsGraphDivOrUnion = true;
            public static bool IsGraphLevelOrVertical = true;
            public static SymbolType symbolType = SymbolType.None;
            public static bool IsLineFill = false;
            public static bool IsXAdapter = false;
            public static bool IsYAdapter = false;
            public static double XMax = 10;
            public static double YMax = 5;
            public static int YAveNum = 2;
            public static bool bZoomLevel = true;
            public static bool bZoomVertical = true;
            public static bool bIsShowPoint = true;
        }

        public class GraphTvConfig
        {
            /// <summary>
            /// 用于判断当前是否需要显示图形，默认显示
            /// </summary>
            public static bool IsVisable = true;
            public static bool IsGraphX = true;
            public static bool IsGraphY = true;
            public static bool IsGraphZ = true;
            public static string ZED_TITLE1 = "X信号";
            public static string ZED_TITLE2 = "Y信号";
            public static string ZED_TITLE3 = "Z信号";
            /// <summary>
            /// 底色
            /// </summary>
            public static Color zedControlColor = Color.Black;
            /// <summary>
            /// 边框
            /// </summary>
            public static Color graphBorderColor = Color.White;
            /// <summary>
            /// 字体颜色
            /// </summary>
            public static Color graphFontColor = Color.White;
            /// <summary>
            /// 3字形:
            /// </summary>
            public static bool IsBold = true;
            public static bool IsAntiAlias = true;
            public static bool IsItalic = true;
            public static float titleFontSize = 12f;
            public static float axisFontSize = titleFontSize - 1.5f;
            public static AxisType LineAxisType = AxisType.Linear;
            public static Color LineColor = Color.LightGreen;
            public static Color gridLineColor = Color.White;
            public static bool IsGridLine = true;
            public static float lineSize = 2f;
            public static bool IsGraphDivOrUnion = true;
            //public static bool IsGraphLevelOrVertical = true;
            public static SymbolType symbolType = SymbolType.None;
            public static bool IsLineFill = false;
            public static bool IsXAdapter = false;
            public static bool IsYAdapter = false;
            public static double XMax = 10;
            public static double YMax = 5;
            public static int YAveNum = 2;
            ///默认选择阈值
            ///
            public static bool threshold1 = true;
            public static bool threshold2 = false;
            public static bool threshold3 = false;
            public static bool threshold4 = false;
            public static bool threshold5 = false;
            public static bool threshold6 = false;
            public static bool threshold7 = false;
            public static bool threshold8 = false;
            public static bool threshold9 = false;
            public static bool threshold10 = false;

            public static bool bZoomLevel = true;
            public static bool bZoomVertical = true;
            public static bool bIsShowPoint = true;
        }
    }
}
